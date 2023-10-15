using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Trios
    public GameObject tiroFogo;
    public GameObject tiroGelo;
    public GameObject tiroRaio;
    private Vector3 localDeDisparo;
    
    private float tempoDeCadaAtaque;
    public float tiroAtual;
    
    public bool onFire;
    public bool onFireFisico;
    
    //Ataque Fisicos
    public GameObject attackDireita;
    public GameObject attackCima;
    public GameObject attackBaixo;
    
    //Outras coisas
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        onFire = false;
        onFireFisico = false;
        tiroAtual = 1;
        ProcurarObjetos();
    }

    // Update is called once per frame
    void Update()
    {
        ProcurarObjetos();
        Atira();
        EscolherTiro();
    }

    void ProcurarObjetos()
    {
        localDeDisparo = GameObject.Find("LocalDeDisparoPlayer").transform.position;
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    void Atira()
    {
        //Instanceia o tiro e calcula o tempo entre eles
        if (!onFire || !onFireFisico)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (tiroAtual == 1)
                {
                    tempoDeCadaAtaque = 0.5f;
                    onFire = true;
                    onFireFisico = true;
                    AtaqueFisico();
                }
                if (tiroAtual == 2)
                {
                    tempoDeCadaAtaque = 3;
                    onFire = true;
                    Instantiate(tiroFogo, localDeDisparo, tiroFogo.transform.rotation);
                }
                if (tiroAtual == 3)
                {
                    tempoDeCadaAtaque = 3;
                    onFire = true;
                    Instantiate(tiroGelo, localDeDisparo, tiroGelo.transform.rotation);
                }
                if (tiroAtual == 4)
                {
                    tempoDeCadaAtaque = 3;
                    onFire = true;
                    Instantiate(tiroRaio, localDeDisparo, tiroRaio.transform.rotation);
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
            }
        }
    }

    void EscolherTiro()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tiroAtual = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tiroAtual = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tiroAtual = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tiroAtual = 4;
        }
    }

    void AtaqueFisico()
    {
        if (_playerController.animTiros == 1 || _playerController.animTiros == 2)
        {
            attackDireita.SetActive(true);
        }

        if (_playerController.animTiros == 3)
        {
            attackCima.SetActive(true);
        }
            
        if (_playerController.animTiros == 4)
        {
            attackBaixo.SetActive(true);
        }
    }
}
