using System;
using UnityEngine;
public interface IInputManager 
{
     bool Move { get; }
     int Rotation { get; }
     bool Fire { get; }
}
