using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for Order_Management.xaml
    /// </summary>
    public partial class Order_Management : Window
    {
        IOrderRepository orderRepo;
        IOrderDetailRepository orderDetailRepo;
        public Order_Management()
        {
            InitializeComponent();
            orderRepo = new OrderRepository();
            orderDetailRepo = new OrderDetailRepository();
            //var date = Convert.ToDateTime(dpMinDate.Text).ToString("yyyy/MM/dd");
            dpStartDate.DisplayDate = DateTime.Now;
            dpEndDate.DisplayDate = DateTime.Now;
        }

        double GetTotal(List<Order> list)
        {
            double total = 0;
            try
            {
                foreach (var o in list)
                {
                    if (o.Freight != null)
                    {
                        total += (double)(o.Freight.Value);
                    }
                    List<OrderDetail> odList = orderDetailRepo.GetAllOrderDetailsByOrderID(o.OrderId);
                    foreach (var od in odList)
                    {
                        total += ((double)od.UnitPrice * od.Quantity * (1 - od.Discount));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bug in GetTotal function!");
            }
            return total;
        }

        bool CheckForm()
        {
            bool check = true;
            if (!int.TryParse(txtMemberId.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Xin lỗi, Member ID phải là số nguyên dương!", "Error message");
                check = false;
            }
            else if (!decimal.TryParse(txtFreight.Text, out decimal x) || x <= 5000)
            {
                MessageBox.Show("Xin lỗi, Freight phải lớn hơn 5000!", "Error message");
                check = false;
            }
            return check;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Order> list = orderRepo.GetAllOrders();
                txtTotal.Text = Math.Round(GetTotal(list), 3).ToString();
                LoadOrderList(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnLoad_Click function!");
            }
        }

        private void LoadOrderList(List<Order> list)
        {
            lvOrders.Items.Refresh();
            lvOrders.ItemsSource = list;
            if (list.Count > 0)
            {
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnDetail.IsEnabled = true;
            }
            else
            {
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnDetail.IsEnabled = false;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Create_Order window = new Create_Order();
            window.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc muốn xoá hoá đơn mã số: {txtOrderId.Text}?", "System message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int orderID = int.Parse(txtOrderId.Text);
                Order order = orderRepo.GetOrderByID(orderID);
                if (orderRepo.DeleteOrder(orderID)) // sql tự xoá các liên kết bên bảng khoá ngoại [Order detail]
                {
                    MessageBox.Show($"Xoá thành công mã hoá đơn: {orderID}!", "System message");
                    LoadOrderList(orderRepo.GetAllOrders());
                }
                else
                {
                    MessageBox.Show($"Xin lỗi, xoá hoá đơn thất bại!", "Error message");
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm())
            {
                int orderID = int.Parse(txtOrderId.Text);
                int memID = int.Parse(txtMemberId.Text);
                decimal freight = decimal.Parse(txtFreight.Text);
                Order order = orderRepo.GetOrderByID(orderID);
                order.MemberId = memID;
                order.Freight = freight;
                if (orderRepo.UpdateOrder(order))
                {
                    MessageBox.Show($"Cập nhật hoá đơn mã số {orderID} thành công!", "System message");
                    LoadOrderList(orderRepo.GetAllOrders());
                }
                else
                {
                    MessageBox.Show($"Cập nhật hoá đơn mã số {orderID} thất bại!", "Error message");
                }
            }
        }
        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtOrderId.Text);
            OrderDetail_Management window = new OrderDetail_Management(id);
            window.Show();
        }

        private void btnStatistics_Click(object sender, RoutedEventArgs e) // không biết cách so sánh 2 datepicker
        {


        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to quit?", "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }


    }
}
