using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldData))]
public class FieldDataEditor : Editor
{
	private FieldData targetObject;

	private void OnEnable()
	{
		targetObject = (FieldData)this.target;
	}

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.BeginVertical();

		targetObject.modelPrefab = (GameObject)EditorGUILayout.ObjectField("Model Prefab", targetObject.modelPrefab, typeof(Object), false);
		targetObject.areaType = (AreaType)EditorGUILayout.EnumPopup("Area Type", targetObject.areaType);

		if (targetObject.areaType == AreaType.Rectangle)
		{
			targetObject.widthLength = EditorGUILayout.IntField("Width", targetObject.widthLength);
			targetObject.rectangleLength = EditorGUILayout.IntField("Length", targetObject.rectangleLength);
		}

		if (targetObject.areaType == AreaType.Equilateral)
		{
			targetObject.sideLength = EditorGUILayout.IntField("Length", targetObject.sideLength);
		}

		if (targetObject.areaType == AreaType.Isosceles)
		{
			targetObject.heightLength = EditorGUILayout.IntField("Height", targetObject.heightLength);
			targetObject.baseLength = EditorGUILayout.IntField("Base", targetObject.baseLength);
		}

		EditorGUILayout.EndVertical();

		if (EditorGUI.EndChangeCheck())
		{
			EditorUtility.SetDirty(target);
		}
	}
}