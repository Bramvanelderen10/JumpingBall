using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static Quaternion ToQuat(this Vector3 vector)
        {
            Quaternion quat = new Quaternion(0, 0, 0, 0);
            quat.eulerAngles = vector;

            return quat;
        }
    }
}
