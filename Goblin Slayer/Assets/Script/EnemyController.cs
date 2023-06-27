using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float distancia;
    public float _speedEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(transform.position, target.position);
        var heading = (transform.position - target.position);
        var direction = heading / distance;
        
        if (distance >= distancia*3)
        {
            target.Translate(direction * (_speedEnemy * Time.deltaTime) );
        }
        else if (distance <= distancia)
        {
            target.Translate(-direction * (_speedEnemy * Time.deltaTime) );
        }
    }
}
