using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace {

    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    int borderedResolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp) {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        borderedResolution = resolution + 2;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

        
    }

    public void ConstructMesh() {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        Vector3[] borderVertices = new Vector3[resolution * 4 + 4];
        int[] borderTriangles = new int[24 * resolution];

        int triIndex = 0;
        int borderTriIndex = 0;

        Vector2[] uv = (mesh.uv.Length == vertices.Length) ? mesh.uv : new Vector2[vertices.Length];

        int[,] vertexIndicesMap = new int[borderedResolution, borderedResolution];
        int meshVertexIndex = 0;
        int borderVertexIndex = -1;

        for (int y = 0; y < borderedResolution; y++) {
            for (int x = 0; x < borderedResolution; x++) {
                bool isBorderVertex = y == 0 || y == borderedResolution - 1 || x == 0 || x == borderedResolution - 1;

                if (isBorderVertex) {
                    vertexIndicesMap[x, y] = borderVertexIndex;
                    borderVertexIndex--;
                } else {
                    vertexIndicesMap[x, y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }
        }

        for (int y = 0; y < borderedResolution; y++) {
            for (int x = 0; x < borderedResolution; x++) {
                int i = vertexIndicesMap[x, y];
                Vector2 percent = new Vector2(x - 1, y - 1) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                float unscaledElevation = shapeGenerator.CalculateUnscaledElevation(pointOnUnitSphere);
                Vector3 vertexPosition = pointOnUnitSphere * shapeGenerator.GetScaledElevation(unscaledElevation);
                float vertexUV = unscaledElevation;
                AddVertex(vertexPosition, vertexUV, i, ref vertices, ref uv, ref borderVertices);

                if (x != borderedResolution - 1 && y != borderedResolution - 1) {
                    int a = vertexIndicesMap[x, y];
                    int b = vertexIndicesMap[x + 1, y];
                    int c = vertexIndicesMap[x , y + 1];
                    int d = vertexIndicesMap[x + 1, y + 1];

                    AddTriangle(a, d, c, ref triIndex, ref borderTriIndex, ref triangles, ref borderTriangles);

                    AddTriangle(d, a, b, ref triIndex, ref borderTriIndex, ref triangles, ref borderTriangles);
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = CalculateNormals(ref triangles, ref borderTriangles, ref vertices, ref borderVertices);

        mesh.uv = uv;
    }

    public void AddVertex(Vector3 vertexPosition, float uv, int vertexIndex, ref Vector3[] vertices, ref Vector2[] uvs, ref Vector3[] borderVertices) {
        if (vertexIndex < 0) {
            borderVertices[-vertexIndex - 1] = vertexPosition;
        } else {
            vertices[vertexIndex] = vertexPosition;
            uvs[vertexIndex].y = uv;
        }
    }

    public void AddTriangle(int a, int b, int c, ref int triIndex, ref int borderTriIndex, ref int[] triangles, ref int[] borderTriangles) {
        if (a < 0 || b < 0 || c < 0) {
            borderTriangles[borderTriIndex] = a;
            borderTriangles[borderTriIndex + 1] = b;
            borderTriangles[borderTriIndex + 2] = c;
            borderTriIndex += 3;
        }
        else {
            triangles[triIndex] = a;
            triangles[triIndex + 1] = b;
            triangles[triIndex + 2] = c;
            triIndex += 3;
        }
    }

    Vector3[] CalculateNormals(ref int[] triangles, ref int[] borderTriangles, ref Vector3[] vertices, ref Vector3[] borderVertices) {
        Vector3[] vertexNormals = new Vector3[mesh.vertices.Length];
        int triangleCount = triangles.Length / 3;
        int borderTriangleCount = borderTriangles.Length / 3;

        for (int i = 0; i < triangleCount; i++) {
            int normalTriangleindex = i * 3;
            int vertexIndexA = triangles[normalTriangleindex];
            int vertexIndexB = triangles[normalTriangleindex + 1];
            int vertexIndexC = triangles[normalTriangleindex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC, ref vertices, ref borderVertices);

            vertexNormals[vertexIndexA] += triangleNormal;
            vertexNormals[vertexIndexB] += triangleNormal;
            vertexNormals[vertexIndexC] += triangleNormal;
        }

        for (int i = 0; i < borderTriangleCount; i++) {
            int normalTriangleindex = i * 3;
            int vertexIndexA = borderTriangles[normalTriangleindex];
            int vertexIndexB = borderTriangles[normalTriangleindex + 1];
            int vertexIndexC = borderTriangles[normalTriangleindex + 2];

            Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC, ref vertices, ref borderVertices);

            if (vertexIndexA > 0)
                vertexNormals[vertexIndexA] += triangleNormal;
            if (vertexIndexB > 0)
                vertexNormals[vertexIndexB] += triangleNormal;
            if (vertexIndexC > 0)
                vertexNormals[vertexIndexC] += triangleNormal;
        }

        for (int i = 0; i < vertexNormals.Length; i++) {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    Vector3 SurfaceNormalFromIndices(int a, int b, int c, ref Vector3[] vertices, ref Vector3[] borderVertices) {
        Vector3 pointA = (a < 0) ? borderVertices[-a - 1] : vertices[a];
        Vector3 pointB = (b < 0) ? borderVertices[-b - 1] : vertices[b];
        Vector3 pointC = (c < 0) ? borderVertices[-c - 1] : vertices[c];

        return Vector3.Cross(pointB - pointA, pointC - pointA).normalized;
    }

    public void UpdateUVs(ColorGenerator colorGenerator) {
        Vector2[] uv = mesh.uv;

        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;

                uv[i].x = colorGenerator.BiomePercentFromPoint(pointOnUnitSphere);
            }
        }

        mesh.uv = uv;
    }     
}
