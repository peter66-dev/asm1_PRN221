using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using System.Collections.Generic;
using System.Windows;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for Order_History.xaml
    /// </summary>
    public partial class Order_History : Window
    {

        IOrderRepository orRepo;

        public Order_History(int memID)
        {
            orRepo = new OrderRepository();
            InitializeComponent();
            LoadOrdersList(memID);
        }

        private void LoadOrdersList(int memID)
        {
            lvOrders.Items.Refresh();
            List<Order> list = orRepo.GetOrdersByMemberID(memID);
            if (list.Count == 0)
            {
                MessageBox.Show("Xin lỗi, bạn chưa mua gì ỏ của hàng chúng tôi!", "Message");
            }
            else
            {
                lvOrders.ItemsSource = list;
            }
        }
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
