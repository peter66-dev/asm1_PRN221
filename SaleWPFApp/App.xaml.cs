﻿using FStoreLibrary.Repository;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton<Login>();
            services.AddSingleton<Admin_Form>();
            services.AddSingleton<Product_Management>();
            services.AddSingleton<Member_Management>();
            services.AddSingleton<Member_Detail>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            //var member_detail = serviceProvider.GetService<Member_Detail>();
            //member_detail.Show();
            Login login = new Login();
            login.Show();
        }
    }
}