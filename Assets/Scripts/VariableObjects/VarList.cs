using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarList<T> : ScriptableObject
{
    public List<T> contents;
    
    public void Add(T obj)
    {
        contents.Add(obj);
    }

    public T Get(int i)
    {
        if (contents.Count>i)
        {
            return contents[i];
        }
        return default;
    }

    public List<T> GetContents()
    {
        List<T> newList = new List<T>();
        foreach (T item in contents)
        {
            newList.Add(item);
        }
        return newList;
    }

    public int Count()
    {
        return contents.Count;
    }

    public void Clear()
    {
        contents.Clear();
    }
}
