using UnityEngine;
using System.Collections;

public class TrainCarEvent : EventBase
{
	public TrainCar Car;
	
	protected TrainCarEvent(TrainCar car)
	{
		Car = car;
	}
	
	protected TrainCarEvent() : this(null)
	{
	}
}
