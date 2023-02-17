using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("---- ATTRIBUTES ----")] 
    [SerializeField, Tooltip("Velocidad del jugador")]
    private float speed;
    
    [SerializeField, Tooltip("Velocidad del jugador en el aire")]
    private float speedInAir;
    
    [SerializeField, Tooltip("Fuerza de salto")]
    private float jumpForce;


    [Space]
    [Header("--- FOOT ----")]
    [SerializeField, Tooltip("Transform que representa la posición de los pies del jugador")]
    private Transform foot;
    
    [SerializeField, Tooltip("Radio de los pies para determinar si el player está tocando el suelo o no")]
    private float radius;
    
    [SerializeField, Tooltip("Layer mask para determinar la colisión contra el suelo")]
    private LayerMask layerMaskFloor;

    
    
    // Private variables //
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float horizontalInput;
    private bool canJump;
    
    void Start()
    {
        // Obtenemos los componentes del player
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        //############# INPUTS ################
        
        // Capturamos el valor de los axis AD puesto que el jugador solo se moverá de izquierda a derecha
        horizontalInput = Input.GetAxisRaw("Horizontal");


        //############# LÓGICA SALTO ################
        
        // Logica salto
        canJump = Physics2D.OverlapCircle(foot.transform.position, radius, layerMaskFloor);

        if (canJump)
        {
            // Asignamos los valores a los parámetros del animator para decir que el personaje no está cayendo
            _animator.SetBool("Falling", false);
            
            // Lógica salto
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Disparamos el trigger para saltar
                _animator.SetTrigger("Jump");
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            // Asignamos los valores a los parámetros del animator para decir que el personaje está cayendo
            _animator.SetBool("Falling", true);
        }
        
        
        //############# CONTROL IDLE ################

        
        // Ni no nos estamos moviendo lo indicamos en el animator para representar las animacioenes de Idle
        if (horizontalInput == 0)
        {
            _animator.SetBool("Walking", false);
            return;
           
        }
        
        
        //############# CONTROL ORIENTACIÓN PLAYER ################

        // Miramos el valor de la X para saber en que dirección tiene que apuntar el jugador
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {            
            // Flipeamos al personaje para que mire a la izquierda
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }

        if (canJump)
        {
            // Asignamos los valores a los parámetros del animator
            _animator.SetBool("Walking", true);
        }

        
    }

    private void FixedUpdate()
    {
        // Movemos al personaje con una velocidad o otra dependiendo si está en el aire o en el suelo
        if (canJump)
        {
            _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(horizontalInput * speedInAir, _rigidbody2D.velocity.y);

        }

    }


    /// <summary>
    /// Método de Unity para Dibujar, en este caso dibujaremos una esfera para saber el radio de los pies
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(foot.transform.position, radius);
    }
}
