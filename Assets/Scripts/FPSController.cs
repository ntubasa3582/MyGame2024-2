using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] GameObject _camera;    //カメラ
    [SerializeField] GameObject _bullet;//銃口
    Vector3 _pos = default;
    Quaternion cameraRot, characterRot;
    Rigidbody _rb;
    [SerializeField] GameObject _projectiles;   //投げるオブジェクト
    [SerializeField] float _jumpParameter = 1.0f;   //ジャンプのパラメータ
    [SerializeField] float Xsensityvity = 3f, Ysensityvity = 3f;    //視点の感度
    [SerializeField] float _speed = 0.1f;    //プレイヤーの移動速度
    float x, z;
    bool _isGround = true;
    bool cursorLock = true;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cameraRot = _camera.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        _camera.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        UpdateCursorLock();
        if (_isGround )
        {
            if (Input.GetButtonDown("Jump"))    //プレイヤーのジャンプ処理
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _jumpParameter, _rb.velocity.z);
                _isGround = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = (GameObject)Instantiate(_projectiles, _bullet.transform.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            Vector3 vector3 = _bullet.transform.position - transform.position;
            vector3.y = _camera.transform.rotation.x * -10;
            ballRigidbody.AddForce(vector3 * 1000);
        }
    }

    private void FixedUpdate()
    {
        //x = 0;
        //z = 0;

        //x = Input.GetAxisRaw("Horizontal") * speed;
        //z = Input.GetAxisRaw("Vertical") * speed;
        //transform.position += cam.transform.forward * z + cam.transform.right * x;
        _pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));    //プレイヤーの移動処理
        _pos = Camera.main.transform.TransformDirection(_pos);  //カメラからの角度で座標に変換する
        _pos.y = 0;
        float verticalVelocity = _rb.velocity.y;
        _rb.velocity = _pos * _speed + Vector3.up * verticalVelocity;
    }


    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}