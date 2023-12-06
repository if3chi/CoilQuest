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
        private readonly int _rows = 15, _cols = 15;
        private readonly Image[,] _gridImages;

        public MainWindow()
        {
            InitializeComponent();
            _gridImages = SetupGrid();
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
    }
}