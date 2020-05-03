using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GenerationParameters : ScriptableObject {
    public int minSeed;
    public int maxSeed;

    public int minNoiseFilters;
    public int maxNoiseFilters;
    public int minRigidNoiseFilters;
    public int maxRigidNoiseFilters;
    public int minNoiseLayers;
    public int maxNoiseLayers;

    public float planetRadiusMin;
    public float planetRadiusMax;
    public float baseRoughnessMin;
    public float baseRoughnessMax;
    public float roughnessMin;
    public float roughnessMax;
    public float persistanceMin;
    public float persistanceMax;
    public float strengthMin;
    public float strengthMax;
    public float seaLevelMin;
    public float seaLevelMax;
    public float weightMultiplierMin;
    public float weightMultiplierMax;

    public int minBiomes;
    public int maxBiomes;
    public int minBiomeColors;
    public int maxBiomeColors;
    public int minOceanColors;
    public int maxOceanColors;

    public float shoreHueMin;
    public float shoreHueMax;
    public float shoreSaturationMin;
    public float shoreSaturationMax;
    public float shoreValueMin;
    public float shoreValueMax;
    public float baseHueMin;
    public float baseHueMax;
    public float baseSaturationMin;
    public float baseSaturationMax;
    public float baseValueMin;
    public float baseValueMax;
    public float oceanColorFlux;
    public float oceanSpacingFlux;
    public float intrabiomeColorFlux;
    public float interbiomeColorFlux;
    public float biomeColorSpacingFlux;
    public float biomeLocationFlux;
    public float tintHueMin;
    public float tintHueMax;
    public float tintSaturationMin;
    public float tintSaturationMax;
    public float tintValueMin;
    public float tintValueMax;
    public float tintPercentMin;
    public float tintPercentMax;

    public int minColorNoiseLayers;
    public int maxColorNoiseLayers;

    public float colorBaseRoughnessMin;
    public float colorBaseRoughnessMax;
    public float colorRoughnessMin;
    public float colorRoughnessMax;
    public float colorPersistanceMin;
    public float colorPersistanceMax;
    public float colorStrengthMin;
    public float colorStrengthMax;
    public float noiseOffsetMin;
    public float noiseOffsetMax;
    public float noiseStrengthMin;
    public float noiseStrengthMax;
    public float blendAmountMin;
    public float blendAmountMax;

    public Shader planetShader;
}
