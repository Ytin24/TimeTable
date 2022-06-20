using Kwork__2.UserControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kwork__2
{
    internal class MakeReplaceTable
    {

        public ReplacementsTable table;
        SqlConnection connection;
        public string ClassName;
        List<Replacement> Replace = new();
        List<TextBlock> TextBlocks = new List<TextBlock>();
        List<string> Classes = new List<string>();
        Dictionary<TextBlock, int> pairs;
        Dictionary<string, int> ClassD = new();
        public MakeReplaceTable()
        {

        }
        public void ClearAll()
        {
            foreach (var tb in TextBlocks)
            {
                table.GridUc.Children.Remove(tb);
            }
            Replace.Clear();
            TextBlocks.Clear();
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

            TextBlocks.Clear();
            for (int i = 1; i < table.GridUc.RowDefinitions.Count; i++)
            {
                for (int j = 1; j < table.GridUc.ColumnDefinitions.Count; j++)
                {
                    TextBlock textBlock = new TextBlock()
                    {
                        Text = $"Замен нет",
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
            Replace.Clear();
            connection.Open();
            string readString = $"select * from Replacements r join Class c On r.ClassId = c.ClassId where c.ClassName = N'{ClassName}'";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    if (dataRead.HasRows)
                    {
                        while (dataRead.Read())
                        {

                            Replacement Item = new()
                            {
                                Class = dataRead.GetValue(5).ToString(),
                                Add = dataRead.GetValue(2).ToString(),
                                Remove = dataRead.GetValue(1).ToString(),
                                Lesson = dataRead.GetValue(3).ToString(),

                            };
                            Replace.Add(Item);
                        }
                    }


                    else
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            Replacement Item = new()
                            {
                                Class = "?",
                                Add = "?",
                                Remove = "?",
                                Lesson = "?",

                            };
                            Replace.Add(Item);
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

                    for (int i = 0; i < Replace.Count; i++)
                    {
                        if (tb.Name == $"TextBlock{Replace[i].Lesson}{secondint}")
                        {
                            switch (secondint)
                            {
                                case 1:
                                    tb.Text = Replace[i].Remove;
                                    break;
                                case 2:
                                    tb.Text = Replace[i].Add;
                                    break;
                                case 3:
                                    tb.Text = Replace[i].Class;
                                    break;
                                case 4:
                                    tb.Text = Replace[i].Lesson;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
