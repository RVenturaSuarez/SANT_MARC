using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest_Usuarios : MonoBehaviour
{
    public static WebRequest_Usuarios Instance;
    
    public Usuarios datos_usuarios_webRequest;


    private void Awake()
    {
        Instance = this;
    }

    public void Leer_JSON_Usuarios_Y_Crear_Usuario()
    {
        StartCoroutine(Corrutina_LEER_JSON_USUARIOS_Y_CREAR_USUARIO());
    }
    
    public void Leer_JSON_Usuarios_Y_Llamar_Login()
    {
        StartCoroutine(Corrutina_LEER_JSON_USUARIOS_Y_LLAMAR_LOGIN());
    }
    
    public void Escribir_Lista_Usuarios_en_JSON(Usuarios usuarios_data)
    {
        StartCoroutine(Corrutina_ESCRIBIR_LISTA_USUARIOS_EN_JSON(usuarios_data));
    }
    
    
    [ContextMenu("Crear Lista USUARIOS VACIA")]
    public void Crear_Lista_Usuarios_Vacia()
    {
        StartCoroutine(Corrutina_Crear_Lista_Usuarios_Vacia(datos_usuarios_webRequest));
    }
    
    
    
    /// <summary>
    /// Método para leer los usuarios que están en el JSON para luego crear un nuevo usuario a partir de los
    /// datos introducidos por la UI
    /// </summary>
    /// <returns></returns>
    private IEnumerator Corrutina_LEER_JSON_USUARIOS_Y_CREAR_USUARIO()
    {
        UnityWebRequest web = UnityWebRequest.Get(Constantes.URL_FILES_JSON_APP_eSPORTS + Constantes.NOMBRE_FILE_USUARIOS + Constantes.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            datos_usuarios_webRequest = JsonUtility.FromJson<Usuarios>(web.downloadHandler.text);
            UIManager.instance.datos_Usuarios = datos_usuarios_webRequest;
            UIManager.instance.CrearUsuario();
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }
    
    
    /// <summary>
    /// Método para leer los usuarios que están en el JSON para luego realizar el lógin a partir de los datos introducidos
    /// en la UI
    /// </summary>
    /// <returns></returns>
    private IEnumerator Corrutina_LEER_JSON_USUARIOS_Y_LLAMAR_LOGIN()
    {
        UnityWebRequest web = UnityWebRequest.Get(Constantes.URL_FILES_JSON_APP_eSPORTS + Constantes.NOMBRE_FILE_USUARIOS + Constantes.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            datos_usuarios_webRequest = JsonUtility.FromJson<Usuarios>(web.downloadHandler.text);
            UIManager.instance.datos_Usuarios = datos_usuarios_webRequest;
            UIManager.instance.Login();
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }
    
    
    /// <summary>
    /// Método para escribir en el fichero JSON de la nube los usuarios que existen de la aplicación app eSports
    /// </summary>
    /// <param name="usuarios_data">Usuarios que tenemos que añadir al json</param>
    /// <returns></returns>
    private IEnumerator Corrutina_ESCRIBIR_LISTA_USUARIOS_EN_JSON(Usuarios usuarios_data)
    {
        WWWForm form = new WWWForm();
        form.AddField(Constantes.PARAM_FILE, Constantes.NOMBRE_FILE_USUARIOS);
        form.AddField(Constantes.PARAM_TEXT, JsonUtility.ToJson(usuarios_data));
        
        UnityWebRequest web = UnityWebRequest.Post(Constantes.URL_FILE_TO_WRITE, form);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }
    
    
    private IEnumerator Corrutina_Crear_Lista_Usuarios_Vacia(Usuarios datos_usuarios_webRequest)
    {
        WWWForm form = new WWWForm();
        form.AddField(Constantes.PARAM_FILE, Constantes.NOMBRE_FILE_USUARIOS);
        form.AddField(Constantes.PARAM_TEXT, JsonUtility.ToJson(datos_usuarios_webRequest));
        
        UnityWebRequest web = UnityWebRequest.Post(Constantes.URL_FILE_TO_WRITE, form);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }
}
