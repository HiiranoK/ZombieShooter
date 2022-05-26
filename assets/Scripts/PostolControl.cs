using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostolControl : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomTiru;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ControlaAudio.instancia.PlayOneShot(SomTiru);
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
        }
    }
}
