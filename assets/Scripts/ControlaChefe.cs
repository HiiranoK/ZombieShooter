using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private GameObject player;
    private NavMeshAgent Agente;
    public Status StatusChefe;
    private AnimacaoPersonagens animacaoChefe;
    private Movimentacao movimentoChefe;
    private HUD scriptControlaHUD;
    private int tempoParaSumir = 3;
    public AudioClip SomDeMorte;
    private int pontuacao = 25;
    public KitMedico KitMedico;
    public Slider SliderVidaChefe;
    public Image imagemSlider;
    public Color CorDaVidaMaxima, CorDaVidaMinima;
    void Start()
    {
        player = GameObject.FindWithTag(Tags.player);
        Agente = GetComponent<NavMeshAgent>();
        StatusChefe = GetComponent<Status>();
        Agente.speed = StatusChefe.velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagens>();
        movimentoChefe = GetComponent<Movimentacao>();
        scriptControlaHUD = GameObject.FindObjectOfType<HUD>() as HUD;
        SliderVidaChefe.maxValue = StatusChefe.VidaInicial;
        AtualizarInterface();
    }

    // Update is called once per frame
    void Update()
    {
        Agente.SetDestination(player.transform.position);
        animacaoChefe.Mover(Agente.velocity.magnitude);
        
        if(Agente.hasPath == true)
        {
            bool estouPertoDoJogador = Agente.remainingDistance <= Agente.stoppingDistance;
            if (estouPertoDoJogador)
            {
                animacaoChefe.atacar(true);
                Vector3 direcao = player.transform.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            }
            else
            {
                animacaoChefe.atacar(false);
            }
        }     
    }
    void AtacaPlayer()
    {
        int dano = Random.Range(30, 50);
        player.GetComponent<PlayerInput>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        StatusChefe.vida -= dano;
        AtualizarInterface();
        if(StatusChefe.vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject, tempoParaSumir);
        this.enabled = false;
        Agente.enabled = false;
        animacaoChefe.Morrer();
        GetComponent<Collider>().enabled = false;
        scriptControlaHUD.AtualizarQuantidadeDeZumbisMortos();
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        Instantiate(KitMedico, transform.position, Quaternion.identity);
        player.GetComponent<PlayerInput>().SomarPontos(pontuacao);
    }
    void AtualizarInterface()
    {
        SliderVidaChefe.value = StatusChefe.vida;
        float porcentagemDaVida = (float)StatusChefe.vida / StatusChefe.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
        imagemSlider.color = corDaVida;
    }
}
