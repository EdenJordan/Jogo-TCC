using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    //Trios
    public GameObject tiroFogo;
    public GameObject tiroGelo;
    public GameObject tiroRaio;
    private Vector3 localDeDisparo;
    
    private float timer;
    private float tiroAtual;
    
    public bool onFire;
    //Outras coisas
    
    // Start is called before the first frame update
    void Start()
    {
        onFire = false;
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
    }

    void Atira()
    {
        //Instanceia o tiro e calcula o tempo entre eles
        if (!onFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (tiroAtual == 1)
                {
                    timer = 3;
                    onFire = true;
                    Instantiate(tiroFogo, localDeDisparo, tiroFogo.transform.rotation);
                }
                if (tiroAtual == 2)
                {
                    timer = 3;
                    onFire = true;
                    Instantiate(tiroGelo, localDeDisparo, tiroGelo.transform.rotation);
                }
                if (tiroAtual == 3)
                {
                    timer = 3;
                    onFire = true;
                    Instantiate(tiroRaio, localDeDisparo, tiroRaio.transform.rotation);
                }
            }
        }
        if (onFire)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                onFire = false;
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
    }
}
