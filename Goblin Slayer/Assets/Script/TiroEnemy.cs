using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroEnemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private GameObject _Player;
    private Vector2 _Direcao;
    
    public float speed;
    private float timerdestroy;
    public int danoParaDar;
    
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        rig = GetComponent<Rigidbody2D>();
        
        _Direcao = (_Player.transform.position - transform.position).normalized;
        
        timerdestroy = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timerdestroy -= Time.deltaTime;
        
        if (timerdestroy <= 0)
        {
            Destroy(gameObject);
            timerdestroy = 2.5f;
        }
        
        rig.velocity = _Direcao * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ZonaDeDano"))
        {
            VidaPlayer.instance.DanoPlayer(danoParaDar);
        }
    }
}
