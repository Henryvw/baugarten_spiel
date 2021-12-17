using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	[SerializeField] private int money;
	[SerializeField] private float startTime;
	[SerializeField] private float interval;

	public void Start()
	{
		startTime = 0.0f;
		EconomyManager.Instance.totalMoney = 400;
	}

	public void FixedUpdate()
	{
		startTime += Time.fixedDeltaTime;

		if(startTime > interval)
		{
			// Adding like this will constantly increase the amount of water with time
			//EconomyManager.Instance.totalWater += water;
//			Debug.Log("Total Water:" + EconomyManager.Instance.totalWater);

			// Adding like this will constantly increase the amount of electricity via time
			//EconomyManager.Instance.totalElectricity += electricity;
//			Debug.Log("Total Electricity:" + EconomyManager.Instance.totalWater);

			// Adding like this will constantly increase the amount of money via time
			//EconomyManager.Instance.totalMoney += money;
//			Debug.Log("Total Money:" + EconomyManager.Instance.totalMoney);
//			Debug.Log("Total Money:" + EconomyManager.Instance.totalMoney);

			startTime = 0.0f;
		}

	}

	// void AddWater()
	// {
//		EconomyManager.Instance.totalWater = 100;
//	}

}
