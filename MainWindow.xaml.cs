using System.Windows;

namespace SaperCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Init();


            void Init()
            {
                InitializeComponent();
            }
            
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            Saper saper = new Saper();

            MineField.Text = "";

            int width = 0, height = 0, bombCount = 0;

            if ((bool)easy.IsChecked)
            { width = 10; height = 8; bombCount = 8; }
            else if ((bool)medium.IsChecked)
            { width = 18; height = 14; bombCount = 40; }
            else if ((bool)hard.IsChecked)
            { width = 24; height = 20; bombCount = 99; MineField.FontSize = 5; }
            else { return; }
            
            saper.CreateField(width, height, bombCount);

            int j = 0;
            for (int i = 0; i < saper.field.Length; i++)
            {
                if (j == width) { MineField.Text += "\n"; j = 0; }

                if (saper.field[i] == 'B')
                    MineField.Text += "💣";
                else
                    MineField.Text += " " + saper.field[i] + " ";
                j++;
            }
        }
    }
}
