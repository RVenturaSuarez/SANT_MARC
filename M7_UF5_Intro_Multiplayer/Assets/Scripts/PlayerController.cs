using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("---- Player Components ----")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform feetPoint;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask floorMask;

    [Space] [Header("---- Player Attributes ----")] 
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float radius;
    
    // Private Attributes
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    
    

    void Update()
    {
        // Capturamos los inputs del jugador y los almacenamos en las Variables de tipo
        // Vector3 y Vector2
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        // Lógica movimiento Player
        MovePlayer();
        
        // Lógica movimiento Cámera
        MovePlayerCamera();
    }


    /// <summary>
    /// Método para mover al jugador en la dirección que pulsamos
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
        _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Creamos una sphere desde nuestros pies para comprobar si chocamos con la layerMask
            if (Physics.CheckSphere(feetPoint.position, radius, floorMask))
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Lógica para mover la cámara
    /// </summary>
    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity;
        xRot = Math.Clamp(xRot, -60f, 60f);
        
        // Rotación del cuerpo del player mediante el movimiento del mouse Horizontal
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);

        // Rotación de la cámara mediante el movimiento del mouse en Vertical
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f ,0f);
    }
}
