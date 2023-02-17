using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField, Tooltip("Velocidad del jugador")] 
   private float speed;
   
   
   // Private variables
   private Rigidbody2D _rigidbody2D;
   private Animator _animator;
   private Vector2 _input;
   

   void Start()
   {
       // Recuperamos los componentes del player
       _rigidbody2D = GetComponent<Rigidbody2D>();
       _animator = GetComponent<Animator>();
   }
  
  
   void Update()
   {
       // Capturamos el valor de los axis WASD
       _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


       if (_input.magnitude == 0)
       {
           _animator.SetBool("Walking", false);
           return;
           
       }
       
        // Asignamos los valores a los parámetros del animator
       _animator.SetBool("Walking", true);
       _animator.SetFloat("X", _input.x);
       _animator.SetFloat("Y", _input.y);
       
   }

   private void FixedUpdate()
   {
       // Movemos al personaje
       _rigidbody2D.MovePosition(_rigidbody2D.position + _input * (speed * Time.deltaTime));
   }
   
}

