using UnityEngine;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
	[SerializeField] private int cropPrice = 10;
	[SerializeField] private GameObject testPlant = default;
	[SerializeField] private GameObject[] modelPrefabs = default;

	[Range(1f, 60f)]
	[Tooltip("in seconds")]
	[SerializeField] private float timeToGrow = 10f;

	[HideInInspector]
	public bool hasCrops = false;
	private float playbackTime = 0f;
	private int numberOfCrops = 0;

	private List<GameObject> currentCrops = new List<GameObject>();

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
		int randomIndex = Random.Range(0, modelPrefabs.Length);
		GameObject selectedPrefab = modelPrefabs[randomIndex];
		return selectedPrefab;
	}

	// In building script: detect if Raycast is colliding with object tagged "field", if yes, on click call PlantField()
	// currently called by Player Cursor as a separate click detection
	public void PlantField(int seedCount)
	{
		if (hasCrops)
		{
			Debug.Log(gameObject.name + " already has crops growing on it!");
			return;
		}

		Debug.Log(gameObject.name + " has been planted with " + seedCount + " seeds.");
		numberOfCrops = seedCount;

		CreateCrops();
	}

	public void HarvestField()
	{
		Debug.Log(gameObject.name + " has been harvested, yielding " + numberOfCrops + " crops!");
		EconomyManager.Instance.totalMoney += cropPrice * numberOfCrops;

		DestroyCurrentCrops();

		playbackTime = 0f;
		numberOfCrops = 0;
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
		GameObject selectedPlantPrefab = GetPlantPrefab();

		foreach (SeedPoint seed in seedPoints)
		{
			GameObject newPlant = Instantiate(selectedPlantPrefab, seed.transform.position, seed.transform.rotation) as GameObject;
			newPlant.transform.parent = seed.transform;
			currentCrops.Add(newPlant);
		}
	}

	private GameObject GetPlantPrefab()
	{
		return testPlant;
	}
}
