using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public Color defaultColor = Color.white;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	HexCell[] cells;
	HexCellPriorityQueue searchFrontier;
	Canvas gridCanvas;
	HexMesh hexMesh;
	HexCell currentPathFrom, currentPathTo;

	bool currentPathExists;
	int searchFrontierPhase;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	void Start () {
		hexMesh.Triangulate(cells);
	}

	public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width;
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
	}

	public void FindPath (HexCell fromCell, HexCell toCell) {
		ClearPath();
		currentPathFrom = fromCell;
		currentPathTo = toCell;
		currentPathExists = Search(fromCell, toCell);
		ShowPath();
	}

	bool Search (HexCell fromCell, HexCell toCell) {
		searchFrontierPhase += 2;
		if (searchFrontier == null) {
			searchFrontier = new HexCellPriorityQueue();
		}
		else {
			searchFrontier.Clear();
		}

		fromCell.SearchPhase = searchFrontierPhase;
		fromCell.Distance = 0;
		searchFrontier.Enqueue (fromCell);
		while (searchFrontier.Count > 0) {
			HexCell current = searchFrontier.Dequeue();
			current.SearchPhase += 1;

			if (current == toCell) {
				return true;
			}

			for (HexDirection d = HexDirection.NE; d <= HexDirection.S; d++) {
				HexCell neighbor = current.GetNeighbor(d);
				if (neighbor == null ||
					neighbor.SearchPhase > searchFrontierPhase) {
					continue;
				}
				int distance = current.Distance;
				distance += 10;
				if (neighbor.SearchPhase < searchFrontierPhase) {
					neighbor.SearchPhase = searchFrontierPhase;
					neighbor.Distance = distance;
					neighbor.PathFrom = current;
					neighbor.SearchHeuristic =
						neighbor.coordinates.DistanceTo (toCell.coordinates);
					searchFrontier.Enqueue (neighbor);
				} else if (distance < neighbor.Distance) {
					neighbor.Distance = distance;
					neighbor.PathFrom = current;
					int oldPriority = neighbor.SearchPriority;
					searchFrontier.Change (neighbor, oldPriority);
				}
			}
		}
		return true;
	}

	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = x * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.innerRadius * 2f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = new HexCoordinates(x, z);
		cell.color = defaultColor;

		if (x > 0) {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
		if (z > 0) {
			if (x < width - 1) {
				cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
			}
			cell.SetNeighbor(HexDirection.S, cells[i - width]);
			if (x > 0) {
				cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
			}
		}

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		cell.uiRect = label.rectTransform;
		
	}

	void ShowPath () {
		if (currentPathExists) {
			HexCell current = currentPathTo;
			while (current != currentPathFrom) {
				current.EnableHighlight(Color.white);
				current = current.PathFrom;
			}
		}
		currentPathFrom.EnableHighlight(Color.blue);
		currentPathTo.EnableHighlight(Color.red);
	}

	void ClearPath () {
		if (currentPathExists) {
			HexCell current = currentPathTo;
			while (current != currentPathFrom) {
				current.DisableDistanceLabel();
				current = current.PathFrom;
			}
			current.DisableDistanceLabel();
			currentPathExists = false;
		}
		currentPathFrom = currentPathTo = null;
	}

	public HexCell GetCell (Ray ray) {
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			return GetCell(hit.point);
		}
		return null;
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width;
		return cells[index];
	}

	public HexCell GetCell (HexCoordinates coordinates) {
		int z = coordinates.Z;
		if (z < 0 || z >= height) {
			return null;
		}
		int x = coordinates.X + z / 2;
		if (x < 0 || x >= width) {
			return null;
		}
		return cells[x + z * width];
	}
}