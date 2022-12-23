using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Data;

namespace Otzovik
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window
    {
        DataBase dataBase = new DataBase();
        public Registr()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Timerlbl.Content = DateTime.Now.ToLongTimeString();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Login_box.Text) && !string.IsNullOrWhiteSpace(Login_box.Text) &&
               !string.IsNullOrEmpty(Password_box.Text) && !string.IsNullOrWhiteSpace(Password_box.Text))
            {
                if (checkUser())

                {
                    return;
                }
                var userLogins = Login_box.Text;
                var userPassword = Password_box.Text;
                string querystring = $"insert into Users(userLogin, userPassword) values ('{userLogins}', '{userPassword}')";


                SqlCommand sqlCommand = new SqlCommand(querystring, dataBase.GetConnection());

                dataBase.Open_Connection();

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!");
                    new Auth().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ошибка создания аккаунта!");
                }
                dataBase.Close_Connection();
            }
            else
            {
                MessageBox.Show("Проверьте корректность введённых данных");
            }
            
        }

        private Boolean checkUser()
        {
            var log = Login_box.Text;
            var pass = Password_box.Text;


            string querystring = $"select UserId, userLogin, userPassword from Users where userLogin = '{log}' and userPassword = '{pass}';";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand sql = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = sql;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new Auth().Show();
            this.Hide();
        }
    }
}
