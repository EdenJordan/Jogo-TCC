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
    public int animDirecao;

    private float voltarAandar;
    public bool estaCongelado = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animDirecao = 4;
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        //funções
        Move();
        Direcao();
        Animacoes();

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
        
        if (_gameManager.onFire || _gameManager.onFireFisico)
        {
            _Speed = 0;
        }
        else
        {
            _Speed = 3;
        }
    }

    void Move()
    {
        _H = Input.GetAxisRaw("Horizontal");
        _V = Input.GetAxisRaw("Vertical");

        Vector2 _Moviment = new Vector2(_H, _V).normalized * _Speed;
        GetComponent<Rigidbody2D>().velocity = _Moviment;
    }

    public void Animacoes()
    {
        //Ataque fogo ==========================================
        if (animDirecao == 1 || animDirecao == 2) //Direita
        {
            if (_gameManager.tiroAtual == 3)
            {
                if (_gameManager.onFire)
                {
                    anim.SetInteger("Transicao", 1);
                }
                else
                {
                    anim.SetInteger("Transicao", 0);
                }
            }
        }
        
        if (animDirecao == 3) //cima
        {
            if (_gameManager.tiroAtual == 3)
            {
                if (_gameManager.onFire)
                {
                    anim.SetInteger("Transicao", 2);
                }
                else
                {
                    anim.SetInteger("Transicao", 0);
                }
            }
        }
        
        if (animDirecao == 4) //Baixo
        {
            if (_gameManager.tiroAtual == 3)
            {
                if (_gameManager.onFire)
                {
                    anim.SetInteger("Transicao", 3);
                }
                else
                {
                    anim.SetInteger("Transicao", 0);
                }
            }
        }
        
        //Ataque Espada ==========================================
        if (VidaPlayer.instance.escudo == false)
        {
            if (_H > 0 && !_gameManager.onFireFisico)
            {
                anim.SetBool("Direita", true);
                anim.SetBool("Cima", false);
            }
            if (_H < 0 && !_gameManager.onFireFisico)
            {
                anim.SetBool("Direita", true);
                anim.SetBool("Cima", false);
            }
            if (_V > 0 && !_gameManager.onFireFisico)
            {
                anim.SetBool("Cima", true);
                anim.SetBool("Direita", false);
            }
            if (_V < 0 && !_gameManager.onFireFisico)
            {
                anim.SetBool("Cima", false);
                anim.SetBool("Direita", false);
            }
        }
        //=========================================================
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

    public void Direcao()
    {
        if (_H > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            animDirecao = 1;//Direita
        }
        if (_H < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            animDirecao = 2;//Esquerda
        }
        if (_V > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            animDirecao = 3;//Cima
        }
        if (_V < 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            animDirecao = 4;//Baixo
        }
        //==========================================================================
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animDirecao = 1;//Direita
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animDirecao = 2;//Esquerda
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animDirecao = 3;//Cima
        }
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            animDirecao = 4;
        }
    }
}
