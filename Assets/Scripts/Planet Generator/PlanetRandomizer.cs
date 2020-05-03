using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRandomizer : MonoBehaviour {
    public GenerationParameters g;
    public Planet planet;

    private int seed;

    void Awake() {
        seed = Random.Range(g.minSeed, g.maxSeed);
        RegenerateShape();
        RegenerateColor();
    }

    public void RegenerateShape() {
        ShapeSettings settings = ScriptableObject.CreateInstance("ShapeSettings") as ShapeSettings;

        float planetRadius = Random.Range(g.planetRadiusMin, g.planetRadiusMax);
        float minValue = Random.Range(g.seaLevelMin, g.seaLevelMax);

        int numLayers = Random.Range(g.minNoiseLayers, g.maxNoiseLayers);
        float strength = Random.Range(g.strengthMin, g.strengthMax);
        float roughness = Random.Range(g.roughnessMin, g.roughnessMax);
        float persistance = Random.Range(g.persistanceMin, g.persistanceMax);
        float baseRoughness = Random.Range(g.baseRoughnessMin, g.baseRoughnessMax);  

        settings.planetRadius = planetRadius;

        int numFilters = Random.Range(g.minNoiseFilters, g.maxNoiseFilters);
        int numRigidNoiseFilters = Random.Range(g.minRigidNoiseFilters, g.maxRigidNoiseFilters);
        if (numRigidNoiseFilters >= numFilters) numRigidNoiseFilters = numFilters - 1;

        settings.noiseLayers = new ShapeSettings.NoiseLayer[numFilters];
        for (int i = 0; i < numFilters; i++) {
            settings.noiseLayers[i] = new ShapeSettings.NoiseLayer();
            settings.noiseLayers[i].noiseSettings = new NoiseSettings();
        }

        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        settings.noiseLayers[0].noiseSettings.filterType = NoiseSettings.FilterType.Simple;

        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.numLayers = numLayers;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.strength = strength;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.roughness = roughness;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.persistance = persistance;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.baseRoughness = baseRoughness;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.minValue = minValue;
        settings.noiseLayers[0].noiseSettings.simpleNoiseSettings.seed = seed;

        settings.noiseLayers[0].enabled = true;

        for (int i = 1; i < numFilters; i++) {
            numLayers = Random.Range(g.minNoiseLayers, g.maxNoiseLayers);
            strength = Random.Range(g.strengthMin, g.strengthMax);
            roughness = Random.Range(g.roughnessMin, g.roughnessMax);
            persistance = Random.Range(g.persistanceMin, g.persistanceMax);
            baseRoughness = Random.Range(g.baseRoughnessMin, g.baseRoughnessMax);

            if (i < numFilters - numRigidNoiseFilters) {
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
                settings.noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Simple;

                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.numLayers = numLayers;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.strength = strength;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.roughness = roughness;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.persistance = persistance;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.baseRoughness = baseRoughness;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.minValue = 0;
                settings.noiseLayers[i].noiseSettings.simpleNoiseSettings.seed = seed;
            } else {
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();
                settings.noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Rigid;

                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.numLayers = numLayers;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.strength = strength;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.roughness = roughness;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.persistance = persistance;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.baseRoughness = baseRoughness;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.minValue = 0;
                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.seed = seed;

                settings.noiseLayers[i].noiseSettings.rigidNoiseSettings.weightMultiplier = Random.Range(g.weightMultiplierMin, g.weightMultiplierMax);
            }

            settings.noiseLayers[i].enabled = true;
            settings.noiseLayers[i].useFirstLayerAsMask = true;
        }

        planet.shapeSettings = settings;
    }

    public void RegenerateColor() {
        ColorSettings settings = ScriptableObject.CreateInstance("ColorSettings") as ColorSettings;
        settings.planetMaterial = new Material(g.planetShader);

        int numBiomes = Random.Range(g.minBiomes, g.maxBiomes);
        settings.biomeColorSettings = new ColorSettings.BiomeColorSettings();
        settings.biomeColorSettings.biomes = new ColorSettings.BiomeColorSettings.Biome[numBiomes];
        float biomeSpacing = 1f / (float)numBiomes;

        Color shoreColor = UnityEngine.Random.ColorHSV(g.shoreHueMin, g.shoreHueMax, g.shoreSaturationMin, g.shoreSaturationMax, g.shoreValueMin, g.shoreValueMax);
        Color baseColor = UnityEngine.Random.ColorHSV(g.baseHueMin, g.baseHueMax, g.baseSaturationMin, g.baseSaturationMax, g.baseValueMin, g.baseValueMax);

        for (int i = 0; i < numBiomes; i++) {
            settings.biomeColorSettings.biomes[i] = new ColorSettings.BiomeColorSettings.Biome();

            int numBiomeColors = Random.Range(g.minBiomeColors, g.maxBiomeColors);
            GradientColorKey[] gradientColorKeys = new GradientColorKey[numBiomeColors];
            float biomeColorSpacing = 1f / (float)numBiomeColors;

            float red = Mathf.Clamp(baseColor.r + Random.Range(-g.interbiomeColorFlux, g.interbiomeColorFlux), 0f, 1f);
            float green = Mathf.Clamp(baseColor.g + Random.Range(-g.interbiomeColorFlux, g.interbiomeColorFlux), 0f, 1f);
            float blue = Mathf.Clamp(baseColor.b + Random.Range(-g.interbiomeColorFlux, g.interbiomeColorFlux), 0f, 1f);
            Color biomeBaseColor = new Color(red, green, blue);

            gradientColorKeys[0] = new GradientColorKey(shoreColor, 0f);
            gradientColorKeys[1] = new GradientColorKey(biomeBaseColor, Mathf.Clamp(biomeColorSpacing + Random.Range(-g.biomeColorSpacingFlux, g.biomeColorSpacingFlux), 0f, 1f));

            red = Mathf.Clamp(biomeBaseColor.r + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
            green = Mathf.Clamp(biomeBaseColor.g + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
            blue = Mathf.Clamp(biomeBaseColor.b + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
            Color nextColor = new Color(red, green, blue);

            for (int j = 2; j < numBiomeColors; j++) {
                gradientColorKeys[j] = new GradientColorKey(nextColor, Mathf.Clamp(j * biomeColorSpacing + Random.Range(-g.biomeColorSpacingFlux, g.biomeColorSpacingFlux), 0f, 1f));

                red = Mathf.Clamp(nextColor.r + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
                green = Mathf.Clamp(nextColor.g + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
                blue = Mathf.Clamp(nextColor.b + Random.Range(-g.intrabiomeColorFlux, g.intrabiomeColorFlux), 0f, 1f);
                nextColor = new Color(red, green, blue);
            }

            GradientAlphaKey[] gradientAlphaKeys = { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) };
            settings.biomeColorSettings.biomes[i].gradient = new Gradient();
            settings.biomeColorSettings.biomes[i].gradient.SetKeys(gradientColorKeys, gradientAlphaKeys);

            settings.biomeColorSettings.biomes[i].tint = UnityEngine.Random.ColorHSV(g.tintHueMin, g.tintHueMax, g.tintSaturationMin, g.tintSaturationMax, g.tintValueMin, g.tintValueMax);
            settings.biomeColorSettings.biomes[i].tintPercent = Random.Range(g.tintPercentMin, g.tintPercentMax);

            settings.biomeColorSettings.biomes[i].startHeight = Mathf.Clamp(i * biomeSpacing + Random.Range(-g.biomeLocationFlux, g.biomeLocationFlux), 0f, 1f);
        }

        settings.biomeColorSettings.noise = new NoiseSettings();
        settings.biomeColorSettings.noise.filterType = NoiseSettings.FilterType.Simple;
        settings.biomeColorSettings.noise.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

        settings.biomeColorSettings.noise.simpleNoiseSettings.baseRoughness = Random.Range(g.colorBaseRoughnessMin, g.colorBaseRoughnessMax);
        settings.biomeColorSettings.noise.simpleNoiseSettings.roughness = Random.Range(g.colorRoughnessMin, g.colorRoughnessMax);
        settings.biomeColorSettings.noise.simpleNoiseSettings.persistance = Random.Range(g.colorPersistanceMin, g.colorPersistanceMax);
        settings.biomeColorSettings.noise.simpleNoiseSettings.strength = Random.Range(g.colorStrengthMin, g.colorStrengthMax);
        settings.biomeColorSettings.noise.simpleNoiseSettings.numLayers = Random.Range(g.minColorNoiseLayers, g.maxColorNoiseLayers);
        settings.biomeColorSettings.noise.simpleNoiseSettings.seed = seed;
        settings.biomeColorSettings.noise.simpleNoiseSettings.minValue = 0f;

        settings.biomeColorSettings.noiseOffset = Random.Range(g.noiseOffsetMin, g.noiseOffsetMax);
        settings.biomeColorSettings.noiseStrength = Random.Range(g.noiseStrengthMin, g.noiseStrengthMax);
        settings.biomeColorSettings.blendAmount = Random.Range(g.blendAmountMin, g.blendAmountMax);

        int numOceanColors = Random.Range(g.minOceanColors, g.maxOceanColors);
        float oceanColorSpacing = 1f / (float)numOceanColors;
        GradientColorKey[] oceanColorKeys = new GradientColorKey[numOceanColors];

        oceanColorKeys[0] = new GradientColorKey(shoreColor, 1f);

        float oceanRed = Mathf.Clamp(shoreColor.r + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
        float oceanGreen = Mathf.Clamp(shoreColor.g + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
        float oceanBlue = Mathf.Clamp(shoreColor.b + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
        Color nextOceanColor = new Color(oceanRed, oceanGreen, oceanBlue);

        for (int i = 2; i < numOceanColors; i++) {
            oceanColorKeys[i] = new GradientColorKey(nextOceanColor, Mathf.Clamp((numOceanColors - i) * oceanColorSpacing + Random.Range(-g.oceanSpacingFlux, g.oceanSpacingFlux), 0f, 1f));

            oceanRed = Mathf.Clamp(nextOceanColor.r + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
            oceanGreen = Mathf.Clamp(nextOceanColor.g + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
            oceanBlue = Mathf.Clamp(nextOceanColor.b + Random.Range(-g.oceanColorFlux, g.oceanColorFlux), 0f, 1f);
            nextOceanColor = new Color(oceanRed, oceanGreen, oceanBlue);
        }

        GradientAlphaKey[] oceanAlphaKeys = { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) };
        settings.oceanColor = new Gradient();
        settings.oceanColor.SetKeys(oceanColorKeys, oceanAlphaKeys);

        planet.colorSettings = settings;
    }
}
