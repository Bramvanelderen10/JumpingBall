using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float _force = 2f;
    public float _maxVelocity = 10f;
    public float _jumpVelocity = 2f;

    private Rigidbody _rb;
    private Player _player;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddForce(new Vector3(-_force, 0, 0), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddForce(new Vector3(_force, 0, 0), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(new Vector3(0, 0, (_force / 1.0f)), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(new Vector3(0, 0, -(_force / 1.0f)), ForceMode.Force);
        }

        Quaternion rotation = Rail.Instance.GetRailRotation(transform.position);
        print(rotation.eulerAngles);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_player.CanJump())
            {
                Vector3 vel = _rb.velocity;
                vel.y = _jumpVelocity;
                _rb.velocity = vel;
                _player.Jump();
            }            
        }

        if (_rb.velocity.x > _maxVelocity)
        {
            Vector3 velocity = _rb.velocity;
            velocity.x = _maxVelocity;
            _rb.velocity = velocity;
        }

        if (_rb.velocity.z > _maxVelocity)
        {
            Vector3 velocity = _rb.velocity;
            velocity.z = _maxVelocity;
            _rb.velocity = velocity;
        }

        if (_rb.velocity.x < -_maxVelocity)
        {
            Vector3 velocity = _rb.velocity;
            velocity.x = -_maxVelocity;
            _rb.velocity = velocity;
        }

        if (_rb.velocity.z < -_maxVelocity)
        {
            Vector3 velocity = _rb.velocity;
            velocity.z = -_maxVelocity;
            _rb.velocity = velocity;
        }
    }
}
