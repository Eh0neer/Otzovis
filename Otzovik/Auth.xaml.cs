using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;
using Otzovik.ClassRoom;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        DataBase dataBase = new DataBase();
        SqlCommand command = new SqlCommand();

        public Auth()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            string userss = Login_box.Text;
            GetDrop.userss = userss.ToString();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Timerlbl.Content = DateTime.Now.ToLongTimeString();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
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

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginUser();
            string userss = Login_box.Text;
            GetDrop.userss = userss.ToString();
        }

        public void LoginUser()
        {

            var loginUser = Login_box.Text;
            var passwordUser = Password_box.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string querystring = $"select UserId, userLogin, userPassword from Users where userLogin = '{loginUser}' and userPassword = '{passwordUser}'";
            command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                new MainWindow().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new Registr().Show();
            this.Hide();
        }
    }
}
