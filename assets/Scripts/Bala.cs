using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Bala : MonoBehaviour
    {
        public float Velocidade = 20;
        private Rigidbody rbBala;
        public PlayerInput Player;
        public int Dano = 1;

    private void Start()
    {
        rbBala = GetComponent<Rigidbody>();
        Player = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void FixedUpdate()
        {
            rbBala.MovePosition(rbBala.position + transform.forward * Velocidade * Time.deltaTime);
        }

        void OnTriggerEnter(Collider objetoDeColisao)
        {
            Destroy(gameObject);
            if (objetoDeColisao.tag == "Inimigo") {
                objetoDeColisao.GetComponent<ZumbiControl>().TomarDano(Dano);
            }     
        }
    }


