using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ColorsDropdownFolder : MonoBehaviour {
    public PlanetUI planetUI;

    public List<ColorSettings> items;

    // Start is called before the first frame update
    void Start() {
        var dropdown = transform.GetComponent<TMPro.TMP_Dropdown>();
        dropdown.ClearOptions();

        List<TMPro.TMP_Dropdown.OptionData> dropdownList = new List<TMPro.TMP_Dropdown.OptionData>();
        foreach (ColorSettings s in items) {
            TMPro.TMP_Dropdown.OptionData item = new TMPro.TMP_Dropdown.OptionData(s.name);
            dropdownList.Add(item);
        }

        dropdown.AddOptions(dropdownList);
    }

    public void OnSelectItem(int index) {
        planetUI.planet.colorSettings = items[index];
        planetUI.planet.OnColorSettingsUpdated();
    }
}
