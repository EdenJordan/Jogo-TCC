using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    private GameManager _gameManager;

    public bool bauFechado;
    
    // Start is called before the first frame update
    void Start()
    {
        bauFechado = true;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.fases == 2 && bauFechado)
            {
                _gameManager.tiroAtual = 3;
                _gameManager.slot11.SetActive(false); //espada
                _gameManager.slot12.SetActive(false); //escudo
                _gameManager.slot13.SetActive(true); //pergaminho1
                
                _gameManager.slot9.SetActive(false); //desativa hud anterior
                _gameManager.slot10.SetActive(false); //desativa hud anterior
                
                Audio.instance.coletapergaminhos.Play();
                Pause.instance.painelPergaminhoFogo.SetActive(true);
                bauFechado = false;
            }
            
            if (GameManager.instance.fases == 4 && bauFechado)
            {
                _gameManager.tiroAtual = 4;
                _gameManager.slot1.SetActive(false); //espada
                _gameManager.slot2.SetActive(false); //escudo
                _gameManager.slot3.SetActive(false); //pergaminho1
                _gameManager.slot4.SetActive(true); //pergaminho2
                
                _gameManager.slot11.SetActive(false); //desativa hud anterior
                _gameManager.slot12.SetActive(false); //desativa hud anterior
                _gameManager.slot13.SetActive(false); ///desativa hud anterior
                
                
                
                Audio.instance.coletapergaminhos.Play();
                Pause.instance.painelPergaminhoGelo.SetActive(true);
                bauFechado = false;
            }
            
            if (GameManager.instance.fases == 6 && bauFechado)
            {
                if (_gameManager.pergaminhoFogo && _gameManager.pergaminhoRaio && !_gameManager.pergaminhoGelo)
                {
                    _gameManager.slot14.SetActive(false); //espada
                    _gameManager.slot15.SetActive(false); //escudo
                    _gameManager.slot16.SetActive(false); //pergaminho1
                    _gameManager.slot17.SetActive(true); //pergaminho2
                    
                    
                    _gameManager.slot1.SetActive(false); //espada
                    _gameManager.slot2.SetActive(false); //escudo
                    _gameManager.slot3.SetActive(false); //pergaminho1
                    _gameManager.slot4.SetActive(false); //pergaminho2
                }
                if (_gameManager.pergaminhoRaio && _gameManager.pergaminhoGelo && !_gameManager.pergaminhoFogo)
                {
                    _gameManager.slot5.SetActive(false); //espada
                    _gameManager.slot6.SetActive(false); //escudo
                    _gameManager.slot7.SetActive(true); //pergaminho1
                    _gameManager.slot8.SetActive(false); //pergaminho2
                    
                    _gameManager.slot1.SetActive(false); //espada
                    _gameManager.slot2.SetActive(false); //escudo
                    _gameManager.slot3.SetActive(false); //pergaminho1
                    _gameManager.slot4.SetActive(false); //pergaminho2
                }
                Audio.instance.coletapergaminhos.Play();
                Pause.instance.painelEscolherPergaminho.SetActive(true);
                bauFechado = false;
            }
        }
    }
}
