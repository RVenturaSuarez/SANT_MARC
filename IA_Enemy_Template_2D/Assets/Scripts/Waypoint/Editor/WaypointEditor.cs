using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoints))]
public class WaypointEditor : Editor
{
     // Necesitamos un target para poder modificarlo
     Waypoints WaypointTarget => target as Waypoints;


     private void OnSceneGUI()
     {
          // Si no existe ningún elemento en el array de Waypoints no haremos nada
          if (WaypointTarget.WaypointsArray == null)
          {
               return;
          }
          
          Handles.color = Color.red;

          for (int i = 0; i < WaypointTarget.WaypointsArray.Length; i++)
          {
               EditorGUI.BeginChangeCheck();
               Vector3 currentPoint = WaypointTarget.CurrentPosition + WaypointTarget.WaypointsArray[i];

               Vector3 newPoint = Handles.FreeMoveHandle(currentPoint, Quaternion.identity, 0.7f,
                    new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);
               
               // Creamos un texto para indicar el numero del waypoint y definimos sus propiedades
               GUIStyle texto = new GUIStyle();
               texto.fontStyle = FontStyle.Bold;
               texto.fontSize = 16;
               texto.normal.textColor = Color.black;
               
               // lo posicionamos
               Vector3 alignment = Vector3.down * 0.3f + Vector3.right * 0.3f;
               Handles.Label(WaypointTarget.CurrentPosition + WaypointTarget.WaypointsArray[i] + alignment, 
                    $"{i + 1}", texto);

               if (EditorGUI.EndChangeCheck())
               {
                    Undo.RecordObject(target, "Free Move Handle");
                    WaypointTarget.WaypointsArray[i] = newPoint - WaypointTarget.CurrentPosition;
               }
          }
          
     }
}
