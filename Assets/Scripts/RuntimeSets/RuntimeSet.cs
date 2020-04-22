using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
	private List<T> items = new List<T>();

	public void Init()
	{
		items.Clear();
	}

	public T GetItemAtIndex(int index)
	{
		return items[index];
	}

	public void Add(T t)
	{
		if (!items.Contains(t)) items.Add(t);
	}
	public void Remove(T t)
	{
		if (items.Contains(t)) items.Remove(t);
	}

	public int Count()
	{
		return items.Count;
	}
}
