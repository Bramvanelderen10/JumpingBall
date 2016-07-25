using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string _envTag;
    public int _maxJumps = 2;

    [HideInInspector]
    public bool _grounded = false;
    private int _jumpCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
