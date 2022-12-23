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
using System.Data;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для OtzwAll.xaml
    /// </summary>
    public partial class OtzwAll : Window
    {
        DataBase dataBase = new DataBase();
        public OtzwAll()
        {
            InitializeComponent();
            Grid();
            

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            new otzw().Show();
            this.Hide();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

        public void Grid()
        {
            string qerystring = $"select Courses.courseName as 'Название',UsersCourses.CourseRate as 'Рейтинг', UsersCourses.Reviews as 'Отзыв'  from Courses inner join UsersCourses on UsersCourses.CoursesId = Courses.CoursesId group by Courses.courseName, UsersCourses.Reviews, UsersCourses.CourseRate".ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(qerystring, dataBase.GetConnection());
            DataTable table = new DataTable("Table");
            adapter.Fill(table);
            MainDataGrid.ItemsSource = table.DefaultView;
            adapter.Update(table);
        }

        
    }
}
