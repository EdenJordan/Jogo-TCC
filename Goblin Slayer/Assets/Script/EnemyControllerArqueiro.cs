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
    private Transform targetPlayer;

    private float _speedEnemy;
    private float distance;
    public Vector3 direction;

    public float _RangeMax = 15;
    public float _RangeMin = 3;
    public float _RangeAttackMax = 7;
    public float _RangeAttackMin = 6;
    public float _Tolerancia = 0.5f;
    
    public EnemyState _estado;
    
    //Tiro
    public Transform arco;
    public GameObject _Flecha;
    public Transform localDeDisparo;
    private Vector2 dir;
    private Vector2 _Direcao;
    private GameObject cloneFlecha;

    private float timer;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        _estado = EnemyState.parado;
        _speedEnemy = 3;
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
            //Nada
            _estado = EnemyState.parado;
        }

        switch (_estado)
        {
            case EnemyState.parado:
                Parado();
                break;
            case EnemyState.aproximando:
                Aproximando();
                break;
            case EnemyState.afastando:
                Afastando();
                break;
            case EnemyState.atacando:
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
            _speedEnemy = 1.8f;
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
            _speedEnemy = 1.8f;
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