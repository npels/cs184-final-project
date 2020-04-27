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
    solarSystemDropdown.AddOptions(new List<string>(nameToSolarSystemObject.Keys));
  }

}
