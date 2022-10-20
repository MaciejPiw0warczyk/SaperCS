using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SaperCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool gameState = true;
        public MainWindow()
        {
            Init();
        }

        void Init()
        {
            InitializeComponent();
            NewMineField.ShowGridLines = true;
        }
        int width = 0, height = 0, bombCount = 0;
        private void NewGame(object sender, RoutedEventArgs e)
        {
            Saper.Init();
            NewMineField.RowDefinitions.Clear();
            NewMineField.ColumnDefinitions.Clear();
            NewMineField.Children.Clear();
            buttonList.Clear();
            textBlocksList.Clear();
            list.Clear();


            if ((bool)easy.IsChecked)
            { width = 10; height = 8; bombCount = 8; }
            else if ((bool)medium.IsChecked)
            { width = 18; height = 14; bombCount = 40; }
            else if ((bool)hard.IsChecked)
            { width = 24; height = 20; bombCount = 99; }
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
                    Emoji.Wpf.TextBlock block = new Emoji.Wpf.TextBlock();
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
                    NewMineField.Children.Add(block);

                    textBlocksList.Add(block);

                    Button button = new Button();
                    text = "Index" + k.ToString() + j.ToString();
                    button.Name = text;
                    button.Uid = k.ToString() + j.ToString();
                    button.SetValue(Grid.RowProperty, k);
                    button.SetValue(Grid.ColumnProperty, j);
                    button.Click += OnButtonPress;
                    button.Background = Brushes.Honeydew;
                    button.Content = "H";
                    button.Effect = new DropShadowEffect();
                    NewMineField.Children.Add(button);

                    buttonList.Add(button);
                }
        }

        private void OnButtonPress(object sender, RoutedEventArgs e)
        {
            int index = 0, offset;
            var column = Grid.GetColumn((UIElement)sender);
            var row = Grid.GetRow((UIElement)sender);

            foreach (var buton in buttonList)
                if (Grid.GetColumn(buton) == column && Grid.GetRow(buton) == row)
                    index = buttonList.IndexOf(buton);
            if (textBlocksList[index].Text == " ")
                list.Add(index);
            else
                NewMineField.Children.Remove(buttonList[index]);

            for (int i = 0; i < list.Count; i++)
            {
                var x = list[i];

                offset = (width + 1) * -1;
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var io = x + offset;
                        if (!((offset == -1 || offset == ((width + 1) * -1) || offset == (width - 1)) && Grid.GetColumn(buttonList[x]) == 0))
                            if (!((offset == 1 || offset == (width + 1) || offset == ((width - 1) * -1)) && Grid.GetColumn(buttonList[x]) == width - 1))
                                if (io >= 0 && io < buttonList.Count && textBlocksList[io].Text == " ")
                                    if (!list.Contains(io))
                                        list.Add(io);
                        offset++;
                    }
                    offset += width - 3;
                }
            }



            foreach (var loc in list)
            {
                offset = (width + 1) * -1;
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var io = loc + offset;
                        if (!((offset == -1 || offset == ((width + 1) * -1) || offset == (width - 1)) && Grid.GetColumn(buttonList[loc]) == 0))
                            if (!((offset == 1 || offset == (width + 1) || offset == ((width - 1) * -1)) && Grid.GetColumn(buttonList[loc]) == width - 1))
                                if (io >= 0 && io < buttonList.Count)
                                    NewMineField.Children.Remove(buttonList[io]);
                        offset++;
                    }
                    offset += width - 3;
                }
            }
        }

        public int GetPlace(int value, int place)
        {
            return ((value % (place * 10)) - (value % place)) / place;
        }


        List<Button> buttonList = new List<Button>();
        List<Emoji.Wpf.TextBlock> textBlocksList = new List<Emoji.Wpf.TextBlock>();
        List<int> list = new();
    }
}
