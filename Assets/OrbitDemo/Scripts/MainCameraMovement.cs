using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
  public float lookSpeedH = 2f;
  public float lookSpeedV = 2f;
  public float zoomSpeed = 2f;
  public float dragSpeed = 6f;
  public float moveSpeed = 6f;

  [SerializeField]
  Camera mainCamera;
  
  private float yaw = 0f;
  private float pitch = 0f;

  void Update()
  {
    if (SSCamManager.Instance.CurrCamTarget == "Solar system") {
      //Look around with Left Mouse
      if (Input.GetMouseButton(0)) {
        this.yaw = mainCamera.transform.eulerAngles.y;
        this.pitch = mainCamera.transform.eulerAngles.x;

        this.yaw += lookSpeedH * Input.GetAxis("Mouse X");
        this.pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

        mainCamera.transform.eulerAngles = new Vector3(this.pitch, this.yaw, 0f);
      }

      //move camera around with WASD
      Vector3 cameraRight = mainCamera.transform.right;
      Vector3 cameraForward = mainCamera.transform.up;
      Vector3 cameraUp = mainCamera.transform.forward;
      //Debug.Log(cameraRight + ", " + cameraForward);
      Vector3 inputVector = new Vector3(Input.GetAxis("Forward"), Input.GetAxis("Up"), Input.GetAxis("Right"));
      Vector3 moveDirection = (cameraForward * Input.GetAxis("Forward")) + (cameraUp * Input.GetAxis("Up")) + (cameraRight * Input.GetAxis("Right"));
      mainCamera.transform.Translate(moveDirection * Mathf.Sqrt(inputVector.sqrMagnitude) * Time.deltaTime * this.moveSpeed);

      //Zoom in and out with Mouse Wheel
      mainCamera.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }
  }
}

