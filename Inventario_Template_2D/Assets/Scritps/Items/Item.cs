using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("---- PROPERTIES ----")] 
    [SerializeField, Tooltip("Boton que se instanciará en el inventario de la UI y que tendrá una lógica diferente" +
                             " según el objeto")]
    private GameObject ItemBtn;

    [SerializeField, Tooltip("Cantidad de objetos a añadir")]
    private int amount;


    // Private Variables //
    private PlayerInventory _playerInventory;


    private void Start()
    {
        // Recuperamos el script de inventario del player para poder añadirle objetos
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Probamos de añadir el objeto al inventario del jugador pasando los datos necesarios
            // en caso de añadirlo la variable itemAdded quedará a true de lo contraro a false
            bool itemAdded = _playerInventory.CheckInventory(ItemBtn, ItemBtn.name, amount);
            
            // Solo destruimos el objeto si se ha añadido al inventario de lo contrario lo dejaremos en la escena
            if (itemAdded)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
