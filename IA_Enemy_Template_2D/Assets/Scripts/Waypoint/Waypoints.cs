using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Header("---- WAYPOINTS ----")]
    [SerializeField, Tooltip("Array de Vectores3, que representan la posición de los waypoints en el mapa")] 
    private Vector3[] waypointsArray;
    private Transform[] waypointsArrays;

    [Space]
    [Header("---- CUSTOMIZATION ----")]
    [SerializeField, Tooltip("Float para indicar el radio del gizmo " +
                             " que se mostrará como representación del Wapypoint")]
    private float radiusSphereWaypoint;

    
    // ##### Private variables ##### //
    
    // Variable para indicar la posición actual del propietario del script
    private Vector3 currentPosition;
    // Esta variable indica si hemos activao el playMode o no
    private bool gameOn;
    // Index para determinar el waypoint a devolver de la array de Waypoints
    private int currentIndex;
    // Waypoint al que se dirige el personaje
    private Vector3 currentWaypoint;

    // ##### GETTERS & SETTERS ##### //
    public Vector3[] WaypointsArray => waypointsArray;

    public Vector3 CurrentPosition => currentPosition;

    public Vector3 CurrentWaypoint => currentWaypoint;
    

    private void Awake()
    {
        gameOn = true;
        currentPosition = transform.position;

        // Si por cualquier situación el array de waypoints está vacia el waypoint al que se dirigirá el jugador será 
        // su posición actual para evitar errores
        if (waypointsArray == null || waypointsArray.Length <= 0)
        {
            currentWaypoint = transform.position;
        }
        else
        {
            currentIndex = 0;
            currentWaypoint = currentPosition + waypointsArray[currentIndex];
        }

    }


    /// <summary>
    /// Método para devolver el siguiente waypoint de la array de waypoints del personaje/enemigo
    /// </summary>
    /// <returns>Un Vector3 que determinará hacia donde tiene que ir el personaje/enemigo</returns>
    public Vector3 GetNextWayPoint()
    {
        currentIndex = (currentIndex + 1) % waypointsArray.Length;
        return currentPosition + waypointsArray[currentIndex];
    }
    

    private void OnDrawGizmos()
    {
        // Si no estamos en playMode y hemos modificado el transform del personaje/enemigo que tiene este script
        // modificaremos la posición de los waypoints
        if (!gameOn && transform.hasChanged)
        {
            currentPosition = transform.position;
        }
        
        
        // Comprobamos que el array no sea nula o la longitud no sea inferior a 0
        // de ser así saldremos de la lógica con el return
        if (waypointsArray == null || waypointsArray.Length <= 0)
        {
            return;
        }

        // Recorremos todos los elementos del array para representarlos con gizmos Utilizando DrawWireSphere
        for (int i = 0; i < waypointsArray.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(waypointsArray[i] + currentPosition, radiusSphereWaypoint);
            

            // Dibujamos unas líneas entre los waypoints 
            if (i < waypointsArray.LongLength -1)
            {
                Gizmos.color = Color.gray;
                // Dibujamos la línea entre el waypoint actual y el siguiente de la lista
                Gizmos.DrawLine(waypointsArray[i] + currentPosition, waypointsArray[i +1] + currentPosition);
            }
        }
    }
}
