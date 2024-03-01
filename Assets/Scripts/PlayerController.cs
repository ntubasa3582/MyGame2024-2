using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    [SerializeField] float _jumpParameter = 1f;
    Rigidbody _rb;
    Vector3 _pos;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //ƒL[“ü—Í‚Ì’l‚ð•Ï”‚É“ü‚ê‚é
        if (_pos.magnitude > 0 )
        {
            _pos = Camera.main.transform.TransformDirection(_pos);
            _pos.y = 0;
            transform.LookAt(transform.position + _pos);

            float verticalVelocity = _rb.velocity.y;
            _rb.velocity = _pos.normalized * _speed + Vector3.up * verticalVelocity;
        }
        if (Input.GetButtonDown("Jump"))
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpParameter, _rb.velocity.z);
        }
        Cursor.visible = false;
    }
}
