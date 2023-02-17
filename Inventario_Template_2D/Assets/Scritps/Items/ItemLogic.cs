using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    // Private Variables //
    private PlayerInventory _playerInventory;
    
    private void Start()
    {
        // Recuperamos el script de inventario del player para poder añadirle objetos
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    
    public void UseArticle()
    {
        Debug.Log("Hola soy " + gameObject.name);
        
        // Actualizamos la info del articulo del inventario
        _playerInventory.UseItemInventory(gameObject.name);
    }
}
