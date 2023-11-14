using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{ 
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Jogar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Fase1");
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
    public void DesselecionarButao()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
