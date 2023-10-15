using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControllerCorpoACorpo : MonoBehaviour
{
    private Transform targetPlayer;

    private float _speedEnemy;
    private float distance;
    public Vector3 direction;
    
    public float _RangeMax = 15;
    private float timer;
    private float danoParaDar = 0;
    public bool estaAtacando;

    // Start is called before the first frame update
    void Start()
    {
        _speedEnemy = 2;
        targetPlayer = FindObjectOfType<PlayerController>().transform;
        //Tiro
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CalcularDistancia();

        if (distance > _RangeMax)
        {
            _speedEnemy = 0;
        }
        else
        {
            Aproximando();
        }
    }
    void Atacando()
    {
        _speedEnemy = 0;
        timer -= Time.deltaTime;
            
        if (timer <= 0)
        {
            estaAtacando = true;
            VidaPlayer.instance.DanoPlayer(danoParaDar = 2);
            timer = 2;
        }
        else
        {
            estaAtacando = false;
        }
    }
    void Aproximando()
    {
        if (!estaAtacando)
        {
            _speedEnemy = 2;
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, _speedEnemy * Time.deltaTime);
        }
    }
    void CalcularDistancia()
    {
        distance = Vector2.Distance(transform.position, targetPlayer.position);
        var heading = (transform.position - targetPlayer.position);
        direction = heading / distance;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Atacando();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _speedEnemy = 2;
        }
    }
}
