using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            Destroy(other.gameObject);
    }
}
