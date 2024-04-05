using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGauge : MonoBehaviour
{
    Vector3 _playerPos = default;
    GameObject _player = default;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = new Vector3(0 , _playerPos.y, 0);
        //transform.rotation = Camera.main.transform.rotation;

    }
}
