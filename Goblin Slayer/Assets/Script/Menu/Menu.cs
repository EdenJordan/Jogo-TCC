using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{ 
    public GameObject _DontDestroy;

    public static Menu instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void MenuPrincipal()
    {
        if (_DontDestroy != null)
        {
            Destroy(_DontDestroy.gameObject);
        }
        SceneManager.LoadScene("Menu");
    }
    public void Jogar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CutsCenesIniciais");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Sair()
    {
        Application.Quit();
        Debug.Log("Sair do jogo");
        DesselecionarButao();
    }
    
    public void ReiniciarCena()
    {
        Destroy(_DontDestroy.gameObject);
        SceneManager.LoadScene("Fase1");
    }
    public void DesselecionarButao()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void DontDestroy()
    {
        if (_DontDestroy != null)
        {
            Destroy(_DontDestroy.gameObject);
        }
    }
}
