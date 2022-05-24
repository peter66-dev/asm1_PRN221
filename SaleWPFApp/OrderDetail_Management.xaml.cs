using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using System.Collections.Generic;
using System.Windows;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for OrderDetail_Management.xaml
    /// </summary>
    public partial class OrderDetail_Management : Window
    {
        IOrderDetailRepository orderDetailRepo;
        public OrderDetail_Management(int id)
        {
            orderDetailRepo = new OrderDetailRepository();
            InitializeComponent();
            LoadList(id);
        }

        void LoadList(int orderID)
        {
            lvOrderDetails.Items.Refresh();
            List<OrderDetail> list = orderDetailRepo.GetAllOrderDetailsByOrderID(orderID);
            if (list.Count != 0)
            {
                lvOrderDetails.ItemsSource = list;
                txtSearch.Text = orderID.ToString();
            }
            else
            {
                MessageBox.Show("Xin lỗi, các hoá đơn chi tiết này không có trong hệ thống!", "Error message");
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtSearch.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Xin lỗi, mã số hoá đơn phải lớn hơn 0", "Error message");
            }
            else
            {
                LoadList(id);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
