using UnityEngine;
using System.Collections;

[System.Serializable]
public class TriggerEventBase : EventBase
{
	[SerializeField]protected Collider _collider;	
	
	public Collider GetCollider
	{
		get { return _collider; }
	}  

    public TriggerEventBase(Collider collider)
    {   		
		_collider = collider;
    }	
	
	public TriggerEventBase()
	{
		_collider = null;
	}
}
