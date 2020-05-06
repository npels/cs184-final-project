using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShapesDropdownFolder : MonoBehaviour {
    public PlanetUI planetUI;

    public List<ShapeSettings> items;

    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<TMPro.TMP_Dropdown>();
        dropdown.ClearOptions();

        List<TMPro.TMP_Dropdown.OptionData> dropdownList = new List<TMPro.TMP_Dropdown.OptionData>();
        foreach (ShapeSettings s in items) {
            TMPro.TMP_Dropdown.OptionData item = new TMPro.TMP_Dropdown.OptionData(s.name);
            dropdownList.Add(item);
        }

        dropdown.AddOptions(dropdownList);
    }

    public void OnSelectItem(int index) {
        planetUI.planet.shapeSettings = items[index];
        planetUI.planet.OnShapeSettingsUpdated();
        planetUI.LoadSettings();
    }
}
