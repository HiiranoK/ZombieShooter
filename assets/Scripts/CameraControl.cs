using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Player;
    Vector3 distanciaCompensada;
    public Vector3 offSet = new Vector3(0,25,0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + offSet;
    }
}
