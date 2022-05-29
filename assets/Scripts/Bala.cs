using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Bala : MonoBehaviour
    {
        public float Velocidade = 20;
        private Rigidbody rbBala;
        public int Dano = 1;

    private void Start()
    {
        rbBala = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void FixedUpdate()
        {
            rbBala.MovePosition(rbBala.position + (transform.forward * Velocidade) * Time.deltaTime);
        }

        void OnTriggerEnter(Collider objetoDeColisao)
        {
        Quaternion rotacaoOposta = Quaternion.LookRotation(transform.forward);
            if (objetoDeColisao.tag == "Inimigo" && objetoDeColisao.GetComponent<ZumbiControl>().statusZumbi.vida >0) { 
                objetoDeColisao.GetComponent<ZumbiControl>().TomarDano(Dano);
                objetoDeColisao.GetComponent<ZumbiControl>().Sangrar(transform.position, rotacaoOposta);
            }
            else if(objetoDeColisao.tag == "ChefeDeFase" && objetoDeColisao.GetComponent<ControlaChefe>().StatusChefe.vida >0){
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(Dano);
                objetoDeColisao.GetComponent<ControlaChefe>().Sangrar(transform.position, rotacaoOposta);
        }
        Destroy(gameObject);
        }
    }


