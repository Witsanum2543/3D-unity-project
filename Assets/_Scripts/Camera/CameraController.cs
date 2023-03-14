using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _freeLookCamera;

    private bool _isRotatingCamera = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) {
            _isRotatingCamera = true;
        }
        else if (Input.GetMouseButtonUp(2)) {
            _isRotatingCamera = false;
        }

        if (_isRotatingCamera) {
            float mouseX = Input.GetAxis("Mouse X");
            _freeLookCamera.m_XAxis.Value += mouseX;
        }
    }
}
