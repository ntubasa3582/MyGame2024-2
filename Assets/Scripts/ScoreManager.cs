using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance => _instance;
    static ScoreManager _instance;
    public int Score => _score;
    int _score = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<ScoreManager>();
            _instance = this;
        }
    }

    /// <summary>_scoreの値を変える変数</summary>
    public void ScoreValueChange(int value)
    {
        _score += value; //値を変える
    }
}
