using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Character Variables")]
    public float speed;
    public Camera playerCamera;
    public Transform targetPoint;
    public Transform respawn;

    [Space(10)]
    [Header("Pickups Variables")]
    public bool haveAPickup;
    private GameObject pickup;
    public float rayCastRange;
    public LayerMask pickupMask;
    
    
    // Private Variables
    private float horizontalInput;
    private float verticalInput;



    void Update()
    {
        // Accedemos a los axis horizontales y verticales
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        // Lógica para mover al jugador
        transform.Translate(Vector3.forward * (speed * verticalInput * Time.deltaTime));
        transform.Translate(Vector3.right * (speed * horizontalInput * Time.deltaTime));


        // Lógica para controlar que el jugador no caiga de cierta altura y vuelva a un respawn
        if (transform.position.y <= -0.5f)
        {
            transform.position = respawn.transform.position; // Movemos la posición del jugador a la de un respawn
        }


        // Si el jugador pulsa la letra E realizaremos la siguiente lógica
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            // Primero miramos si nuestra variable bool esta a False, indicando que no tenemos un objeto en ese momento
            if (!haveAPickup) 
            {
                // Creamos una variable de tipo RaycastHit
                RaycastHit hit; 
                
                // Con la función Physics.Raycast no devolverá un true o false con lo parámetros que le pasamos
                // En caso que el resultado sea true entraremos en el IF
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayCastRange, pickupMask))
                {
                    
                    haveAPickup = true; // Indicamos que tenemos un objeto por si pulsamos de nuevo la E que la lógica sea diferente
                    hit.transform.position = targetPoint.transform.position; // El objeto que hemos impactado pasa a una posición indicada
                    hit.transform.parent = targetPoint; // Indicamos que el objeto que hemos impactado ahora es hijo nuestro
                    hit.rigidbody.isKinematic = true; // Activamos isKinematic del objeto impactado para evitar que le afecte la gravedad
                    pickup = hit.transform.gameObject; // Guardamos en nuestra variable Pickup la información del objeto con el que impactamos.
                }
            }
            else
            {
                haveAPickup = false; // Indicamos que ya no tenemos un objetos por si pulsamos de nuevo la E volvamos a la lógica anterior
                pickup.transform.parent = null; // El objetos que teniamos ya no es nuestro hijo por lo que no seguirá nuestro movimiento
                pickup.GetComponent<Rigidbody>().isKinematic = false; // Desactivamos la propiedad isKinematic del objeto para que le afete la gravedad de nuevo
            }
            
        }
        
    }
    
}
