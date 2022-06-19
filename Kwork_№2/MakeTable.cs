using Kwork__2.UserControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Kwork__2
{
    class MakeTable
    {
        public Table table;
        SqlConnection connection;
        public string ClassName;
        List<schedule> schedules = new List<schedule>();
        List<TextBlock> TextBlocks = new List<TextBlock>();
        List<Button> Buttons = new List<Button>();
        List<string> Classes = new List<string>();
        Dictionary<TextBlock, int> pairs;
        Dictionary<string, int> ClassD = new();
        bool IsDirector;
        public MakeTable(bool IsDirector)
        {
            this.IsDirector = IsDirector;
        }
        public void ClearAll()
        {
            foreach (var tb in TextBlocks)
            {
                table.GridUc.Children.Remove(tb);
            }
            if (IsDirector)
            {
                foreach (var butt in Buttons)
                {
                    table.GridUc.Children.Remove(butt);
                }
            }
            schedules.Clear();
            TextBlocks.Clear();
            Buttons.Clear();
            Classes.Clear();
            pairs.Clear();
            ClassD.Clear();
            
        }
        public void Make()
        {
            connection = DbConnect.connection;
            pairs = new();
            GetClasses();
            if (table == null)
            {
                return;
            }
            foreach (var tb in TextBlocks)
            {
                table.GridUc.Children.Remove(tb);
            }
            if (IsDirector)
            {
                foreach(var butt in Buttons)
                {
                    table.GridUc.Children.Remove(butt);
                }
            }
            TextBlocks.Clear();
            for (int i = 1; i < table.GridUc.RowDefinitions.Count - 1; i++)
            {
                for (int j = 1; j < table.GridUc.ColumnDefinitions.Count; j++)
                {
                    TextBlock textBlock = new TextBlock()
                    {
                        Text = $"ABC {i}:{j}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Name = $"TextBlock{i}{j}",
                        
                    };
                    pairs.Add(textBlock, i);
                    Grid.SetRow(textBlock, i);
                    Grid.SetColumn(textBlock, j);
                    table.GridUc.Children.Add(textBlock);
                    TextBlocks.Add(textBlock);

                }
            }
            if (IsDirector)
            {
                for (int i = 1; i < table.GridUc.RowDefinitions.Count - 1; i++)
                {
                    for (int j = 1; j < table.GridUc.ColumnDefinitions.Count; j++)
                    {
                        Button button = new Button()
                        {
                            Background = Brushes.Transparent,
                            Foreground = Brushes.Transparent,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = table.GridUc.RowDefinitions[3].Height.Value,
                            Width = table.GridUc.ColumnDefinitions[3].Width.Value,
                            Name = $"k{i}{j}",
                            Style = table.ButtonResource.Style
                            
                        };
                        button.Click += (o, e) =>
                        {
                            var tb = TextBlocks.First(x => x.Name == $"TextBloc{button.Name}");
                            
                            string NewData = Microsoft.VisualBasic.Interaction.InputBox("Введите новое значение", "Изменение");
                            tb.Text = NewData;
                            UpdateDataToSQl(tb, NewData);

                            

                        };
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        table.GridUc.Children.Add(button);
                        Buttons.Add(button);
                    }
                }
            }
            GetDataFromSql();
            AddDataToTextblock();
        }

        private void GetClasses()
        {
            Classes.Clear();
            ClassD.Clear();
            
            connection.Open();
            string readString = $"select ClassName,ClassId from Class";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    int i = 1;
                    while (dataRead.Read())
                    {
                        Classes.Add(dataRead.GetValue(0).ToString());
                        ClassD.Add(dataRead.GetValue(0).ToString(), i);
                        i++;
                    }
                }
            }
            connection.Close();
            table.ClassName.ItemsSource = Classes;
            table.ClassName.Items.Refresh();
        }
        private void GetDataFromSql()
        {
            if (table.ClassName.SelectedItem == null) ClassName = "1А";
            else ClassName = table.ClassName.SelectedItem.ToString();
            schedules.Clear();
            connection.Open();
            string readString = $"select * from Schedules s join Class c On s.ClassId = c.ClassId where c.ClassName = N'{ClassName}'";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    if (dataRead.HasRows)
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
                                if (Subject.NameSubject[i] == string.Empty) Subject.NameSubject[i] = "Уроков нет";
                            }
                            schedule schedule = new()
                            {
                                DayOfWeek = (int)dataRead.GetValue(6),
                                subject = Subject
                            };
                            schedules.Add(schedule);

                        }
                    }
                    

                    else
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            int y = x + 1;

                            scheduleSubject Subject = new()
                            {
                                NameSubject = new string[5]
                            };
                            for (int i = 0; i < 5; i++)
                            {
                                int j = i + 1;
                                Subject.NameSubject[i] = "???";
                            }
                            schedule schedule = new()
                            {
                                DayOfWeek = y,
                                subject = Subject
                            };
                            schedules.Add(schedule);
                        }
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
        private void UpdateDataToSQl(TextBlock DT,string Data)
        {
            
            var Key = pairs[DT];
            List <TextBlock> list = new();
            foreach(var Value in pairs)
            {
                if(Value.Value == Key)
                {
                    list.Add(Value.Key);
                }
            }
            connection.Open();
            string readString =
                "UPDATE Schedules\n" +
                $"Set\n" +
                $"First = N'{list[0].Text}',\n" +
                $"Second = N'{list[1].Text}',\n" +
                $"Third = N'{list[2].Text}',\n" +
                $"Fourth = N'{list[3].Text}',\n" +
                $"Fifth = N'{list[4].Text}'\n" +
                $"where DayOfWeek = {Key} and ClassId = {(ClassD[ClassName])}";
                
            SqlCommand readCommand = new SqlCommand(readString, connection);
            SqlDataReader dataRead = readCommand.ExecuteReader();
            connection.Close();
        }
    }
}
