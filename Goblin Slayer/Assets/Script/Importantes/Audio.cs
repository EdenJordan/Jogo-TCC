using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource menu;
    public AudioSource floresta;
    public AudioSource caverna;
    public AudioSource alavanca;
    public AudioSource caixa;
    public AudioSource dano;
    public AudioSource espada;
    public AudioSource arco;
    public AudioSource fogo;
    public AudioSource gelo;
    public AudioSource raio;
    public AudioSource selecaoMenu;
    public AudioSource coletapergaminhos;
    public AudioSource escudo;
    public AudioSource agua;

    public static Audio instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
