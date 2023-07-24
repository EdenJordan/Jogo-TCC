using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform player;
    
    private float speed;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerController").transform;
        speed = 8;
        timer = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movimenta o tiro
        if (player.rotation.y == 0)
        {
            rig.velocity = Vector2.right * speed;
        }
        else if (player.rotation.y == 180)
        {
            rig.velocity = Vector2.left * speed;
        }
        
        //Destroi o tiro
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
