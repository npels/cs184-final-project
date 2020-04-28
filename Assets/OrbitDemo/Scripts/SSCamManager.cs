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
