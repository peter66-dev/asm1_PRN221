using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for Create_Order.xaml
    /// </summary>
    public partial class Create_Order : Window
    {
        IProductRepository productRepo;
        IMemberRepository memberRepo;
        IOrderRepository orderRepo;
        IOrderDetailRepository orderDetailRepo;
        List<Product> cart;

        public Create_Order()
        {
            InitializeComponent();
            productRepo = new ProductRepository();
            memberRepo = new MemberRepository();
            orderRepo = new OrderRepository();
            orderDetailRepo = new OrderDetailRepository();
            cart = new List<Product>();
        }

        void AddToCart(Product pro)
        {
            bool checkExist = false;
            foreach (var p in cart)
            {
                if (p.ProductId == pro.ProductId)
                {
                    p.UnitsInStock += pro.UnitsInStock;
                    checkExist = true;
                }
            }
            if (!checkExist)
            {
                cart.Add(pro);
            }
        }

        private void LoadCart(List<Product> list)
        {
            lvCarts.Items.Refresh(); // tâm linh 
            lvCarts.ItemsSource = list;
        }

        private void txtProID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProID.Text, out int id) && id > 0)
                {
                    Product pro = productRepo.GetProductByID(id);
                    if (pro == null)
                    {
                        MessageBox.Show($"Xin lỗi, không tồn tại sản phẩm với id: {txtProID.Text}", "Error message");
                        txtProID.Text = "";
                        txtProName.Text = "";
                        txtCateID.Text = "";
                        txtWeight.Text = "";
                        txtUnitPrice.Text = "";
                        txtUnitsInStock.Text = "";
                    }
                    else
                    {
                        txtProName.Text = pro.ProductName;
                        txtCateID.Text = pro.CategoryId.ToString();
                        txtWeight.Text = pro.Weight;
                        txtUnitPrice.Text = pro.UnitPrice.ToString();
                    }
                }
                else
                {
                    MessageBox.Show($"Xin lỗi, Product ID không hợp lệ: {txtProID.Text}", "Error message");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid input");
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                if (email.Length != 0)
                {
                    Member mem = memberRepo.GetMemberByEmail(email);
                    if (mem == null)
                    {
                        MessageBox.Show($"Xin lỗi, không tồn tại thành viên với email: {email}", "Error message");
                        txtCompanyName.Text = "";
                        txtCity.Text = "";
                        txtCountry.Text = "";
                        txtMemberID.Text = "";
                    }
                    else
                    {
                        txtMemberID.Text = mem.MemberId.ToString();
                        txtCompanyName.Text = mem.CompanyName;
                        txtCity.Text = mem.City;
                        txtCountry.Text = mem.Country;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid input");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) // coi lại chỗ add trùng sản phẩm và khác sản phẩm!
        {
            if (txtCateID.Text.Trim().Length != 0 && txtMemberID.Text.Trim().Length != 0
                && int.TryParse(txtUnitsInStock.Text, out int quantity) && quantity > 0)
            {
                int proID = int.Parse(txtProID.Text);
                Product pro = productRepo.GetProductByID(proID);
                if (quantity > pro.UnitsInStock)
                {
                    MessageBox.Show("Xin lỗi, chúng tôi không đủ số lượng cho sản phẩm này!", "Error message");
                }
                else
                {
                    pro.UnitsInStock = quantity;
                    AddToCart(pro);
                    LoadCart(cart);
                    txtTotal.Text = CalculateTotal(cart).ToString();
                    MessageBox.Show("Thêm sản phẩm vào giỏ hàng thành công!", "System message");
                    btnSave.IsEnabled = true;
                    txtEmail.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Xin lỗi, bạn phải nhập thông tin khách hàng và sản phẩm cần mua trước đã!", "Error message");
            }
        }

        double CalculateTotal(List<Product> list)
        {
            double total = 0;
            if (list.Count != 0)
            {
                foreach (var pro in list)
                {
                    total += (double)(pro.UnitsInStock * pro.UnitPrice);
                }
            }
            return total;
        }

        double CalculateReturnMoney(double total, float discount, double pay, double freight)
        {
            return Math.Round(pay - total * (1 - discount) - freight, 3);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count != 0)
            {
                try
                {
                    double total = double.Parse(txtTotal.Text);
                    double payMoney = double.Parse(txtPayAmount.Text);
                    double freight = double.Parse(txtFreight.Text);
                    float discount = float.Parse(txtDiscount.Text);
                    double returnMoney = CalculateReturnMoney(total, discount, payMoney, freight);
                    if (returnMoney >= 0)
                    {
                        string checkQuantityMsg = productRepo.CheckQuantity(cart);
                        if (checkQuantityMsg.Length == 0)
                        {
                            Order o = new Order()
                            {
                                OrderId = orderRepo.CreateNewOrderID(),
                                MemberId = int.Parse(txtMemberID.Text),
                                OrderDate = DateTime.Now,
                                RequiredDate = DateTime.Now,
                                ShippedDate = DateTime.Now,
                                Freight = (decimal)freight

                            };
                            if (productRepo.SubQuantity(cart) == cart.Count && orderRepo.InsertOrder(o) && orderDetailRepo.InsertOrderDetails(cart, o.OrderId, discount))
                            {
                                MessageBox.Show("Tạo hoá đơn thành công!", "Message");
                                txtReturnAmount.Text = returnMoney.ToString();
                                btnSave.IsEnabled = false;
                                btnAdd.IsEnabled = false;
                                btnCancel.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Xin lỗi, không đủ sản phẩm đề cung cấp cho đơn hàng này!", "Error message");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xin lỗi, chúng tôi không đủ số lượng của các sản phẩm: " + checkQuantityMsg, "Error message");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xin lỗi, số tiền khách trả phải >= số tiền hoá đơn!", "Error message");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Xin lỗi, bạn cần nhập đúng % discount, freight và số tiền khách trả!", "Invalid input");
                }
            }
            else
            {
                MessageBox.Show("Xin lỗi, bạn chưa mua gì cả!", "Error message");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to quit?", "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
