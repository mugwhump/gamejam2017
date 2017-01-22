public enum Direction {

	UP,
    RIGHT,
    DOWN,
    LEFT,
    NONE
}

public static class Dir {
    public static Direction getClockwise(Direction dir) {
        switch (dir) {
            case Direction.UP: return Direction.RIGHT;
            case Direction.RIGHT: return Direction.DOWN;
            case Direction.DOWN: return Direction.LEFT;
            case Direction.LEFT: return Direction.UP;
            case Direction.NONE:
            default:
                return Direction.NONE;
        }
    }

    public static Direction getCounterClockwise(Direction dir) {
        return getClockwise(getClockwise(getClockwise(dir)));
    }

    public static UnityEngine.Vector2 toVec(Direction dir) {
        switch (dir) {
            case Direction.UP: return new UnityEngine.Vector2(0, 1);
            case Direction.RIGHT: return new UnityEngine.Vector2(1, 0);
            case Direction.DOWN: return new UnityEngine.Vector2(0, -1);
            case Direction.LEFT: return new UnityEngine.Vector2(-1, 0);
            case Direction.NONE:
            default:
                return new UnityEngine.Vector2(0, 0);
        }
    }
}