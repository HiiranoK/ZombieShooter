using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour, IMatavel
{
    Vector3 vetor;
    public LayerMask MascaraChao;
    public HUD ScriptHUD;
    public AudioClip SomDeDano;
    public int PontosTotais;
    public GameObject textoVitoria;
    private MovimentacaoMouse movimentaPlayer;
    private AnimacaoPersonagens animacaoPlayer;
    public Status StatusPlayer;

    // Start is called before the first frame update
    void Start()
    {
        movimentaPlayer = GetComponent<MovimentacaoMouse>();
        animacaoPlayer = GetComponent <AnimacaoPersonagens>();
        StatusPlayer = GetComponent<Status>();
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

}
