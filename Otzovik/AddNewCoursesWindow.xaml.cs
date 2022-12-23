using DocumentFormat.OpenXml.Office2010.Excel;
using Otzovik.ClassRoom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для AddNewCoursesWindow.xaml
    /// </summary>
    public partial class AddNewCoursesWindow : Window
    {
        DataBase dataBase = new DataBase();
        SqlCommand sqlCommand = new SqlCommand();
        SqlCommand comm = new SqlCommand();
        Courses courses = new Courses();
        SqlCommand update = new SqlCommand();
        FeerSSQEntities db = new FeerSSQEntities();

        public AddNewCoursesWindow()
        {
            InitializeComponent();
            string idcour = NameCourse.Text;
            GetDrop.courid = idcour.ToString();
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
           this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

       

        private void SaveAndReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameCourse.Text) && !string.IsNullOrEmpty(NameCourse.Text) &&
                !string.IsNullOrWhiteSpace(DescriptionCourse.Text) && !string.IsNullOrEmpty(DescriptionCourse.Text))
            {
                dataBase.Open_Connection();
                sqlCommand = new SqlCommand("insert into Courses (courseName, courseDescription) values (@courseName, @courseDescription)", dataBase.GetConnection());
                sqlCommand.Parameters.AddWithValue("@courseName", NameCourse.Text);
                sqlCommand.Parameters.AddWithValue("@courseDescription", DescriptionCourse.Text);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно добавленно");
            }
            else
            {
                MessageBox.Show("Данный курс уже имеется, добавить отзыв");
                
            }
                new Reviewss().Show();
                this.Hide();


            string idcour = NameCourse.Text;
            GetDrop.courid = idcour.ToString();
        }
        
        

        private void NameCourse_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Resources["txt"] = NameCourse.Text;
        }
    }
}
