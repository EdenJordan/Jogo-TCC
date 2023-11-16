using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemy : MonoBehaviour
{
    public static VidaEnemy instance;
    private float vidaAtual;
    public float vidaMaxima;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
    }
    
    public void vida(int DanoParaReceber)
    {
        Audio.instance.dano.Play();
        vidaAtual -= DanoParaReceber;

        if (vidaAtual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AtaquePlayer"))
        {
            vida(2);
        }
        if (col.gameObject.CompareTag("TiroPlayer"))
        {
            if (TiroPlayer.instance.tiroFogo)
            {
                vida(2);
            }
            if (TiroPlayer.instance.tiroGelo)
            {
                vida(1);
            }
            if (TiroPlayer.instance.tiroRaio)
            {
                vida(3);
            }
        }
    }
}
