using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitaFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // Desabilita VSync
        Application.targetFrameRate = 60; // Configura frameRate
#endif
    }
}
