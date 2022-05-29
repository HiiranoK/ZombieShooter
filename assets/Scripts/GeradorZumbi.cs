using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//IEnumerator nome(){} declare isso em uma função que rodará um laço.
//yield return null; necessário para pular o laço no frame e impedir um loop infinito.
//StartCoroutine usado para chamar o IEnumerator
public class GeradorZumbi : MonoBehaviour

{
    public float TempoGerarZumbi = 1;
    float contadorTempo = 0;
    public GameObject Zumbi;
    public LayerMask LayerZumbi;
    public float distanciaGeracao = 7;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject player;
    private int quantidadeMaxZumbisvivos = 3;
    private int QuantidadeDeZumbisvivos;
    private float tempoProximoAumentoDeDificuldade = 5;
    private float contadorDeAumentarDificuldade;
    
    private void Start()
    {
        player = GameObject.FindWithTag(Tags.player);
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        for(int i = 0; i< quantidadeMaxZumbisvivos; i++)
        {
            gerarNovoZumbi();
        }
    }
    void FixedUpdate()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, player.transform.position) > distanciaDoJogadorParaGeracao;
        if (possoGerarZumbisPelaDistancia && QuantidadeDeZumbisvivos < quantidadeMaxZumbisvivos)
        {
                contadorTempo += Time.deltaTime;
                if (contadorTempo >= TempoGerarZumbi)
                {
                    gerarNovoZumbi();
                    contadorTempo = 0;
                }
        }
        if (Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            if(quantidadeMaxZumbisvivos <= 7)
            quantidadeMaxZumbisvivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }
    }
    
    void OnDrawGizmos() { 
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,distanciaGeracao);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaDoJogadorParaGeracao);
    }
    void gerarNovoZumbi()
    {
        Vector3 posicaoSpawn = AleatorizarPosicao();
        //Collider[] colisores = Physics.OverlapSphere(posicaoSpawn, 1, LayerZumbi);

        ZumbiControl zumbi = Instantiate(Zumbi, posicaoSpawn, transform.rotation).GetComponent<ZumbiControl>();
        zumbi.meuGerador = this;
        QuantidadeDeZumbisvivos++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao.y = 0;
        posicao += transform.position;
        return posicao;
    }
    public void DiminuirQuantidadeDeZumbisVivos()
    {
        QuantidadeDeZumbisvivos--;
    }
}
