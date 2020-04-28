using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUI : MonoBehaviour {

    public Planet planet;

    public GameObject planetEditor;
    public GameObject sceneCamera;

    public TMPro.TMP_InputField seedInput;
    public Slider sizeSlider;
    public Slider landShapeSlider;
    public Slider roughnessSlider;
    public Slider heightSlider;
    public Slider seaLevelSlider;

    public TMPro.TMP_Dropdown shapesDropdown;
    public TMPro.TMP_Dropdown colorsDropdown;

    public GameObject returnToEditorButton;
    public Slider zoomSlider;

    private void Start() {
        LoadSettings();
    }

    public void LoadSettings() {
        var nl = planet.shapeSettings.noiseLayers[0];
        switch (nl.noiseSettings.filterType) {
            case NoiseSettings.FilterType.Simple:
                seedInput.text = nl.noiseSettings.simpleNoiseSettings.seed.ToString();
                landShapeSlider.value = nl.noiseSettings.simpleNoiseSettings.baseRoughness;
                roughnessSlider.value = nl.noiseSettings.simpleNoiseSettings.roughness;
                heightSlider.value = nl.noiseSettings.simpleNoiseSettings.strength;
                seaLevelSlider.value = nl.noiseSettings.simpleNoiseSettings.minValue;
                break;
            case NoiseSettings.FilterType.Rigid:
                seedInput.text = nl.noiseSettings.rigidNoiseSettings.seed.ToString();
                landShapeSlider.value = nl.noiseSettings.rigidNoiseSettings.baseRoughness;
                roughnessSlider.value = nl.noiseSettings.rigidNoiseSettings.roughness;
                heightSlider.value = nl.noiseSettings.rigidNoiseSettings.strength;
                seaLevelSlider.value = nl.noiseSettings.rigidNoiseSettings.minValue;
                break;
            default:
                break;
        }
        sizeSlider.value = planet.shapeSettings.planetRadius;
    }

    public void OnChangeSeed() {
        if (seedInput.text != "") {
            foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
                switch (nl.noiseSettings.filterType) {
                    case NoiseSettings.FilterType.Simple:
                        nl.noiseSettings.simpleNoiseSettings.seed = int.Parse(seedInput.text);
                        break;
                    case NoiseSettings.FilterType.Rigid:
                        nl.noiseSettings.rigidNoiseSettings.seed = int.Parse(seedInput.text);
                        break;
                    default:
                        break;
                }
            }
            planet.OnShapeSettingsUpdated();

            planet.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.seed = int.Parse(seedInput.text);
            planet.OnColorSettingsUpdated();
        }
    }

    public void OnRandomizeSeed() {
        int seed = Random.Range(int.MinValue / 10, int.MaxValue / 10);
        seedInput.text = seed.ToString();
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.seed = seed;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.seed = seed;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();

        planet.colorSettings.biomeColorSettings.noise.simpleNoiseSettings.seed = seed;
        planet.OnColorSettingsUpdated();
    }

    public void OnRandomizePlanetType() {
        ShapesDropdownFolder s = shapesDropdown.transform.GetComponent<ShapesDropdownFolder>();
        int type = Random.Range(0, s.items.Count);
        s.OnSelectItem(type);
        shapesDropdown.value = type;
    }

    public void OnChangeSize() {
        planet.shapeSettings.planetRadius = sizeSlider.value;
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeSize() {
        planet.shapeSettings.planetRadius = Random.Range(sizeSlider.minValue, sizeSlider.maxValue);
        sizeSlider.value = planet.shapeSettings.planetRadius;
        planet.OnShapeSettingsUpdated();
    }

    public void OnChangeLandShape() {
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.baseRoughness = landShapeSlider.value;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.baseRoughness = landShapeSlider.value;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeLandShape() {
        float landShape = Random.Range(landShapeSlider.minValue, landShapeSlider.maxValue);
        landShapeSlider.value = landShape;
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.baseRoughness = landShape;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.baseRoughness = landShape;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnChangeRoughness() {
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.roughness = roughnessSlider.value;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.roughness = roughnessSlider.value;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeRoughness() {
        float roughness = Random.Range(roughnessSlider.minValue, roughnessSlider.maxValue);
        roughnessSlider.value = roughness;
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.roughness = roughness;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.roughness = roughness;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnChangeHeight() {
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.strength = heightSlider.value;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.strength = heightSlider.value;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeHeight() {
        float height = Random.Range(heightSlider.minValue, heightSlider.maxValue);
        heightSlider.value = height;
        foreach (ShapeSettings.NoiseLayer nl in planet.shapeSettings.noiseLayers) {
            switch (nl.noiseSettings.filterType) {
                case NoiseSettings.FilterType.Simple:
                    nl.noiseSettings.simpleNoiseSettings.strength = height;
                    break;
                case NoiseSettings.FilterType.Rigid:
                    nl.noiseSettings.rigidNoiseSettings.strength = height;
                    break;
                default:
                    break;
            }
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnChangeSeaLevel() {
        switch (planet.shapeSettings.noiseLayers[0].noiseSettings.filterType) {
            case NoiseSettings.FilterType.Simple:
                planet.shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.minValue = seaLevelSlider.value;
                break;
            case NoiseSettings.FilterType.Rigid:
                planet.shapeSettings.noiseLayers[0].noiseSettings.rigidNoiseSettings.minValue = seaLevelSlider.value;
                break;
            default:
                break;
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeSeaLevel() {
        float seaLevel = Random.Range(seaLevelSlider.minValue, seaLevelSlider.maxValue);
        seaLevelSlider.value = seaLevel;
        switch (planet.shapeSettings.noiseLayers[0].noiseSettings.filterType) {
            case NoiseSettings.FilterType.Simple:
                planet.shapeSettings.noiseLayers[0].noiseSettings.simpleNoiseSettings.minValue = seaLevel;
                break;
            case NoiseSettings.FilterType.Rigid:
                planet.shapeSettings.noiseLayers[0].noiseSettings.rigidNoiseSettings.minValue = seaLevel;
                break;
            default:
                break;
        }
        planet.OnShapeSettingsUpdated();
    }

    public void OnRandomizeColor() {
        ColorsDropdownFolder s = colorsDropdown.transform.GetComponent<ColorsDropdownFolder>();
        int type = Random.Range(0, s.items.Count);
        s.OnSelectItem(type);
        colorsDropdown.value = type;
    }

    public void OnRandomizeEverything() {
        OnRandomizeColor();
        OnRandomizeHeight();
        OnRandomizeLandShape();
        OnRandomizeRoughness();
        OnRandomizePlanetType();
        OnRandomizeSeaLevel();
        OnRandomizeSeed();
        OnRandomizeSize();
    }

    public void OnAdvancedOptions() {
        // TODO
    }

    public void OnPreview() {
        planetEditor.SetActive(false);
        returnToEditorButton.SetActive(true);
        planet.resolution = 256;
        planet.GeneratePlanet();
        zoomSlider.value = 1;
        sceneCamera.GetComponent<Animation>().Play("CameraMoveLeft");
        sceneCamera.GetComponent<CameraSettings>().originalPosition = new Vector3(0, 0, 3.75f);
    }

    public void OnDone() {
        // TODO
    }

    public void OnReturnToEditor() {
        planetEditor.SetActive(true);
        returnToEditorButton.SetActive(false);
        planet.resolution = 64;
        planet.GeneratePlanet();
        zoomSlider.value = 1;
        sceneCamera.GetComponent<Animation>().Play("CameraMoveRight");
        sceneCamera.GetComponent<CameraSettings>().originalPosition = new Vector3(1.5f, 0, 3.75f);
    }

    public void OnZoom() {
        sceneCamera.transform.position = sceneCamera.GetComponent<CameraSettings>().originalPosition * zoomSlider.value;
    }
}
