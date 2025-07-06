using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    private Vector3 cameraOffset = new Vector3(1, 2.5f, -8);
    private float mouseSensitivity = 3.0f;
    private float zoomSpeed = 2f;
    private float minZoom = 3f;
    private float maxZoom = 15f;
    private float yaw = 0; // goc xoay trai - phai, xoay theo truc y (truc dung)
    private float pitch = 12; // goc xoay len xuong, xoay theo truc x (truc ngang x)
    private float minPitch = -20;
    private float maxPitch = 60;
    

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        
    }

    private void LateUpdate()
    {

        // Zoom bằng lăn chuột giữa
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cameraOffset.z += scroll * zoomSpeed;
            cameraOffset.z = Mathf.Clamp(cameraOffset.z, -maxZoom, -minZoom);
        }


        // Chi xoay khi nhan Ctrl
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch); 
        }

        // Luôn theo sau nhân vật, chỉ thay đổi góc khi nhấn Ctrl
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * cameraOffset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }


}
