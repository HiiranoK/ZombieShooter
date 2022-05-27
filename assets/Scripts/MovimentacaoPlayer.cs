using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoPlayer : Movimentacao
{
    public void Rotacao(LayerMask MascaraChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 15, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 50, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;
            Rotacionar(posicaoMiraJogador);
        }
    }
}
