using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    private GameObject player;
    private GameObject localDeNascenca;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        localDeNascenca = GameObject.FindWithTag("LocalDeNascenca");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.transform.position = localDeNascenca.transform.position;
        }
    }
}
