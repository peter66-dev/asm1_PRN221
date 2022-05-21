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
    /// Interaction logic for Member_Management.xaml
    /// </summary>
    public partial class Member_Management : Window
    {
        IMemberRepository repo;
        public Member_Management()
        {
            InitializeComponent();
            repo = new MemberRepository();
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

        private Member GetMemberObject()
        {
            Member mem = null;
            try
            {
                mem = new Member
                {
                    MemberId = int.Parse(txtMemId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member object");
            }
            return mem;
        }

        public void LoadMemberList()
        {
            lvMembers.ItemsSource = repo.GetAllMembers();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckForm())
                {
                    Member member = GetMemberObject();
                    if (repo.InsertMember(member))
                    {
                        LoadMemberList();
                        MessageBox.Show($"{member.Email} inserted successfully!", "Insert member");
                    }
                    else
                    {
                        MessageBox.Show("Xin lỗi, Member ID này đã tồn tại!", "Insert member");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert member");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckForm())
                {
                    Member member = GetMemberObject();
                    if (repo.UpdateMember(member))
                    {
                        LoadMemberList();
                        MessageBox.Show($"{member.Email} updated successfully!", "Update member");
                    }
                    else
                    {
                        MessageBox.Show("Xin lỗi, cập nhật thông tin khách hàng thất bại!", "Insert member");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetMemberObject();
                if (MessageBox.Show($"Bạn thật sự muốn xoá {member.Email}?", "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (repo.DeleteMember(member.MemberId))
                    {
                        LoadMemberList();
                        MessageBox.Show($"{member.Email} deleted successfully!", "Delete member");
                    }
                    else
                    {
                        MessageBox.Show("Xin lỗi, xoá thông tin khách hàng thất bại!", "Insert member");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
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