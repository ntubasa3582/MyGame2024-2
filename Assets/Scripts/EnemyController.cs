using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyGenerator _enemyGenerator;
    [SerializeField] int _hp;

    public void HpValueChange(int value)
    {
        _hp -= value;
    }

    void Update()
    {
        //hpが０になったら_enemyGeneratorのspawnCountを-1してスコアを１プラスして自身を破棄する
        if (_hp <= 0)
        {
            _enemyGenerator = GameObject.FindObjectOfType<EnemyGenerator>();
            _enemyGenerator.ChangeSpawnCount(-1);
            ScoreManager.Instance.ScoreValueChange(1);
            UiManager.Instance.ScoreTextUpdate();
            Destroy(gameObject);
        }
    }
}
