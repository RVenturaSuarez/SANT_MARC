using System;
using UnityEngine;

[Serializable]
public class Slot_Inventory : MonoBehaviour
{
    [SerializeField, Tooltip("Variable que indica que si el slot del inventario está ocupado o no")]
    public bool isUsed;
}
