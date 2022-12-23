using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Data.Entity;
using Otzovik.ClassRoom;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Courses courses = new Courses();
        SqlCommand command = new SqlCommand();
        DataBase dataBase = new DataBase();
        DataTable table = new DataTable();
        FeerSSQEntities db = new FeerSSQEntities();
        DataGrid dg = new DataGrid();


        public MainWindow()
        {
            InitializeComponent();
            SerchCourses();
            GetSrc();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void OtzwBtn_Click(object sender, RoutedEventArgs e)
        {
            new OtzwAll().Show();
            this.Hide();
        }

        private void AddNewCourseBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddNewCoursesWindow().Show();
            this.Hide();
        }

        private void FiltrTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SerchCourses();
        }
        private void SerchCourses()
        {
            var currentCourses = FeerSSQEntities.GetContext().Courses.ToList();
            currentCourses = currentCourses.Where(p => p.courseName.ToLower().Contains(FiltrTextBox.Text.ToLower())).ToList();

            MainDataGrid.ItemsSource = currentCourses.OrderBy(p => p.courseName).ToList();
        }

        public void GetSrc()
        {
            string qerystring = $"select distinct(Courses.courseName) as 'Название', avg(UsersCourses.CourseRate) as 'Рейтинг', Courses.courseDescription as 'Описание'\r\nfrom UsersCourses \r\ninner join Courses on UsersCourses.CoursesId = Courses.CoursesId\r\ngroup by Courses.courseName, Courses.CoursesId, Courses.courseDescription;".ToString();
            SqlDataAdapter adapter = new SqlDataAdapter(qerystring, dataBase.GetConnection());
            DataTable dataTable = new DataTable("Table");
            adapter.Fill(dataTable);
            MainDataGrid.ItemsSource = dataTable.DefaultView;
            adapter.Update(dataTable);
        }

    }
}
