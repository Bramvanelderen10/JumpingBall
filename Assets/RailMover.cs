using UnityEngine;
using System.Collections;

public class RailMover : MonoBehaviour {

    public Transform _target;
    public Transform _lookAt;

    private Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        _transform.position = Rail.Instance.ProjectPositionOnRail(_target.position);
        _transform.LookAt(_lookAt.position);
	}
}
