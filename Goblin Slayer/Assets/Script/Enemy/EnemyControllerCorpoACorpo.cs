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
    
    public float moveSpeed = 2.0f;
    public float attackRange = 1.5f;
    public float perseguicaoRange;

    private Transform player;

    public GameObject ataque;
    public bool estaAtacando;
    private float timeFire = 0;

    //congelamento
    private float voltarAandar;
    public bool estaCongelado;
    public GameObject gelo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        ataque.SetActive(false);

        voltarAandar = 0;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        //voltar a andar
        if (estaCongelado)
        {
            gelo.SetActive(true);
            voltarAandar += Time.deltaTime;
            
            if (voltarAandar >= 2)
            {
                moveSpeed = 2;
                voltarAandar = 0;
                estaCongelado = false;
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
                }
            }

            //ataque e perseguição
            if (distanceToPlayer <= attackRange)
            {
                if (!estaAtacando)
                {
                    Audio.instance.espada.Play();
                    estaAtacando = true;
                    ataque.SetActive(true);
                    timeFire = 0;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
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
