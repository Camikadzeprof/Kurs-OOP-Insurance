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
    /// Логика взаимодействия для PayoutWindow.xaml
    /// </summary>
    public partial class PayoutWindow : Window
    {
        public int IdInc { get; set; }
        public PayoutWindow()
        {
            InitializeComponent();
        }

        private static BaseDbContext dbContext = new BaseDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork(dbContext);

        private void PayoutWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IncIdTextBlock.Text = IdInc.ToString();
        }

        private void SavePayoutbtn_Click(object sender, RoutedEventArgs e)
        {
            Payout pay = new Payout();
            pay.IdIncident = IdInc;
            pay.Sum = Convert.ToInt32(SumTextBox.Text);
            unitOfWork.PayoutRepository.Add(pay);
            unitOfWork.Commit();
            this.Close();
        }
    }
}
