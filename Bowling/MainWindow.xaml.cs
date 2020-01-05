using Bowling.Classes;
using System.Windows;

namespace Bowling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game newGame;
        public MainWindow()
        {
            InitializeComponent();
            newGame = new Game();
            DataContext = newGame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newGame.inputGameData();
            newGame.Calculation();
            newGame.outputGameData();
            newGame.cleanData();
        }

        

    }
}
