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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TempMonit
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> result = new List<string>();
        private string[] date = new string[10];
        private string[] temp = new string[54];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            int maxtemp = int.Parse(tb_maxTemp.Text);
            int maxtime = int.Parse(tb_maxTime.Text);
            int mintemp = int.Parse(tb_minTemp.Text);
            int mintime = int.Parse(tb_minTime.Text);
            char[] separators = new char[] { ' ', '.', ':'};
            date = tb_date.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            temp = tb_temp.Text.Split(' ');
        }
    }
}
