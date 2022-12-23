using Otzovik.ClassRoom;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для otzw.xaml
    /// </summary>
    public partial class otzw : Window
    {
        SqlCommand sqlCommand = new SqlCommand();
        DataBase dataBase = new DataBase();
        public otzw()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NameCourse.Text) && !string.IsNullOrEmpty(NameCourse.Text))
            {
                dataBase.Open_Connection();
                sqlCommand = new SqlCommand("insert into Courses (courseName) values (@courseName)", dataBase.GetConnection());
                sqlCommand.Parameters.AddWithValue("@courseName", NameCourse.Text);


                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Успешно добавлено");
                
            }
            else
            {
                MessageBox.Show("Ошибка");
                
            }
                new Reviewss().Show();
                this.Hide();
            string idcour = NameCourse.Text;
            GetDrop.courid = idcour.ToString();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new OtzwAll().Show();
            this.Hide();
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
    }
}
