using CoilQuest.Logik;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoilQuest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int _rows = 15, _cols = 15, _gameSpeed = 100;
        private readonly Image[,] _gridImages;
        private readonly GameState _state;

        private readonly Dictionary<GridValue, ImageSource> _gridValToImage = new()
        {
            {GridValue.Empty, Images.Empty },
            {GridValue.Snake, Images.Body },
            {GridValue.Food, Images.Food },
        };

        public MainWindow()
        {
            InitializeComponent();
            _gridImages = SetupGrid();
            _state = new(_rows, _cols);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            await GameLoop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_state.GameOver) return;

            switch (e.Key)
            {
                case Key.Left:
                    _state.ChangeDirection(Direction.Left); break;
                case Key.Right:
                    _state.ChangeDirection(Direction.Right); break;
                case Key.Up:
                    _state.ChangeDirection(Direction.Up); break;
                case Key.Down:
                    _state.ChangeDirection(Direction.Down); break;
            }
        }

        private async Task GameLoop()
        {
            while (!_state.GameOver)
            {
                await Task.Delay(_gameSpeed);
                _state.Move();
                Draw();
            }
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[_rows, _cols];
            GameGrid.Rows = _rows;
            GameGrid.Columns = _cols;

            for (int row = 0; row < _rows; row++)
            {
                for (int cols = 0; cols < _cols; cols++)
                {
                    Image image = new() { Source = Images.Empty };
                    images[row, cols] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void Draw()
        {
            DrawGrid();
            ScoreText.Text = $"Score {_state.Score}";
        }

        private void DrawGrid()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    GridValue gridValue = _state.Grid[row, col];
                    _gridImages[row, col].Source = _gridValToImage[gridValue];
                }
            }
        }
    }
}