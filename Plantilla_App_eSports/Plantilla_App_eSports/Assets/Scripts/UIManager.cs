using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    
    [Header("---- PANELES APP ----")]
    public GameObject panel_login;
    public GameObject panel_crear_usuario;
    public GameObject panel_menu_principal;
    public GameObject panel_perfil_personal;
    public GameObject panel_lista_usuarios;
    
    [Header("---- ELEMENTOS PANTALLA LOGIN ----"),Space(10)]
    public TMP_InputField email_usuario_login_inputField;
    public TMP_InputField password_usuario_login_inputField;
    
    [Header("---- UI PANTALLA CREAR USUARIO ----"),Space(10)]
    public TMP_InputField nombre_usuario_crear_inputField;
    public TMP_InputField email_usuario_crear_inputField;
    public TMP_InputField password_usuario_crear_inputField;

    [Header("---- UI PANTALLA PERFIL PERSONAL ----"),Space(10)]
    public TMP_Text nombre_usuario_panel_pefil;
    public TMP_Text email_usuario_panel_perfil;

    [Header("---- UI PANTALLA LISTA USUARIOS ----"),Space(10)]
    public TMP_Text nombre_usuario_panel_listaUsuarios;
    public TMP_Text email_usuario_panel_listaUsuarios;


    [Header("----- DATOS USUARIOS ----"), Space(10)]
    public Usuarios datos_Usuarios;
    public Usuario usuario_conectado;  
    public List<Usuario> listaUsuarios_TMP = new List<Usuario>();
    public bool existeUsuarioConMismosDatos;


    private void Awake()
    {
        instance = this;
    }

    

    /// <summary>
    /// Método para crear un usuario a partir de la información introducida en la UI de crear usuario
    /// </summary>
    public void CrearUsuario()
    {
        
        // Primero verificamos que todos los input field de la pantalla de crear usuario estén informados para 
        // poder crear un nuevo usuario
        if (!string.IsNullOrEmpty(nombre_usuario_crear_inputField.text) 
            && !string.IsNullOrEmpty(email_usuario_crear_inputField.text) 
            && !string.IsNullOrEmpty(password_usuario_crear_inputField.text))
        {
            
            // Reseteamos la lista de Usuarios
            listaUsuarios_TMP = new List<Usuario>();
            
            // Creamos un nuevo usuario con los datos que obtenemos de la UI (de los input field)
            Usuario nuevo_usuario = new Usuario();
            nuevo_usuario.nombre = nombre_usuario_crear_inputField.text;
            nuevo_usuario.email = email_usuario_crear_inputField.text;
            nuevo_usuario.password = password_usuario_crear_inputField.text;

            existeUsuarioConMismosDatos = false;

            // Verificamos que los datos del nuevo usuario no existan en la lista de Usuarios
            foreach (Usuario usuario in datos_Usuarios.listaUsuarios)
            {
                if (nuevo_usuario.email.Equals(usuario.email))
                {
                    // Si entramos en este if quiere decir que hemos introducido un email que ya existe en la lista 
                    // de usuario, por lo que mostraremos un mensaje de error al usuario
                    existeUsuarioConMismosDatos = true;
                    Toast.Show("Mismo email encontrado");
                    // Salimos del foreach ya que hemos encontrado un usuario con el mismo email que hemos informado
                    break;
                }
                
                // Solo llegará a esta línea si no entra en el if
                listaUsuarios_TMP.Add(usuario);
            }

            // Revisamos el valor de la variable existeUsuarioConMismosDatos para saber si añadimos el nuevo usuario a la 
            // lista de usuarios
            if (existeUsuarioConMismosDatos == false)
            {
                // Si entramos en este if quiere decir que los datos introducidos en la UI no coinciden con ningún usuario
                // de la lista de usuarios
                listaUsuarios_TMP.Add(nuevo_usuario);

                datos_Usuarios.listaUsuarios = listaUsuarios_TMP.ToArray();
                
                WebRequest_Usuarios.Instance.Escribir_Lista_Usuarios_en_JSON(datos_Usuarios);
                
                Toast.Show("Usuario creado", ToastColor.Purple);
                // Llamamos al método ResetInputField_CrearUsuario para vaciar el texto de los inputfield de la pantalla de crear usuario
                ResetInputField_CrearUsuario();
            }
            
        }
        else
        {
            // Si entramos aquí es porque el usuario no ha introducido datos en todos los input field de la pantalla 
            // de crear usuario
            Toast.Show("Introduce todos los datos para crear un usuario");
        }



    }

    

    /// <summary>
    /// Método para realizar la lógica de verificar los datos introducidos por los inputs fields de la pantalla de login
    /// e iniciar sesión si coincide con los datos de algún usuario existente
    /// </summary>
    public void Login()
    {
        
        // Primero verificamos que los input fiel de la pantalla de login estén informados 
        if (!string.IsNullOrEmpty(email_usuario_login_inputField.text) && !string.IsNullOrEmpty(password_usuario_login_inputField.text))
        {
            // Recorremos toda la lista de usuarios para verificar que la información introducida en los input fields
            // sea la misma que la de un usuario existente, en caso que exista mostraremos el panel menú principal
            // si no existe ningún usuario con la información introducida, mostraremos un mensaje de error
            foreach (Usuario usuario in datos_Usuarios.listaUsuarios)
            {
                if (email_usuario_login_inputField.text.Equals(usuario.email) && password_usuario_login_inputField.text.Equals(usuario.password))
                {
                    // Si entramos en este IF es porque hemos encontrado un usuario de la lista de usuarios con los mismos datos que los inputs
                    // field de la pantalla de login
                    existeUsuarioConMismosDatos = true;
                    
                    // Guardamos la información del usuario que tiene la misma info que los inputs field en nuestra
                    // variable usuario_conectado, para luego poder acceder facilmente
                    usuario_conectado = usuario;
                    
                    // Salimos del foreach porque ya hemos encontrado el usuario
                    break;
                }
            }

            // Miramos el valor de la variable existeUsuarioConMismosDatos para mostras un mensaje al usuario o pasarlo 
            // a la siguiente pantalla
            if (existeUsuarioConMismosDatos == true)
            {
                // Primero reseteamos los datos introducidos en los inputs de la pantalla de login
                ResetInputField_Login();
                Mostrar_Pantalla_MenuPrincipal();
                Mostrar_Panel_PerfilPersonal();
            }
            else
            {
                // Mostramos un mensaje de error al usuario ya que no se ha encontrado ningún usuario con los datos introducidos
                // en los inputs field de la UI de login
                Toast.Show("Los datos introducidos no son correctos");
            }
        }
        else
        {
            // Entraremos en esta lógica cuando el usuario no rellene la información de los input field de la pantalla de Login
            Toast.Show("Tienes que introducir todos los datos para iniciar sesión");
        }
        
    }
    
    
    /// <summary>
    /// Método para resetear los campos input field de la pantalla de Crear usuario
    /// </summary>
    private void ResetInputField_CrearUsuario()
    {
        nombre_usuario_crear_inputField.text = "";
        email_usuario_crear_inputField.text = "";
        password_usuario_crear_inputField.text = "";
    }


    /// <summary>
    /// Método para resetear los campos input field de la pantalla Login
    /// </summary>
    private void ResetInputField_Login()
    {
        email_usuario_login_inputField.text = "";
        password_usuario_login_inputField.text = "";
    }


    /// <summary>
    /// Método para rellenar los datos del perfil personal de la persona conectada 
    /// </summary>
    public void Rellenar_Info_Perfil_Personal()
    {
        // Utilizamos los datos del usuario conectado para rellenar los datos del perfil personal
        nombre_usuario_panel_pefil.text = usuario_conectado.nombre;
        email_usuario_panel_perfil.text = usuario_conectado.email;
    }



    /// <summary>
    /// Método para mostrar la pantalla de login y ocultar el resto
    /// </summary>
    public void Mostrar_Pantalla_Login()
    {
        panel_menu_principal.SetActive(false);
        panel_crear_usuario.SetActive(false);
        ResetInputField_Login();
        panel_login.SetActive(true);
    }

    
    
    /// <summary>
    /// Método para mostrar la pantalla de login y ocultar el resto
    /// </summary>
    public void Mostrar_Pantalla_CrearUsuario()
    {
        panel_login.SetActive(false);
        panel_menu_principal.SetActive(false);
        ResetInputField_CrearUsuario();
        panel_crear_usuario.SetActive(true);
    }
    
    
    
    /// <summary>
    /// Método para mostrar la pantalla de login y ocultar el resto
    /// </summary>
    public void Mostrar_Pantalla_MenuPrincipal()
    {
        panel_login.SetActive(false);
        panel_crear_usuario.SetActive(false);
        panel_menu_principal.SetActive(true);
    }
    
    /// <summary>
    /// Método para mostrar el panel de perfil personal y ocultar el resto de paneles del menu principal
    /// </summary>
    public void Mostrar_Panel_PerfilPersonal()
    {
        Rellenar_Info_Perfil_Personal();
        panel_lista_usuarios.SetActive(false);
        panel_perfil_personal.SetActive(true);
    }
    
    /// <summary>
    /// Método para mostrar el panel de listaUsuarios y ocultar el resto de paneles del menu principal
    /// </summary>
    public void Mostrar_Panel_ListaUsuarios()
    {
        panel_perfil_personal.SetActive(false);
        panel_lista_usuarios.SetActive(true);
    }
    


    /// <summary>
    /// Método para indicar un mensaje al usuario que esta funcionalidad aun no está implementadas
    /// </summary>
    public void Mostrar_Mensaje_FuncionalidadNoImplementada()
    {
        Toast.Show("Funcionalidad por implementar");
    }
    

    /// <summary>
    /// Método para salir de la app tanto en play mode como en build
    /// </summary>
    public void SalirAPP()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    
}
