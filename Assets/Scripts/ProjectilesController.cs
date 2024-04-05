using UnityEngine;

public class ProjectilesController : MonoBehaviour
{
    [SerializeField] float _lifeTime = 5;
    ClearSystem _scoreManager;
    float _timer = 0;
    void Start()
    {
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
        if (other.gameObject.tag == "Enemy")    //敵に触れたらスコアを1増やしてデストロイする
        {
            ThisDestroy();
        }
    }
    void ThisDestroy()
    {
        Destroy(gameObject);
    }
}
