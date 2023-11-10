using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    private Rigidbody2D rig;
    private PlayerController player;
    private GameManager _gamaManager;
    
    private float speed;
    private float tempoParaDestroirOTiro;
    private float tempoParaAtirar;

    public bool tiroFogo;
    public bool tiroGelo;
    public bool tiroRaio;

    private int danoParaDar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _gamaManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rig = GetComponent<Rigidbody2D>();
        tempoParaDestroirOTiro = 3;
        tempoParaAtirar = 3;
        danoParaDar = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimenta o tiro
        if (tiroFogo && !tiroGelo && !tiroRaio)
        {
            speed = 8;
        }
        if (!tiroFogo && tiroGelo && !tiroRaio)
        {
            speed = 6;
        }
        if (!tiroFogo && !tiroGelo && tiroRaio)
        {
            speed = 10;
        }

        //Direita
        if (player.animTiros == 1 && tempoParaAtirar == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            rig.velocity = Vector2.right * speed;
        }
        //Esquerda
        else if (player.animTiros == 2 && tempoParaAtirar == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            rig.velocity = Vector2.left * speed;
        }
        //Cima
        else if (player.animTiros == 3 && tempoParaAtirar == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rig.velocity = Vector2.up * speed;
        }
        //Baixo
        else if (player.animTiros == 4 && tempoParaAtirar == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            rig.velocity = Vector2.down * speed;
        }
        
        //Destroi o tiro
        tempoParaDestroirOTiro -= Time.deltaTime;
        tempoParaAtirar -= Time.deltaTime;
        
        if (tempoParaDestroirOTiro <= 0)
        {
            Destroy(gameObject);
        }
        
        //Tempo Para Atirar
        if (tempoParaAtirar <= 0)
        {
            tempoParaAtirar = 3;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (tiroFogo)
            {
                danoParaDar = 2;
                VidaEnemy.instance.vida(danoParaDar);
                Destroy(gameObject);
            }
            if (tiroGelo)
            {
                danoParaDar = 1;
                VidaEnemy.instance.vida(danoParaDar);
                EnemyControllerArqueiro.instance._speedEnemy = 0;
                Destroy(gameObject);
            }
            if (tiroRaio)
            {
                danoParaDar = 3;
                VidaEnemy.instance.vida(danoParaDar);
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("Enemy2"))
        {
            if (tiroFogo)
            {
                danoParaDar = 2;
                VidaEnemy.instance.vida(danoParaDar);
                Destroy(gameObject);
            }
            if (tiroGelo)
            {
                danoParaDar = 1;
                VidaEnemy.instance.vida(danoParaDar);
                EnemyControllerCorpoACorpo.instance.moveSpeed = 0;
                Destroy(gameObject);
            }
            if (tiroRaio)
            {
                danoParaDar = 3;
                VidaEnemy.instance.vida(danoParaDar);
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("Boss"))
        {
            if (tiroFogo)
            {
                danoParaDar = 2;
                Boss.instance.TakeDamage(danoParaDar);
                Destroy(gameObject);
            }
            if (tiroGelo)
            {
                danoParaDar = 1;
                Boss.instance.TakeDamage(danoParaDar);
                Destroy(gameObject);
            }
            if (tiroRaio)
            {
                danoParaDar = 3;
                Boss.instance.TakeDamage(danoParaDar);
                Destroy(gameObject);
            }
        }
    }
}
