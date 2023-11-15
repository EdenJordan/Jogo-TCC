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
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (painelPause.activeSelf)
            {
                painelPause.SetActive(false);
                Time.timeScale = 1;
                DesselecionarButao();
            }
            else
            {
                painelPause.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (painelEscolherPergaminho.activeSelf || painelGameOver.activeSelf || painelPergaminhoFogo.activeSelf || painelPergaminhoGelo.activeSelf)
        {
            Time.timeScale = 0;
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
        GameManager.instance.pergaminhoFogo = false;
        GameManager.instance.pergaminhoRaio = true;
        painelEscolherPergaminho.SetActive(false);
    }
    
    public void SubstituirPergaminhoGelo()
    {
        GameManager.instance.pergaminhoGelo = false;
        GameManager.instance.pergaminhoRaio = true;
        painelEscolherPergaminho.SetActive(false);
    }

    public void FecharSelecaoDePergaminhos()
    {
        painelEscolherPergaminho.SetActive(false);
    }
    
    public void FecharPainelPergaminhoDeFogo()
    {
        painelPergaminhoFogo.SetActive(false);
    }
    public void FecharPainelPergaminhoDeGelo()
    {
        painelPergaminhoGelo.SetActive(false);
    }
}
