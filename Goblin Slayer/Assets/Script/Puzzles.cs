using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles : MonoBehaviour
{
    public GameObject ponte;
    private float timer;
    public int quantidadeDeObjetos;
    private Animator animponte;

    // Start is called before the first frame update
    void Start()
    {
        animponte = ponte.GetComponent<Animator>();
        animponte.SetBool("PonteEmergindo", false);
        quantidadeDeObjetos = 0;
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (quantidadeDeObjetos <= 0)
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                ponte.GetComponent<BoxCollider2D>().enabled = true;
                animponte.SetBool("PonteEmergindo", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            timer = 1;
            quantidadeDeObjetos++;
            animponte.SetBool("PonteEmergindo", true);
            ponte.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        
    }*/
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            quantidadeDeObjetos--;
        }
    }
}
