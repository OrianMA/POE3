using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public abstract void OnEnter(object[] entity);
    public abstract void UpdateState();
    public abstract void OnHurt();
    public abstract void OnExit();
}