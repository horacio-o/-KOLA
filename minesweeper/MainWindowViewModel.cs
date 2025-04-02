using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace minesweeper
{
    class Button2 : Button
    {
        public bool isMine = false;
        public bool was_visited = false;
        public int value = 0;
        public int x_coordinate = 0;
        public int y_coordinate = 0;
    }
    internal class MainWindowViewModel
    {
        int current_num_of_found_mines = 0;
        int number_of_mines = 0;
        int real_size = 0;
        Button2[,] grid_of_buttons;
        public void GenerateButtons(Grid grid, int size, int numberOfMines)
        {
            real_size = size;
            grid_of_buttons = new Button2[size, size];
            for (int i = 0; i < size; i++)//vytvoří buttons
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < size; j++)
                {
                    Button2 button = new Button2();
                    button.Click += ButtonClicked;
                    button.MouseRightButtonDown += RightClick;

                    // Nastavení pozice v Gridu
                    Grid.SetRow(button, i + 1);
                    Grid.SetColumn(button, j + 1);

                    // Přidání tlačítka do Gridu
                    grid.Children.Add(button);

                    // Přidání tlačítka do pole tlačítek
                    grid_of_buttons[i, j] = button;
                    button.x_coordinate = i; 
                    button.y_coordinate = j;
                }
            }
            
            Random rnd = new Random();
            for (int i = 0; i < numberOfMines;) //určí náhodně, které buttons jsou miny. Je teoreticky možné, že generace min bude trvat nekonečně dlouhou dobu, ale to se nestane 
            {
                int x_value = rnd.Next(size);
                int y_value = rnd.Next(size);
                if(!grid_of_buttons[x_value, y_value].isMine)
                {
                    grid_of_buttons[x_value, y_value].isMine = true;
                    number_of_mines++;
                    i++;
                }
            }

            for (int i = 0; i < size; i++) //dá value políčkám, kolik je min okolo ––––––––––––––– NEDOTÝKAT, VŮBEC
            {
                for (int j = 0; j < size; j++)
                {
                    if(i - 1 > -1 & j - 1 > -1)
                    {
                        if (grid_of_buttons[i - 1, j - 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(i - 1 > -1)
                    {
                        if(grid_of_buttons[i - 1, j].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(i - 1 > -1 & j + 1 < size)
                    {
                        if(grid_of_buttons[i - 1, j + 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(j - 1 > -1)
                    {
                        if(grid_of_buttons[i, j - 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(j + 1 < size)
                    {
                        if(grid_of_buttons[i, j + 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(i + 1 < size & j - 1 > -1)
                    {
                        if(grid_of_buttons[i + 1, j - 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if(i + 1 <  size)
                    {
                        if(grid_of_buttons[i + 1, j].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                    if (i + 1 < size & j + 1 < size)
                    {
                        if (grid_of_buttons[i + 1, j + 1].isMine)
                        {
                            grid_of_buttons[i, j].value++;
                        }
                    }
                }
            }
        }
        private void BFS(Button2 button, Button2[,] buttons)
        {
            if (!button.was_visited)
            {
                button.was_visited = true;

                switch (button.value)
                {
                    case 0:
                        button.Background = Brushes.Gray;
                        break;

                    case 1:
                        button.Background = Brushes.SkyBlue;
                        button.Content = button.value;
                        break;

                    case 2:
                        button.Background = Brushes.LightGreen;
                        button.Content = button.value;
                        break;

                    case 3:
                        button.Background = Brushes.OrangeRed;
                        button.Content = button.value;
                        break;

                    case 4:
                        button.Background = Brushes.Violet;
                        button.Content = button.value;
                        break;

                    case 5:
                        button.Background = Brushes.Yellow;
                        button.Content = button.value;
                        break;

                    case 6:
                        button.Background = Brushes.Cyan;
                        button.Content = button.value;
                        break;

                    case 7:
                        button.Background = Brushes.LightGray;
                        button.Content = button.value;
                        break;

                    case 8:
                        button.Background = Brushes.Red;
                        button.Content = button.value;
                        break;
                }

                if(button.x_coordinate - 1 > -1 & button.value == 0) //jestli políčko nad buttonem existuje a button má value 0, nechci vytvářet metodu IsValidCoordinate(), prosím.... nechci
                {
                    BFS(buttons[button.x_coordinate - 1, button.y_coordinate], buttons);
                }
                if(button.y_coordinate - 1 > -1 & button.value == 0)
                {
                    BFS(buttons[button.x_coordinate, button.y_coordinate - 1], buttons);
                }
                if(button.x_coordinate + 1 < real_size & button.value == 0)
                {
                    BFS(buttons[button.x_coordinate + 1, button.y_coordinate], buttons);
                }
                if(button.y_coordinate + 1 < real_size & button.value == 0)
                {
                    BFS(buttons[button.x_coordinate, button.y_coordinate + 1], buttons);
                }
            }
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button2 target_button = (Button2)sender;
            if(target_button.value == 0)
            {
                BFS(target_button, grid_of_buttons);
            }
            if(target_button.isMine)
            {
                Application.Current.Shutdown();
            }
            else if(!target_button.isMine)
            {
                if(target_button.value != 0)
                {
                    target_button.Content = target_button.value;
                }
                switch(target_button.value)
                {
                    case 0:
                        target_button.Background = Brushes.Gray;
                        break;

                    case 1:
                        target_button.Background = Brushes.SkyBlue;
                        break;

                    case 2:
                        target_button.Background = Brushes.LightGreen;
                        break;

                    case 3:
                        target_button.Background = Brushes.OrangeRed;
                        break;

                    case 4:
                        target_button.Background = Brushes.Violet;
                        break;

                    case 5:
                        target_button.Background = Brushes.Yellow;
                        break;

                    case 6:
                        target_button.Background = Brushes.Cyan;
                        break;

                    case 7:
                        target_button.Background = Brushes.LightGray;
                        break;

                    case 8:
                        target_button.Background = Brushes.Red;
                        break;
                }
                // dává barvy hodnotám od 0 do 8
            }
        }
        private void RightClick(object sender, RoutedEventArgs e)
        {
            Button2 target_button = ((Button2)sender);
            if( target_button.Content == null)
            {
                target_button.Content = "🚩";
                if(target_button.isMine)
                {
                    current_num_of_found_mines++;
                    if(current_num_of_found_mines == number_of_mines)
                    {
                        Grid.SetColumn(target_button, (real_size + 2) / 2);
                        Grid.SetRow(target_button, (real_size + 2) / 2);
                        target_button.Content = "Vítězství :3";
                        target_button.Background = Brushes.HotPink;
                    }
                }
            }
            else
            {
                target_button.Content = null;
                if(target_button.isMine)
                {
                    current_num_of_found_mines--;
                }
            }
        }
    }
}