using UnityEngine;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
	[System.Serializable]
	public class FieldPreset
	{
		public GameObject modelPrefab;
		public AreaType areaType;
		private int sideLengthHeight;
		private int baseWidth;

		public void Init()
		{
			sideLengthHeight = GetRandom1to32();
			baseWidth = GetRandom1to32();
		}

		private int GetRandom1to32()
		{
			return Random.Range(1, 32);
		}

		public int GetSideLengthHeight()
		{
			return sideLengthHeight;
		}

		public int GetBaseWidth()
		{
			return baseWidth;
		}

		public int GetArea()
		{
			switch (areaType)
			{
				case AreaType.Equilateral:
					return AreaFormulas.GetEquilateralArea(sideLengthHeight);
				case AreaType.Isosceles:
					return AreaFormulas.GetIsoscelesArea(sideLengthHeight, baseWidth);
				case AreaType.Rectangle:
					return AreaFormulas.GetRectangleArea(sideLengthHeight, baseWidth);
				default:
					Debug.LogError("Area type not supported.");
					return 0;
			}
		}
	}

	[SerializeField] private FieldPreset[] presets = default;
	[SerializeField] private GameObject testPlant = default;

	[Range(1f, 60f)]
	[Tooltip("in seconds")]
	[SerializeField] private float timeToGrow = 10f;

	[HideInInspector]
	public bool hasCrops = false;
	[HideInInspector]
	public bool cropsFullyGrown = false;
	private float playbackTime = 0f;
	private int numberOfCrops = 0;
	private int cropPrice = 1;

	private List<GameObject> currentCrops = new List<GameObject>();
	private FieldPreset selectedPreset;
	private GameObject selectedPlantPrefab;
	private int maxCrops;

	private void Start()
	{
		DestroyModel();
		CreateNewModel();
	}

	private void Update()
	{
		if (hasCrops)
		{
			GrowCropsOverTime();
		}
	}

	private void GrowCropsOverTime()
	{
		playbackTime += Time.deltaTime;
		foreach (GameObject crop in currentCrops)
		{
			if (crop.GetComponent<Animator>() != null)
			{
				crop.GetComponent<Animator>().SetFloat("Grow", playbackTime / timeToGrow);
			}
		}

		if (playbackTime >= timeToGrow)
		{
			cropsFullyGrown = true;
		}
	}

	private void DestroyModel()
	{
		if (gameObject.transform.Find("Field Model") != null)
		{
			Destroy(gameObject.transform.Find("Field Model").gameObject);
		}
	}

	private void CreateNewModel()
	{
		GameObject model = Instantiate(GetRandomFieldPrefab(), transform.position, transform.rotation) as GameObject;
		model.transform.parent = transform;
		model.name = "Field Model";
	}

	private GameObject GetRandomFieldPrefab()
	{
		int randomIndex = Random.Range(0, presets.Length);
		selectedPreset = presets[randomIndex];
		selectedPreset.Init();
		GameObject selectedPrefab = selectedPreset.modelPrefab;
		return selectedPrefab;
	}

	// In building script: detect if Raycast is colliding with object tagged "field", if yes, on click call PlantField()
	// currently called by Player Cursor as a separate click detection
	public void PlantField(int seedCount, int newCropPrice)
	{
		if (hasCrops)
		{
			Debug.LogWarning("Crops already planted on this field.");
			return;
		}

		cropPrice = newCropPrice;
		maxCrops = selectedPreset.GetArea();

		if (seedCount > maxCrops)
		{
			int wastedSeedsCount = seedCount - maxCrops;

			FindObjectOfType<PopUpHandler>().CreateNewPopUp($"You wasted {wastedSeedsCount} seeds.");

			numberOfCrops = maxCrops;
		}
		else
		{
			numberOfCrops = seedCount;
		}

		CreateCrops();
	}

	public void HarvestField()
	{
		if (!cropsFullyGrown)
		{
			Debug.LogWarning("Crops are not ready to be harvested.");
			return;
		}

		FindObjectOfType<PopUpHandler>().CreateNewPopUp($"You harvested {numberOfCrops} crops!");

		EconomyManager.Instance.totalMoney += cropPrice * numberOfCrops;

		DestroyCurrentCrops();

		playbackTime = 0f;
		numberOfCrops = 0;
		cropsFullyGrown = false;
		hasCrops = false;
	}

	private void DestroyCurrentCrops()
	{
		foreach (GameObject crop in currentCrops)
		{
			Destroy(crop);
		}
		currentCrops.Clear();
	}

	private void CreateCrops()
	{
		hasCrops = true;

		SeedPoint[] seedPoints = GetComponentsInChildren<SeedPoint>();
		selectedPlantPrefab = GetPlantPrefab();

		int maxPoints = seedPoints.Length;
		int numberOfPoints = (numberOfCrops * maxPoints) / maxCrops;
		int coefficient = maxPoints / numberOfPoints;
		int pointIndex = 0;

		while (pointIndex < maxPoints)
		{
			CreateCropAtPoint(seedPoints[pointIndex]);
			pointIndex += coefficient;
		}
	}

	private void CreateCropAtPoint(SeedPoint seed)
	{
		GameObject newPlant = Instantiate(selectedPlantPrefab, seed.transform.position, seed.transform.rotation) as GameObject;
		newPlant.transform.parent = seed.transform;
		currentCrops.Add(newPlant);
	}

	private GameObject GetPlantPrefab()
	{
		return testPlant;
	}

	public FieldPreset GetSelectedPreset()
	{
		return selectedPreset;
	}
}
