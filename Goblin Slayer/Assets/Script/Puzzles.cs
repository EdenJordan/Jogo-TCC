using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzles : MonoBehaviour
{
    public GameObject ponte;
    private Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ponte.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Botao")
        {
            ponte.SetActive(true);
            ponte.GetComponent<SpriteRenderer>().color = new Color(0.4056604f, 0.4056604f, 0.4056604f, 1);
            ponte.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
