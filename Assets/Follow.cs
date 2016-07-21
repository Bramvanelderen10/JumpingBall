﻿using UnityEngine;
using System.Collections;
using ExtensionMethods;

public class Follow : MonoBehaviour {

    public Vector3 _offset = new Vector3(0f, 2.2f, -3.2f);
    public Vector3 _rotation = new Vector3(12f, 0f, 0f);
    public float _dampTime = 0.15f;

    public Transform _target;
        
    private Vector3 _velocity = Vector3.zero;
    private Camera _camera;

    void Start () {
        _camera = GetComponent<Camera>();

        transform.position = _target.position + _offset;
        transform.rotation = _rotation.ToQuat();

    }

	void Update () {
        FollowTarget();
	}

    void FollowTarget()
    {
        transform.position -= _offset;
        Vector3 point = _camera.WorldToViewportPoint(_target.position);
        Vector3 delta = _target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _dampTime) + _offset;
        transform.rotation = _rotation.ToQuat();
    }
}
