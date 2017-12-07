using UnityEngine;

[System.Serializable]
public struct HexCoordinates {

	[SerializeField]
	private int x, z;

	public int X {
		get {
			return x;
		}
	}

	public int Z {
		get {
			return z;
		}
	}

	public HexCoordinates (int x, int z) {
		this.x = x;
		this.z = z;
	}

	public static HexCoordinates FromOffsetCoordinates (int x, int z) {
		return new HexCoordinates(x - z / 2, z);
	}

	public int DistanceTo (HexCoordinates other) {
		int iX = x < other.x ? other.x - x : x - other.x;
		int iZ = z < other.z ? other.z - z : z - other.z;

		// Vertical and Horizontal
		if (iX == 0 || iZ == 0) {
			return iX + iZ;
		// Diagonal
		} else if ((iX - iZ) == 0) {
			return
				(iX + iZ) / 2;
		// Everything else
		} else {
			return (iX > iZ ? iX : iZ);
		}
	}

	public static HexCoordinates FromPosition (Vector3 position) {
		float x = position.x / (HexMetrics.innerRadius * 2f);
		float z = position.z / (HexMetrics.innerRadius * 2f);
		float y = -x;

//		float offset = position.z / (HexMetrics.outerRadius * 3f);
//		x -= offset;
//		y -= offset;


		int iX = Mathf.RoundToInt(x);
		//int iZ = Mathf.CeilToInt(z);
		int iY = Mathf.RoundToInt(y);
		//int iZ = Mathf.RoundToInt(-x -y);
		int iZ = Mathf.RoundToInt(z);

//		if (iX + iY + iZ != 0) {
//			float dX = Mathf.Abs(x - iX);
//			float dY = Mathf.Abs(y - iY);
//			float dZ = Mathf.Abs(-x -y - iZ);
//
//			if (dX > dY && dX > dZ) {
//				iX = -iY - iZ;
//			}
//			else if (dZ > dY) {
//				iZ = -iX - iY;
//			}
//		}

		return new HexCoordinates(iX, iZ);
	}

	public override string ToString () {
		return "(" +
			X.ToString() + ", " + Z.ToString() + ")";
	}

	public string ToStringOnSeparateLines () {
		return X.ToString() + "\n" + "\n" + Z.ToString();
	}
}