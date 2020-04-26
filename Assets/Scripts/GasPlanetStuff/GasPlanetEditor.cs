using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GasPlanet))]
public class GasPlanetEditor : Editor
{
    GasPlanet gasPlanet;
    Editor shapeEditor;
    Editor colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                gasPlanet.GenerateGasPlanet();
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            gasPlanet.GenerateGasPlanet();
        }
            
        DrawSettingsEditor(gasPlanet.gasShapeSettings, gasPlanet.OnShapeSettingsUpdated, ref gasPlanet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(gasPlanet.gasColorSettings, gasPlanet.OnColorSettingsUpdated, ref gasPlanet.colorSettingsFoldout, ref colorEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        gasPlanet = (GasPlanet)target;
    }
}
