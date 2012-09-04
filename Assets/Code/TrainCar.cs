using UnityEngine;
using System.Collections;

public class TrainCar : BaseObject
{
	
	public Color CarColor = Color.white;
	public int CarNumber = 0;
	
	private float _swayAmt;
	private int _swayDirection;
	private int _swayTime;
	
	private bool _passEventFired = false;
	
	// Use this for initialization
	void Start () 
	{
		_swayAmt = Random.Range(1.0f, 1.5f);
		_swayDirection = 1;
				
		//StartCoroutine("ChangeSwayDirection");
		//StartCoroutine("CarSway");
		
	}
	
//	// Update is called once per frame
	void Update () 
	{
		if (_transform.position.x + (_renderer.bounds.size.x * 0.5f) < Player.Instance.BaseTransform.position.x && !_passEventFired)
		{
			_passEventFired = true;
			EventManager.Instance.Post(this, new TrainCarPassed(this));
		}
		
	}
	
//	IEnumerable CarSway()
//	{
//		while(true)
//		{
//			_transform.position += new Vector3(0, _transform.position.y + ((_swayAmt * _swayDirection) * Time.deltaTime), 0);
//		}
//		
//	}
//	
//	IEnumerable ChangeSwayDirection()
//	{
//		while(true)
//		{
//			yield return new WaitForSeconds(Random.Range(1f,3f));
//			_swayDirection *= -1;
//		}
//	}
	
	public void OnCollisionEnter(Collision collision)
	{		
		if (collision.collider.tag == "Player")
		{			
			EventManager.Instance.Post(this, new TrainCarTouched(this));
		}
	}
	
	public void OnCollisionStay(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			EventManager.Instance.Post(this, new TrainCarTouched(this));
		}
	}
	
	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			
		}
	}	
}
