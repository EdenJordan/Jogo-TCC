using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public static VidaPlayer instance;
    
    private Pause _pause;
    
    public static int vidaMaxima;
    public static int vidaAtual;

    public bool escudo;
    public GameObject escudoObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _pause = GameObject.Find("MenuManager").GetComponent<Pause>();
        vidaMaxima = 10;
        vidaAtual = vidaMaxima;
        escudo = false;
        escudoObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (escudo)
        {
            escudoObject.SetActive(true);
            PlayerController.instance._Speed = 0;
            //inserir animação
        }
        else
        {
            escudoObject.SetActive(false);
        }
        if (!escudo && PlayerController.instance.estaCongelado == false)
        {
            PlayerController.instance._Speed = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TiroEnemy")
        {
            Destroy(col.gameObject);
        }
    }

    public void DanoPlayer(int danoParaReceber)
    {
        if (!escudo)
        {
            vidaAtual -= danoParaReceber;

            if (vidaAtual <= 0)
            {
                vidaAtual = 0;
                _pause.GameOver();
            }
        }
    }
}
