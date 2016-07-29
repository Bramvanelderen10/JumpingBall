using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public class PoolableObject : MonoBehaviour
{
    public void Destroy()
    {
        this.gameObject.SetActive(false);
    }
}

