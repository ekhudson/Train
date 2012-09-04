using UnityEngine;
using System.Collections;

	/// <summary>
	/// Title: Grendel Engine
	/// Author: Elliot Hudson
	/// Date: Feb 15, 2012
	/// 
	/// Filename: GameManager.cs
	/// 
	/// Summary: Essentially holds information that might need to be
	/// accessed by a variety of objects in the scene, and contains
	/// global references to useful scripts (ie. UserInput, EntityManager)
	///  
	/// </summary>

public class GameManager : Singleton<GameManager>
{
	#region PUBLIC VARIABLES
	public string ApplicationTitle = "Grendel";
	public string ApplicationVersion = "1.0";
	public bool DebugBuild = true;
	public float GlobalSpeed = 1f;
	public float GlobalSpeedIncrement = 0.01f;	
	#endregion		
	
	protected TrainCar _currentTouchedCar;
	protected int _numberOfPassedCars = 0;
	
	protected override void Awake()
	{		
		base.Awake();		
	}

	// Use this for initialization
	void Start ()
	{				
		Console.Instance.OutputToConsole(string.Format("Starting up {0} {1}", ApplicationTitle, ApplicationVersion), Console.Instance.Style_Admin);
		ConnectionRegistry.Instance.BuildConnections();
		EventManager.Instance.AddHandler<TrainCarTouched>(AssignTouchedCar);
		EventManager.Instance.AddHandler<TrainCarPassed>(CarPassHandler);
	}
	
	void Update ()
	{
		GlobalSpeed += (GlobalSpeedIncrement * Time.deltaTime);
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			Player.Instance.ResetPlayer();
		}
		
		
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0f,0f, Screen.width, 32f), GUI.skin.box);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(string.Format("{0} {1}", GameManager.Instance.ApplicationTitle, GameManager.Instance.ApplicationVersion));
		GUILayout.FlexibleSpace();
		GUILayout.Label(string.Format("Passed: {0}", _numberOfPassedCars), GUILayout.Width(128));
		GUILayout.FlexibleSpace();
		GUILayout.Label(Time.timeSinceLevelLoad.ToString(), GUILayout.Width(128));
		GUILayout.EndArea();
		
		GUILayout.EndHorizontal();
		
		if (_currentTouchedCar != null)
		{
			Vector3 pos = Camera.main.WorldToScreenPoint(_currentTouchedCar.transform.position);
			GUI.Label(new Rect(pos.x, pos.y, 128, 64), "TOUCHED", GUI.skin.box);		
			_currentTouchedCar = null;
		}
	}
	
	public void AssignTouchedCar(object sender, TrainCarTouched evt)
	{		
		_currentTouchedCar = evt.Car;
	}	
	
	public void CarPassHandler(object sender, TrainCarPassed evt)
	{
		_numberOfPassedCars++;
	}
}
