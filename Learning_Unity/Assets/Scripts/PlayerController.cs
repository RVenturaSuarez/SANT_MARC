using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Velocities")] 
    public float speed;
    public float crouchSpeed;
    public float runSpeed;
    public float rotationSpeed;
    
    [Header("Forces")]
    public float jumpForce;
    public float impulsoGolpe;

    private const string HORIZONTAL_STR = "Horizontal";
    private const string VERTICAL_STR = "Vertical";
    private const string VELX_STR = "VelX";
    private const string VELY_STR = "VelY";
    private const string SALTE_STR = "Salte";
    private const string TOCO_SUELO_STR = "TocoSuelo";
    private const string AGACHADO_STR = "Agachado";
    private const string CORRIENDO_STR = "Corriendo";
    private const string GOLPEO_STR = "Golpeo";
    
    private float horizontalInput;
    private float verticalInput;
    private bool canJump;
    private float initialSpeed;
    private bool estoyAtacando;
    private bool avanzoSolo;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    

    void Start()
    {
        initialSpeed = speed;
        
        // Inicializamos los componentes del player
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        // Capturamos el valor de los inputs.
        horizontalInput = Input.GetAxis(HORIZONTAL_STR);
        verticalInput = Input.GetAxis(VERTICAL_STR);

        // Actualizamos las variables del animator
        _animator.SetFloat(VELX_STR, horizontalInput);
        _animator.SetFloat(VELY_STR, verticalInput);


        if (Input.GetKeyDown(KeyCode.Mouse0) && canJump && !estoyAtacando)
        {
            _animator.SetTrigger(GOLPEO_STR);
            estoyAtacando = true;
        }
        
        
        // Comprobamos si el jugador puede saltar y aplicamos la lógica correspondiente
        if (canJump)
        {
            if (!estoyAtacando)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    _animator.SetBool(AGACHADO_STR, true);
                    speed = crouchSpeed;
                }
                else
                {
                    _animator.SetBool(AGACHADO_STR, false);
                    speed = initialSpeed;
                }
            
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _animator.SetBool(CORRIENDO_STR, true);
                    speed = runSpeed;
                }
                else
                {
                    _animator.SetBool(CORRIENDO_STR, false);
                    speed = initialSpeed;
                } 
            }
            
            _animator.SetBool(TOCO_SUELO_STR, true);
        }
        else
        {
            IAmFalling();
        }
        

    }
    
    private void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            // Lógica para el desplazamiento del jugador
            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
            transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * rotationSpeed); 
        }
        
        if (avanzoSolo)
        {
            _rigidbody.velocity = transform.forward * impulsoGolpe;
        }
    }

    /// <summary>
    /// Método para aplicar una fuerza de impulso al jugador y simular un salto
    /// </summary>
    private void Jump()
    {
        _animator.SetBool(SALTE_STR, true);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
    }

    /// <summary>
    /// Método que analiza si el jugador esta en el aire
    /// </summary>
    private void IAmFalling()
    {
        _animator.SetBool(TOCO_SUELO_STR, false);
        _animator.SetBool(SALTE_STR, false);
    }
    
    
    /// <summary>
    /// Getter & setters de la variable canJump
    /// </summary>
    public bool CanJump {
        get => canJump;
        set => canJump = value;
    }

    public void DejeDeGolpear()
    {
        estoyAtacando = false;
    }

    public void AvanzoSolo()
    {
        avanzoSolo = true;
    }

    public void DejoDeAvanzar()
    {
        avanzoSolo = false;
    }
}
