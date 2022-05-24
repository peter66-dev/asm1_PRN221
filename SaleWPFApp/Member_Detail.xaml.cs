using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using SaleWPFApp;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Member_Detail.xaml
    /// </summary>
    public partial class Member_Detail : Window
    {
        public bool InsertOrUpdate { get; set; } // true > update
        public Member MemberInfo { get; set; }
        public MemberRepository repo;

        public Member_Detail()
        {
            repo = new MemberRepository();
            InitializeComponent();
            Loaded += Member_Detail_Loaded;

        }

        private void Member_Detail_Loaded(object sender, RoutedEventArgs e)
        {
            txtMemId.IsEnabled = !InsertOrUpdate;
            btnOrderHistory.Visibility = InsertOrUpdate ? Visibility.Visible : Visibility.Hidden;
            if (InsertOrUpdate)//UPDATE => KHÔNG ĐƯỢC UPDATE ROLE - ROLE CHỈ ĐƯỢC TẠO
            {
                txtMemId.Text = MemberInfo.MemberId.ToString();
                txtMemId.IsEnabled = false;
                txtEmail.Text = MemberInfo.Email;
                txtCompanyName.Text = MemberInfo.CompanyName;
                txtPassword.Text = MemberInfo.Password;
                txtCity.Text = MemberInfo.City;
                txtCountry.Text = MemberInfo.Country;
            }
        }

        private bool CheckForm()
        {
            bool check = true;
            if (!int.TryParse(txtMemId.Text, out int id) || id < 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Member ID phải là số nguyên dương!", "Warning message");
            }
            else if (!txtEmail.Text.Contains("@fstore.com"))
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Email không hợp lệ! Phải có \"@fstore.com\"!", "Warning message");
            }
            else if (txtCompanyName.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Company Name không được bỏ trống!", "Warning message");
            }
            else if (txtCity.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin City không được bỏ trống!", "Warning message");
            }
            else if (txtCountry.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Country không được bỏ trống!", "Warning message");
            }
            else if (txtPassword.Text.Trim().Length == 0)
            {
                check = false;
                MessageBox.Show("Xin lỗi, thông tin Password không được bỏ trống!", "Warning message");
            }
            return check;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm())
            {
                string email = txtEmail.Text;
                string company_name = txtCompanyName.Text;
                string city = txtCity.Text;
                string country = txtCountry.Text;
                string password = txtPassword.Text;
                int id = int.Parse(txtMemId.Text);
                Member mem = new Member()
                {
                    MemberId = id,
                    Email = email,
                    CompanyName = company_name,
                    City = city,
                    Country = country,
                    Password = password
                };
                if (InsertOrUpdate) // Update
                {
                    if (repo.UpdateMember(mem))
                    {
                        MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "System message");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin khách hàng không thành công!", "System message");
                    }
                }
                else              // Insert
                {
                    if (repo.InsertMember(mem))
                    {
                        MessageBox.Show("Tạo mới khách hàng thành công!", "System message");
                    }
                    else
                    {
                        MessageBox.Show("Xin lỗi, Member ID này đã tồn tại!", "System message");
                    }
                }
            }
        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            int memID = int.Parse(txtMemId.Text);
            Order_History window = new Order_History(memID);
            window.Show();
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