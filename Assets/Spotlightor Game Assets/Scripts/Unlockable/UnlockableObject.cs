using UnityEngine;
using System.Collections;

public abstract class UnlocakableObject : ScriptableObject
{
	public virtual string Identifier {
		get { return name;}
	}

	public abstract string Title {
		get;
	}

	protected abstract string DescriptionLocked {
		get;
	}

	protected abstract string DescriptionUnlocked {
		get;
	}

	public string Description {
		get {
			if (IsUnlocked)
				return DescriptionUnlocked;
			else
				return DescriptionLocked;
		}
	}
	
	public bool IsUnlocked {
		get {
			return BasicDataTypeStorage.GetInt (Identifier) > 0;
		}
	}
	
	public virtual void Unlock ()
	{
		BasicDataTypeStorage.SetInt (Identifier, 1);
	}
	
	public virtual void Lock ()
	{
		BasicDataTypeStorage.SetInt (Identifier, 0);
	}
}
