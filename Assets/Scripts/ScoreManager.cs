using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int _score { get; private set; } = 0;
    void Start()
    {
        
    }

    public void AddScore(int _addValue)
    {
        _score += _addValue;
        Debug.Log("“G‚ğ" + _score + "‘Ì“|‚µ‚½I");
    }
}
