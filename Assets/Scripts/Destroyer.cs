using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.GetComponent<PoolableObject>())
            {
                other.gameObject.GetComponent<PoolableObject>().Destroy();
            }
        }
    }
}
