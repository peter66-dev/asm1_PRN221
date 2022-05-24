using FStoreLibrary.Repository;
using Microsoft.Extensions.DependencyInjection;
using SaleWPFApp;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton<Login>();
            services.AddSingleton<Admin_Form>();
            services.AddSingleton<Product_Management>();
            services.AddSingleton<Member_Management>();
            services.AddSingleton<Member_Detail>();
            services.AddSingleton<Order_Management>();
            services.AddSingleton<OrderDetail_Management>();
            services.AddSingleton<Create_Order>();
            services.AddSingleton<Order_History>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var window = serviceProvider.GetService<Login>();
            window.Show();
            //var login = serviceProvider.GetService<Login>();
            //login.Show();
        }
    }
}