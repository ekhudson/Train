using UnityEngine;
using System.Collections;

public class TrainCarTouched : TrainCarEvent
{
	public TrainCarTouched(TrainCar car)
	{
		base.Car = car;
	}
}
