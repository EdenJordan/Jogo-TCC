using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject painelPause;
    public GameObject painelGameOver;
    public GameObject painelEscolherPergaminho;

    private bool isGameOver;

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

        if (painelEscolherPergaminho.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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
        painelEscolherPergaminho.SetActive(false);
    }
    
    public void SubstituirPergaminhoGelo()
    {
        GameManager.instance.pergaminhoGelo = false;
        painelEscolherPergaminho.SetActive(false);
    }

    public void FecharSelecaoDePergaminhos()
    {
        painelEscolherPergaminho.SetActive(false);
    }
}
