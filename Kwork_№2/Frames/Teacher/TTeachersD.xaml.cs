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

namespace Kwork__2.Frames
{
    /// <summary>
    /// Логика взаимодействия для TTeachersD.xaml
    /// </summary>
    public partial class TTeachersD : Page
    {
        MakeTeachersTable makeTable;
        public TTeachersD()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (makeTable == null) makeTable = new MakeTeachersTable();
            makeTable.table = Table;
            makeTable.Make();
            makeTable.table.Teacher.SelectionChanged += (o, e) =>
            {
                makeTable.Make();
            };
        }
    }
}
