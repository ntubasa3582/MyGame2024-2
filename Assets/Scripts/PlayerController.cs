using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<GameObject> _effects = new List<GameObject>();
    [SerializeField] GameObject _effectInstancePos = default;               //エフェクトの生成座標になるゲームオブジェクト
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jumpParameter = 1f;
    Rigidbody _rb = default;
    Vector3 _pos = default;
    int _effectCount = 0;                                          //エフェクトの順番をカウントする変数
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));    //プレイヤーの移動処理
        _pos = Camera.main.transform.TransformDirection(_pos);  //カメラからの角度で座標に変換する
        _pos.y = 0;
        transform.LookAt(transform.position + _pos);

        float verticalVelocity = _rb.velocity.y;
        _rb.velocity = _pos * _speed + Vector3.up * verticalVelocity;


        if (Input.GetButtonDown("Jump"))    //プレイヤーのジャンプ処理
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpParameter, _rb.velocity.z);
        }

        if (Input.GetButtonDown("Fire1"))   //エフェクトを生成する
        {
            Quaternion effectEuler;
            if (_effectCount == 0)
            {
                effectEuler = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
            }
            else
            {
                effectEuler = Quaternion.Euler(-90, this.transform.rotation.eulerAngles.y, 0);
            }

            Instantiate(_effects[_effectCount], _effectInstancePos.transform.position, effectEuler);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            _effectCount++;
            if (_effectCount >= _effects.Count)
            {
                _effectCount = 0;
            }
        }

        Cursor.visible = false;
    }
}
