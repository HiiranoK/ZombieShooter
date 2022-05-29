using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZumbiControl : MonoBehaviour, IMatavel
{
    public float andar = 2f;
    private GameObject Player;
    private Movimentacao movimentoZumbi;
    private AnimacaoPersonagens animaZumbi;
    [HideInInspector]
    public Status statusZumbi;
    public AudioClip SomDeMorte;
    private int Pontuacao = 1;
    private float moscar = 20;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagal;
    private int tempoEntreVagais = 4;
    public float porcentagemGerarKitMedico = 0.15f;
    public GameObject KitMedicoPrefab;
    private HUD ScriptControlaHUD;
    [HideInInspector]
    public GeradorZumbi meuGerador;
    public int tempoParaSumir = 2;
    public GameObject particulaSangue;
    public Slider SliderVidaZumbi;
    public Image imagemSliderZumbi;
    public Color CorDaVidaMaxima, CorDaVidaMinima;

    // Start is called before the first frame update
    void Start()
    {
        statusZumbi = GetComponent<Status>();
        VidaRandomica();
        statusZumbi.vida = statusZumbi.VidaInicial;
        Player = GameObject.FindWithTag(Tags.player);
        movimentoZumbi = GetComponent<Movimentacao>();
        animaZumbi = GetComponent<AnimacaoPersonagens>();
        ScriptControlaHUD = GameObject.FindObjectOfType(typeof(HUD)) as HUD; // Forma de chamar um scrypt de HUD de maneira private;
        RandomSkin();
        Pontuacao = statusZumbi.vida;
        SliderVidaZumbi.maxValue = statusZumbi.VidaInicial;
        AtualizarInterface();
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
        int geraTipoZumbi = Random.Range(1, transform.childCount -1);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusZumbi.vida -= dano;
        AtualizarInterface();
        if(statusZumbi.vida <= 0)
        {
            Morrer();
        }
    }

    public void Sangrar(Vector3 posicao, Quaternion rotacao) 
    {
        Instantiate(particulaSangue, posicao, rotacao);
    }

    public int VidaRandomica()
    {
        statusZumbi.VidaInicial = Random.Range(1, 5);
        return statusZumbi.VidaInicial;
    }

    public void Morrer()
    {
        Destroy(gameObject, tempoParaSumir);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
        animaZumbi.Morrer(); 
        ScriptControlaHUD.AtualizarQuantidadeDeZumbisMortos();
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        Player.GetComponent<PlayerInput>().SomarPontos(Pontuacao);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }
    public void Vagar()
    {
        contadorVagal -= Time.deltaTime;
        if(contadorVagal <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagal = tempoEntreVagais + Random.Range(-2f,2f);
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
    void AtualizarInterface()
    {
        if(statusZumbi.vida < statusZumbi.VidaInicial)
        {
            SliderVidaZumbi.gameObject.SetActive(true);
        }
        SliderVidaZumbi.value = statusZumbi.vida;
        float porcentagemDaVida = (float)statusZumbi.vida / statusZumbi.VidaInicial;
        Color corDaVida = Color.Lerp(CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
        imagemSliderZumbi.color = corDaVida;
    }
}
