using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectPool<T> where T:Object
{
	private T _prefab;
	private List<T> _pool;
	protected List<T> Pool {
		get {
			if (_pool == null)
				_pool = new List<T> ();
			return _pool;
		}
	}
	
	public ObjectPool(T prefab)
	{
		_prefab = prefab;
	}
	
	public void DisposeObject (T objToDispose)
	{
		Pool.Add(objToDispose);
	}
	
	public void DisposeObjectAndDeactivate(T objToDispose)
	{
		DisposeObject(objToDispose);
		(objToDispose as Component).gameObject.SetActive(false);
	}

	public T GetInstance ()
	{
		T instance;
		if(Pool.Count > 0){
			instance = Pool[0];
			Pool.RemoveAt(0);
		}else {
			instance = GameObject.Instantiate(_prefab) as T;
		}
		return instance;
	}
	
	public T GetInstanceAndActivate()
	{
		T instance = GetInstance();
		(instance as Component).gameObject.SetActive(true);
		return instance;
	}
}

