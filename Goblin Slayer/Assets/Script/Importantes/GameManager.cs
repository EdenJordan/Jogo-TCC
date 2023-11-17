using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Hud
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;

    public GameObject fadeOut;
    
    //Vida
    public Text vidaPlayer;

    //Tiros
    public GameObject tiroFogo;
    public GameObject tiroGelo;
    public GameObject tiroRaio;
    private Vector3 localDeDisparo;
    
    private float tempoDeCadaAtaque;
    public float tiroAtual;
    
    public bool onFire;
    public bool onFireFisico;

    public bool pergaminhoFogo;
    public bool pergaminhoGelo;
    public bool pergaminhoRaio;
    
    //Ataque Fisicos
    public GameObject attackDireita;
    public GameObject attackCima;
    public GameObject attackBaixo;
    
    //Outras coisas
    private PlayerController _playerController;
    public static GameManager instance;
    public int fases;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Audio.instance.caverna.Stop();
        Audio.instance.menu.Stop();
        Audio.instance.floresta.Play();
        onFire = false;
        onFireFisico = false;
        tiroAtual = 1;
        ProcurarObjetos();

        fases = 1;
        
        //hud
        slot1.SetActive(true); //espada
        slot2.SetActive(false); //escudo
        slot3.SetActive(false); //pergaminho1
        slot4.SetActive(false); //pergaminho2
    }

    // Update is called once per frame
    void Update()
    {
        ProcurarObjetos();
        Atira();
        EscolherTiro();
        HudMenu();

        vidaPlayer.text = " x " + VidaPlayer.vidaAtual;
    }

    void ProcurarObjetos()
    {
        localDeDisparo = GameObject.Find("LocalDeDisparoPlayer").transform.position;
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    void Atira()
    {
        if (!onFireFisico)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (tiroAtual == 1) //espada
                {
                    Audio.instance.espada.Play();
                    tempoDeCadaAtaque = 0.3f;
                    onFireFisico = true;
                    AtaqueFisico();
                }
            }
        }
        //Instanceia o tiro e calcula o tempo entre eles
        if (!onFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (tiroAtual == 2) //escudo
                {
                    Audio.instance.escudo.Play();
                    tempoDeCadaAtaque = 0.5f; //tava 3
                    VidaPlayer.instance.escudo = true;
                }

                if (tiroAtual == 3) //tirofogo
                {
                    Audio.instance.fogo.Play();
                    tempoDeCadaAtaque = 0.45f; //tava 2
                    onFire = true;
                    Instantiate(tiroFogo, localDeDisparo, tiroFogo.transform.rotation);
                }
                if (tiroAtual == 4)//tirogelo
                {
                    Audio.instance.gelo.Play();
                    tempoDeCadaAtaque = 0.45f; //tava 2
                    onFire = true;
                    Instantiate(tiroGelo, localDeDisparo, tiroGelo.transform.rotation);
                }

                if (tiroAtual == 5) //tiroraio
                {
                    Audio.instance.raio.Play();
                    tempoDeCadaAtaque = 0.45f; //tava 2
                    onFire = true;
                    Instantiate(tiroRaio, localDeDisparo, tiroRaio.transform.rotation);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (tiroAtual == 2)
                {
                    VidaPlayer.instance.escudo = false;
                }
            }
            
        }
        if (onFire || onFireFisico)
        {
            tempoDeCadaAtaque -= Time.deltaTime;
            
            if (tempoDeCadaAtaque <= 0)
            {
                attackDireita.SetActive(false);
                attackCima.SetActive(false);
                attackBaixo.SetActive(false);
                onFire = false;
                onFireFisico = false;
                VidaPlayer.instance.escudo = false;
            }
        }
    }

    void EscolherTiro()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            tiroAtual = 1; //espada
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            tiroAtual = 2; //escudo
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (pergaminhoFogo)
            {
                tiroAtual = 3; //tirofogo
            }
            else if (pergaminhoRaio)
            {
                tiroAtual = 5; //tiroraio
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pergaminhoGelo)
            {
                tiroAtual = 4; //tirogelo
            }
            else if (pergaminhoRaio)
            {
                tiroAtual = 5; //tiroraio
            }
        }
    }

    void AtaqueFisico()
    {
        if (_playerController.animDirecao == 1 || _playerController.animDirecao == 2)
        {
            attackDireita.SetActive(true);
        }

        if (_playerController.animDirecao == 3)
        {
            attackCima.SetActive(true);
        }
            
        if (_playerController.animDirecao == 4)
        {
            attackBaixo.SetActive(true);
        }
    }

    public void HudMenu()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Audio.instance.selecaoMenu.Play();
            slot1.SetActive(true); //espada
            slot2.SetActive(false); //escudo
            slot3.SetActive(false); //pergaminho1
            slot4.SetActive(false); //pergaminho2
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Audio.instance.selecaoMenu.Play();
            slot1.SetActive(false); //espada
            slot2.SetActive(true); //escudo
            slot3.SetActive(false); //pergaminho1
            slot4.SetActive(false); //pergaminho2
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Audio.instance.selecaoMenu.Play();
            slot1.SetActive(false); //espada
            slot2.SetActive(false); //escudo
            slot3.SetActive(true); //pergaminho1
            slot4.SetActive(false); //pergaminho2
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Audio.instance.selecaoMenu.Play();
            slot1.SetActive(false); //espada
            slot2.SetActive(false); //escudo
            slot3.SetActive(false); //pergaminho1
            slot4.SetActive(true); //pergaminho2
        }
    }

    public void FadeSetActive(bool valor)
    {
        fadeOut.SetActive(valor);
    }
}