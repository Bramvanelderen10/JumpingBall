using UnityEngine;
using System.Collections;

public class RailMover : MonoBehaviour {

    public Transform _target;
    public Transform _lookAt;

    public bool _smoothMovement = true;

    private Transform _transform;
    private Vector3 lastPosition;

	void Start () {
        _transform = transform;
        lastPosition = _transform.position;
	}

	void Update () {
        if (_smoothMovement)
        {
            lastPosition = Vector3.Lerp(lastPosition, Rail.Instance.ProjectPositionOnRail(_target.position), Time.deltaTime);
            _transform.position = lastPosition;
        } else
        {
            _transform.position = Rail.Instance.ProjectPositionOnRail(_target.position);
        }        
        _transform.LookAt(_lookAt.position);
	}
}
