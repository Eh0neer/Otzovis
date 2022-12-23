using Otzovik.ClassRoom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для Reviewss.xaml
    /// </summary>
    public partial class Reviewss : Window
    {
        DataBase db = new DataBase();
        DataBase data = new DataBase();
        SqlCommand sqlCommand = new SqlCommand();
        SqlCommand sql = new SqlCommand();
        AddNewCoursesWindow add = new AddNewCoursesWindow();
        private readonly AddNewCoursesWindow addNewCoursesWindow;
        public Reviewss()
        {
            InitializeComponent();

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string courss = GetDrop.courid;
            string ursser = GetDrop.userss;
            Console.Write(courss);
            var userid = new Auth().Login_box.ToString();

            if (!string.IsNullOrEmpty(OtzwTbx.Text) && !string.IsNullOrWhiteSpace(OtzwTbx.Text) &&
               !string.IsNullOrEmpty(RateTbx.Text) && !string.IsNullOrWhiteSpace(RateTbx.Text))
            {
                data.Open_Connection();
                sqlCommand = new SqlCommand($"insert into UsersCourses (Reviews, CourseRate, UserId, CoursesId) values (@Reviews, @CourseRate, (select Users.UserId from Users where userLogin = '{ursser}'),(select Courses.CoursesID from Courses where Courses.CourseName = '{courss}'))", data.GetConnection());
                sqlCommand.Parameters.AddWithValue("@Reviews", OtzwTbx.Text);
                sqlCommand.Parameters.AddWithValue("@CourseRate", RateTbx.Text);
                sqlCommand.Parameters.AddWithValue("@UserId", ursser);
                sqlCommand.Parameters.AddWithValue("@CoursesId", courss);



                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно добавленно");


                new MainWindow().Show();
                this.Hide();
            }


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

    }
}
