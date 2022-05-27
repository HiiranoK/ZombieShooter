using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    [HideInInspector]
    public int vida;
    public float velocidade = 5f;
    
    void Awake()
    {
        vida = VidaInicial;
    }
}