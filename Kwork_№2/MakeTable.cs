using Kwork__2.UserControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Kwork__2
{
    class MakeTable
    {
        public Table table;
        public SqlConnection connection;
        public string ClassName;
        List<schedule> schedules = new List<schedule>();
        List<TextBlock> TextBlocks = new List<TextBlock>();
        List<string> Classes = new List<string>();
        public void Make()
        {
            GetClasses();
            if (table == null)
            {
                return;
            }
            foreach (var tb in TextBlocks)
            {
                table.GridUc.Children.Remove(tb);
            }
            for (int i = 1; i < table.GridUc.RowDefinitions.Count - 1; i++)
            {
                for (int j = 1; j < table.GridUc.ColumnDefinitions.Count; j++)
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
                    table.GridUc.Children.Add(TextBlock);
                    TextBlocks.Add(TextBlock);
                    GetDataFromSql();
                    AddDataToTextblock();
                }
            }
        }
        private void GetClasses()
        {
            table.ClassName.ItemsSource = Classes;
            connection.Open();
            string readString = $"select ClassName from Class";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        Classes.Add(dataRead.GetValue(0).ToString());
                    }
                }
            }
            table.ClassName.SelectedItem = Classes[0];
            connection.Close();
        }
        private void GetDataFromSql()
        {
            ClassName = table.ClassName.SelectedItem.ToString();
            if(ClassName == string.Empty) ClassName = "1Б";
            connection.Open();
            string readString = $"select * from Schedules s join Class c On s.ClassId = c.ClassId where c.ClassName = N'{ClassName}'";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        scheduleSubject Subject = new()
                        {
                            NameSubject = new string[5]
                        };
                        for (int i = 0; i < 5; i++)
                        {
                            int j = i + 1;
                            Subject.NameSubject[i] = dataRead.GetValue(j).ToString();
                        }
                        schedule schedule = new()
                        {
                            DayOfWeek = (int)dataRead.GetValue(6),
                            subject = Subject
                        };
                        schedules.Add(schedule);
                    }
                }
            }
            connection.Close();
        }
        private void AddDataToTextblock()
        {
            foreach (TextBlock tb in TextBlocks)
            {
                for (int x = 0; x < 5; x++)
                {
                    int secondint = x + 1;

                    for (int i = 0; i < schedules.Count; i++)
                    {
                        if (tb.Name == $"TextBlock{schedules[i].DayOfWeek}{secondint}")
                        {
                            tb.Text = schedules[i].subject.NameSubject[x];
                        }
                    }
                }
            }
        }
    }
}
