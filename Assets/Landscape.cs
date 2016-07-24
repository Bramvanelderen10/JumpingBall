using UnityEngine;
using System.Collections;

public class Landscape : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "env")
        {
            Destroy(this.gameObject);
        }
    }
}
