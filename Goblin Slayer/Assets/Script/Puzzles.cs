using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles : MonoBehaviour
{
    public GameObject ponte;
    private bool colisao;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1;
        ponte.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (colisao == false)
        {
            timer -= Time.deltaTime;
            
            if (timer <= 0)
            {
                ponte.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 0);
                ponte.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            timer = 1;
            colisao = true;
            ponte.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 1);
            ponte.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Caixa")
        {
            colisao = false;
        }
    }
}
