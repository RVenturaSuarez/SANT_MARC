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
                GameManager.instance.isRedCubeOK = true;
                GameManager.instance.CheckStateCubes();
            }
            else
            {
                GameManager.instance.isGreenCubeOK = true;
                GameManager.instance.CheckStateCubes();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Red_Cube"))
        {
            GameManager.instance.isRedCubeOK = false;
            GameManager.instance.CheckStateCubes();
        }
        else
        {
            GameManager.instance.isGreenCubeOK = false;
            GameManager.instance.CheckStateCubes();
        }

    }

}
