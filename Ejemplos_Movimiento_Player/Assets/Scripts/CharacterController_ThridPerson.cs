using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_ThridPerson : MonoBehaviour
{
    [Header("--- Player Components ---")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    
    [Space]
    [Header("--- Player Attributes ---")]
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;

    // Private Attributes
    private Vector3 velocity;
    private Vector3 playerMovementInput;
    private float xRot;
    private float horizontalInput;
    
    
    // Update is called once per frame
    void Update()
    {
        // Capturamos los inputs del jugador y los almacenamos en las Variables de tipo Vector3 y Vector2
        playerMovementInput = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        horizontalInput = Input.GetAxis("Horizontal");
        
        MovePlayer();
        
        animator.SetFloat("x", horizontalInput);
        animator.SetFloat("y", playerMovementInput.z);

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
                animator.SetTrigger("jump");
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }
        
        characterController.Move(moveVector * (speed * Time.deltaTime));
        characterController.Move(velocity * Time.deltaTime);
        
        transform.Rotate(Vector3.up * (rotSpeed * horizontalInput * Time.deltaTime));

    }
    
    
}
