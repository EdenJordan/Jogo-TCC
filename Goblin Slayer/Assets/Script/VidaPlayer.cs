using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    private Pause _pause;
    
    private float vidaMaxima;
    private float vidaAtual;
    
    // Start is called before the first frame update
    void Start()
    {
        _pause = GameObject.Find("MenuManager").GetComponent<Pause>();
        vidaMaxima = 4;
        vidaAtual = vidaMaxima;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TiroEnemy")
        {
            vidaAtual -= 1;
            Destroy(col.gameObject);

            if (vidaAtual <= 0)
            {
                vidaAtual = 0;
                _pause.GameOver();
            }
        }
    }
}
