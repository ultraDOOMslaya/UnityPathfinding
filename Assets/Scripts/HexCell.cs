using UnityEngine;
using UnityEngine.UI;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

	public RectTransform uiRect;

	public Color color;

	public HexCell NextWithSamePriority { get; set; }

	//Visualizing the path to a destination.
	public HexCell PathFrom { get; set; }

	//Smarter searching.
	public int SearchHeuristic { get; set; }

	public int SearchPhase { get; set; }

	public HexUnit Unit { get; set; }

	int distance;

	[SerializeField]
	HexCell[] neighbors;

	public void UpdateDistanceLabel () {
		Text label = uiRect.GetComponent<Text>();
		label.text = distance == int.MaxValue ? "" : distance.ToString();
	}

	public void DisableDistanceLabel () {
		Text label = uiRect.GetComponent<Text>();
		label.text = "";
	}

	public Vector3 Position {
		get {
			return transform.localPosition;
		}
	}

	public int Distance {
		get {
			return distance;
		}
		set {
			distance = value;
			// For full frontier label searching.
			//UpdateDistanceLabel();
		}
	}

	public int SearchPriority {
		get {
			return distance + SearchHeuristic;
		}
	}

	public HexCell GetNeighbor (HexDirection direction) {
		return neighbors[(int)direction];
	}

	public void SetNeighbor (HexDirection direction, HexCell cell) {
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}

	public void DisableHighlight () {
		Text label = uiRect.GetComponent<Text>();
		label.color = Color.black;
	}

	public void EnableHighlight (Color color) {
		Text label = uiRect.GetComponent<Text>();
		label.text = distance == int.MaxValue ? "" : distance.ToString();
		label.color = color;
	}
}