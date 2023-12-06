namespace CoilQuest.Logik
{
    public class Direction
    {
        public int RowOffSet { get; }
        public int ColOffSet { get; }

        public readonly static Direction Left = new(0, -1);
        public readonly static Direction Right = new(0, 1);
        public readonly static Direction Up = new(-1, 0);
        public readonly static Direction Down = new(1, 0);

        private Direction(int rowOffSet, int colOffSet)
        {
            RowOffSet = rowOffSet;
            ColOffSet = colOffSet;
        }

        public Direction Opposite() { return new Direction(-RowOffSet, -ColOffSet); }

        public override bool Equals(object? obj)
        {
            return obj is Direction direction &&
                   RowOffSet == direction.RowOffSet &&
                   ColOffSet == direction.ColOffSet;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffSet, ColOffSet);
        }

        public static bool operator ==(Direction? left, Direction? right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction? left, Direction? right)
        {
            return !(left == right);
        }
    }
}
