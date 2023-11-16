using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle1e2 : MonoBehaviour
{
    public GameObject passagem;
    private float timer;
    public int quantidadeDeObjetos;
    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = passagem.GetComponent<Animator>();
        quantidadeDeObjetos = 0;
        timer = 1;
        if (GameManager.instance.fases == 1)
        {
            Animator.SetBool("PonteEmergindo", false);
            passagem.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 0);
        }

        if (GameManager.instance.fases == 3)
        {
            Animator.SetBool("PilarDesativando", false);
            passagem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (quantidadeDeObjetos <= 0)
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                if (GameManager.instance.fases == 1)
                {
                    Animator.SetBool("PonteEmergindo", false);
                    passagem.GetComponent<BoxCollider2D>().enabled = true;
                }

                if (GameManager.instance.fases == 3)
                {
                    Animator.SetBool("PilarDesativando", false);
                    passagem.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            Audio.instance.alavanca.Play();
            timer = 1;
            quantidadeDeObjetos++;
            if (GameManager.instance.fases == 1)
            {
                Animator.SetBool("PonteEmergindo", true);
                passagem.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (GameManager.instance.fases == 3)
            {
                Animator.SetBool("PilarDesativando", true);
                passagem.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            Audio.instance.alavanca.Play();
            quantidadeDeObjetos--;
        }
    }
}
