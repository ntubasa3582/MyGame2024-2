using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterControllerMoveMethod _moveMethod = CharacterControllerMoveMethod.Move;
    [SerializeField] float _speed = 3f;
    CharacterController _controller = default;
    void Start()
    {
        _controller = GetComponent<CharacterController> ();
    }

    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        switch (_moveMethod)
        {
            case CharacterControllerMoveMethod.Move:
                _controller.Move(dir.normalized * _speed * Time.deltaTime);

                //if (_controller.isGrounded)
                //{
                //    Debug.Log("接地しています");
                //}
                //else
                //{
                //    Debug.Log("接地していません");
                //}

                break;
            case CharacterControllerMoveMethod.SimpleMove:
                _controller.SimpleMove(dir.normalized * _speed);
                break;
            default:
                break;
        }
    }

    enum CharacterControllerMoveMethod
    {
        /// <summary>Move メソッドを使う</summary>
        Move,
        /// <summary>SimpleMove メソッドを使う</summary>
        SimpleMove,
    }
}
