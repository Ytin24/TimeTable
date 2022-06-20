using Kwork__2.UserControls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwork__2
{
    internal class MakeTeachersTable
    {
        public TeachersTable table;
        SqlConnection connection;
        int CountT;
        List<Teachers> teachers;
        List<String> teachersNames;
        public void Make()
        {
            teachers = new();
            teachersNames = new();
            GetTeachers();
            string name;
            if (table.Teacher.SelectedItem != null) { table.TeacherName.Text = table.Teacher.SelectedItem.ToString(); name = table.Teacher.SelectedItem.ToString(); }
            else { table.TeacherName.Text = teachers[0].TeacherName; name = teachers[0].TeacherName; }
            table.TeacherClasses.Text = teachers.FirstOrDefault(x => x.TeacherName == name).ClassesNames;

        }
        private void GetTeachers()
        {
            connection = DbConnect.connection;
            connection.Open();
            string readString = $"Select Count(Teachers.TeacherId)[ids] from Teachers";
            SqlCommand readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {

                    while (dataRead.Read())
                    {
                        CountT = (int)dataRead.GetValue(0);
                    }
                }
            }
            connection.Close();
            connection.Open();
            readString = $"SELECT CONCAT(t.LastName,' ',t.FirstName,' ',t.MiddleName) from Teachers t Order by t.TeacherId";
            readCommand = new SqlCommand(readString, connection);
            using (SqlDataReader dataRead = readCommand.ExecuteReader())
            {
                if (dataRead != null)
                {
                    while (dataRead.Read())
                    {
                        Teachers item = new()
                        {
                            TeacherName = dataRead.GetValue(0).ToString(),
                        };
                        teachers.Add(item);
                    }
                }
            }
            connection.Close();
            for (int i = 1; i <= CountT; i++)
            {
                int j = i - 1;
                connection.Open();
                readString = $"SELECT c.ClassName from Teachers t join Class c on c.TeacherId = t.TeacherId where t.TeacherId = {i}";
                readCommand = new SqlCommand(readString, connection);
                using (SqlDataReader dataRead = readCommand.ExecuteReader())
                {
                    if (dataRead != null)
                    {
                        while (dataRead.Read())
                        {
                            teachers[j].ClassesNames += $"{ dataRead.GetValue(0).ToString()} ";
                        }
                    }
                }
                connection.Close();
                foreach (var a in teachers)
                {
                    teachersNames.Add(a.TeacherName);
                }
                table.Teacher.ItemsSource = teachersNames;
            }
        }
    }
}
