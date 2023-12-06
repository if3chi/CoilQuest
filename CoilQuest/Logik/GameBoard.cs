using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoilQuest.Logik
{
    public class GameBoard
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction? Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Position> _snakePositions = new();
        private readonly Random _random = new();

        public GameBoard(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            SpawnSnake();
            SpawnFood();
        }

        public Position HeadPosition() { return _snakePositions.First.Value; }
        public Position TailPosition() { return _snakePositions.Last.Value; }
        public IEnumerable<Position> SnakePosition() { return _snakePositions; }

        public void Move()
        {
            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Snake || hit == GridValue.Outside)
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                SpawnHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                SpawnHead(newHeadPos);
                Score++;
                SpawnFood();
            }

        }

        public void ChangeDirection(Direction direction)
        {
            Dir = direction;
        }

        private void SpawnHead(Position position)
        {
            _snakePositions.AddFirst(position);
            Grid[position.Row, position.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = _snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            _snakePositions.RemoveLast();
        }

        private void SpawnSnake()
        {
            int row = Rows / 2;

            for (int col = 0; col <= 3; col++)
            {
                Grid[row, col] = GridValue.Snake;
                _snakePositions.AddFirst(new Position(row, col));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (Grid?[row, col] == GridValue.Empty) yield return new Position(row, col);
                }
            }
        }

        private void SpawnFood()
        {
            List<Position> empty = new(EmptyPositions());

            if (empty.Count == 0) return;

            Position position = empty[_random.Next(empty.Count)];
            Grid[position.Row, position.Col] = GridValue.Food;
        }

        private bool IsOutsideGrid(Position position)
        {
            return position.Row < 0 || position.Row >= Rows
                || position.Col < 0 || position.Col >= Cols;
        }

        private GridValue WillHit(Position newHeadPos)
        {
            if (IsOutsideGrid(newHeadPos)) return GridValue.Outside;

            if (newHeadPos == TailPosition()) return GridValue.Empty;

            return Grid[newHeadPos.Row, newHeadPos.Col];
        }
    }
}
