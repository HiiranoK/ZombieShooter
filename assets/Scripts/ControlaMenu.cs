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
        StartCoroutine(MudarCena("tela"));
    }
    IEnumerator MudarCena (string name)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(name);
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
    public void SairDoJogo()
    {
        StartCoroutine(Sair());
    }
    IEnumerator Sair()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
