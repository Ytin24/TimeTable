using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kwork__2.Frames.Director
{
    /// <summary>
    /// Логика взаимодействия для DReplaceD.xaml
    /// </summary>
    public partial class DReplaceD : Page
    {
        public DReplaceD()
        {
            InitializeComponent();
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReplacementsTable_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= Table.Grid.RowDefinitions.Count; i++)
            {
                for (int j = 1; j <= Table.Grid.ColumnDefinitions.Count; j++)
                {
                    TextBox tb = new TextBox()
                    {
                        Text = "ABC",
                    };
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    Table.Grid.Children.Add(tb);
                }
            }
        }
    }
}
