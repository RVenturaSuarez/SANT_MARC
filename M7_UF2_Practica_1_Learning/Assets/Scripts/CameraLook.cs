using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensiblity = 80f;
    public Transform playerBody;
    
    private float mouseX;
    private float mouseY;
    
    private float xRotation = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        
        transform.localRotation = Quaternion.Euler(xRotation,0 ,0);
        
        playerBody.Rotate(Vector3.up * mouseX * mouseSensiblity * Time.deltaTime);
        
    }
}
