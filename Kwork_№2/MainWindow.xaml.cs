using Kwork__2.Frames;
using Kwork__2.Frames.Director;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Kwork__2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string lang = "ru-RU";
        SqlConnection connection;
        #region Frames
        TReplaceD replace = new();
        TScheduleD schedule = new();
        TTeachersD teachers = new();
        DReplaceD replaceD = new();
        DScheduleD scheduleD = new();
        DTeachersD teachersD = new();
#endregion
        public MainWindow()
        {
            InitializeComponent();

        }
        #region db
        private void Button_Click(object sender, RoutedEventArgs e) //Подключение к БД
        {
            string connectionString;
            DbConnect.HostDb = Host.Text;
            DbConnect.PortDb = Port.Text;
            DbConnect.NameDb = Name.Text;
            DbConnect.LoginDb = Login.Text;
            DbConnect.PasswordDb = Password.Text;

            if (DbConnect.PortDb == "")
            {

                connectionString = $"Connect Timeout=5;Data Source={DbConnect.HostDb};Initial Catalog={DbConnect.NameDb};User ID={DbConnect.LoginDb};Password={DbConnect.PasswordDb} ";

            }
            else
            {
                connectionString = $"Connect Timeout=5;Data Source={DbConnect.HostDb}, {DbConnect.PortDb};Initial Catalog={DbConnect.NameDb};User ID={DbConnect.LoginDb};Password={DbConnect.PasswordDb} ";

            }
            connection = new SqlConnection(connectionString);
            DbConnect.connection = connection;


            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                if (lang == "ru-RU")
                {
                    MessageBox.Show("Невозможно подключиться к базе данных\nПроверьте данные подключения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else { MessageBox.Show("Unable to connect to the database\nCheck the connection data", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            connection.Close();
            PopUpDb.Visibility = Visibility.Collapsed;


        }

        private void DbConnection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PopUpDb.Visibility = Visibility.Visible;
        }
        #endregion
        private void DirectorLogin_Click(object sender, RoutedEventArgs e)
        {
            PopUpD.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Авторизация Директора
        {
            try
            {

                connection.Open();
                string readString = "select * from Directors";

                SqlCommand readCommand = new SqlCommand(readString, connection);
                using (SqlDataReader dataRead = readCommand.ExecuteReader())
                {
                    if (dataRead != null)
                    {
                        while (dataRead.Read())
                        {

                            if (dpassword.Text == (string)dataRead.GetValue(0))
                            {
                                Director.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                if (lang == "ru-RU")
                                {
                                    MessageBox.Show("Неправильный пароль", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else { MessageBox.Show("Invalid password", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                            }
                        }
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                if (lang == "ru-RU")
                {
                    MessageBox.Show("Невозможно подключиться к базе данных\nПроверьте данные подключения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else { MessageBox.Show("Unable to connect to the database\nCheck the connection data", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            PopUpD.Visibility = Visibility.Collapsed;

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Teacher.Visibility = Visibility.Collapsed;
            Director.Visibility = Visibility.Collapsed;
            DFrame.Navigate(null);
            TFrame.Navigate(null);
            scheduleD.Back();
            schedule.Back();
            replace.Back();
            replaceD.Back();
        }
        private void DTeachers_Click(object sender, RoutedEventArgs e)
        {
            DFrame.Navigate(teachersD);
        }
        private void DReplacements_Click(object sender, RoutedEventArgs e)
        {
            DFrame.Navigate(replaceD);
        }

        private void DSchedule_Click(object sender, RoutedEventArgs e)
        {
            DFrame.Navigate(scheduleD);
        }

        private void TeacherLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                Teacher.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                if (lang == "ru-RU")
                {
                    MessageBox.Show("Невозможно подключиться к базе данных\nПроверьте данные подключения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else { MessageBox.Show("Unable to connect to the database\nCheck the connection data", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            connection.Close();
            
        }

        private void TReplacements_Click(object sender, RoutedEventArgs e)
        {
            TFrame.Navigate(replace);
        }

        private void TSchedule_Click(object sender, RoutedEventArgs e)
        {
            TFrame.Navigate(schedule);
        }

        private void TTeachers_Click(object sender, RoutedEventArgs e)
        {
            TFrame.Navigate(teachers);
        }

        private void ChangeLanguage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ResourceDictionary dict = Application.Current.Resources.MergedDictionaries[2];
            if (lang == "ru-RU")
            {
                dict.Source = new Uri("Lang/lang_en-US.xaml", UriKind.Relative);
                lang = "en-EU";
            }
            else
            {
                dict.Source = new Uri("Lang/lang.xaml", UriKind.Relative);
                lang = "ru-RU";
            }

        }
    }
}
