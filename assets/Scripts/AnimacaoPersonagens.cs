using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagens : MonoBehaviour
{
    private Animator meuAnimacoes;

    private void Awake()
    {
        meuAnimacoes = GetComponent<Animator>();
    }
    public void atacar(bool estado)
    {
        meuAnimacoes.SetBool("Atacando", estado);
    }
    public void Mover(float valorDeMovimento)
    {
        meuAnimacoes.SetFloat("Correndo", valorDeMovimento);
    }
    public void Morrer()
    {
        meuAnimacoes.SetTrigger("Morrer");
        this.enabled = false;
    }
}
