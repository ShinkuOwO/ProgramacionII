using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 anteriorPosicionCamara;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        anteriorPosicionCamara = cameraTransform.position;
    }
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - anteriorPosicionCamara.x) * -0.5f;
        transform.Translate(new Vector3(deltaX, 0, 0));
        anteriorPosicionCamara = cameraTransform.position;
    }
}
