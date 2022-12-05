using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    [Header("--- Player Components ---")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController characterController;
    
    [Space]
    [Header("--- Player Attributes ---")]
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;

    // Private Attributes
    private Vector3 velocity;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    
    
    // Update is called once per frame
    void Update()
    {
        // Capturamos los inputs del jugador y los almacenamos en las Variables de tipo Vector3 y Vector2
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        MovePlayer();
        MovePlayerCamera();
        
    }
    
    
    /// <summary>
    /// Método para mover al jugador hacia la dirección que esté pulsado con el teclado
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput);

        if (characterController.isGrounded)
        {
            velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }
        
        characterController.Move(moveVector * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);

    }
    
    
    /// <summary>
    /// Método para rotar al player y la cámara
    /// </summary>
    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity;
        xRot = Math.Clamp(xRot, -60f, 60f); // Definimos la máxima rotación de la cámara hacia arriba y abajo
        
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f); // Rotamos al jugador
        
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f,0f); // Rotamos a la cámara
    }
}
