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

	[SerializeField] CursorMapping[] cursorMappings = null;

	private GameObject currentTarget;

	private void Update()
	{
		if (InteractWithField()) { return; }

		SetCursor(CursorType.None);
	}

	private bool InteractWithField()
	{
		GameObject target;
		RaycastHit hit;
		bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
		if (hasHit && hit.collider.gameObject.tag == "Field")
		{
			target = hit.collider.gameObject;
			currentTarget = target;
			HandleInteractionType();
			return true;
		}
		currentTarget = null;
		return false;
	}

	private void HandleInteractionType()
	{
		if (currentTarget.tag == "Field")
		{
			Field field = currentTarget.GetComponent<Field>();
			if (!field.hasCrops)
			{
				SetCursor(CursorType.PlantableField);
				if (Input.GetMouseButtonDown(0))
				{
					FindObjectOfType<MarketHandler>().StartSeedPurchaseForField(field);
				}
			}
			else
			{
				SetCursor(CursorType.NotPlantable);
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
}