using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    private Rigidbody2D rig;
    private PlayerController player;
    private GameManager _gamaManager;
    
    private float speed;
    private float timerDestroy;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _gamaManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rig = GetComponent<Rigidbody2D>();
        speed = 8;
        timerDestroy = 3;
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimenta o tiro
        
        //Direita
        if (player.animTiros == 1 && timer == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            rig.velocity = Vector2.right * speed;
        }
        //Esquerda
        else if (player.animTiros == 2 && timer == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            rig.velocity = Vector2.left * speed;
        }
        //Cima
        else if (player.animTiros == 3 && timer == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            rig.velocity = Vector2.up * speed;
        }
        //Baixo
        else if (player.animTiros == 4 && timer == 3 && _gamaManager.tiroAtual != 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            rig.velocity = Vector2.down * speed;
        }
        
        //Destroi o tiro
        timerDestroy -= Time.deltaTime;
        timer -= Time.deltaTime;
        
        if (timerDestroy <= 0)
        {
            Destroy(gameObject);
        }
        if (timer <= 0)
        {
            timer = 3;
        }
    }
}
