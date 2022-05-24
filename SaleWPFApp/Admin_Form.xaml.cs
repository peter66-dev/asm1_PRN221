using SaleWPFApp;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Admin_Form.xaml
    /// </summary>
    public partial class Admin_Form : Window
    {
        public Admin_Form()
        {
            InitializeComponent();
        }

        private void btnMemberMng_Click(object sender, RoutedEventArgs e)
        {
            Member_Management window = new Member_Management();
            window.Show();
            //this.Close();
        }

        private void btnProductMng_Click(object sender, RoutedEventArgs e)
        {
            Product_Management window = new Product_Management();
            window.Show();
            //this.Close();
        }

        private void btnOrderMng_Click(object sender, RoutedEventArgs e)
        {
            Order_Management window = new Order_Management();
            window.Show();
            //this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            this.Close();
        }
    }
}