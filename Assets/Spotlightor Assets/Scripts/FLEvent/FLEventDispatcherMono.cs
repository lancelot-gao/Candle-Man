using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FLEventDispatcherMono : MonoBehaviour, IFLEventDispatcher
{
	private FLEventDispatcher _eventDispatcher;

	private FLEventDispatcher eventDispatcher {
		get {
			if (_eventDispatcher == null)
				_eventDispatcher = new FLEventDispatcher ();
			return _eventDispatcher;
		}
	}

	public void DispatchEvent (FLEventBase e)
	{
		if (e.target != null) {
			// This event is dispatched from other target
			e = e.Clone ();
		}
		e.target = this;
		eventDispatcher.DispatchEvent (e);
	}
	
	public void AddEventListener (Enum eventType, FLEventBase.FLEventHandler handler)
	{
		eventDispatcher.AddEventListener (eventType, handler);
	}

	public void AddEventListener (string eventType, FLEventBase.FLEventHandler handler)
	{
		eventDispatcher.AddEventListener (eventType, handler);
	}
	
	public void RemoveEventListener (Enum eventType, FLEventBase.FLEventHandler handler)
	{
		eventDispatcher.RemoveEventListener (eventType, handler);
	}

	public void RemoveEventListener (string eventType, FLEventBase.FLEventHandler handler)
	{
		eventDispatcher.RemoveEventListener (eventType, handler);
	}
}

