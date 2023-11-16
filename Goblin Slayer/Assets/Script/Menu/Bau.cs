using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    public bool bauFechado;
    
    // Start is called before the first frame update
    void Start()
    {
        bauFechado = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.fases == 2 && bauFechado)
            {
                Pause.instance.painelPergaminhoFogo.SetActive(true);
                bauFechado = false;
            }
            
            if (GameManager.instance.fases == 4 && bauFechado)
            {
                Pause.instance.painelPergaminhoGelo.SetActive(true);
                bauFechado = false;
            }
            
            if (GameManager.instance.fases == 6 && bauFechado)
            {
                Pause.instance.painelEscolherPergaminho.SetActive(true);
                bauFechado = false;
            }
        }
    }
}
