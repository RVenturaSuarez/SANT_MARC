using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Transform targetPoint;
    public Camera playerCamera;
    public float pickupRange;
    public LayerMask pickupMask;

    public bool haveAPickup;
    private RaycastHit pickup;
    
    private float horizontalInput;
    private float verticalInput;
    
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.forward * (speed * verticalInput * Time.deltaTime));
        transform.Translate(Vector3.right * (speed * horizontalInput * Time.deltaTime));


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!haveAPickup)
            {
                Ray CamaraRey = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(CamaraRey, out RaycastHit hit, pickupRange, pickupMask))
                {
                    haveAPickup = true;
                    hit.transform.position = targetPoint.transform.position;
                    hit.transform.parent = targetPoint;
                    hit.rigidbody.isKinematic = true;
                    pickup = hit;
                }
            }
            else
            {
                haveAPickup = false;
                pickup.transform.parent = null;
                pickup.rigidbody.isKinematic = false;
            }
            
        }
        
    }
}
