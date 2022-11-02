using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Character Variables")]
    public float speed;
    public Camera playerCamera;
    public Transform targetPoint;
    public Transform respawn;

    [Space]
    [Header("Pickups Variables")]
    public bool haveAPickup;
    private RaycastHit pickup;
    public float pickupRange;
    public LayerMask pickupMask;
    


    // Private Variables
    private float horizontalInput;
    private float verticalInput;



    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.forward * (speed * verticalInput * Time.deltaTime));
        transform.Translate(Vector3.right * (speed * horizontalInput * Time.deltaTime));


        if (transform.position.y <= -0.5f)
        {
            transform.position = respawn.transform.position;
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!haveAPickup)
            {
                RaycastHit hit;
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, pickupRange, pickupMask))
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
