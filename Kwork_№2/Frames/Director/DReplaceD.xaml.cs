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
        MakeReplaceTable ReplaceTable;
        public DReplaceD()
        {
            InitializeComponent();
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (ReplaceTable == null) ReplaceTable = new();
            ReplaceTable.table = Table;
            ReplaceTable.Make();
            ReplaceTable.table.ClassName.SelectionChanged += (o, e) =>
            {
                ReplaceTable.Make();
            };
        }
        public void Back()
        {
            try
            {
                ReplaceTable.ClearAll();
            }
            catch (Exception ex) { }
        }
    }
}
