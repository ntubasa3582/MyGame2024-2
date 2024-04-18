using UnityEngine;

public class BulletController : MonoBehaviour
{
    PauseGame _pauseGame;
    Rigidbody _rigidbody;
    MoneyCounter _moneyCounter;
    Vector3 _angularVelocity;
    Vector3 _velocity;
    [SerializeField] float _lifeTime = 5;
    public float _enemyDamage = 0;
    float _timer = 0;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pauseGame = FindAnyObjectByType<PauseGame>();
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
    {

        if (isPause)
        {
            _angularVelocity = _rigidbody.angularVelocity;
            _velocity = _rigidbody.velocity;
            _rigidbody.Sleep();
        }
        else
        {
            _rigidbody.WakeUp();
            _rigidbody.angularVelocity = _angularVelocity;
            _rigidbody.velocity = _velocity;
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;   //一定時間したらデストロイする
        if (_timer > _lifeTime)
        {
            ThisDestroy();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")    //敵に触れたらHPを減らしてデストロイする
        {
            ThisDestroy();
        }
    }
    void ThisDestroy()
    {
        Destroy(gameObject);
    }
}
