using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
	private bool isOnTarget = false;
	private GameObject currentTarget;

	private void Update()
	{
		if (isOnTarget = IsHittingSomething())
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnFire();
			}
		}
	}

	private void OnFire()
	{
		if (!isOnTarget) { return; }

		if (currentTarget.tag == "Field")
		{
			currentTarget.GetComponent<Field>().PlantField();
		}
	}

	private bool IsHittingSomething()
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

	private static Ray GetMouseRay()
	{
		return Camera.main.ScreenPointToRay(Input.mousePosition);
	}
}