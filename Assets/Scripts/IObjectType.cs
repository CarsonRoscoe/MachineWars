using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum ObjectType { Solid, Dangerous, Enemy }

public abstract class IObjectType : MonoBehaviour {
    public ObjectType Type;
}
