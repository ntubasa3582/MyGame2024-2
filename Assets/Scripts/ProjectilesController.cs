using UnityEngine;

public class ProjectilesController : MonoBehaviour
{
    [SerializeField]GameObject _player = default;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(0.0f, 90.0f, 1.0f);
        rb.AddForce(force, ForceMode.Impulse);
    }
    void Update()
    {
        
    }
}
