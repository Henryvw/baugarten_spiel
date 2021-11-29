using UnityEngine;

public static class AreaFormulas
{
	public static int GetEquilateralArea(int sideLength)
	{
		int area = (int)Mathf.Round((Mathf.Sqrt(3) / 4) * sideLength * sideLength);
		return area;
	}

	public static int GetIsoscelesArea(int heightLength, int baseLength)
	{
		int area = (int)Mathf.Round((heightLength * baseLength) / 2);
		return area;
	}

	public static int GetRectangleArea(int widthLength, int rectangleLength)
	{
		int area = (int)Mathf.Round(widthLength * rectangleLength);
		return area;
	}
}