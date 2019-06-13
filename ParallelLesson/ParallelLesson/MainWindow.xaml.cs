using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParallelLesson
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DropBox dropBox;

        public MainWindow()
        {
            InitializeComponent();

            dropBox = new DropBox();

            GridRefresh();            
        }

        private async Task GridRefresh() {

            List<OneString> ssd = new List<OneString>();

            var result = await dropBox.ListRootFolder();

            foreach (var i in result)
            {
                OneString s = new OneString();
                s.Text = i;
                ssd.Add(s);
            }

            ss.ItemsSource = ssd;
        }

        private void buttonSingUpClick(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "" || textBoxPassword.Text == "")
                WarningText.Visibility = Visibility.Visible;
            else
            {
                WarningText.Visibility = Visibility.Collapsed;

                using (UsersContext context = new UsersContext())
                {
                    context.Users.Add(new User
                    {
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text
                    });
                }
            }
            
        }

        private void buttonSingInClick(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "" || textBoxPassword.Text == "")
                WarningText.Visibility = Visibility.Visible;
            else
            {
                WarningText.Visibility = Visibility.Collapsed;

                using (UsersContext context = new UsersContext())
                {
                    foreach (var i in context.Users)
                    {
                        if (i.Login == textBoxLogin.Text || i.Password == textBoxPassword.Text)
                        {
                            girdRegistration.Visibility = Visibility.Collapsed;
                            gridMain.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }


    private async Task UploadAsync(string name, string path)
    {
            await dropBox.Upload(name, "", path);

            GridRefresh();
    }


    private void bottonDownloadClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 2;

            string path = "";
            string name = "";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {                
                path = dialog.FileName;
                foreach (var i in path.Split('\\'))
                {
                    name = i;
                }
            }

            UploadAsync(name, path);

        }
    }
}
