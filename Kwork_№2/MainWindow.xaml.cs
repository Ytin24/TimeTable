﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Kwork__2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbConnect dbConnect;
        SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString;
            dbConnect = new DbConnect()
            {
                HostDb = Host.Text,
                PortDb = Port.Text,
                NameDb = Name.Text,
                LoginDb = Login.Text,
                PasswordDb = Password.Text
            };

            if (dbConnect.PortDb == "")
            {

                connectionString = $"Connect Timeout=5;Data Source={dbConnect.HostDb};Initial Catalog={dbConnect.NameDb};User ID={dbConnect.LoginDb};Password={dbConnect.PasswordDb} ";

            }
            else
            {
                connectionString = $"Connect Timeout=5;Data Source={dbConnect.HostDb}, {dbConnect.PortDb};Initial Catalog={dbConnect.NameDb};User ID={dbConnect.LoginDb};Password={dbConnect.PasswordDb} ";

            }
            
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
        List<Replesment> zamenas = new List<Replesment>();
        private void TZamena_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void TRaspisanie_Click(object sender, RoutedEventArgs e)
        {
            TDataRaspisanie.Visibility = Visibility.Visible;
            MakeTable makeTable = new MakeTable(IsDirector:false);
            makeTable.table = TSchedule;
            makeTable.connection = connection;
            makeTable.Make();
            makeTable.table.ClassName.SelectionChanged += (o, e) =>
            {
                makeTable.Make();
            };
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
            DDataRasp.Visibility = Visibility.Visible;
            MakeTable makeTable = new MakeTable(IsDirector: true);
            makeTable.table = DSchedule;
            makeTable.connection = connection;
            makeTable.Make();
            makeTable.table.ClassName.SelectionChanged += (o, e) =>
            {
                makeTable.Make();
            };
        }

        private void DZamena_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void TTeachers_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
