using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    private GameObject player;
    private float angle;
    
    //vida do boss
    public int maxHealth;
    private int currentHealth;
    public Slider barraDeVida;
    
    //ataque
    public GameObject fogoBallPrefab;
    public GameObject geloBallPrefab;
    public GameObject raioBallPrefab;
    public GameObject ataqueBastao;

    public float attackRange = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;
    public int danoParaDar;
    public bool isFire;
    public bool estaAtacando;
    private float timeFire;

    //seguir jogador 
    private float followTimer = 3f;
    public bool isFollowingPlayer;
    public float speed;

    //congelamento
    private float voltarAandar;
    public bool estaCongelado;
    public GameObject gelo;
    
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        //seguir jogador
        isFollowingPlayer = true;
        voltarAandar = 0;
        
        //vida
        currentHealth = maxHealth;
        barraDeVida.value = currentHealth;
        
        //tiros
        ataqueBastao.SetActive(false);
        isFire = false;
        timeFire = 0;
    }

    void Update()
    {
        //voltar a andar
        if (estaCongelado)
        {
            gelo.SetActive(true);
            voltarAandar += Time.deltaTime;
            
            if (voltarAandar >= 2)
            {
                speed = 2;
                voltarAandar = 0;
                estaCongelado = false;
            }
        }
        else
        {
            gelo.SetActive(false);
        }

        //barra de vida
        barraDeVida.value = currentHealth;
        
        //desativa o ataque
        timeFire += Time.deltaTime;
        
        if (timeFire >= 0.8f)
        {
            isFire = false;
            ataqueBastao.SetActive(false);
            timeFire = 0;
        }

        //procura o jogador
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Tiro
        Vector2 direcao = (transform.position - player.transform.position).normalized;
        angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        //segeue o jogador 
        if (player != null)
        {
            if (!isFollowingPlayer)
            {
                followTimer += Time.deltaTime;
                if (followTimer >= 1f)
                {
                    isFollowingPlayer = true;
                    followTimer = 2f;
                }
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (player.transform.position != transform.position)
            {
                if (isFollowingPlayer)
                {
                    if (distanceToPlayer >= attackRange)
                    {
                        // Se o boss estiver seguindo o jogador, atualize o temporizador
                        followTimer -= Time.deltaTime;
                        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                        if (followTimer <= 0f)
                        {
                            isFollowingPlayer = false;
                        }
                    }
                    else
                    {
                        isFollowingPlayer = false;
                    }
                }
            }
            else
            {
                isFollowingPlayer = false;
            }
            
            //ataque fisico
            if (!isFollowingPlayer && distanceToPlayer < attackRange && Time.time - lastAttackTime > attackCooldown)
            {
                AttackMelee(player);
            }
            //ataque a distancia
            if (!isFollowingPlayer && distanceToPlayer > attackRange && Time.time - lastAttackTime > attackCooldown)
            {
                AttackMagic();
            }
        }
    }

    void AttackMelee(GameObject player)
    {
        isFire = true;
        timeFire = 0f;
        ataqueBastao.SetActive(true);
        lastAttackTime = Time.time;
    }

    void AttackMagic()
    {
        int randomAttack = Random.Range(1, 4); // 1 para fogo, 2 para gelo, 3 para raio

        GameObject magicPrefab = null;

        switch (randomAttack)
        {
            case 1:
                magicPrefab = fogoBallPrefab;
                Audio.instance.fogo.Play();
                break;
            case 2:
                magicPrefab = geloBallPrefab;
                Audio.instance.gelo.Play();
                break;
            case 3:
                Audio.instance.raio.Play();
                magicPrefab = raioBallPrefab;
                break;
            default:
                break;
        }

        if (magicPrefab != null)
        {
            // Instanciar o ataque mágico (por exemplo, instanciar uma bola de fogo)
            Instantiate(magicPrefab, transform.position, Quaternion.Euler(0,0,angle + 90));
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int danoParaReceber)
    {
        Audio.instance.dano.Play();
        currentHealth -= danoParaReceber;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Morte();
        }
    }

    void Morte()
    {
        Audio.instance.caverna.Stop();
        Audio.instance.floresta.Play();
        Menu.instance.DontDestroy();
        SceneManager.LoadScene("CutsCenesFinais");
        Destroy(gameObject);
    }
    //dano ao jogador
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AtaquePlayer"))
        {
            TakeDamage(2);
        }
        if (col.gameObject.CompareTag("ZonaDeDano") && isFire)
        {
            VidaPlayer.instance.DanoPlayer(danoParaDar);
            isFire = false;
            timeFire = 0;
        }
    }
}
