using UnityEngine;
using System.Collections;

public class TrainCarPassed : TrainCarEvent
{
	public TrainCarPassed(TrainCar car)
	{
		base.Car = car;
	}
}
