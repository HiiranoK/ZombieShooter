using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correr : MonoBehaviour
{
    private float energia = 3;
    public float contadorTempo ;
    GameObject Player;
    public float VelocidadeCorrendo = 3f;
    private bool correndo = false; 
    private bool cansado = false;

    private void Start()
    {
        Player = GameObject.FindWithTag(Tags.player);
    }
    void Update()
    {
        PlayerCorrendo();
        Contador();
        Descansar();
    }
    void PlayerCorrendo()
    {
        if(cansado == false)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Player.GetComponent<PlayerInput>().StatusPlayer.velocidade *= VelocidadeCorrendo;
                correndo = true;
            }
            else if (Input.GetButtonUp("Fire3"))
            {
                Player.GetComponent<PlayerInput>().StatusPlayer.velocidade /= VelocidadeCorrendo;
                correndo = false;

            }
        }
    }
    void Contador()
    {
        if(correndo == true)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= energia)
            {
                cansado = true;
                Player.GetComponent<PlayerInput>().StatusPlayer.velocidade /= VelocidadeCorrendo;
                correndo = false;
            }
        }
    }
    void Descansar()
    {
        if(contadorTempo > 0 && Player.GetComponent<PlayerInput>().vetor.magnitude <= 0.1)
        {
            contadorTempo -= (Time.deltaTime / 2);
            if(contadorTempo <= 1)
            {
                cansado = false;
            }
        }
    }
}
