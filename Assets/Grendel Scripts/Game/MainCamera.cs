using UnityEngine;
using System.Collections;

public class MainCamera : Singleton<MainCamera>
{
	
	public float CameraUpperLimit = 180f;
	public float CameraLowerLimit = 10f;
	
	public Transform CamSetupFirstPerson;
	public Transform CamSetupThirdPerson;
	public Transform CamSetupTopdown;
	
	public bool MakeBlockingObjectsTransparent = true;
	public float BlockingObjectTransparencyAmount = 0.5f;

	private Transform[] _cameraSetups = new Transform[3];
	private Transform _currentCamera;
	private int _currentCameraIndex = 1;
	private Vector3 _initialRotation = Vector3.zero;
	
	private float _distanceToPlayer = 5.0f;
	
	//TODO: Cam Setups should be a datatype that defines special
	//cam parameters, such as locking cam 'look' in RTS mode
	//and hiding the player in FPS mode
	
	
	// Use this for initialization
	void Start () 
	{
		_cameraSetups = new Transform[] {CamSetupFirstPerson, CamSetupThirdPerson, CamSetupTopdown};
	//	_currentCamera = _cameraSetups[_currentCameraIndex];
		
		//_initialRotation.x = 90f;
		
		_distanceToPlayer = (Player.Instance.BaseTransform.position -  transform.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () 
	{ 
	   		
		if(!Console.Instance.ShowConsole) //TODO: Replace this with a paused game state
		{
			//Vector3 r = new Vector3(Input.GetAxis("Vertical") * UserInput.Instance.MouseSensitivityVertical, 0f, 0f); //vertical rotation  
	    	
			//_initialRotation += r;
			
//			if (_initialRotation.x > CameraUpperLimit)
//			{
//				_initialRotation.x = CameraUpperLimit;
//			}
//			else if (_initialRotation.x < CameraLowerLimit)
//			{
//				_initialRotation.x = CameraLowerLimit;
//			}
//			else
//			{			
//				Vector3 testRot = transform.rotation.eulerAngles + r;
//				transform.rotation = Quaternion.Euler(testRot);
//			}
		}
		
		if (MakeBlockingObjectsTransparent) { CheckIfViewBlocked(); }
	}
	
	void CheckIfViewBlocked()
	{
		
		Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
		Ray _ray = Camera.main.ViewportPointToRay(screenCenter);
		RaycastHit _rayHit = new RaycastHit();	
		
		LayerMask layerMask = 1 << LayerMask.NameToLayer("PlayerSearchLayer") | 1 << LayerMask.NameToLayer("ZombieSearchLayer") | 1 << LayerMask.NameToLayer("TriggerLayer");
		
		if(Physics.Raycast(_ray, out _rayHit, _distanceToPlayer, ~layerMask) && _rayHit.transform != Player.Instance.transform )
		{
			Renderer R = _rayHit.collider.renderer;            
            // TODO: maybe implement here a check for GOs that should not be affected like the player

            
			AutoTransparent AT = R.GetComponent<AutoTransparent>();
	        if (AT == null) // if no script is attached, attach one
	        {
	             AT = R.gameObject.AddComponent<AutoTransparent>();
	        }
	        AT.BeTransparent(); // get called every frame to reset the falloff
			
			
			
		}
		
		
	}
	
	public void CycleCameras()
	{
		_currentCameraIndex++;
		
		if(_currentCameraIndex > (_cameraSetups.Length - 1)) {_currentCameraIndex = 0;}
		
		_currentCamera = _cameraSetups[_currentCameraIndex];
		
		SwitchCamera(_currentCamera);		
	}
	
	public void SwitchCamera(Transform newSetup)
	{
		Console.Instance.OutputToConsole("Going to Camera Setup: " + _cameraSetups[_currentCameraIndex].name, Console.Instance.Style_Admin);
		transform.position = newSetup.position;		
	}
}//end class
