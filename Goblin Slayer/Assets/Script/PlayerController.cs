using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private GameManager _gameManager;
    private GameObject player;
    private Animator anim;
    
    public float _Speed;
    public int animTiros;

    private float voltarAandar;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animTiros = 4;
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        //voltar a andar
        if (_Speed == 0)
        {
            voltarAandar += Time.deltaTime;
            
            if (voltarAandar >= 2)
            {
                _Speed = 3;
                voltarAandar = 0;
            }
        }
        //============
        Move();
    }

    void Move()
    {
        float _H = Input.GetAxisRaw("Horizontal");
        float _V = Input.GetAxisRaw("Vertical");

        Vector2 _Moviment = new Vector2(_H, _V).normalized * _Speed;
        //transform.Translate(_Moviment*Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = _Moviment;

        if (_H > 0 && !_gameManager.onFireFisico)
        {
            animTiros = 1;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Direita", true);
            anim.SetBool("Cima", false);
        }
        if (_H < 0 && !_gameManager.onFireFisico)
        {
            animTiros = 2;
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetBool("Direita", true);
            anim.SetBool("Cima", false);
        }
        if (_V > 0 && !_gameManager.onFireFisico)
        {
            animTiros = 3;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Cima", true);
            anim.SetBool("Direita", false);
        }
        if (_V < 0 && !_gameManager.onFireFisico)
        {
            animTiros = 4;
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Cima", false);
            anim.SetBool("Direita", false);
        }
    }
}
