using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Hud
    //espada,escudo,fogo e gelo:
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    //espada,escudo,gelo e raio:
    public GameObject slot5;
    public GameObject slot6;
    public GameObject slot7;
    public GameObject slot8;
    //espada e escudo
    public GameObject slot9;
    public GameObject slot10;
    //espada,escudo, fogo:
    public GameObject slot11;
    public GameObject slot12;
    public GameObject slot13;
    //espada,escudo,fogo e raio:
    public GameObject slot14;
    public GameObject slot15;
    public GameObject slot16;
    public GameObject slot17;
    
    
    
    

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
        slot9.SetActive(true); //espada
        slot10.SetActive(false); //escudo
        
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
        if (!pergaminhoFogo && !pergaminhoGelo)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Audio.instance.selecaoMenu.Play();
                slot9.SetActive(true);
                slot10.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Audio.instance.selecaoMenu.Play();
                slot9.SetActive(false);
                slot10.SetActive(true);
            }
        }
        if (pergaminhoGelo && pergaminhoFogo)
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

        if (pergaminhoGelo && pergaminhoRaio)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Audio.instance.selecaoMenu.Play();
                slot5.SetActive(true); //espada
                slot6.SetActive(false); //escudo
                slot7.SetActive(false); //pergaminho1
                slot8.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Audio.instance.selecaoMenu.Play();
                slot5.SetActive(false); //espada
                slot6.SetActive(true); //escudo
                slot7.SetActive(false); //pergaminho1
                slot8.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Audio.instance.selecaoMenu.Play();
                slot5.SetActive(false); //espada
                slot6.SetActive(false); //escudo
                slot7.SetActive(true); //pergaminho1
                slot8.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Audio.instance.selecaoMenu.Play();
                slot5.SetActive(false); //espada
                slot6.SetActive(false); //escudo
                slot7.SetActive(false); //pergaminho1
                slot8.SetActive(true); //pergaminho2
            }
        }
        if (pergaminhoFogo && pergaminhoRaio)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Audio.instance.selecaoMenu.Play();
                slot14.SetActive(true); //espada
                slot15.SetActive(false); //escudo
                slot16.SetActive(false); //pergaminho1
                slot17.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Audio.instance.selecaoMenu.Play();
                slot14.SetActive(false); //espada
                slot15.SetActive(true); //escudo
                slot16.SetActive(false); //pergaminho1
                slot17.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Audio.instance.selecaoMenu.Play();
                slot14.SetActive(false); //espada
                slot15.SetActive(false); //escudo
                slot16.SetActive(true); //pergaminho1
                slot17.SetActive(false); //pergaminho2
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Audio.instance.selecaoMenu.Play();
                slot14.SetActive(false); //espada
                slot15.SetActive(false); //escudo
                slot16.SetActive(false); //pergaminho1
                slot17.SetActive(true); //pergaminho2
            }
            
        }

        if (pergaminhoFogo && !pergaminhoGelo)
        {
          
            if (Input.GetKeyDown(KeyCode.E))
            {
                Audio.instance.selecaoMenu.Play();
                slot11.SetActive(true); //espada
                slot12.SetActive(false); //escudo
                slot13.SetActive(false); //pergaminho1
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Audio.instance.selecaoMenu.Play();
                slot11.SetActive(false); //espada
                slot12.SetActive(true); //escudo
                slot13.SetActive(false); //pergaminho1
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Audio.instance.selecaoMenu.Play();
                slot11.SetActive(false); //espada
                slot12.SetActive(false); //escudo
                slot13.SetActive(true); //pergaminho1
            }
            
        }


    }

    public void FadeSetActive(bool valor)
    {
        fadeOut.SetActive(valor);
    }
}
