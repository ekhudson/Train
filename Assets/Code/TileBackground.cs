using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TileBackground
{
	[SerializeField]
	public List<Transform> Tiles = new List<Transform>();
	
	public float ScrollSpeed = 1f;
	
	private float _tileWidth;
	
	
	// Use this for initialization
	void Start () 
	{
		_tileWidth = Tiles[0].renderer.bounds.size.x;
	}
	
	// Update is called once per frame
//	void Update () 
//	{
//	
//	}
}
