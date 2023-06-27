using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _Speed;
    void Start()
    {
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
    }
}
