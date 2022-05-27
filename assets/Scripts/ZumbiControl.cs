using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZumbiControl : MonoBehaviour, IMatavel
{
    public float andar = 2f;
    private GameObject Player;
    private Movimentacao movimentoZumbi;
    private AnimacaoPersonagens animaZumbi;
    private Status statusZumbi;
    public AudioClip SomDeMorte;
    private int Pontuacao = 1;
    private float moscar = 20;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagal;
    private float tempoEntreVagais = 5;
    public float porcentagemGerarKitMedico = 0.15f;
    public GameObject KitMedicoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag(Tags.player);
        movimentoZumbi = GetComponent<Movimentacao>();
        animaZumbi = GetComponent<AnimacaoPersonagens>();
        statusZumbi = GetComponent<Status>();
        RandomSkin();
        VidaRandomica();
        Pontuacao = statusZumbi.vida;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 15);
    }
        void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Player.transform.position);
        
        movimentoZumbi.Rotacionar(direcao);
        animaZumbi.Mover(direcao.magnitude);

        if (distancia >= moscar)
        {
            Vagar();
        }

        else if (distancia > andar)
        {
            direcao = Player.transform.position - transform.position;
            movimentoZumbi.movimentar(direcao , statusZumbi.velocidade);
            animaZumbi.atacar(false);
        }
        else
        {
            direcao = Player.transform.position - transform.position;
            animaZumbi.atacar(true);
        }
    }
    void AtacaPlayer()
    {
        int DanoZumbi = 20;
        Player.GetComponent<PlayerInput>().TomarDano(DanoZumbi);

    }
    void RandomSkin()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusZumbi.vida -= dano;
        if(statusZumbi.vida <= 0)
        {
            Morrer();
        }
    }
    public int VidaRandomica()
    {
        statusZumbi.vida = Random.Range(1, 6);
        return statusZumbi.vida;
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        Player.GetComponent<PlayerInput>().SomarPontos(Pontuacao);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
    }
    public void Vagar()
    {
        contadorVagal -= Time.deltaTime;
        if(contadorVagal <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagal = tempoEntreVagais;
        }

        bool ficouPerto = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.5;
        if (ficouPerto == false)
            {
            direcao = posicaoAleatoria - transform.position;
            movimentoZumbi.movimentar(direcao, statusZumbi.velocidade);
        }
    }
    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 15;
        posicao.y = 0;
        posicao += transform.position;
        return posicao;
    }
    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position,Quaternion.identity);
        }
    }
}
