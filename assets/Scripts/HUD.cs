using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Slider SliderVidaJogador;
    public Slider SliderEnergiaJogador;
    private PlayerInput ScriptPlayerInput;
    private Correr ScriptCorrer;
    public GameObject PainelGameOver;
    public Text TextoFimDeJogo;
    public Text TextoDeHighscore;
    private float sobrevivenciaMaxima;
    public int ContadorZumbisMortos;
    public Text TextoContadorZumbisMortos;
    private int RecordeZumbisMortos;
    public Text TextoRecordeZumbisMortos;
    public Text TextoComunista;
    

    void Start()
    {
        sobrevivenciaMaxima = PlayerPrefs.GetFloat("PontuacaoMaxima");
        RecordeZumbisMortos = PlayerPrefs.GetInt("RecordeZumbisMortos");
        Time.timeScale = 1;
        ScriptPlayerInput = GameObject.FindWithTag(Tags.player).GetComponent<PlayerInput>();
        ScriptCorrer = GameObject.FindWithTag(Tags.player).GetComponent<Correr>();
        SliderVidaJogador.maxValue = ScriptPlayerInput.StatusPlayer.vida;
        AtualizarSliderVida();
        SliderEnergiaJogador.maxValue = ScriptCorrer.energia;
    }

    // Update is called once per frame
    public void AtualizarSliderVida()
    {
        SliderVidaJogador.value = ScriptPlayerInput.StatusPlayer.vida;
    }
    public void AtualizarSliderEnergia()
    {
        SliderEnergiaJogador.value = ScriptCorrer.energia - ScriptCorrer.contadorTempo;
    }
    public void GameOver()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int Segundos = (int)(Time.timeSinceLevelLoad % 60);

        
        TextoFimDeJogo.text = "Você Sobreviveu por "+ minutos +":" + Segundos +" minutos e marcou "+ ScriptPlayerInput.PontosTotais +" pontos.";

        guardarPontuacaoMaxima(minutos, Segundos);
        GuardarZumbisMortos(ContadorZumbisMortos);
    }
    public void Reiniciar() { 
        SceneManager.LoadScene("tela");
    }
    public void AtualizarQuantidadeDeZumbisMortos()
    {
        ContadorZumbisMortos++;
        TextoContadorZumbisMortos.text = string.Format("X {0}",ContadorZumbisMortos);
        if(ContadorZumbisMortos >= 999)
        {
            TextoContadorZumbisMortos.text = "999";
        }
    }
    void guardarPontuacaoMaxima(int min, int seg){
        if (Time.timeSinceLevelLoad > sobrevivenciaMaxima)
        {
            sobrevivenciaMaxima = Time.timeSinceLevelLoad;
            TextoDeHighscore.text = string.Format("Seu melhor tempo é: {0}:{1}!", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", sobrevivenciaMaxima);
        }
        else
        {
            int minSobrevivenciaMaxima = (int)(sobrevivenciaMaxima / 60);
            int segSobrevivenciaMaxima = (int)(sobrevivenciaMaxima % 60);
            TextoDeHighscore.text = string.Format("Seu melhor tempo é: {0}min e{1}seg!",minSobrevivenciaMaxima,segSobrevivenciaMaxima);
        }
    
    }
    void GuardarZumbisMortos(int ContadorZumbis)
    {
        if(ContadorZumbis > RecordeZumbisMortos)
        {
            RecordeZumbisMortos = ContadorZumbis;
            TextoRecordeZumbisMortos.text = string.Format("Recorde de Zumbis mortos em um único Round: {0}",RecordeZumbisMortos);
            PlayerPrefs.SetInt("RecordeZumbisMortos",RecordeZumbisMortos);
        }
        else
        {
            TextoRecordeZumbisMortos.text = string.Format("Recorde de Zumbis mortos em um único Round: {0}", RecordeZumbisMortos);
        }
    }
    public void Vitoria()
    {
        GameOver();
        TextoFimDeJogo.text = "Parabéns, você sobreviveu a essa noite!!!, e marcou " + ScriptPlayerInput.PontosTotais + " Pontos.";
    }
    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(1.5f, TextoComunista));
    }
    IEnumerator DesaparecerTexto(float tempoDeSumoco,Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(tempoDeSumoco);
        float contador = 0;
        while(textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumoco;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}