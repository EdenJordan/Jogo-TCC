using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }
    public void Creditos()
    {
        SceneManager.LoadScene(3);
    }
    public void Sair()
    {
        Application.Quit();
        Debug.Log("Sair do jogo");
    }
}
