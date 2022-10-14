using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        }
        void Init()
        {
            InitializeComponent();
            NewMineField.ShowGridLines = true;
        }
        private void NewGame(object sender, RoutedEventArgs e)
        {
            Saper.Init();
            NewMineField.RowDefinitions.Clear();
            NewMineField.ColumnDefinitions.Clear();
            NewMineField.Children.Clear();
            buttonList.Clear();
            textBlocksList.Clear();

            int width = 0, height = 0, bombCount = 0;

            if ((bool)easy.IsChecked)
            { width = 10; height = 8; bombCount = 8; }
            else if ((bool)medium.IsChecked)
            { width = 18; height = 14; bombCount = 40; }
            else if ((bool)hard.IsChecked)
            { width = 24; height = 20; bombCount = 99; } //MineField.FontSize = 5; }
            else { return; }

            Saper.CreateField(width, height, bombCount);

            for (int i = 0; i < height; i++)
                NewMineField.RowDefinitions.Add(new RowDefinition());
            for (int j = 0; j < width; j++)
                NewMineField.ColumnDefinitions.Add(new ColumnDefinition());


            int index = 0;
            for (int k = 0; k < height; k++)
                for (int j = 0; j < width; j++)
                {
                    TextBlock block = new TextBlock();
                    block.SetValue(Grid.RowProperty, k);
                    block.SetValue(Grid.ColumnProperty, j);
                    block.FontSize = 20;
                    block.TextAlignment = TextAlignment.Center;
                    block.VerticalAlignment = VerticalAlignment.Center;
                    string text = "Index" + k.ToString() + j.ToString();
                    block.Name = text;

                    if (Saper.field[index] == 'B')
                        block.Text += "💣";
                    else
                        block.Text += Saper.field[index];
                    index++;

                    textBlocksList.Add(block);
                    NewMineField.Children.Add(block);

                }




            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    Button button = new Button();
                    string text = "Index" + i.ToString() + j.ToString();
                    button.Name = text;
                    button.SetValue(Grid.RowProperty, i);
                    button.SetValue(Grid.ColumnProperty, j);
                    button.Click += OnButtonPress;
                    button.Background = Brushes.Honeydew;
                    button.Content = "H";
                    NewMineField.Children.Add(button);

                    buttonList.Add(button);
                }
        }

        private void OnButtonPress(object sender, RoutedEventArgs e)
        {
            NewMineField.Children.Remove((FrameworkElement)sender);
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            return;
        }

        List<Button> buttonList = new List<Button>();
        List<TextBlock> textBlocksList = new List<TextBlock>();
    }
}
