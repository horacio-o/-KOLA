using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace minesweeper
{
    class Button2 : Button
    {
        public bool isMine = false; 
        public int Value = 0;
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
            for (int i = 0; i < numberOfMines; i++) //určí náhodně, které buttons jsou miny. Je teoreticky možné, že generace min bude trvat nekonečně dlouhou dobu, ale to se nestane 
            {
                int x_value = rnd.Next(size);
                int y_value = rnd.Next(size);
                if(!buttons[x_value, y_value].isMine)
                {
                    buttons[x_value, y_value].isMine = true;
                }
                else
                {
                    i--;
                }
            }
            return buttons;
        }
        //TODO: 1. right click 2. získání hodnoty, když na to kliknu 3. když je hodnota 0, udělat BFS

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            if(((Button2)sender).isMine)
            {
                Application.Current.Shutdown();
            }
            else
            {
                ((Button2)sender).Content = ((Button2)sender).Value;
            }
        } 
    }
}