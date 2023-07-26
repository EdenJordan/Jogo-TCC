using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject painelPause;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
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
}
