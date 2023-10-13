using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public static VidaPlayer instance;
    
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

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TiroEnemy")
        {
            Destroy(col.gameObject);
        }
    }

    public void DanoPlayer(float danoParaReceber)
    {
        vidaAtual -= danoParaReceber;

        if (vidaAtual <= 0)
        {
            vidaAtual = 0;
            _pause.GameOver();
        }
    }
}
