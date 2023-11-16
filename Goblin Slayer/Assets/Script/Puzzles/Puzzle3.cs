using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{
    public bool alavanca1;
    public bool alavanca2;
    public bool alavanca3;
    public Animator Animator;
    public bool colisao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colisao)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            colisao = !colisao;
            
            if (alavanca1)
            {
                Audio.instance.alavanca.Play();
                Puzzle3olhos.instance.animOlho1 = !Puzzle3olhos.instance.animOlho1;
                Puzzle3olhos.instance.animOlho2 = !Puzzle3olhos.instance.animOlho2;
            }
            if (alavanca2)
            {
                Audio.instance.alavanca.Play();
                Puzzle3olhos.instance.animOlho1 = !Puzzle3olhos.instance.animOlho1;
                Puzzle3olhos.instance.animOlho2 = !Puzzle3olhos.instance.animOlho2;
                Puzzle3olhos.instance.animOlho3 = !Puzzle3olhos.instance.animOlho3;
            }
            if (alavanca3)
            {
                Audio.instance.alavanca.Play();
                Puzzle3olhos.instance.animOlho2 = !Puzzle3olhos.instance.animOlho2;
                Puzzle3olhos.instance.animOlho3 = !Puzzle3olhos.instance.animOlho3;
            }
        }
    }
}
