using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;

	public HexGrid hexGrid;

	private Color activeColor;

	HexCell searchFromCell, searchToCell;

	void Awake () {
		SelectColor(0);
	}

	void Update () {
		if (
			Input.GetMouseButton(0) &&
			!EventSystem.current.IsPointerOverGameObject()
		) {
			HandleInput();
		}
	}

	// Throw this method call in HandleInput to get it working for debugging
	void TouchCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		//Debug.Log("touched at " + coordinates.ToString());
	}

	HexCell GetCellUnderCursor () {
		return
			hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
	}

	void HandleInput () {
		HexCell currentCell = GetCellUnderCursor();
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetKey (KeyCode.LeftShift) && searchToCell != currentCell) {
			if (searchFromCell != currentCell) {
				if (searchFromCell) {
					searchFromCell.DisableHighlight ();
				}
				searchFromCell = currentCell;
				currentCell.UpdateDistanceLabel ();
				searchFromCell.EnableHighlight (Color.blue);
				if (searchToCell) {
					hexGrid.FindPath (searchFromCell, searchToCell);
				}
			}
		} 
//		else if (Physics.Raycast(inputRay, out hit)) {
//			//hexGrid.ColorCell(hit.point, activeColor);
//			//Debug.Log("x is: " + currentCell.coordinates.X + " and y is: " + currentCell.coordinates.Z);
//			hexGrid.FindPath(currentCell);
//		}
		else if (searchFromCell && searchFromCell != currentCell) {
			if (searchFromCell != currentCell) {
				searchToCell = currentCell;
				searchToCell.UpdateDistanceLabel ();
				hexGrid.FindPath (searchFromCell, searchToCell);
			}
		}
	}

	public void SelectColor (int index) {
		activeColor = colors[index];
	}
}