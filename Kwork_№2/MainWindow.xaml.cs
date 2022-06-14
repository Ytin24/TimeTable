using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            catch (Exception ex)
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

        }

        private void TeacherLogin_Click(object sender, RoutedEventArgs e)
        {
            bool pass = true;
            try
            {
                connection.Open();
            }
            catch (Exception ex)
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

        List<TextBlock> TextBlocks = new List<TextBlock>();
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            

            foreach(var tb in TextBlocks)
            {
                AaA.GridUc.Children.Remove(tb);
            }
            for (int i = 1; i < AaA.GridUc.RowDefinitions.Count - 1; i++)
            {
                for (int j = 1; j < AaA.GridUc.ColumnDefinitions.Count; j++)
                {
                    TextBlock TextBlock = new TextBlock()
                    {
                        Text = $"ABC {i}:{j}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Name = $"TextBlock{i}{j}"
                    };

                    Grid.SetRow(TextBlock, i);
                    Grid.SetColumn(TextBlock, j);
                    AaA.GridUc.Children.Add(TextBlock);
                        TextBlocks.Add(TextBlock);
                }
            }
            connection.Open();
            string readString = "select * from Raspisanie r where r.ClassId = 1";
            List<Raspisanie> raspisanies = new List<Raspisanie>();
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        RaspisanieDay day = new RaspisanieDay()
                        {
                            НазваниеПредмета = new string[5]
                        };
                        day.НазваниеПредмета[0] = dataRead?.GetValue(1).ToString();
                        day.НазваниеПредмета[1] = dataRead?.GetValue(2).ToString();
                        day.НазваниеПредмета[2] = dataRead?.GetValue(3).ToString();
                        day.НазваниеПредмета[3] = dataRead?.GetValue(4).ToString();
                        day.НазваниеПредмета[4] = dataRead?.GetValue(5).ToString();

                        Raspisanie rasp = new Raspisanie()
                        {
                            ДеньНедели = (int)dataRead.GetValue(6),
                            Day = day
                        };
                        raspisanies.Add(rasp);
                    }
                }
            }
            connection.Close();
            foreach (TextBlock tb in TextBlocks)
            {
                for (int x = 0; x < 5; x++)
                {
                    int secondint = x + 1;
                    for (int i = 0; i < raspisanies.Count; i++)
                    {


                        if (tb.Name == $"TextBlock{raspisanies[i].ДеньНедели}{secondint}")
                        {
                            tb.Text = raspisanies[i].Day.НазваниеПредмета[x];
                        }
                    }
                }
            }

        }
    }
}
