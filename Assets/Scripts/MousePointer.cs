using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
	public new Camera camera;

	RaycastHit hit;
	Ray ray;

	public GameObject selectedBuilding;
	GameObject tmpObject;

	public GameObject currentSpawnObject;

	public GameObject FormulasToolkitPanel;
	public bool formulasToolkitPanelIsActive;

	public GameObject BuildingsPanel;
	public bool buildingsPanelIsActive;

	public GameObject SeedsPanel;
	public bool seedsPanelIsActve;

	// Start is called before the first frame update
	void Start()
	{
		FormulasToolkitPanel.SetActive(false);
		formulasToolkitPanelIsActive = false;

		BuildingsPanel.SetActive(false);
		buildingsPanelIsActive = false;

		SeedsPanel.SetActive(false);
		seedsPanelIsActve = false;
	}

	// Update is called once per frame
	void Update()
	{
		ray = camera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			Debug.DrawRay(ray.origin, ray.direction * 2000, Color.green, 3000, false);
		}

		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Left button clicked first time");
			Debug.Log("selectedBuilding is " + selectedBuilding);
			//			Debug.Log("Moving LEFT: mousePositionX =" + Input.mousePosition.x);
			if (selectedBuilding != null)
			{
				HandleBuildingCreation();
			}
		}
		else if (Input.GetMouseButtonDown(1))
		{
			Debug.Log("Right button clicked");
			Destroy(selectedBuilding);

			// Had to add this after debugging. Without this step on Right-click, the Select methods are confused and don't reassign their objects.
			currentSpawnObject = null;
		}
		else
		{
			// This is the HOVER function... 
			if (selectedBuilding != null)
			{
				// This is a place to explore maybe a Baugarten equation coming into contact with a space
				Vector3 nearestPoint = GameObject.Find("Terrain").GetComponent<TerrainGenerator>().nearestGridPoint(hit.point);
				selectedBuilding.transform.SetPositionAndRotation(new Vector3(nearestPoint.x, 1, nearestPoint.z), selectedBuilding.transform.rotation);
				//			    Debug.Log("In hover mode");

			}
		}
	}

	// BUG: Farm Stays Green after built
	// BUG: Field is not Green before being built
	private void HandleBuildingCreation()
	{
		if (EconomyManager.Instance.totalMoney >= 100)
		{
			tmpObject = Instantiate(selectedBuilding);
			tmpObject.transform.position = hit.point;

			Vector3 nearestPoint = GameObject.Find("Terrain").GetComponent<TerrainGenerator>().nearestGridPoint(hit.point);
			tmpObject.transform.SetPositionAndRotation(new Vector3(nearestPoint.x, 1, nearestPoint.z), tmpObject.transform.rotation);
			tmpObject.GetComponent<MeshRenderer>().material.color = Color.white;

			// Reenables the collider on the field so that it's "plantable"
			if (tmpObject.GetComponent<BoxCollider>() != null)
			{
				tmpObject.GetComponent<BoxCollider>().enabled = true;
			}

			EconomyManager.Instance.totalMoney -= 100;
			Debug.Log("Left button clicked second time");
			selectedBuilding = null;
		}
	}

	public void SelectHouse()
	{
		Debug.Log("currentSpawnObject before ANYTHING else, just on click = " + currentSpawnObject);
		Debug.Log("selectedBuilding before ANYTHING else, just on click = " + selectedBuilding);

		if (selectedBuilding != null)
		{
			Destroy(selectedBuilding);
		}

		if (currentSpawnObject == null || currentSpawnObject.name != "House")
		{

			// Also here this is a place to explore maybe a Baugarten equation coming into contact with a space

			currentSpawnObject = GameObject.Find("House");
			Debug.Log("currentSpawnObject after finding the house = " + currentSpawnObject);
			selectedBuilding = Instantiate(currentSpawnObject);
			Debug.Log("selectedBuilding after instantiating the currentSpawnObject = " + selectedBuilding);
			// selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;
			selectedBuilding.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
		}
	}

	public void SelectClearLandHoe()
	{
		Debug.Log("currentSpawnObject before ANYTHING else, just on click = " + currentSpawnObject);
		Debug.Log("selectedBuilding before ANYTHING else, just on click = " + selectedBuilding);

		if (selectedBuilding != null)
		{
			Destroy(selectedBuilding);
		}

		if (currentSpawnObject == null || currentSpawnObject.name != "GardenRows_Ground")
		{

			// Also here this is a place to explore maybe a Baugarten equation coming into contact with a space

			currentSpawnObject = GameObject.Find("Field");
			Debug.Log("currentSpawnObject after finding the house = " + currentSpawnObject);
			selectedBuilding = Instantiate(currentSpawnObject);
			Debug.Log("selectedBuilding after instantiating the currentSpawnObject = " + selectedBuilding);
			// selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;
			selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;
			selectedBuilding.GetComponent<BoxCollider>().enabled = false;
		}
	}

	public void SelectEquilateralTriangleTool()
	{
		if (currentSpawnObject == null || currentSpawnObject.name != "EquilateralTriangleTool")
		{
			if (selectedBuilding != null)
			{
				Destroy(selectedBuilding);
			}
			// Also here this is a place to explore maybe a Baugarten equation coming into contact with a space

			currentSpawnObject = GameObject.Find("EquilateralTriangleTool");
			Debug.Log(currentSpawnObject);
			selectedBuilding = Instantiate(currentSpawnObject);
			Debug.Log(selectedBuilding);
			selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;

		}
	}

	public void SelectIsoscelesTriangleTool()
	{
		if (currentSpawnObject == null || currentSpawnObject.name != "IsoscelesTriangleTool")
		{
			if (selectedBuilding != null)
			{
				Destroy(selectedBuilding);
			}
			// Also here this is a place to explore maybe a Baugarten equation coming into contact with a space

			currentSpawnObject = GameObject.Find("IsoscelesTriangleTool");
			Debug.Log(currentSpawnObject);
			selectedBuilding = Instantiate(currentSpawnObject);
			Debug.Log(selectedBuilding);
			selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;

		}
	}

	public void SelectRectangleTool()
	{
		if (currentSpawnObject == null || currentSpawnObject.name != "RectangleTool")
		{
			if (selectedBuilding != null)
			{
				Destroy(selectedBuilding);
			}
			// Also here this is a place to explore maybe a Baugarten equation coming into contact with a space

			currentSpawnObject = GameObject.Find("RectangleTool");
			Debug.Log(currentSpawnObject);
			selectedBuilding = Instantiate(currentSpawnObject);
			Debug.Log(selectedBuilding);
			selectedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;

		}
	}

	public void SelectFormulasToolkitPanel()
	{
		if (formulasToolkitPanelIsActive == true)
		{
			FormulasToolkitPanel.SetActive(false);
			formulasToolkitPanelIsActive = false;
		}

		else if (formulasToolkitPanelIsActive == false)
		{
			FormulasToolkitPanel.SetActive(true);
			formulasToolkitPanelIsActive = true;

			BuildingsPanel.SetActive(false);
			buildingsPanelIsActive = false;

			SeedsPanel.SetActive(false);
			seedsPanelIsActve = false;
		}

	}

	public void SelectBuildingsPanel()
	{
		if (buildingsPanelIsActive == true)
		{
			BuildingsPanel.SetActive(false);
			buildingsPanelIsActive = false;
		}

		else if (buildingsPanelIsActive == false)
		{
			BuildingsPanel.SetActive(true);
			buildingsPanelIsActive = true;

			FormulasToolkitPanel.SetActive(false);
			formulasToolkitPanelIsActive = false;

			SeedsPanel.SetActive(false);
			seedsPanelIsActve = false;
		}
	}

	public void SelectSeedsPanel()
	{
		if (seedsPanelIsActve == true)
		{
			SeedsPanel.SetActive(false);
			seedsPanelIsActve = false;
		}

		else if (seedsPanelIsActve == false)
		{
			SeedsPanel.SetActive(true);
			seedsPanelIsActve = true;

			FormulasToolkitPanel.SetActive(false);
			formulasToolkitPanelIsActive = false;

			BuildingsPanel.SetActive(false);
			buildingsPanelIsActive = false;
		}
	}
}



// Summary of the process of these Methods (From Debugging Earlier)
//1. When I left-click on the House button, it triggers the "SelectHouse" method of the assigned script.
//2. currentSpawnObject is null. (I haven't yet sent it to find the other thing).
//3. selectedBuilding is null. (I haven't yet instantiated it based on the currentSpawnObject Finding of the House in my in-game assets).
//4. There is this mysterious 4-line expression (maybe check in Skillshare for it) that deletes the selectedBuilding if it's there. It's not, so it proceeds.
//5. The method then checks whether currentSpawnObject is null or if it exists but has a different name than House.
//6. It doesn't (there is no currentSpawnObject yet, so it is null). So the method Finds the existing asset house, assigns it as selectedBuilding, and makes it green.
//7. Meanwhile, the Update method is constantly running, checking for If (GetMouseButtonDown(0)). Because I left-clicked once on the House Button, it already fires. 
//8. The update method then checks to make sure that selectedBuilding does not equal "null." It checks this, because it will be using the selectedBuilding (turning it into TmpObject) to compare with the Terrain location and SetPositionAndRotation to the 2nd left-click location.
//9. The update method on the 2nd click places the house on the terrain at the x,y,z  coordinates.
//10. The update method is also constantly checking for a right-click. If there is a right-click, then the selectedBuilding is destroyed.
//11. And NOW, thanks to my Debugging!!, I also added to clear out the SpawnObject variable.
