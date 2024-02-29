using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    CharacterController _controller = default;
    Animator _animator;
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //キー入力の値を変数に入れる
        PlayerMove(pos);
        if (Input.GetKey("w")|| Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            _animator.Play("Walk");
        }
        else
        {
            _animator.SetBool("IsWalk", true);
            _animator.Play("Idle");
            _animator.SetBool("IsWalk", false);
        }
    }

    void PlayerMove(Vector3 dir)
    {
        dir = Camera.main.transform.TransformDirection(dir);
        dir = dir.normalized * _speed * Time.deltaTime;
        dir.y = 0;
        _controller.Move(dir);
        var position = transform.position + dir;
        //if (_controller.isGrounded)
        //{
        //    Debug.Log("接地しています");
        //}
        //else
        //{
        //    Debug.Log("接地していません");
        //}
        transform.LookAt(position);
    }
}
