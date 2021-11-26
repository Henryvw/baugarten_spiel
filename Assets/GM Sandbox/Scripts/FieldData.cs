using UnityEngine;

[CreateAssetMenu(fileName = "FieldData", menuName = "baugarten_spiel/FieldData", order = 0)]
public class FieldData : ScriptableObject
{
	public GameObject modelPrefab = default;
	public AreaType areaType = default;
	public int sideLength = 1;
	public int heightLength = 1;
	public int baseLength = 1;
	public int widthLength = 1;
	public int rectangleLength = 1;
}