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
        private void NewGame(object sender, RoutedEventArgs e)
        {
            Saper.Init();
            NewMineField.RowDefinitions.Clear();
            NewMineField.ColumnDefinitions.Clear();
            NewMineField.Children.Clear();
            buttonList.Clear();
            textBlocksList.Clear();
            list.Clear();

            int width = 0, height = 0, bombCount = 0;

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

                    textBlocksList.Add(block);
                    NewMineField.Children.Add(block);

                }




            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    Button button = new Button();
                    string text = "Index" + i.ToString() + j.ToString();
                    button.Name = text;
                    button.Uid = i.ToString() + j.ToString();
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
            int index = 0;

            for (int i = 0; i < buttonList.Count; i++)
                if (buttonList[i].Uid == ((FrameworkElement)sender).Uid)
                { index = i; NewMineField.Children.Remove(buttonList[i]); list.Add(i); break; }

            //var tem = index;

            //while (tem<textBlocksList.Count)
            //{
            //    tem = index;

            //    while (textBlocksList[tem].Text == " ")
            //    {
            //        if (tem - 11 >= 0 && textBlocksList[tem - 11].Text == " ")
            //            list.Add(tem - 11);
            //        if (tem - 10 >= 0 && textBlocksList[tem - 10].Text == " ")
            //            list.Add(tem - 10);
            //        if (tem - 9 >= 0 && textBlocksList[tem - 9].Text == " ")
            //            list.Add(tem - 9);
            //        if (tem - 1 >= 0 && textBlocksList[tem - 1].Text == " ")
            //            list.Add(tem - 1);
            //        if (tem + 1 <= textBlocksList.Count && textBlocksList[tem + 1].Text == " ")
            //            list.Add(tem + 1);
            //        if (tem + 9 <= textBlocksList.Count && textBlocksList[tem + 9].Text == " ")
            //            list.Add(tem + 9);
            //        if (tem + 10 <= textBlocksList.Count && textBlocksList[tem + 10].Text == " ")
            //            list.Add(tem + 10);
            //        if (tem + 11 <= textBlocksList.Count && textBlocksList[tem + 11].Text == " ")
            //            list.Add(tem + 11);
            //        tem++;
            //    }
            //}

            for (int j = 0; j < list.Count; j++)
            {
                int i = list[j];

                var temp = i-11;
                if (temp >= 0 && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i - 10;
                if (temp >= 0 && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp-11, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i - 9;
                if (temp >= 0 && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i - 1;
                if (temp >= 0 && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i+1;
                if (temp <= textBlocksList.Count && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i + 9;
                if (temp <= textBlocksList.Count && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i + 10;
                if (temp <= textBlocksList.Count && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

                temp = i + 11;
                if (temp <= textBlocksList.Count && textBlocksList[temp].Text == " ") //&& GetPlace(index, 10) == GetPlace(temp, 10))
                {
                    if (textBlocksList[temp].Text == " " && !list.Contains(temp))
                        list.Add(temp);
                }

            }

            foreach (var item in list)
                NewMineField.Children.Remove(buttonList[index]);
        }

        //    private void VerticalSweep(int index, bool b)
        //    {
        //        for (int i = index + 10; i < buttonList.Count; i += 10)
        //        {
        //            if (textBlocksList[i].Text == " " || textBlocksList[i - 10].Text == " ")
        //            { NewMineField.Children.Remove(buttonList[i]); if (b) { HorizontalSweep(i); } }
        //            else
        //                break;
        //            if (textBlocksList[i].Text == " ")
        //            { NewMineField.Children.Remove(buttonList[i]); if (b) { HorizontalSweep(i); } list.Add(i); }
        //            else
        //                break;
        //        }

        //        for (int i = index - 10; i >= 0; i -= 10)
        //        {
        //            if (textBlocksList[i].Text == " " || textBlocksList[i + 10].Text == " ")
        //            { NewMineField.Children.Remove(buttonList[i]); if (b) { HorizontalSweep(i); } }
        //            else
        //                break;
        //            if (textBlocksList[i].Text == " ")
        //            { NewMineField.Children.Remove(buttonList[i]); if (b) { HorizontalSweep(i); } list.Add(i); }
        //            else
        //                break;
        //        }

        //    }
        //    private void HorizontalSweep(int index)
        //{

        //    for (int i = index + 1; GetPlace(index, 10) == GetPlace(i, 10) || i < 10 && i < textBlocksList.Count; i += 1)
        //    {
        //        if (textBlocksList[i].Text == " " || textBlocksList[i - 1].Text == " ")
        //        { NewMineField.Children.Remove(buttonList[i]); VerticalSweep(i, false); }
        //        else
        //            break;
        //        if (textBlocksList[i].Text == " ")
        //        { NewMineField.Children.Remove(buttonList[i]); VerticalSweep(i, false); list.Add(i); }
        //        else
        //            break;

        //    }

        //    for (int i = index - 1; GetPlace(index, 10) == GetPlace(i, 10) && i >= 0; i -= 1)
        //    {
        //        if (textBlocksList[i].Text == " " || textBlocksList[temp].Text == " ")
        //        { NewMineField.Children.Remove(buttonList[i]); VerticalSweep(i, false); }
        //        else
        //            break;
        //        if (textBlocksList[i].Text == " ")
        //        { NewMineField.Children.Remove(buttonList[i]); VerticalSweep(i, false); list.Add(i); }
        //        else
        //            break;
        //    }

        //}


        public int GetPlace(int value, int place)
        {
            return ((value % (place * 10)) - (value % place)) / place;
        }


        List<Button> buttonList = new List<Button>();
        List<Emoji.Wpf.TextBlock> textBlocksList = new List<Emoji.Wpf.TextBlock>();
        List<int> list = new();
    }
}
