using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour, IMatavel, ICuravel
{
    public Vector3 vetor;
    public LayerMask MascaraChao;
    public HUD ScriptHUD;
    public AudioClip SomDeDano;
    public int PontosTotais;
    public GameObject textoVitoria;
    private MovimentacaoPlayer movimentaPlayer;
    private AnimacaoPersonagens animacaoPlayer;
    public Status StatusPlayer;

    void Start()
    {
        StatusPlayer = GetComponent<Status>();
        movimentaPlayer = GetComponent<MovimentacaoPlayer>();
        animacaoPlayer = GetComponent <AnimacaoPersonagens>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        vetor = new Vector3(eixoX, 0, eixoZ);
    }
    void FixedUpdate()
    {
        animacaoPlayer.Mover(vetor.magnitude);
        movimentaPlayer.movimentar(vetor,StatusPlayer.velocidade);
        movimentaPlayer.Rotacao(MascaraChao);
    }
   
    public void TomarDano(int dano)
    {
        StatusPlayer.vida -= dano;
        ScriptHUD.AtualizarSliderVida();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (StatusPlayer.vida <= 0)
        {
            Morrer();
        }

    }
    public void SomarPontos(int ponto)
    {
        PontosTotais += (ponto);
    }
    public void Morrer()
    {
        ScriptHUD.GameOver();
    }

    public void CurarVida(int QuantidadeDeCura)
    {
        StatusPlayer.vida += QuantidadeDeCura;
        if(StatusPlayer.vida > StatusPlayer.VidaInicial)
        {
            StatusPlayer.vida = StatusPlayer.VidaInicial;
        }
        ScriptHUD.AtualizarSliderVida();
    }

}
