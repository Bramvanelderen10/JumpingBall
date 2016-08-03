using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player Instance;

    public string _envTag;
    public int _maxJumps = 2;

    public float _force = 20f;
    public float _gravityMultiplier = 20f;

    [HideInInspector]
    public bool _grounded = false;
    private int _jumpCount = 0;
    private Rigidbody _rb;

    private float _bonusForce = 0f;

    private float _lastYPosition;

	void Awake()
    {
        Instance = this;
    }

	void Start () {
        _rb = GetComponent<Rigidbody>();
	}

    void Update()
    {
        //if (_grounded)
        //{
        //    _lastYPosition = transform.position.y;
        //} else
        //{
        //    if (transform.position.y < (_lastYPosition - 5f))
        //    {
        //        this.gameObject.SetActive(false);
        //    }
        //}
    }
    
    void FixedUpdate()
    {
        if (_grounded)
        {
            _rb.AddForce((Physics.gravity * _gravityMultiplier) * _rb.mass);
        }
        else
        {
            _rb.AddForce((Physics.gravity * (_gravityMultiplier / 4f) * _rb.mass));
        }

        Quaternion rotation = Rail.Instance.GetRailRotation(transform.position);
        Vector3 vel = _rb.velocity;
        vel.z = (rotation * new Vector3(0, 0, _force + _bonusForce)).z;
        vel.x = (rotation * new Vector3(0, 0, _force + _bonusForce)).x;
        _rb.velocity = vel;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _envTag)
        {
            _grounded = true;
            _jumpCount = 0;
        }
    }
    
    public bool CanJump()
    {
        return (_jumpCount < _maxJumps);
    }
    
    public void Jump()
    {
        if (_jumpCount < _maxJumps)
        {
            _grounded = false;
            _jumpCount++;
        }
    }

    public void EditBonusForce(float force)
    {
        _bonusForce = force;
    }
    
        
}
