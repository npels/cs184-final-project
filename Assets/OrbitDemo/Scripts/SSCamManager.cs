using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SSCamManager : MonoBehaviour
{
    #region Singleton
  public static SSCamManager Instance;
  void Awake()
  {
    Instance = this;
  }
  #endregion

    [SerializeField]
    CinemachineVirtualCamera planetCam;

    string _currCamTarget = "Solar system";
    public string CurrCamTarget {
        get { return _currCamTarget; }
        set {
            string prevCamTarget = _currCamTarget;
            string newCamTarget = value;
        
            bool camShouldBeActive = false;
            int newCamPriority = 0;
            if (!newCamTarget.Equals("Solar system")) {
                newCamPriority = 10;
                camShouldBeActive = true;

                GameObject newTarget = GameManager.Instance.nameToSolarSystemObject[newCamTarget];
                planetCam.LookAt = newTarget.transform;
                planetCam.Follow = newTarget.transform;
                if (newCamTarget.Contains("Planet")) {
                  newTarget.GetComponent<TrailRenderer>().enabled = false;
                }

                for (int i = 0; i < newTarget.transform.childCount; i++) {
                  newTarget.transform.GetChild(i).gameObject.SetActive(true);
                }
            } 

            if (prevCamTarget.Contains("Planet")) {
              GameObject oldTarget = GameManager.Instance.nameToSolarSystemObject[prevCamTarget];
              oldTarget.GetComponent<TrailRenderer>().enabled = true;

              for (int i = 0; i < oldTarget.transform.childCount; i++) {
                oldTarget.transform.GetChild(i).gameObject.SetActive(false);
              }
            }
            planetCam.Priority = newCamPriority;
            planetCam.gameObject.SetActive(camShouldBeActive);

            _currCamTarget = newCamTarget;
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

}
