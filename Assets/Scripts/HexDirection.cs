public enum HexDirection {
	NE, E, SE, N, SW, W, NW, S
}

public static class HexDirectionExtensions {

	public static HexDirection Opposite (this HexDirection direction) {
		return (int)direction < 4 ? (direction + 4) : (direction - 4);
	}

	public static HexDirection Previous (this HexDirection direction) {
		return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
	}

	public static HexDirection Next (this HexDirection direction) {
		return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
	}

	public static HexDirection Previous2 (this HexDirection direction) {
		direction -= 2;
		return direction >= HexDirection.NE ? direction : (direction + 8);
	}

	public static HexDirection Next2 (this HexDirection direction) {
		direction += 2;
		return direction <= HexDirection.NW ? direction : (direction - 8);
	}
}