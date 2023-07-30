using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    public float _Speed;
    private Animator anim;
    public int animTiros;
    
    void Start()
    {
        animTiros = 4;
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float _H = Input.GetAxisRaw("Horizontal");
        float _V = Input.GetAxisRaw("Vertical");

        Vector2 _Moviment = new Vector2(_H, _V).normalized * _Speed;
        //transform.Translate(_Moviment*Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = _Moviment;

        if (_H > 0)
        {
            animTiros = 1;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Direita", true);
            anim.SetBool("Cima", false);
        }
        if (_H < 0)
        {
            animTiros = 2;
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetBool("Direita", true);
            anim.SetBool("Cima", false);
        }
        if (_V > 0)
        {
            animTiros = 3;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Cima", true);
            anim.SetBool("Direita", false);
        }
        if (_V < 0)
        {
            animTiros = 4;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Cima", false);
            anim.SetBool("Direita", false);
        }
    }
}
