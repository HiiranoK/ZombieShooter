using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Slider SliderVidaJogador;
    private PlayerInput ScriptPlayerInput;
    public GameObject PainelGameOver;
    public Text TextoFimDeJogo;
    public Text TextoDeHighscore;
    private float sobrevivenciaMaxima;
    public GameObject PainelPontuacaoMaxima;

    void Start()
    {
        sobrevivenciaMaxima = PlayerPrefs.GetFloat("PontuacaoMaxima");
        Time.timeScale = 1;
        ScriptPlayerInput = GameObject.FindWithTag(Tags.player).GetComponent<PlayerInput>();
        SliderVidaJogador.maxValue = ScriptPlayerInput.StatusPlayer.vida;
        AtualizarSliderVida();
    }

    // Update is called once per frame
    public void AtualizarSliderVida()
    {
        SliderVidaJogador.value = ScriptPlayerInput.StatusPlayer.vida;
    }
    public void GameOver()
    {
        PainelGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int Segundos = (int)(Time.timeSinceLevelLoad % 60);

        TextoFimDeJogo.text = "Você Sobreviveu por "+ minutos +":" + Segundos +" minutos e marcou "+ ScriptPlayerInput.PontosTotais +" pontos.";

        guardarPontuacaoMaxima(minutos, Segundos);
    }
    public void Reiniciar() { 
        SceneManager.LoadScene("tela");
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
    public void Vitoria()
    {
        GameOver();
        PainelPontuacaoMaxima.SetActive(false);
        TextoFimDeJogo.text = "Parabéns, você sobreviveu a essa noite!!!, e marcou "+ ScriptPlayerInput.PontosTotais+" Pontos.";
    }
}