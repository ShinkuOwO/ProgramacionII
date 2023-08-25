using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteDeFps : MonoBehaviour
{
    private int limiteFPS = 60;
    void Start()
    {
        Application.targetFrameRate = limiteFPS;
    }
}

