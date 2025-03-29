﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int value = 0;
        public List<Button2> mine_neighbours = new List<Button2>();
    }
    internal class MainWindowViewModel
    {
        public Button2[,] GenerateButtons(Grid grid, int size, int numberOfMines)
        {
            Button2[,] buttons = new Button2[size, size];
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
                    buttons[i, j] = button;
                }
            }
            
            Random rnd = new Random();
            for (int i = 0; i < numberOfMines;) //určí náhodně, které buttons jsou miny. Je teoreticky možné, že generace min bude trvat nekonečně dlouhou dobu, ale to se nestane 
            {
                int x_value = rnd.Next(size);
                int y_value = rnd.Next(size);
                if(!buttons[x_value, y_value].isMine)
                {
                    buttons[x_value, y_value].isMine = true;
                    i++;
                    continue;
                }
            }

            for (int i = 0; i < size; i++) //dá value políčkám, kolik je min okolo ––––––––––––––– NEDOTÝKAT, VŮBEC
            {
                for (int j = 0; j < size; j++)
                {
                    Button2 target_button = buttons[i, j];
                    if(i - 1 > -1 & j - 1 > -1)
                    {
                        if (buttons[i - 1, j - 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(i - 1 > -1)
                    {
                        if(buttons[i - 1, j].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(i - 1 > -1 & j + 1 < size)
                    {
                        if(buttons[i - 1, j + 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(j - 1 > -1)
                    {
                        if(buttons[i, j - 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(j + 1 < size)
                    {
                        if(buttons[i, j + 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(i + 1 < size & j - 1 > -1)
                    {
                        if(buttons[i + 1, j - 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if(i + 1 <  size)
                    {
                        if(buttons[i + 1, j].isMine)
                        {
                            target_button.value++;
                        }
                    }
                    if (i + 1 < size & j + 1 < size)
                    {
                        if (buttons[i + 1, j + 1].isMine)
                        {
                            target_button.value++;
                        }
                    }
                }
            }

            return buttons;
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button2 target_button = (Button2)sender;
            if( target_button.Content != null)
            {
                return;
            }
            if( target_button.isMine )
            {
                Application.Current.Shutdown();
            }
            else
            {
                target_button.Content = ((Button2)sender).value;
                switch (target_button.value)
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
            }
        }
        private void RightClick(object sender, RoutedEventArgs e)
        {
            if( ((Button2)sender).Content == null)
            {
                ((Button2)sender).Content = "🚩";
            }
            else
            {
                ((Button2)sender).Content = null;
            }
        }
    }
}
        //TODO:2. získání hodnoty, když na to kliknu 3. když je hodnota 0, udělat BFS