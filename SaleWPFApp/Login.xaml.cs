using FStoreLibrary.DataAccess;
using FStoreLibrary.Repository;
using SalesWPFApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        MemberRepository memberRepository;
        public Login()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
        }

        private Account GetDataFromJsonFile()
        {
            Account admin = new Account();
            try
            {
                string json = File.ReadAllText("appsettings.json");
                admin = JsonSerializer.Deserialize<Account>(json, null);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return admin;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUserId.Text.ToString();
            string password = txtPassword.Password.ToString();
            Account admin = GetDataFromJsonFile();
            if (email.Equals(admin.Email) && password.Equals(admin.Password))
            {
                Admin_Form window = new Admin_Form();
                window.isAdmin = true;
                window.Show();
            }
            else
            {
                bool isLogined = false;
                List<Member> accounts = memberRepository.GetAllMembers();
                foreach (var tmp in accounts)
                {
                    if (tmp.Email.Equals(email) && tmp.Password.Equals(tmp.Password)) // Member login
                    {
                        isLogined = !isLogined;
                        Member_Detail window = new Member_Detail();
                        window.InsertOrUpdate = true;
                        window.MemberInfo = tmp;
                        window.Show();
                    }
                }
                if (!isLogined)
                {
                    MessageBox.Show("Tài khoản và mật khẩu không hợp lệ!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to quit?", "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Member_Detail member_detail = new Member_Detail();
            member_detail.InsertOrUpdate = false;
            member_detail.Show();
        }
    }
}