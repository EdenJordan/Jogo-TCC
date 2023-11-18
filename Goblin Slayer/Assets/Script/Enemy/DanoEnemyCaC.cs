using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoEnemyCaC : MonoBehaviour
{
    private Transform player;
    public float attackRange = 1f;
    
    public int danoParaDar;
    public bool dano;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        dano = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            dano = true;
            timer = 0;
        }
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (dano)
            {
                
                VidaPlayer.instance.DanoPlayer(danoParaDar);
                dano = false;
            }
        }
    }
}
