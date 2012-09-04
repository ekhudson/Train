using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour 
{
	[SerializeField]
	public List<TileBackground> Backgrounds = new List<TileBackground>();
	
	private Vector3 _currentScrollSpeed;
	private TileBackground _currentBackground;
	private Transform _currentTile;

	// Use this for initialization
	void Start () 
	{
		if (Backgrounds.Count <= 0)
		{
			Console.Instance.OutputToConsole("BackgroundManager: Found no Backgrounds", Console.Instance.Style_Admin);
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{		
		for(int i = 0; i < Backgrounds.Count; i++)
		{
			_currentBackground = Backgrounds[i];
			_currentScrollSpeed = new Vector3(_currentBackground.ScrollSpeed * Time.deltaTime, 0 ,0);
			for(int j = 0; j < _currentBackground.Tiles.Count; j++)
			{
				_currentTile = _currentBackground.Tiles[j];
				_currentTile.position -= (_currentScrollSpeed * GameManager.Instance.GlobalSpeed);
				
				if(!_currentTile.renderer.isVisible && _currentTile.position.x < Camera.main.transform.position.x)
				{
					_currentTile.position += new Vector3(_currentTile.renderer.bounds.size.x * _currentBackground.Tiles.Count, 0, 0);
				}
				
			}			
		}		
	}
}
