using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour, IMatavel, ICuravel
{
    public Vector3 vetor;
    public LayerMask MascaraChao;
    public HUD ScriptControlaHUD;
    public AudioClip SomDeDano;
    public int PontosTotais;
    public GameObject textoVitoria;
    private MovimentacaoPlayer movimentaPlayer;
    private AnimacaoPersonagens animacaoPlayer;
    public Status StatusPlayer;
    public GameObject player;
    private int skinDePreferencia;

    void Start()
    {
        skinDePreferencia = PlayerPrefs.GetInt("SkinDePreferencia");
        StatusPlayer = GetComponent<Status>();
        movimentaPlayer = GetComponent<MovimentacaoPlayer>();
        animacaoPlayer = GetComponent <AnimacaoPersonagens>();
        player = GameObject.FindWithTag(Tags.player);
        TrocandoSkinDoPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        vetor = new Vector3(eixoX, 0, eixoZ);
        ScriptControlaHUD.AtualizarSliderEnergia();
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
        ScriptControlaHUD.AtualizarSliderVida();
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
        ScriptControlaHUD.GameOver();
    }

    public void CurarVida(int QuantidadeDeCura)
    {
        StatusPlayer.vida += QuantidadeDeCura;
        if(StatusPlayer.vida > StatusPlayer.VidaInicial)
        {
            StatusPlayer.vida = StatusPlayer.VidaInicial;
        }
        ScriptControlaHUD.AtualizarSliderVida();
    }
    public void TrocandoSkinDoPlayer()
    {
        player.transform.GetChild(skinDePreferencia).gameObject.SetActive(true);
    }
}
