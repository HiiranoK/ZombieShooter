using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private Rigidbody meuRigidbody;
    void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
   public void movimentar(Vector3 direcao,float velocidade)
    {
        meuRigidbody.MovePosition(meuRigidbody.position + (direcao.normalized * velocidade * Time.deltaTime));
    }
   public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }
    //public void Morrer()
    //{
    //    meuRigidbody.constraints = RigidbodyConstraints.None;
    //    meuRigidbody.velocity = Vector3.zero;
    //    if (Time.deltaTime > tempoMorrer)
    //    {
    //        GetComponent<Collider>().enabled = false;
    //    }
    //}
}
