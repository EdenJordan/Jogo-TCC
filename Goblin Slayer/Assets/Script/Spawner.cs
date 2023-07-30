using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyAtual;
    public GameObject enemy;
    public Transform spawner;

    private bool stop;
    
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAtual == null && !stop)
        {
            stop = true;
            Instantiate(enemy, spawner.position, transform.rotation);
        }
    }
}
