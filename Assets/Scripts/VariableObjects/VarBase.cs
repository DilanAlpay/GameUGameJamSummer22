using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarBase<T> : Variable
{
    public T value;

    public T GetValue()
    {
        return value;
    }

    public void SetValue(T obj)
    {
        value = obj;
    }

    public virtual void Copy(VarBase<T> variable)
    {
        value = variable.value;
    }

    public virtual void Change(T obj)
    {
    }

    public override void Clear()
    {
        value = default;
    }
    public override string ToString()
    {
        return value.ToString();
    }
}
