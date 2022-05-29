using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorBoss : MonoBehaviour
{
    // Start is called before the first frame update

    private float tempoParaGerar;
    public float tempoEntreGeracoes;
    public GameObject ChefePrefab;
    private HUD scriptHUD;
    public Transform[] PosicoesPossiveisDegeracao;
    private Transform player;

    private void Start()
    {
        tempoParaGerar = tempoEntreGeracoes;
        scriptHUD = GameObject.FindObjectOfType(typeof(HUD)) as HUD;
        player = GameObject.FindWithTag(Tags.player).transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaGerar)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoPlayer();
            Instantiate(ChefePrefab,posicaoDeCriacao, Quaternion.identity);
            scriptHUD.AparecerTextoChefeCriado();
            tempoParaGerar = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
    Vector3 CalcularPosicaoMaisDistanteDoPlayer()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach(Transform posicao in PosicoesPossiveisDegeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, player.position);
            if(distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}
