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
using System.Data.SqlClient;
using System.Data;

namespace Kwork__2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbConnect dbConnect;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dbConnect = new DbConnect()
            {
                HostDb = Host.Text,
                PortDb = Port.Text,
                NameDb = Name.Text,
                LoginDb = Login.Text,
                PasswordDb = Password.Text
            };
            PopUpDb.Visibility = Visibility.Hidden;
            var connectionString = $"Data Source={dbConnect.HostDb}, {dbConnect.PortDb};Initial Catalog={dbConnect.NameDb};User ID={dbConnect.LoginDb};Password={dbConnect.PasswordDb} ";
            var connection = new SqlConnection(connectionString);
        }

        private void DBConnection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpDb.Visibility = Visibility.Visible;
        }

        private void DirectorLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
