using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
	private static EconomyManager _instance;

	[SerializeField]
		private int totalWater;
		private int totalElectricity;
		private int totalMoney;

	public static EconomyManager Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject obj = new GameObject("EconomyManager");
				obj.AddComponent<EconomyManager>();
			}
			return _instance;
		}

	}

	private void Awake()
	{
		_instance = this;
	}
}
