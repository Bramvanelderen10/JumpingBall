using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    public Transform _raycastTarget;
    public float _minNegativeVelocity = -3f;

    private Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!GetComponent<Player>()._grounded && _rb.velocity.y < _minNegativeVelocity)
        {
            if (Physics.Linecast(transform.position, _raycastTarget.position))
            {
                Vector3 velocity = _rb.velocity;
                velocity.y = _minNegativeVelocity;
                _rb.velocity = velocity;
            }
        }        
	}
}
