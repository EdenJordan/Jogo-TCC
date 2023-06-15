using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _Speed;
    public Transform target;
    public float distancia;
    public float _speedEnemy;
    void Start()
    {
    }

    void Update()
    {
        Move();
        var distance = Vector2.Distance(transform.position, target.position);
        var heading = (transform.position - target.position);
        var direction = heading / distance;
        
        if (distance >= distancia*3)
        {
            target.Translate(direction * (_speedEnemy * Time.deltaTime) );
        }
        else if (distance <= distancia)
        {
            target.Translate(-direction * (_speedEnemy * 3 * Time.deltaTime) );
        }

    }

    void Move()
    {
        float _H = Input.GetAxis("Horizontal");
        float _V = Input.GetAxis("Vertical");

        Vector2 _Moviment = new Vector2(_H, _V).normalized * _Speed;
        transform.Translate(_Moviment*Time.deltaTime);
    }
}
