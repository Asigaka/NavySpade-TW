using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IActiveChanger 
{
    public UnityEvent OnActiveChange { get; }
}
