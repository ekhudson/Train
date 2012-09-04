using UnityEngine;
using System.Collections;

public class TrainManager : Singleton<TrainManager> 
{
	public int NumberOfCars = 50;
	public int CarSpacing = 5;
	public GameObject FirstCar;
	public float TrainSpeed = 3f;
	
	public Material[] TrainMaterials = new Material[0];
	
	//private Vector3 _currentTrainSpeed;
	
	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < NumberOfCars; i++)
		{
			GameObject newCar = (GameObject)GameObject.Instantiate(FirstCar,
		                           new Vector3( 5 + FirstCar.transform.position.x + (CarSpacing * i), FirstCar.transform.position.y, FirstCar.transform.position.z), Quaternion.identity); 
			newCar.transform.parent = transform;
			newCar.name += "_" + i.ToString();
			//newCar.GetComponent<TrainCar>().CarNumber = i;
			newCar.GetComponent<TrainCar>().CarNumber = i;
			newCar.renderer.sharedMaterial = TrainMaterials[Random.Range(0, TrainMaterials.Length)];
		}
		
		//_currentTrainSpeed = new Vector3(TrainSpeed * Time.deltaTime, 0,0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//_currentTrainSpeed = new Vector3(TrainSpeed * Time.deltaTime, 0,0);;
		transform.position -= new Vector3( (TrainSpeed * GameManager.Instance.GlobalSpeed) * Time.deltaTime, 0, 0);
				
	}
	
	
}
