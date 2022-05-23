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
            this.Close();
        }

        private void btnProductMng_Click(object sender, RoutedEventArgs e)
        {
            Product_Management window = new Product_Management();
            window.Show();
            this.Close();
        }

        private void btnOrderMng_Click(object sender, RoutedEventArgs e)
        {
            Order_Management
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            this.Close();
        }
    }
}