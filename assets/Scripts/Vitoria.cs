using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitoria : MonoBehaviour
{
    public float tamanhoAcampamento = 7;
    private GameObject Player;
    public HUD textoVitoria;

    void Start()
    {
        Player = GameObject.FindWithTag(Tags.player);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,tamanhoAcampamento);
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < tamanhoAcampamento)
        {
            textoVitoria.Vitoria();
        }
    }
}
