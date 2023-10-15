using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    private float vidaAtual;
    private float vidaMaxima;

    // Start is called before the first frame update
    void Start()
    {
        vidaMaxima = 3;
        vidaAtual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TiroPlayer") 
        {
            vidaAtual -= 1;
            Destroy(col.gameObject);
            
            if (vidaAtual <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (col.gameObject.tag == "AtaquePlayer")
        {
            vidaAtual -= 1;

            if (vidaAtual <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
