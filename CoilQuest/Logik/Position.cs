 namespace CoilQuest.Logik
{
    public class Position
    {
        public int Row;
        public int Col;

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Col == position.Col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public Position Translate(Direction direction)
        {
            return new Position(Row + direction.RowOffSet, Col + direction.ColOffSet);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !(left == right);
        }
    }
}
