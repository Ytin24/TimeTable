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
    /// Логика взаимодействия для DScheduleD.xaml
    /// </summary>
    /// 
    public partial class DScheduleD : Page
    {
        MakeTable makeTable;
        public DScheduleD()
        {
            InitializeComponent();
           
        }

        private void DSchedule_Loaded(object sender, RoutedEventArgs e)
        {
            if(makeTable == null) makeTable = new MakeTable(IsDirector: true);
            makeTable.table = DSchedule;
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
