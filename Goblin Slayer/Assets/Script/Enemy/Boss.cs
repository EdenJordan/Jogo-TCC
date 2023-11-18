using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator Animator;
    public GameObject SrpiteBoss;
    public Transform targetPlayer;


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
        targetPlayer = FindObjectOfType<PlayerController>().transform;
        spriteRenderer = SrpiteBoss.GetComponent<SpriteRenderer>();
        Animator = SrpiteBoss.GetComponent<Animator>();
        
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
        if (ataqueBastao.activeSelf)
        {
            isFire = true;
        }

        if (isFire)
        {
            //tira a velocidade
            speed = 0;
            
            //desativa o ataque
            timeFire += Time.deltaTime;
        
            if (timeFire >= 0.8f)
            {
                VidaPlayer.instance.DanoPlayer(danoParaDar);
                isFire = false;
                ataqueBastao.SetActive(false);
                timeFire = 0;
            }
            
            //troca de animação
            if (targetPlayer.position.y > transform.position.y)
            {
                Animator.SetInteger("Transicao", 6);
            }
            if (targetPlayer.position.y < transform.position.y)
            {
                Animator.SetInteger("Transicao", 12);
            }
        }
        else if (!estaCongelado)
        {
            speed = 2;
        }

        if (targetPlayer.position.y > transform.position.y && !estaCongelado && !isFire && isFollowingPlayer)
        {
            Animator.SetInteger("Transicao", 2);
        }
      
        if (targetPlayer.position.y < transform.position.y && !estaCongelado && !isFire && isFollowingPlayer)
        {
            Animator.SetInteger("Transicao", 8);
        }
        
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
                    speed = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                }
            }
            else if (targetPlayer.position.y < transform.position.y)
            {
                Animator.SetInteger("Transicao", 7);
                gelo.SetActive(true);
                voltarAandar += Time.deltaTime;
          
                if (voltarAandar >= 2)
                {
                    speed = 2;
                    voltarAandar = 0;
                    estaCongelado = false;
                }
            }
        }
        else
        {
            gelo.SetActive(false);
        }

        //barra de vida
        barraDeVida.value = currentHealth;

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
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 6);
                    AttackMelee(player);
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 12);
                    AttackMelee(player);
                }

            }
            else
            {
                if (targetPlayer.position.y > transform.position.y && !estaCongelado && !isFire && !isFollowingPlayer)
                {
                    Animator.SetInteger("Transicao", 2);
                }
      
                if (targetPlayer.position.y < transform.position.y && !estaCongelado && !isFire && !isFollowingPlayer)
                {
                    Animator.SetInteger("Transicao", 8);
                }
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
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 3);
                    magicPrefab = fogoBallPrefab;
                    Audio.instance.fogo.Play();
                }
                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 9);
                    magicPrefab = fogoBallPrefab;
                    Audio.instance.fogo.Play();
                }
                
                break;
            case 2:
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 4);
                    magicPrefab = geloBallPrefab;
                    Audio.instance.gelo.Play();
                }

                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 10);
                    magicPrefab = geloBallPrefab;
                    Audio.instance.gelo.Play();
                }

                break;
            case 3:
                if (targetPlayer.position.y > transform.position.y)
                {
                    Animator.SetInteger("Transicao", 5);
                    Audio.instance.raio.Play();
                    magicPrefab = raioBallPrefab;
                }

                if (targetPlayer.position.y < transform.position.y)
                {
                    Animator.SetInteger("Transicao", 11);
                    Audio.instance.raio.Play();
                    magicPrefab = raioBallPrefab;
                }
                
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
            isFire = false;
            timeFire = 0;
        }
    }
}
