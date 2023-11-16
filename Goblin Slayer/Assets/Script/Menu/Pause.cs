using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject painelPause;
    public GameObject painelGameOver;
    public GameObject painelEscolherPergaminho;
    public GameObject painelPergaminhoFogo;
    public GameObject painelPergaminhoGelo;

    private bool isGameOver;

    public static Pause instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (painelEscolherPergaminho.activeSelf || painelGameOver.activeSelf || painelPergaminhoFogo.activeSelf || painelPergaminhoGelo.activeSelf)
        {
            Audio.instance.selecaoMenu.Play();
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (painelPause.activeSelf)
            {
                VoltarPause();
            }
            else
            {
                Audio.instance.selecaoMenu.Play();
                Time.timeScale = 0;
                painelPause.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
        }

        if (painelPergaminhoFogo.activeSelf)
        {
            GameManager.instance.pergaminhoFogo = true;
        }

        if (painelPergaminhoGelo.activeSelf)
        {
            GameManager.instance.pergaminhoGelo = true;
        }
        
    }
    public void VoltarPause()
    {
        Audio.instance.selecaoMenu.Play();
        painelPause.SetActive(false);
        Time.timeScale = 1;
        DesselecionarButao();
    }
    public void DesselecionarButao()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        painelGameOver.SetActive(true);
    }

    public void SubstituirPergaminhoFogo()
    {
        Audio.instance.selecaoMenu.Play();
        GameManager.instance.pergaminhoFogo = false;
        GameManager.instance.pergaminhoRaio = true;
        painelEscolherPergaminho.SetActive(false);
    }
    
    public void SubstituirPergaminhoGelo()
    {
        Audio.instance.selecaoMenu.Play();
        GameManager.instance.pergaminhoGelo = false;
        GameManager.instance.pergaminhoRaio = true;
        painelEscolherPergaminho.SetActive(false);
    }

    public void FecharSelecaoDePergaminhos()
    {
        Audio.instance.selecaoMenu.Play();
        painelEscolherPergaminho.SetActive(false);
    }
    
    public void FecharPainelPergaminhoDeFogo()
    {
        Audio.instance.selecaoMenu.Play();
        painelPergaminhoFogo.SetActive(false);
    }
    public void FecharPainelPergaminhoDeGelo()
    {
        Audio.instance.selecaoMenu.Play();
        painelPergaminhoGelo.SetActive(false);
    }
}
