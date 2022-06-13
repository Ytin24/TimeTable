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
        SqlConnection connection;
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
            var connectionString = $"Connect Timeout=5;Data Source={dbConnect.HostDb}, {dbConnect.PortDb};Initial Catalog={dbConnect.NameDb};User ID={dbConnect.LoginDb};Password={dbConnect.PasswordDb} ";
            connection = new SqlConnection(connectionString);
            try
            {
                
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невозможно подключиться к базе данных", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            connection.Close();
            PopUpDb.Visibility = Visibility.Collapsed;


        }

        private void DBConnection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpDb.Visibility = Visibility.Visible;
        }

        private void DirectorLogin_Click(object sender, RoutedEventArgs e)
        {
            PopUpD.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                connection.Open();
                string readString = "select * from Director";

                SqlCommand readCommand = new SqlCommand(readString, connection);
                using (SqlDataReader dataRead = readCommand.ExecuteReader())
                {
                    if (dataRead != null)
                    {
                        while (dataRead.Read())
                        {
                            
                            if (dpassword.Text == (string)dataRead.GetValue(0))
                            {
                               
                                MessageBox.Show("OK");
                            }
                            else
                            {
                                MessageBox.Show("Неправильный пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно подключиться к базе данных\nПроверьте данные подключения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            PopUpD.Visibility = Visibility.Collapsed;

        }
        List<Zamena> zamenas = new List<Zamena>();
        private void TZamena_Click(object sender, RoutedEventArgs e)
        {
            TDataZamena.Visibility = Visibility.Visible;
            connection.Open();
            string readString = "select * from Director";
            
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        Zamena item = new Zamena()
                        {
                            Поставили = "1",
                            Убрали = "2",
                            Класс = "33",
                            Урок = "5"
                        };
                        zamenas.Add(item);
                    }
                }
            }
            ZamenaGrid.ItemsSource = zamenas;
            connection.Close();

        }

        private void TRaspisanie_Click(object sender, RoutedEventArgs e)
        {
            TDataRaspisanie.Visibility = Visibility.Visible;
            RaspisanieDay day = new RaspisanieDay()
            {
                НазваниеПредмета = "abc",
                Урок = "1"
            };
            Raspisanie item = new Raspisanie()
            {
                ДеньНедели = "e222",
                Day = day
            };
            Raspisanie item2 = new Raspisanie()
            {
                ДеньНедели = "e3333",
                Day = day
            };
            Raspisanie item3 = new Raspisanie()
            {
                ДеньНедели = "e44444",
                Day = day
            };
            List<Raspisanie> raspisanies = new List<Raspisanie>();
            raspisanies.Add(item);
            raspisanies.Add(item2);
            raspisanies.Add(item3);
            RaspGrid.ItemsSource = raspisanies;
        }

        private void TeacherLogin_Click(object sender, RoutedEventArgs e)
        {
            bool pass = true;
            try
            {
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невозможно подключиться к базе данных\nПроверьте данные подключения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                pass = false;
            }
            if (pass)
            {
                Teacher.Visibility = Visibility.Visible;
                connection.Close();
            }
            

        }

        private void TaddZ_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DTeachers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DRaspisanie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DZamena_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
