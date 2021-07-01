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
using System.IO;

namespace TempMonit
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> result = new List<string>();
        private string[] date = new string[5];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            var maxtemp = tb_maxTemp.Text;
            var maxtime = tb_maxTime.Text;
            var mintemp = tb_minTemp.Text;
            var mintime = tb_minTime.Text;
            char[] separators = new char[] { ' ', '.', ':'};
            date = tb_date.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string[] temp = tb_temp.Text.Split(' '); 
            int timeCount = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if ((maxtemp != "") && (int.Parse(temp[i]) > int.Parse(maxtemp)))
                {
                    timeCount += 10;
                }
                else if ((mintemp != "") && (int.Parse(temp[i]) < int.Parse(mintemp)))
                {
                    timeCount += 10;
                }
            }
            if (timeCount != 0)
            {
                tb_otchet.Text = $"Порог минимальной температуры превышен на {timeCount} минут";
                DateTime ndate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]),
                    int.Parse(date[3]), int.Parse(date[4]), 0);
                for (int i = int.Parse(maxtime)/10; i < temp.Length; i++)
                {
                    if ((int.Parse(temp[i]) > int.Parse(maxtemp)) && (tb_maxTime.Text != null))
                    {
                        result.Add($"{ndate}  {temp[i]}  {maxtemp}  {int.Parse(temp[i]) - int.Parse(maxtemp)}");

                    }
                    else if ((mintime != null) && (int.Parse(temp[i]) < int.Parse(mintemp)))
                    {
                        result.Add($"{ndate}  {temp[i]}  {mintemp}  {int.Parse(temp[i]) - int.Parse(mintemp)}");
                    }
                    ndate = ndate.AddMinutes(10);
                }
                tb_narushen.Text = string.Join(Environment.NewLine, result);
            }
            else
                tb_otchet.Text = "Все меры хранения соблюдены";
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            string path1 = "C:\\Users\\Public\\Documents\\file1.txt";
            string path2 = "C:\\Users\\Public\\Documents\\file2.txt";
            using (var sr = new StreamReader(path1, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                   tb_date.Text = string.Join(Environment.NewLine, line);
                }
            }
            using (var sr = new StreamReader(path2, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    tb_temp.Text = string.Join(Environment.NewLine, line);
                }
            }
        }

        private void btn_safe_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:\\Users\\Public\\Documents\\safe.txt";

            // write new file
            using (var sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {

                foreach (var line in tb_narushen.Text)
                    sw.WriteLine(line);

            }
        }
    }
}
