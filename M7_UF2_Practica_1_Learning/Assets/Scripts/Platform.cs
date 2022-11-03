using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public string target_tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(target_tag))
        {
            if (target_tag.Equals("Red_Cube"))
            {
                GameManager.instance.currentLevelManager.isRedCubeOK = true;
                GameManager.instance.currentLevelManager.CheckStateObjetives();
            }
            else
            {
                GameManager.instance.currentLevelManager.isGreenCubeOK = true;
                GameManager.instance.currentLevelManager.CheckStateObjetives();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(target_tag))
        {
            if (target_tag.Equals("Red_Cube"))
            {
                GameManager.instance.currentLevelManager.isRedCubeOK = false;
                GameManager.instance.currentLevelManager.CheckStateObjetives();
            }
            else
            {
                GameManager.instance.currentLevelManager.isGreenCubeOK = false;
                GameManager.instance.currentLevelManager.CheckStateObjetives();
            }
        }

    }

}
