using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject portal;
    public bool isRedCubeOK;
    public bool isGreenCubeOK;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CheckStateCubes()
    {
        if (isGreenCubeOK && isRedCubeOK)
        {
            portal.SetActive(true);
        } else
        {
            portal.SetActive(false);
        }
    }



}
