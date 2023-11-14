using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    private GameManager _gameManager;
    private GameObject player;
    private Animator anim;

    private float _H;
    
    private float _V;
    
    public float _Speed;
    public int animTiros;

    private float voltarAandar;
    public bool estaCongelado = false;

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
        if (estaCongelado)
        {
            voltarAandar += Time.deltaTime;
            
            if (voltarAandar >= 2)
            {
                _Speed = 3;
                estaCongelado = false;
                voltarAandar = 0;
                
            }
        }
        //============
        Move();

        if (_gameManager.onFire)
        {
            _Speed = 0;
        }
        else
        {
            _Speed = 3;
        }
        
        if (_H > 0) //Direita
        {
            transform.eulerAngles = new Vector2(0, 0);
            if (_gameManager.tiroAtual == 3)
            {
                if (_gameManager.onFire)
                {
                    anim.SetInteger("Transicao", 1);
                }
                else
                {
                    anim.SetInteger("Transicao", 1);
                }
            }
        }
        if (_H < 0) //Esquerda
        {
            transform.eulerAngles = new Vector2(0, 180);
            if (_H == 0 && _V == 0)
            {
                if (_gameManager.tiroAtual == 3)
                {
                    anim.SetInteger("Transicao", 1);
                }
            }
        }
        
        if (_V > 0) //Cima
        {
            transform.eulerAngles = new Vector2(0, 0);
            if (_H == 0 && _V == 0)
            {
                if (_gameManager.tiroAtual == 3)
                {
                    anim.SetInteger("Transicao", 2);
                }
            }
        }
        if (_V < 0) //Baixo
        {
            transform.eulerAngles = new Vector2(0, 180);
            if (_H == 0 && _V == 0)
            {
                if (_gameManager.tiroAtual == 3)
                {
                    anim.SetInteger("Transicao", 3);
                }
            }
        }
        

    }

    void Move()
    {
        _H = Input.GetAxisRaw("Horizontal");
        _V = Input.GetAxisRaw("Vertical");

        Vector2 _Moviment = new Vector2(_H, _V).normalized * _Speed;
        //transform.Translate(_Moviment*Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = _Moviment;

          
        
        if (VidaPlayer.instance.escudo == false)
        {
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

        if (_gameManager.onFireFisico || _gameManager.onFire)
        {
            switch (_gameManager.tiroAtual)
            {
                case 1:
                    anim.SetFloat("Blend",2); // 2 é atacando.
                    break;
                /*case 3:
                    anim.SetFloat("Blend", 3);
                    break;
                case 4:
                    anim.SetFloat("Blend", 3);
                    break;
                case 5:
                    anim.SetFloat("Blend", 3);
                    break;*/
                default:
                    break;
            }
            
        }
        else
        {
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                anim.SetFloat("Blend",0); //0 é idle
            }
            else
            {
                anim.SetFloat("Blend",1); // 1é andando.
            }
        }
        
    }

    public void Idle()
    {
        
    }
}
