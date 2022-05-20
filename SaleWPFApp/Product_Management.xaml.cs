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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Product_Management.xaml
    /// </summary>
    public partial class Product_Management : Window
    {
        public bool isAdmin { get; set; } = false;
        IProductRepository productRepository;
        public Product_Management(IProductRepository repo)
        {
            InitializeComponent();
            productRepository = repo;
        }

        private bool CheckForm()
        {
            bool check = true;
            if (txtProId.Text.Trim().Length == 0 || !int.TryParse(txtProId.Text.ToString(), out int proID) && proID > 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Product ID không hợp lệ!", "Warning message");
            }
            else if (!int.TryParse(txtCateId.Text.ToString(), out int cateID) || cateID < 1 || cateID > 2)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Category ID không hợp lệ!", "Warning message");
            }
            else if (txtProductName.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Product Name không hợp lệ!", "Warning message");
            }
            else if (txtWeight.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Weight không hợp lệ!", "Warning message");
            }
            else if (txtUnitPrice.Text.Trim().Length == 0 || !decimal.TryParse(txtUnitPrice.Text.ToString(), out decimal price) || price < 1000)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Unit Price không hợp lệ! Unit Price phải lớn hơn 1000VNĐ!", "Warning message");
            }
            else if (!Int32.TryParse(txtUnitsInStock.Text.ToString(), out int units) || units < 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Units in stock không hợp lệ!", "Warning message");
            }
            return check;
        }

        private Product GetProductObject()
        {
            Product pro = null;
            try
            {
                pro = new Product
                {
                    ProductId = int.Parse(txtProId.Text.ToString()),
                    CategoryId = int.Parse(txtCateId.Text.ToString()),
                    ProductName = txtProductName.Text.ToString(),
                    Weight = txtWeight.Text.ToString(),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text.ToString()),
                    UnitsInStock = int.Parse(txtUnitsInStock.Text.ToString()),

                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at GetProductObject() function!");
            }
            return pro;
        }

        private void LoadProductList()
        {
            lvProducts.ItemsSource = productRepository.GetAllProducts();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnLoad_Click function!");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckForm())
                {
                    Product pro = GetProductObject();
                    if (productRepository.GetProductByID(pro.ProductId) == null)
                    {
                        if (productRepository.InsertProduct(pro))
                        {
                            LoadProductList();
                            MessageBox.Show($"Inserted {pro.ProductName} successfully!", "Insert message");
                        }
                        else
                        {
                            MessageBox.Show($"Inserting failed!", "Insert message");
                        }
                    }
                    else //đã tồn tại trong system!
                    {
                        MessageBox.Show($"ID của sản phẩm này đã tồn tại!", "Insert error");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnInsert_Click function!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckForm())
                {
                    Product pro = GetProductObject();
                    if (productRepository.GetProductByID(pro.ProductId) != null)
                    {
                        if (productRepository.UpdateProduct(pro))
                        {
                            LoadProductList();
                            MessageBox.Show($"Updated {pro.ProductName} successfully!", "Update message");
                        }
                        else
                        {
                            MessageBox.Show($"Nothing to update!", "Update message");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Xin lỗi, mã sản phẩm này không tồn tại!", "Update message");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnUpdate_Click function!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int proID = int.Parse(txtProId.Text.ToString());
                Product pro = productRepository.GetProductByID(proID);
                if (productRepository.DeleteProduct(proID))
                {
                    LoadProductList();
                    MessageBox.Show($"Deleted {pro.ProductName} successfully!", "Delete message");
                }
                else
                {
                    MessageBox.Show($"Deleting failed!", "Delete message");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error at btnDelete_Click function!");
            }
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