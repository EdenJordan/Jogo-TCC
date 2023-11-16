using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3olhos : MonoBehaviour
{
    public GameObject olho1;
    public GameObject olho2;
    public GameObject olho3;
    
    public bool animOlho1;
    public bool animOlho2;
    public bool animOlho3;

    public GameObject pilar;
    
    public static Puzzle3olhos instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animOlho1 && animOlho2 && animOlho3)
        {
            pilar.GetComponent<BoxCollider2D>().enabled = false;
            pilar.GetComponent<Animator>().SetBool("PilarDesativando", true);
        }
        else
        {
            pilar.GetComponent<BoxCollider2D>().enabled = true;
            pilar.GetComponent<Animator>().SetBool("PilarDesativando", false);
        }
        //=============================================================================
        if (animOlho1)
        {
            olho1.GetComponent<Animator>().SetBool("OlhoAberto",true);
        }
        else
        {
            olho1.GetComponent<Animator>().SetBool("OlhoAberto",false);
        }
        
        //==============================================================================
        if (animOlho2)
        {
            olho2.GetComponent<Animator>().SetBool("OlhoAberto",true);
        }
        else
        {
            olho2.GetComponent<Animator>().SetBool("OlhoAberto",false);
        }
        
        //===============================================================================
        if (animOlho3)
        {
            olho3.GetComponent<Animator>().SetBool("OlhoAberto",true);
        }
        else
        {
            olho3.GetComponent<Animator>().SetBool("OlhoAberto",false);
        }
    }
}
