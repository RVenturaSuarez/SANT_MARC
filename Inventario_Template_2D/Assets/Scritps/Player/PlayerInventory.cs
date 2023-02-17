using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("---- PROPERTIES ----")] 
    [SerializeField, Tooltip("Array de de Slot_Inventory que representan los slots del inventario de la UI para saber" +
                             " si el objecto esta lleno o no")]
    private Slot_Inventory[] slots;
    
    
    // Diccionario para almacenar el nombre de los items y la cantida de ese item
    private Dictionary<string, int> dictionary_inventory = new Dictionary<string, int>();


    
    public bool CheckInventory(GameObject itemToAdd, string itemName, int itemAmount)
    {
        bool itemAdded = false; 
        
        
        // Primero revisamos que el objeto exista en nuestro diccionario
        if (dictionary_inventory.ContainsKey(itemName))
        {
            
            for (int i = 0; i < slots.Length; i++)
            {
                // Miramos cual de los hijos de los slots coincide con el nombre del item
                if (slots[i].transform.GetChild(0).gameObject.name.Equals(itemName))
                {
                    // Sumamos la cantidad a la actual
                    dictionary_inventory[itemName] += itemAmount;

                    // Actualizamos el texto del hijo del hijo del componente slot
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        $"{dictionary_inventory[itemName]}";

                    itemAdded = true;
                    break;
                }

            }

            return itemAdded;
        }
        

        
        // Recorremos toda la array de slots para comprobar si hay espacios disponibles
        for (int j = 0; j < slots.Length; j++)
        {
            // Si el slot que estamos recorriendo no está ocupado añadimos el objeto
            if (!slots[j].isUsed)
            {
                // Miramos que no exista el item en nuestro diccionario
                // En caso de no existir quiere decir que tenemos que crearlo en los slots si existe 
                // solo tendremos que actualizar info
                if (!dictionary_inventory.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, slots[j].transform, false);
                    item.name = item.name.Replace("(Clone)", "");
                    slots[j].isUsed = true;
                    
                    // Agregamos el item al diccionario indicando el nombre del item y su posición
                    dictionary_inventory.Add(itemName, itemAmount);
                    
                    // Obtenemos el primer hijo del objeto creado y le cambiamos el texto para indicar
                    // cantidad del item
                    item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{itemAmount}";

                    itemAdded = true;
                    
                    break;
                }
            }
        }

        return itemAdded;
    }


    public void UseItemInventory(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Miramos cual de los hijos de los slots coincide con el nombre del item
            if (slots[i].transform.GetChild(0).gameObject.name.Equals(itemName))
            {
                // Restamos uno a la cantidad a la actual
                dictionary_inventory[itemName]--;

                // Hemos consumido todos los objetos por lo que destruimos el item del slot
                if (dictionary_inventory[itemName] <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].isUsed = false;
                    dictionary_inventory.Remove(itemName);
                    break;
                }

                // Actualizamos el texto del hijo del hijo del componente slot
                slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    $"{dictionary_inventory[itemName]}";
                
                break;
            }

        }
        
    }
    
    
}
