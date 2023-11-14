using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudancaDeFases : MonoBehaviour
{
    private GameObject player;
    private GameObject localDeNascenca;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        localDeNascenca = GameObject.FindWithTag("LocalDeNascenca");
        player.transform.position = localDeNascenca.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.fases == 1)
            {
                SceneManager.LoadScene("Fase2");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 2)
            {
                //0.04  
                SceneManager.LoadScene("Puzzle2");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 3)
            {
                SceneManager.LoadScene("Fase3");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 4)
            {
                SceneManager.LoadScene("Puzzle3");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 5)
            {
                SceneManager.LoadScene("Fase4");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 6)
            {
                SceneManager.LoadScene("Puzzle4");
                GameManager.instance.fases++;
            }
            else if (GameManager.instance.fases == 7)
            {
                SceneManager.LoadScene("FaseBoss");
                GameManager.instance.fases++;
            }
        }
    }
}
