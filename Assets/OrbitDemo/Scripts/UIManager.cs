using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  #region Singleton
  public static UIManager Instance;
  void Awake()
  {
    Instance = this;
  }
  #endregion

  [SerializeField]
  Dropdown solarSystemDropdown;

  List<string> camList;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void PopulateDropdown(Dictionary<string, GameObject> nameToSolarSystemObject)
  {
    camList = new List<string>(nameToSolarSystemObject.Keys);
    camList.Insert(0, "Solar system");
    solarSystemDropdown.AddOptions(camList);
  }

  public void SetActiveCameraFromDropdown() {
    string planetName = solarSystemDropdown.options[solarSystemDropdown.value].text;
    SSCamManager.Instance.CurrCamTarget = planetName;
  }

}
