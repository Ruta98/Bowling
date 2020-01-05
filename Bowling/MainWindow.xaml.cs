using Bowling.Classes;
using System.Windows;

namespace Bowling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
                
        InputOutputData inputOutputData = new InputOutputData();
        public MainWindow()
        {
            InitializeComponent();                  
            DataContext = inputOutputData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newGame = new Game();
            var gameData=newGame.inputGameData(inputOutputData.InputText);
            inputOutputData.InputText = newGame.GetInputString(gameData);
            var res=newGame.Calculation(gameData);
            inputOutputData.OutputText = newGame.outputGameData(res);
        }

        

    }
}
