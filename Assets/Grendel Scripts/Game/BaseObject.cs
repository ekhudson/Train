using UnityEngine;
using System.Collections;

	/// <summary>
	/// Title: Grendel Engine
	/// Author: Elliot Hudson
	/// Date: Jan 10, 2012
	/// 
	/// Filename: BaseObject.cs
	/// 
	/// Summary: Extends MonoBehavior and provides
	/// usual functionality for game design (such as easy hiding/disabling of objects)
	/// as well as storing reference to commonly used components (ie. transform/gameobject).
	/// Primarily, the BaseObject class is concerned with the technical functions
	/// of the base Monobehavior scripts, providing direct access and control
	/// to those scripts. Things such as Health and Damage, however, are handled in the
	/// BaseActor class which extends BaseObject. 
	/// 
	/// </summary>

public class BaseObject : MonoBehaviour {
	
	//PUBLIC VARIABLES
	public bool DebugMode = false;
	public bool GameObjectActiveOnStart = true;
	public bool RigidBodyAwakeOnStart = false;
	public int UpdatesPerSecond = 30;
	
	//PROTECTED REFERENCES
	protected Transform _transform;
	protected GameObject _gameObject;
	protected Renderer _renderer;
	protected Collider _collider;
	protected Rigidbody _rigidbody;
	protected int _instanceID;
	
	//PROTECTED VARIABLES
	protected bool _isHidden = false;
	protected EditorObject _lastActivator = null;
	protected Collider _lastCollider = null;
	
	//PRIVATE VARIABLES
	private float _updateInterval;
		
	//ACCESSORS
	public Transform BaseTransform
	{
		get { return _transform; }
		set { _transform = value; }
	}
	
	public GameObject BaseGameObject
	{
		get { return _gameObject; }
		set { _gameObject = value; }
	}
	
	public Renderer BaseRenderer
	{
		get { return _renderer; }
		set { _renderer = value; }
	}
	
	public Collider BaseCollider
	{
		get { return _collider; }
		set { _collider = value; }
	}
	
	public Rigidbody BaseRigidbody
	{
		get { return _rigidbody; }
		set { _rigidbody = value; }
	}
	
	public int BaseInstanceID
	{
		get { return _instanceID; }
		set { _instanceID = value; }
	}
	
	public EditorObject LastActivator
	{
		get { return _lastActivator; }
		set { _lastActivator = value; }
	}
	
	public Collider LastCollider
	{
		get { return _lastCollider; }
		set { _lastCollider = value; }
	}
	
	public bool IsHidden
	{
		get { return _isHidden; }
		set { _isHidden = value; }
	}
	
	virtual public void ToggleScript()
	{		
		if (this.enabled) { this.enabled = false; }
		else {this.enabled = true; }		
	}
	
	virtual protected void Awake ()
	{
		//grab component references
		_transform = transform;
		_gameObject = gameObject;
		_renderer = renderer;
		_collider = collider;
		_rigidbody = rigidbody;
		_instanceID = gameObject.GetInstanceID();
		
		//calculate the update interval (assuming ideal target of 60 fps)
		_updateInterval = UpdatesPerSecond / 60f;
		
		//check conditions
		if (_rigidbody != null && !RigidBodyAwakeOnStart) { _rigidbody.Sleep(); }
		if (!GameObjectActiveOnStart) { _gameObject.active = false; }
				
		//start coroutines
		if (_renderer != null) { StartCoroutine( CheckIsHidden() ); } 
	}
	
	// Use this for initialization
	virtual protected void Start () 
	{
	
	}
		
	IEnumerator CheckIsHidden()
	{		
		while(true)
		{
			if (_isHidden && _renderer.enabled) { _renderer.enabled = false; }
			else if (!_isHidden && !_renderer.enabled) { _renderer.enabled = true; }
			yield return new WaitForSeconds(_updateInterval);
		}		
	}}
