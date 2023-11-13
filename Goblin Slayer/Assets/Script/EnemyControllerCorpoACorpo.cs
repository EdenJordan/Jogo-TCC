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

    private Transform player;

    public GameObject ataque;
    public bool estaAtacando;
    private float timeFire = 0;

    private float voltarAandar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        ataque.SetActive(false);

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
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!estaAtacando)
            {
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
