using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    private int vidaAtual;
    private int vidaMaxima;
    public bool enemyArqueiro;

    // Start is called before the first frame update
    void Start()
    {
        vidaMaxima = 2;
        vidaAtual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TiroPlayer" && enemyArqueiro)
        {
            vidaAtual -= 1;
            Destroy(col.gameObject);
            
            if (vidaAtual <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
