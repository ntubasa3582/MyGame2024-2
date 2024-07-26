using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField,Header("エネミーのスポーン上限")] int _enemySpawnLimit;      //エネミーの上限
    [SerializeField] float _spawnInterval;      //エネミー生成までの間隔
    int _randomNumStorage;
    int _spawnCount;        //生成数をカウントする変数
    float _timer;
    void Start()
    {
        _timer = 3;
    }

    void Update()
    {
        if (GameManager.Instance._gameStop == false)
        {
            //一定時間おきにエネミーを生成する
            _timer += Time.deltaTime;
            if (_spawnInterval < _timer)
            {
                if (_spawnCount < _enemySpawnLimit)
                {
                    _timer = 0;
                    Debug.Log("生成");
                    RandomPositionEnemyInstance();   
                }
            }
        }
    }

    void RandomPositionEnemyInstance() //ランダムな座標にエネミーを生成する
    {
        
        int randomPosNum = Random.Range(0, 4);
        if (randomPosNum != _randomNumStorage)      //前回の値が今回の値と違うなら生成する
        {
            _randomNumStorage = randomPosNum;
            Debug.Log(randomPosNum);
            switch (randomPosNum)
            {
                case 0: //左上
                    ObjectInstance(-8f, -0.3f, 0.3f, 4f);
                    break;
                case 1: //右上
                    ObjectInstance(0.3f, 8f, 0.3f, 4f);
                    break;
                case 2: //左下
                    ObjectInstance(-8f, -0.3f, -4f, -0.3f);
                    break;
                case 3: //右下
                    ObjectInstance(0.3f, 8f, -4f, -0.3f);
                    break;
            }   
        }
        //同じならメソッドを再帰呼び出し
        else
        {
            RandomPositionEnemyInstance();
        }
    }
    void ObjectInstance(float xMin,float xMax,float yMin, float yMax)
    {
        var randomPosX = Random.Range(xMin, xMax);
        var randomPosY = Random.Range(yMin, yMax);
        Instantiate(_enemyPrefab, new Vector3(randomPosX, randomPosY, 0), Quaternion.identity);
        ChangeSpawnCount(1);
    }

    public void ChangeSpawnCount(int value)
    {
        _spawnCount += value;
        Debug.Log("現在の敵の数は" + _spawnCount);
    }
}