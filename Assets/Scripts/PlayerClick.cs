using UnityEngine;

public class PlayerClick : MonoBehaviour
{
    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        _gameManager.OnPauseResume += PauseResume;
    }

    void PauseResume(bool pause)
    {
    }

    void Update()
    {
        if (GameManager.Instance._gameStop == false)
        {
            //Enemyタグのついたオブジェクトをクリックしたらオブジェクトを破棄してスコアを1増やす
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit2d)
                {
                    if (hit2d.collider.CompareTag("Enemy"))
                    {
                        EnemyController _enemyController;
                        _enemyController = hit2d.collider.gameObject.GetComponent<EnemyController>();
                        _enemyController.HpValueChange(1);
                    }
                }
            }   
        }
    }
}
