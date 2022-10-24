using EasyUI.Toast;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }  

    [Header("---- Paneles APP ----")]
    public GameObject panel_login;
    public GameObject panel_crear_usuario;
    public GameObject panel_menu_principal;

    [Header("---- Panel Login ----")]
    public TMP_InputField correo_electronico_inputField_login;
    public TMP_InputField password_inputField_login;

    [Header("---- Panel Crear ----")]
    public TMP_InputField correo_electronico_inputField_crear;
    public TMP_InputField password_inputField_crear;

    [Header("---- Panel Menú Principal ----")]
    public TextMeshProUGUI titulo_menu_principal;


    public Usuario usuario_creado;


    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// Método para crear un usuario a partir de los datos que recogemos de la UI
    /// de crear usuario
    /// </summary>
    public void CrearUsuario()
    {
        // Verificamos que los dos campos tienen información
        if(!string.IsNullOrEmpty(correo_electronico_inputField_crear.text) &&
            !string.IsNullOrEmpty(password_inputField_crear.text))
        {
            // Creamos un nuevo usuario con la información que está introducida en la UI.
            Usuario nuevoUsuario = new Usuario();
            nuevoUsuario.email = correo_electronico_inputField_crear.text;
            nuevoUsuario.password = password_inputField_crear.text;

            // Asignamos a nuestra variable de tipo Usuario, el usuario que acabamos de crear
            usuario_creado = nuevoUsuario;

            // Mostramos de nuevo el panel para logearse
            MostrarPanelLogin();

        } else
        {
            // Mostramos mensaje de error en caso de no introducir valor en los campos obligatorios
            Toast.Show("Necesitas introducir valores en los campos obligatorios para crear un usuario");
        }
    }


    public void RealizarLogin()
    {
        // Verificamos que los dos campos tienen información
        if (!string.IsNullOrEmpty(correo_electronico_inputField_login.text) &&
            !string.IsNullOrEmpty(password_inputField_login.text))
        {
            // Verificamos que la info introducida es la misma que la del usuario creado
            if (correo_electronico_inputField_login.text.Equals(usuario_creado.email) &&
                password_inputField_login.text.Equals(usuario_creado.password)) {

                // Realizamos el lógin y cambiamos el texto del panel menú principal
                titulo_menu_principal.text = "Bienvenido " + usuario_creado.email;

                // Mostramos el panel del menu principal
                MostrarPanelMenuPrincipal();

            } else
            {
                // Mostramos mensaje de error en caso de no introducir los mismos datos que la
                // del usuario creado
                Toast.Show("Los datos introducidos son incorrectos");
            }
        } else
        {
            // Mostramos mensaje de error en caso de no introducir valor en los campos obligatorios
            Toast.Show("Necesitas introducir valores en los campos obligatorios para realizar el login");
        }
    }





    public void MostrarPanelLogin()
    {
        panel_login.SetActive(true);
        panel_crear_usuario.SetActive(false);
        panel_menu_principal.SetActive(false);
    }

    public void MostrarPanelCrearUsuario()
    {
        panel_crear_usuario.SetActive(true);
        panel_login.SetActive(false);
        panel_menu_principal.SetActive(false);
    }

    public void MostrarPanelMenuPrincipal()
    {
        panel_menu_principal.SetActive(true);
        panel_login.SetActive(false);
        panel_crear_usuario.SetActive(false);
    }


}
