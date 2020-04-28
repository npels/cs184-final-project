using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlanetCameraControls : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera PlanetCam;

    public float zoomSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SSCamManager.Instance.CurrCamTarget != "Solar system") {
            //Zoom in and out with Mouse Wheel
            PlanetCam.m_Lens.FieldOfView += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
    }
}
