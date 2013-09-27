using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour
{
	public GameObject prefab;
	public int initialPooledCount = 5;
	public int maxPooledCount = 0;
	public float pooledPercentToStartPruneCountDown = 0;
	public float pruneCountDownTime = 5;
	public float percentToPrune = 0.8f;
	public bool debug = false;
	private Stack<GameObject> pooledInstances;
	private List<GameObject> activeInstances;
	private int numInstantiateCalls = 0;
	private int numPoolPopCalls = 0;
	private int maxPooledCountInHistory = 0;

	public List<GameObject> ActiveInstances {
		get {
			return this.activeInstances;
		}
	}

	public bool IsFull {
		get {
			return maxPooledCount > 0 && PooledCount >= maxPooledCount;
		}
	}
	
	public int PooledCount {
		get {
			return pooledInstances != null ? pooledInstances.Count : 0;
		}
	}
	
	public void Initialize ()
	{
		ValidateSettings ();
		InitializePool ();
	}
	
	private void ValidateSettings ()
	{
		initialPooledCount = Mathf.Max (0, initialPooledCount);
		maxPooledCount = maxPooledCount > 0 ? Mathf.Max (maxPooledCount, initialPooledCount) : Mathf.Max (maxPooledCount, 0);
		pruneCountDownTime = Mathf.Max (0, pruneCountDownTime);
		percentToPrune = Mathf.Clamp01 (percentToPrune);
	}
	
	private void InitializePool ()
	{
		pooledInstances = new Stack<GameObject> ();
		activeInstances = new List<GameObject> ();
		if (initialPooledCount > 0) {
			for (int i = 0; i < initialPooledCount; i++) {
				GameObject instance = GameObject.Instantiate (prefab) as GameObject;
				ReturnInstance (instance);
			}
		}
	}

	public GameObject BorrowInstance ()
	{
		GameObject instance = null;
		if (PooledCount > 0) {
			instance = pooledInstances.Pop ();
			instance.SetActive (true);
			numPoolPopCalls ++;
		} else {
			instance = GameObject.Instantiate (prefab) as GameObject;
			numInstantiateCalls ++;
		}
		activeInstances.Add (instance);
		return instance;
	}
	
	public void ReturnInstance (GameObject instance)
	{
		if (!IsFull) {
			instance.SetActive (false);
			instance.transform.parent = transform;
			pooledInstances.Push (instance);
			activeInstances.Remove (instance);
			maxPooledCountInHistory = Mathf.Max (maxPooledCountInHistory, PooledCount);
		} else {
			Debug.LogWarning (string.Format ("GameObjectPool[{0}] is full({1}), no more instance can be pooled. You can increase the maxPooledCount or set it to 0.", gameObject.name, maxPooledCount));
		}
	}
	
	void OnGUI ()
	{
		if (debug) {
			string stats = string.Format (@"Instantiate calls:{0}
Pop calls:{1}
Pooled count:{2}/{3}", numInstantiateCalls, numPoolPopCalls, PooledCount, activeInstances.Count + PooledCount);
			GUI.Box (new Rect (300, 100, 300, 150), stats);
		}
	}
}
