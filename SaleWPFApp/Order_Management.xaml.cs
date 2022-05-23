using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
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

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for Order_Management.xaml
    /// </summary>
    public partial class Order_Management : Window
    {
        IOrderRepository repo;
        public Order_Management()
        {
            InitializeComponent();
            repo = new OrderRepository();
            //var date = Convert.ToDateTime(dpMinDate.Text).ToString("yyyy/MM/dd");
            dpStartDate.DisplayDate = DateTime.Now;
            dpEndDate.DisplayDate = DateTime.Now;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadOrderList(repo.GetAllOrders());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnLoad_Click function!");
            }
        }

        private void LoadOrderList(List<Order> list)
        {
            lvOrders.ItemsSource = list;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e) // không biết cách so sánh 2 datepicker
        {
            
           
        }
    }
}
