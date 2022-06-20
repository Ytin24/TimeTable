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
    /// Логика взаимодействия для TScheduleD.xaml
    /// </summary>
    public partial class TScheduleD : Page
    {
        MakeTable makeTable;
        public TScheduleD()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (makeTable == null) makeTable = new MakeTable(IsDirector: false);
            makeTable.table = TSchedule;
            makeTable.Make();
            makeTable.table.ClassName.SelectionChanged += (o, e) =>
            {
                makeTable.Make();
            };
        }
        public void Back()
        {
            try
            {
                makeTable.ClearAll();
            }
            catch (Exception ex) { }
        }
    }
}
