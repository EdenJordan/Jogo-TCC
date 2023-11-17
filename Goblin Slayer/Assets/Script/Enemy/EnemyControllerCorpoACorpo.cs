using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControllerCorpoACorpo : MonoBehaviour
{
    public static EnemyControllerCorpoACorpo instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    private SpriteRenderer spriteRenderer;

    public float moveSpeed = 2.0f;
    public float attackRange = 1.5f;
    public float perseguicaoRange;

    private Transform player;
    public GameObject SpriteEnemy;
    
    public GameObject ataque;
    public bool estaAtacando;
    private float timeFire = 0;
    private Animator animator;


    //congelamento
    private float voltarAandar;
    public bool estaCongelado;
    public GameObject gelo;

    void Start()
    {
        spriteRenderer = SpriteEnemy.GetComponent<SpriteRenderer>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        animator = SpriteEnemy.GetComponent<Animator>();

        ataque.SetActive(false);

        voltarAandar = 0;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (player.position.y > transform.position.y && !estaCongelado && !estaAtacando && perseguicaoRange >= distanceToPlayer)
        {
            animator.SetInteger("Transicao", 2);
        }
      
        if (player.position.y < transform.position.y && !estaCongelado && !estaAtacando && perseguicaoRange >= distanceToPlayer)
        {
            animator.SetInteger("Transicao", 5);
        }


        if (player.position.y > transform.position.y && !estaCongelado && !estaAtacando && perseguicaoRange <= distanceToPlayer)
        {
            animator.SetInteger("Transicao", 1);
        }
      
        if (player.position.y < transform.position.y && !estaCongelado && !estaAtacando && perseguicaoRange <= distanceToPlayer)
        {
            animator.SetInteger("Transicao", 4);
        }
        
        spriteRenderer.flipX = player.position.x > transform.position.x;


        //voltar a andar
        if (estaCongelado)
        {
            if (player.position.y > transform.position.y)
            {
                animator.SetInteger("Transicao", 1);
                gelo.SetActive(true);
                voltarAandar += Time.deltaTime;
          
                if (voltarAandar >= 2)
                {
                    moveSpeed = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                    animator.SetInteger("Transicao", 0);
                }
            }
            else if (player.position.y < transform.position.y)
            {
                animator.SetInteger("Transicao", 4);
                gelo.SetActive(true);
                voltarAandar += Time.deltaTime;
          
                if (voltarAandar >= 2)
                {
                    moveSpeed = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                    animator.SetInteger("Transicao", 0);
                }
            }

        }
        else
        {
            gelo.SetActive(false);
        }
        
        if (distanceToPlayer <= perseguicaoRange)
        {
            //Tempo entre os ataques
            if (estaAtacando)
            {
                timeFire += Time.deltaTime;
            
                if (timeFire >= 1)
                {
                    ataque.SetActive(false);
                }
                if (timeFire >= 2)
                {
                    estaAtacando = false;
                    animator.SetInteger("Transicao", 0);
                }
            }

            //ataque e perseguição
            if (distanceToPlayer <= attackRange)
            {
                if (!estaAtacando)
                {
                    if (player.position.y > transform.position.y)
                    {
                        animator.SetInteger("Transicao", 3);
                        Audio.instance.espada.Play();
                        estaAtacando = true;
                        ataque.SetActive(true);
                        timeFire = 0;
                    }
                    else if (player.position.y < transform.position.y)
                    {
                        animator.SetInteger("Transicao", 6);
                        Audio.instance.espada.Play();
                        estaAtacando = true;
                        ataque.SetActive(true);
                        timeFire = 0;
                    }

                    
                }
            }
            else if (!estaCongelado)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                if (player.position.y > transform.position.y)
                {
                    animator.SetInteger("Transicao", 2);
                  
                }
                else if (player.position.y > transform.position.y)
                {
                    animator.SetInteger("Transicao", 5);
                  
                }

            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TiroPlayer"))
        {
            if (TiroPlayer.instance.tiroGelo)
            {
                estaCongelado = true;
                moveSpeed = 0;
            }
        }
    }
}
