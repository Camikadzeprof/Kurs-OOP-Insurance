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

namespace InsuranceComp.View
{
    /// <summary>
    /// Логика взаимодействия для WatchIncWindow.xaml
    /// </summary>
    public partial class WatchIncWindow : Window
    {
        public WatchIncWindow()
        {
            InitializeComponent();
        }

        public int Inc { get; set; }

        private void WatchIncWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var dbContext = new BaseDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(dbContext);

            try
            {
                var inc = unitOfWork.IncidentRepository.Entities.FirstOrDefault(n => n.IdIncident == Inc);

                IdTextBlock.Text = inc.IdIncident.ToString();
                NumTextBlock.Text = inc.Num.ToString();
                ExplainTextBlock.Text = inc.Explain;
                StatusTextBlock.Text = inc.Status;
                BitmapImage bitmap = new BitmapImage(new Uri(inc.File));
                Photo.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Okbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
