using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControllerCorpoACorpo : MonoBehaviour
{
    public static EnemyControllerCorpoACorpo instance;
    
    private Transform targetPlayer;
    private void Awake()
    {
        instance = this;
    }
    
    public float moveSpeed = 2.0f;
    public float attackRange = 1.5f;

    private Transform player;
    public int danoParaDar;
    public bool dano = false;
    
    public GameObject ataque;
    public bool podeAtacar;
    private float timeFire = 0;

    private float voltarAandar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        ataque.SetActive(false);
        podeAtacar = false;
        dano = true;

        voltarAandar = 0;
    }

    void Update()
    {
        //voltar a andar
        if (moveSpeed == 0)
        {
            voltarAandar += Time.deltaTime;
            
            if (voltarAandar >= 2)
            {
                moveSpeed = 2;
                voltarAandar = 0;
            }
        }
        //===============================
        timeFire += Time.deltaTime;
        
        if (podeAtacar)
        {
            if (timeFire >= 1)
            {
                ataque.SetActive(false);
            }
            if (timeFire >= 2)
            {
                podeAtacar = false;
            }
        }
        //===============================
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!podeAtacar)
            {
                if (ataque != null)
                {
                    podeAtacar = true;
                    ataque.SetActive(true);
                    timeFire = 0;
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ZonaDeDano"))
        {
            if (ataque != null)
            {
                float timeDano = 0;
                timeDano += Time.deltaTime;
                if (timeDano >= 2)
                {
                    VidaPlayer.instance.DanoPlayer(danoParaDar);
                    timeDano = 0;
                }
            }
        }
    }
}
