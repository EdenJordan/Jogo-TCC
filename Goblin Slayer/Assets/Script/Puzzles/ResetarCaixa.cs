using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetarCaixa : MonoBehaviour
{
    private GameObject caixa;
    private GameObject localCaixa;
    
    // Start is called before the first frame update
    void Start()
    {
        caixa = GameObject.FindWithTag("Caixa");
        localCaixa = GameObject.FindWithTag("LocalCaixa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            caixa.transform.position = localCaixa.transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
