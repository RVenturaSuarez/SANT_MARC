using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbody_ThirdPerson : MonoBehaviour
{
    [Header("--- Player Components ---")]
    [SerializeField] private Transform feetTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Animator animator;
    
    [Space]
    [Header("--- Player Attributes ---")]
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float jumpForce;

    // Private Attributes
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private float horizontalInput;
    

    void Update()
    {
        // Capturamos los inputs del jugador y los almacenamos en las Variables de tipo Vector3 y Vector2
        playerMovementInput = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        horizontalInput = Input.GetAxis("Horizontal");
        
        MovePlayer();
        
        //transform.Translate(Vector3.forward * speed * Time.deltaTime * playerMovementInput.z);
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * horizontalInput);
        animator.SetFloat("x", horizontalInput);
        animator.SetFloat("y", playerMovementInput.z);
    }

    
    /// <summary>
    /// Método para mover al jugador hacia la dirección que esté pulsado con el teclado
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed ;
        _rigidbody.velocity = new Vector3(moveVector.x, _rigidbody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Comprobamos que el transform que simula nuestros pies esté chocando con la layerMask que le indicamos
            if (Physics.CheckSphere(feetTransform.position, 0.1f, floorMask ))
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplicamos un impulso hacia arriba
                animator.SetTrigger("jump");
            }
        }
        
        
    }

}
