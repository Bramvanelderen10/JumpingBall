using UnityEngine;
using System.Collections;
using ExtensionMethods;

public class Follow : MonoBehaviour {

    public bool _delayed = true;

    public Vector3 _offset = new Vector3(0f, 2.2f, -3.2f);
    public Vector3 _rotation = new Vector3(12f, 0f, 0f);
    public float _dampTime = 0.15f;

    public Transform _target;
        
    private Vector3 _velocity = Vector3.zero;
    private Camera _camera;

    void Start () {
        _camera = GetComponent<Camera>();

        transform.localPosition = _target.position + _offset;
        transform.localRotation = _rotation.ToQuat();

    }

	void Update () {
        FollowTarget();
	}

    void FollowTarget()
    {
        if (_delayed)
        {
            transform.localPosition -= _offset;
            Vector3 point = _camera.WorldToViewportPoint(_target.position);
            Vector3 delta = _target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
            Vector3 destination = transform.localPosition + delta;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref _velocity, _dampTime) + _offset;
        } else
        {
            transform.position = _target.position + _offset;
        }
        
        transform.localRotation = _rotation.ToQuat();
    }
}
