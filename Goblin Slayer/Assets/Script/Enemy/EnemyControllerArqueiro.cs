using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum EnemyState
{
    parado,
    aproximando,
    afastando,
    atacando
}
public class EnemyControllerArqueiro : MonoBehaviour
{
    public static EnemyControllerArqueiro instance;
    
    private Transform targetPlayer;
    
    private SpriteRenderer spriteRenderer;

    public float _speedEnemy;
    private float distance;
    public Vector3 direction;

    public float _RangeMax = 15;
    public float _RangeMin = 3;
    public float _RangeAttackMax = 7;
    public float _RangeAttackMin = 6;
    public float _Tolerancia = 0.5f;
    
    public EnemyState _estado;

    private Animator Animator;
    public GameObject SpriteEnemyArqueiro;
    
    //Tiro
    public Transform arco;
    public GameObject _Flecha;
    public Transform localDeDisparo;
    private Vector2 dir;
    private Vector2 _Direcao;
    private GameObject cloneFlecha;

    private float timer;
    private float angle;
    
    //congelamento
    private float voltarAandar;
    public bool estaCongelado;
    public GameObject gelo;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = SpriteEnemyArqueiro.GetComponent<SpriteRenderer>();
        
        _estado = EnemyState.parado;
        _speedEnemy = 3;
        targetPlayer = FindObjectOfType<PlayerController>().transform;
        Animator = SpriteEnemyArqueiro.GetComponent<Animator>();
        //Tiro
        timer = 0.5f;
        
        //=====
        voltarAandar = 0;
    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.flipX = targetPlayer.position.x > transform.position.x;
        
        //voltar a andar
        if (estaCongelado)
        {
            if (targetPlayer.position.y > transform.position.y)
            {
                Animator.SetInteger("Transicao", 1);
                gelo.SetActive(true);
                voltarAandar += Time.deltaTime;

                if (voltarAandar >= 2)
                {
                    _speedEnemy = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                    Animator.SetInteger("Transicao", 0);
                }
            }
            else if (targetPlayer.position.y < transform.position.y)
            {
                Animator.SetInteger("Transicao", 4);
                gelo.SetActive(true);
                voltarAandar += Time.deltaTime;

                if (voltarAandar >= 2)
                {
                    _speedEnemy = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                    Animator.SetInteger("Transicao", 0);
                }
            }
        }
        else
        {
            gelo.SetActive(false);
        }
        
        CalcularDistancia();

        if (distance > _RangeMax)
        {
            //Nada
            _estado = EnemyState.parado;
        }

        switch (_estado)
        {
            case EnemyState.parado:
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 1);
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 4);
                }
                Parado();
                break;
            case EnemyState.aproximando:
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 2);
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 5);
                }
                Aproximando();
                break;
            case EnemyState.afastando:
                if (targetPlayer.position.y > transform.position.y)
                {
                    spriteRenderer.flipX = targetPlayer.position.x < transform.position.x;
                    Animator.SetInteger("Transicao", 5);
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    spriteRenderer.flipX = targetPlayer.position.x < transform.position.x;
                    Animator.SetInteger("Transicao", 2);
                }
                Afastando();
                break;
            case EnemyState.atacando:
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 3);
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 6);
                }
                Atacando();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //Tiro
        dir = (arco.position - targetPlayer.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arco.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    void Parado()
    {
        if (distance <= _RangeMin)
        {
            //Se afasta
            _estado = EnemyState.afastando;
        }
        else if (distance > _RangeMax)
        {
            //Nada
            _estado = EnemyState.parado;
        }
        else if (distance <= _RangeMax && distance > _RangeAttackMax)
        {
            //se Aproxima
            _estado = EnemyState.aproximando;
        }
        else if(distance <= _RangeAttackMax && distance > _RangeAttackMin)
        {
            //atacando
            _estado = EnemyState.atacando;
        }
    }
    void Afastando()
    {
        if (distance >= _RangeAttackMax + _Tolerancia || distance <= _RangeAttackMin - _Tolerancia)
        {
            if (!estaCongelado)
            {
                _speedEnemy = 2f;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, -(_speedEnemy) * Time.deltaTime);
        }
        else
        {
            _estado = EnemyState.atacando;
        }
    }
    void Atacando()
    {
        _speedEnemy = 0;
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            Instantiate(_Flecha, localDeDisparo.position, arco.rotation);
            timer = 2;
        }

        Parado();
    }
    void Aproximando()
    {
        if (distance >= _RangeAttackMax || distance <= _RangeAttackMin)
        {
            if (!estaCongelado)
            {
                _speedEnemy = 1.8f;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, _speedEnemy * Time.deltaTime);
        }
        else
        {
            _estado = EnemyState.atacando;
        }
    }
    void CalcularDistancia()
    {
        distance = Vector2.Distance(transform.position, targetPlayer.position);
        var heading = (transform.position - targetPlayer.position);
        direction = heading / distance;
    }
}