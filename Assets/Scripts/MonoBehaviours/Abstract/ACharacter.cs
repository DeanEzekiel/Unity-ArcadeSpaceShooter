using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter : ACollidable
{
    protected abstract void Move();
    protected abstract void Rotate();
}
