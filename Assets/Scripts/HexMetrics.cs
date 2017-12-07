using UnityEngine;

public static class HexMetrics {

	public const float innerRadius = 5f;

	public const float outerRadius = innerRadius * 1.41421356237f;

	public static Vector3[] corners = {
		new Vector3(innerRadius, 0f, innerRadius),
		new Vector3(innerRadius, 0f, -innerRadius),
		new Vector3(-innerRadius, 0f, -innerRadius),
		new Vector3(-innerRadius, 0f, innerRadius),
		new Vector3(innerRadius, 0f, innerRadius)
	};
}