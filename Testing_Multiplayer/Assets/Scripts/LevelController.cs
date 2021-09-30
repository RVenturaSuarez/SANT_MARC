using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instancia;
    public Text score_text;
    private GameObject[] pickups_list;
    public int max_count_pickups;
    public int current_score;


    private void Awake()
    {
        if (LevelController.instancia == null)
        {
            LevelController.instancia = this;
            DontDestroyOnLoad(gameObject);
            pickups_list = GameObject.FindGameObjectsWithTag("Pickup");
            max_count_pickups = pickups_list.Length;
            current_score =  int.Parse(score_text.text);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        score_text.text = current_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScoreTxt()
    {
        score_text.text = current_score.ToString();
    }

    public void IncreaseScoreTxt()
    {
        current_score++;
        UpdateScoreTxt();
    }
}
