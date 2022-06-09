using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter : ACollidable
{
    protected virtual void Move() { }
    protected virtual void Rotate() { }
}
