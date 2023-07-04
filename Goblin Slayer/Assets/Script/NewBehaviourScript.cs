using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    parado,
    reposicionando,
    atacando
}
public class NewBehaviourScript : MonoBehaviour
{
    private Transform targetPlayer;
    private float _speedEnemy;
    private float distance;
    private Vector3 direction;

    public float _RangeMax = 10;
    public float _RangeMin = 5;
    public float _RangeAttackMax = 7;
    public float _RangeAttackMin = 6;
    public float _Tolerancia = 0.01f;
    
    public EnemyState _estado;
    // Start is called before the first frame update
    void Start()
    {
        _estado = EnemyState.parado;
        _speedEnemy = 3;
        targetPlayer = FindObjectOfType<PlayerController>().transform;
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
            case EnemyState.reposicionando:
                Reposicionando();
                break;
            case EnemyState.atacando:
                Atacando();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    void Atacando()
    {
        _speedEnemy = 0;
        //inserir logica de ataque
        
        Parado();
    }
    void Reposicionando()
    {
        if (distance >= _RangeAttackMax + _Tolerancia || distance <= _RangeAttackMax - _Tolerancia)
        {
            _speedEnemy = 2;
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

    void Parado()
    {
        if (distance > _RangeMax)
        {
            //Nada
            _estado = EnemyState.parado;
        }
        else if ((distance <= _RangeMin) || (distance <= _RangeMax && distance > _RangeAttackMax))
        {
            //se reposiciona
            _estado = EnemyState.reposicionando;
        }
        else if(distance <= _RangeAttackMax && distance > _RangeAttackMin)
        {
            //atacando
            _estado = EnemyState.atacando;
        }
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
