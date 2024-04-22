using UnityEngine;

public class FPSController : MonoBehaviour
{
    MoneyTrade _moneyTrade;
    PauseGame _pauseGame;
    [SerializeField] GameObject _camera;    //カメラ
    [SerializeField] GameObject _muzzle;//銃口
    [SerializeField] GameObject[] _bullets;   //投げるオブジェクト
    Vector3 _pos = default;
    Quaternion cameraRot, characterRot;
    Rigidbody _rb;
    //[SerializeField] float _jumpParameter = 1.0f;   //ジャンプのパラメータ
    [SerializeField] float Xsensityvity = 3f, Ysensityvity = 3f;    //視点の感度
    [SerializeField] float _speed = 0.1f;    //プレイヤーの移動速度
    //bool _isGround = true;  //着地判定
    bool _isPlayerMove = false; //プレイヤーの移動を制限する

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    private void Awake()
    {
        _pauseGame = FindAnyObjectByType<PauseGame>();
        _moneyTrade = FindAnyObjectByType<MoneyTrade>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cameraRot = _camera.transform.localRotation;
        characterRot = transform.localRotation;
    }

    private void OnEnable()
    {
        //デリゲート登録
        _pauseGame.OnPauseResume += PauseResume;
    }

    private void OnDisable()
    {
        //デリゲート解除
        _pauseGame.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {   //マウスカーソルの表示と非表示を切り替える  
        if (isPause)
        {
            _isPlayerMove = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            _isPlayerMove = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (!_isPlayerMove)
        {
            float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
            float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

            cameraRot *= Quaternion.Euler(-yRot, 0, 0);
            characterRot *= Quaternion.Euler(0, xRot, 0);

            //Updateの中で作成した関数を呼ぶ
            cameraRot = ClampRotation(cameraRot);

            _camera.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;

            //if (_isGround)
            //{
            //    if (Input.GetButtonDown("Jump"))    //プレイヤーのジャンプ処理
            //    {
            //        _rb.velocity = new Vector3(_rb.velocity.x, _jumpParameter, _rb.velocity.z);
            //        _isGround = false;
            //    }
            //}

            if (Input.GetMouseButtonDown(0))    //右クリックで弾を撃つ
            {
                GameObject ball = (GameObject)Instantiate(_bullets[_moneyTrade._consumeMoney[0]], _muzzle.transform.position, Quaternion.identity);
                Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
                Vector3 vector3 = _muzzle.transform.position - _camera.transform.position;
                ballRigidbody.AddForce(vector3 * 1000);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isPlayerMove)
        {
            //プレイヤーの移動処理
            _pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _pos = Camera.main.transform.TransformDirection(_pos);  //カメラからの角度で座標に変換する
            _pos.y = 0;
            float verticalVelocity = _rb.velocity.y;
            _rb.velocity = _pos * _speed + Vector3.up * verticalVelocity;
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
}