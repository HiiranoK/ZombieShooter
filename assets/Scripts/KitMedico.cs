using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private GameObject Player;
    private int valorCura = 20;
    private Status VidaDoPlayer;
    private int tempoDeDestruicao = 15;

    private void Start()
    {
        Player = GameObject.FindWithTag(Tags.player);
        VidaDoPlayer = Player.GetComponent<Status>();
        Destroy(gameObject,tempoDeDestruicao);
    }
    private void OnTriggerEnter(Collider ObjetodeColisao)
    {
        if(ObjetodeColisao.tag == Tags.player && VidaDoPlayer.vida != VidaDoPlayer.VidaInicial)
        {
            Player.GetComponent<PlayerInput>().CurarVida(valorCura);
            Destroy(gameObject);
        }

    }
}
