using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomTiru;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ControlaAudio.instancia.PlayOneShot(SomTiru);
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
        }
    }
}
