using UnityEngine;

public class CameraLook : MonoBehaviour
{

    // Variables
    public float mouseSensiblity = 80f;
    public Transform playerBody;
    
    private float mouseX;
    private float mouseY;
    private float xRotation = 0;
    

    void Update()
    {
        // Accedemos a los axis del mouse
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // Lógica para controlar el movimiento de la camara de arriba-abajo
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f); // Definimos lo que podemos subir máximo y lo que podemos bajar máximo
        
        transform.localRotation = Quaternion.Euler(xRotation,0 ,0); // Aplicamos esa rotación a la camara
        
        // Rootamos al cuerpo de nuestro jugador de izquierda y derecha
        // Como nuestro cuerpo es padre de la camara la moverá también
        playerBody.Rotate(Vector3.up * mouseX * mouseSensiblity * Time.deltaTime);
        
    }
}
