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
	private bool canInteractWithFormula = false;
	private GameObject currentTarget;

	private void Update()
	{
		if (Input.GetMouseButtonDown(1)) { ResetToggles(); }

		bool isActive = canInteractWithSeed || canInteractWithHarvest || canInteractWithFormula;

		if (!isActive) { SetCursor(CursorType.None); return; }

		if (InteractWithObject())
		{
			HandleFieldInteraction();
		}
	}

	public void ResetToggles()
	{
		canInteractWithSeed = false;
		canInteractWithHarvest = false;
		canInteractWithFormula = false;
	}

	private bool InteractWithObject()
	{
		GameObject target;
		RaycastHit hit;
		bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
		if (hasHit)
		{
			target = hit.collider.gameObject;
			currentTarget = target;
			return true;
		}
		currentTarget = null;
		return false;
	}

	private void HandleFieldInteraction()
	{
		if (currentTarget.gameObject.tag != "Field")
		{
			SetCursor(CursorType.NotInteractable);
			return;
		}

		if (canInteractWithSeed && InteractWithSeed()) { return; }

		if (canInteractWithHarvest && InteractWithHarvest()) { return; }

		if (canInteractWithFormula && InteractWithFormula()) { return; }
	}

	private bool InteractWithSeed()
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
			return true;
		}
		return false;
	}

	private bool InteractWithHarvest()
	{
		Field field = currentTarget.GetComponent<Field>();
		if (field.hasCrops && field.cropsFullyGrown)
		{
			SetCursor(CursorType.HarvestableField);

			if (Input.GetMouseButtonDown(0))
			{
				field.HarvestField();
				canInteractWithHarvest = false;
			}
			return true;
		}
		return false;
	}

	private bool InteractWithFormula()
	{
		Field field = currentTarget.GetComponent<Field>();

		SetCursor(CursorType.FormulaField);

		if (Input.GetMouseButtonDown(0))
		{
			FindObjectOfType<FormulaHandler>().OpenPanel(field);
			canInteractWithFormula = false;
		}
		return true;
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

	public void ToggleFormulaInteraction(bool value)
	{
		canInteractWithFormula = value;
	}
}