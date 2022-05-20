using FStoreLibrary.DataAccess;
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
    /// Interaction logic for Member_Detail.xaml
    /// </summary>
    public partial class Member_Detail : Window
    {
        public bool InsertOrUpdate { get; set; } // true > update
        public Member MemberInfo { get; set; }
        public Member_Detail()
        {
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
            else
            {
                txtMemId.Visibility = Visibility.Hidden;
                lbMemId.Visibility = Visibility.Hidden;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}