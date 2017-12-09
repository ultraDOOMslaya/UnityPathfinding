using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HexUnit : MonoBehaviour {

	List<HexCell> pathToTravel;

	const float travelSpeed = 4f;

	public HexCell Location {
		get {
			return location;
		}
		set {
			if (location) {
				location.Unit = null;
			}
			location = value;
			value.Unit = this;
			transform.localPosition = value.Position;
		}
	}

	HexCell location;

	public float Orientation {
		get {
			return orientation;
		}
		set {
			orientation = value;
			transform.localRotation = Quaternion.Euler(0f, value, 0f);
		}
	}

	float orientation;


	// This may need a more deterministic solution.
	IEnumerator TravelPath () {
		for (int i = 1; i < pathToTravel.Count; i++) {
			Vector3 a = pathToTravel[i - 1].Position;
			Vector3 b = pathToTravel[i].Position;
			for (float t = 0f; t < 1f; t += Time.deltaTime * travelSpeed) {
				transform.localPosition = Vector3.Lerp(a, b, t);
				yield return null;
			}
		}
	}

	public void Travel (List<HexCell> path) {
		Location = path[path.Count - 1];
		pathToTravel = path;
		StopAllCoroutines();
		StartCoroutine(TravelPath());
	}

	public void ValidateLocation () {
		transform.localPosition = location.Position;
	}

	public bool IsValidDestination (HexCell cell) {
		return !cell.Unit;
	}

	public void Die () {
		location.Unit = null;
		Destroy(gameObject);
	}
}
