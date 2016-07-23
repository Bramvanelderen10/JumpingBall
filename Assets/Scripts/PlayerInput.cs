using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public float _force = 2f;
    public float _gravityMultiplier = 2f;

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

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(new Vector3(0, 0, -(_force / 1.0f)), ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_player.CanJump())
            {
                _rb.AddForce(new Vector3(0, _force, 0), ForceMode.Impulse);
                _player.Jump();
            }            
        }
    }

    void FixedUpdate()
    {
        _rb.AddForce((Physics.gravity * _gravityMultiplier) * _rb.mass);        
    }
}
