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
        public bool isAdmin { get; set; } = false;
        public Admin_Form()
        {
            //if (this.isAdmin)
            //{
            //    InitializeComponent();
            //}
            //else
            //{
            //    MessageBox.Show("Bạn không có quyền mở form này!");
            //}
            InitializeComponent();
        }

        private void btnMemberMng_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProductMng_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrderMng_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}