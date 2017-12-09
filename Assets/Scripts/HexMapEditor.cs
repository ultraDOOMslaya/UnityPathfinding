using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;

	public HexGrid hexGrid;

	public HexUnit unitPrefab;

	private Color activeColor;

	//HexCell searchFromCell, searchToCell;

	void Awake () {
		SelectColor(0);
	}

	void Update () {
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (Input.GetMouseButton(0)) {
				HandleInput();
				return;
			}
			if (Input.GetKeyDown(KeyCode.U)) {
				if (Input.GetKey(KeyCode.LeftShift)) {
					DestroyUnit();
				}
				else {
					CreateUnit();
				}
			}
		}
		//previousCell = null;
	}

	// Throw this method call in HandleInput to get it working for debugging
	void TouchCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		//Debug.Log("touched at " + coordinates.ToString());
	}

	void HandleInput () {
		HexCell currentCell = GetCellUnderCursor();
//		if (Input.GetKey (KeyCode.LeftShift) && searchToCell != currentCell) {
//			if (searchFromCell != currentCell) {
//				if (searchFromCell) {
//					searchFromCell.DisableHighlight ();
//				}
//				searchFromCell = currentCell;
//				currentCell.UpdateDistanceLabel ();
//				searchFromCell.EnableHighlight (Color.blue);
//				if (searchToCell) {
//					hexGrid.FindPath (searchFromCell, searchToCell);
//				}
//			}
//		} else if (searchFromCell && searchFromCell != currentCell) {
//			if (searchFromCell != currentCell) {
//				searchToCell = currentCell;
//				searchToCell.UpdateDistanceLabel ();
//				hexGrid.FindPath (searchFromCell, searchToCell);
//			}
//		}
	}

	HexCell GetCellUnderCursor () {
		return
			hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
	}

	void CreateUnit () {
		HexCell cell = GetCellUnderCursor();
		if (cell && !cell.Unit) {
			hexGrid.AddUnit(
				Instantiate(unitPrefab), cell
			);
		}
	}

	void DestroyUnit () {
		HexCell cell = GetCellUnderCursor();
		hexGrid.RemoveUnit(cell);
	}

	public void SelectColor (int index) {
		activeColor = colors[index];
	}
}