using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private GameObject score_txt;
    private int score_count;
    private float horizontalInput;
    private float verticalInput;

    private PhotonView _view;

    public void Start()
    {
        score_count = 0;
        _view = GetComponent<PhotonView>();
        score_txt = GameObject.FindGameObjectWithTag("Score_txt");

    }

    // Update is called once per frame
    void Update()
    {

        if (_view.IsMine)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);   
            
        }
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            IncrementScore();
            Destroy(other.gameObject);
        }
    }

    private void IncrementScore()
    {
        score_count++;
        score_txt.GetComponent<Text>().text = "" + score_count;
    }
}
