using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IpcimWPF
{
    public partial class MainWindow : Window
    {
        public class IpRecord
        {
            public string DomainName { get; set; }
            public string IpAddress { get; set; }
        }
        private List<IpRecord> records = new List<IpRecord>();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            string filePath = "iplist.csv"; // CSV fájl neve
            if (!File.Exists(filePath))
            {
                MessageBox.Show("CSV fájl nem található: " + filePath);
                return;
            }

            records.Clear();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // első sor a fejléc
            {
                var parts = line.Split(';');
                if (parts.Length == 2)
                {
                    records.Add(new IpRecord { DomainName = parts[0], IpAddress = parts[1] });
                }
            }

            dgData.ItemsSource = null;
            dgData.ItemsSource = records;
        }


        private void btnBevitel_Click(object sender, RoutedEventArgs e)
        {
            string domain = tbDomain.Text.Trim();
            string ip = tbIp.Text.Trim();

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Mindkét mezőt ki kell tölteni!");
                return;
            }

            records.Add(new IpRecord { DomainName = domain, IpAddress = ip });

            dgData.ItemsSource = null;
            dgData.ItemsSource = records;

            tbDomain.Clear();
            tbIp.Clear();
        }
        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FileName = "iplist.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("domain-name;ip-address");
                    foreach (var record in records)
                    {
                        writer.WriteLine($"{record.DomainName};{record.IpAddress}");
                    }
                }

                MessageBox.Show("Mentés sikeres!");
            }
        }
    }
}
}