using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
	[System.Serializable]
	public struct CursorMapping
	{
		public CursorType type;
		public Texture2D texture;
		public Vector2 hotspot;
	}

	[SerializeField] private CursorMapping[] cursorMappings = null;

	private bool canInteractWithSeed = false;
	private bool canInteractWithHarvest = false;
	private GameObject currentTarget;

	private void Update()
	{
		if (Input.GetMouseButtonDown(1)) { ResetToggles(); }

		if (canInteractWithSeed && InteractWithSeed()) { return; }

		if (canInteractWithHarvest && InteractWithHarvest()) { return; }

		SetCursor(CursorType.None);
	}

	private void ResetToggles()
	{
		canInteractWithSeed = false;
		canInteractWithHarvest = false;
	}

	private bool InteractWithSeed()
	{
		GameObject target;
		RaycastHit hit;
		bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
		if (hasHit && hit.collider.gameObject.tag == "Field")
		{
			target = hit.collider.gameObject;
			currentTarget = target;
			HandleSeedInteraction();
			return true;
		}
		currentTarget = null;
		return false;
	}

	private bool InteractWithHarvest()
	{
		GameObject target;
		RaycastHit hit;
		bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
		if (hasHit && hit.collider.gameObject.tag == "Field")
		{
			target = hit.collider.gameObject;
			currentTarget = target;
			HandleHarvestInteraction();
			return true;
		}
		currentTarget = null;
		return false;
	}

	private void HandleSeedInteraction()
	{
		if (currentTarget.tag == "Field")
		{
			Field field = currentTarget.GetComponent<Field>();
			if (!field.hasCrops)
			{
				SetCursor(CursorType.PlantableField);
				if (Input.GetMouseButtonDown(0))
				{
					FindObjectOfType<MarketHandler>().OpenPanel(field);
					canInteractWithSeed = false;
				}
			}
			else
			{
				SetCursor(CursorType.NotInteractable);
			}
		}
	}

	private void HandleHarvestInteraction()
	{
		if (currentTarget.tag == "Field")
		{
			Field field = currentTarget.GetComponent<Field>();
			if (field.hasCrops)
			{
				SetCursor(CursorType.HarvestableField);
				if (Input.GetMouseButtonDown(0))
				{
					field.HarvestField();
					canInteractWithHarvest = false;
				}
			}
			else
			{
				SetCursor(CursorType.NotInteractable);
			}
		}
	}

	private static Ray GetMouseRay()
	{
		return Camera.main.ScreenPointToRay(Input.mousePosition);
	}

	private void SetCursor(CursorType type)
	{
		CursorMapping mapping = GetCursorMapping(type);
		Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
	}

	private CursorMapping GetCursorMapping(CursorType type)
	{
		foreach (CursorMapping mapping in cursorMappings)
		{
			if (mapping.type == type)
			{
				return mapping;
			}
		}
		return cursorMappings[0];
	}

	public void ToggleSeedInteraction(bool value)
	{
		canInteractWithSeed = value;
	}

	public void ToggleHarvestInteraction(bool value)
	{
		canInteractWithHarvest = value;
	}
}