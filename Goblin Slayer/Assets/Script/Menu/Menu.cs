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
            Audio.instance.floresta.Stop();
            Audio.instance.caverna.Stop();
            Destroy(_DontDestroy.gameObject);
        }
        Audio.instance.floresta.Stop();
        Audio.instance.caverna.Stop();
        Audio.instance.menu.Play();
        Audio.instance.selecaoMenu.Play();
        SceneManager.LoadScene("Menu");
    }
    public void Jogar()
    {
        Audio.instance.selecaoMenu.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("CutsCenesIniciais");
    }
    public void Creditos()
    {
        Audio.instance.selecaoMenu.Play();
        SceneManager.LoadScene("Creditos");
    }
    public void Sair()
    {
        Audio.instance.selecaoMenu.Play();
        Application.Quit();
        Debug.Log("Sair do jogo");
        DesselecionarButao();
    }
    
    public void ReiniciarCena()
    {
        Audio.instance.selecaoMenu.Play();
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
