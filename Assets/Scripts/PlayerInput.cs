﻿using UnityEngine;
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
        Quaternion rotation = Rail.Instance.GetRailRotation(transform.position);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddForce((rotation * new Vector3(-_force, 0, 0)), ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddForce((rotation * new Vector3(_force, 0, 0)), ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _player.EditBonusForce(3f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _player.EditBonusForce(-3f);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _player.EditBonusForce(0f);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _player.EditBonusForce(0f);
        }


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
