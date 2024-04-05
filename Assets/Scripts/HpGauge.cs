using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGauge : MonoBehaviour
{
    Vector3 _cameraRot = default;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
