using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroEnemy : MonoBehaviour
{
    private EnemyController _enemyController;
    private Rigidbody2D rig;
    public float speed;
    private float timerdestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GameObject.FindWithTag("Enemy").GetComponent<EnemyController>();
        rig = GetComponent<Rigidbody2D>();
        timerdestroy = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timerdestroy -= Time.deltaTime;
        
        if (timerdestroy <= 0)
        {
            Destroy(gameObject);
            timerdestroy = 3;
        }
        rig.velocity = _enemyController.dir * -speed;
    }
}
