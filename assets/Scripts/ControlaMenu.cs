using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public Transform player;
    public GameObject BotaoSair;
    private int numeroSkin = 1;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.player).transform;
        #if UNITY_STANDALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);
        #endif
    }
    public void Jogar()
    {
        PlayerPrefs.SetInt("SkinDePreferencia",numeroSkin);
        SceneManager.LoadScene("tela");
    }
    public void TrocarSkin()
    {
        player.GetChild(numeroSkin).gameObject.SetActive(false);
        numeroSkin++;

        if (numeroSkin >= player.childCount)
        { 
            numeroSkin = 1;
        }
        player.GetChild(numeroSkin).gameObject.SetActive(true);
    }
    public void Sair()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
