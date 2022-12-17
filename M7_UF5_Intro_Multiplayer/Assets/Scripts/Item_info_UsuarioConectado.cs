
using UnityEngine;
using TMPro;

public class Item_info_UsuarioConectado : MonoBehaviour
{

    public string nickname_usuario;
    
    
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nickname_usuario;
    }

}