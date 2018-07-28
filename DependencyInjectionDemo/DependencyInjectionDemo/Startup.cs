using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using DependencyInjectionDemo.Domain;
using DependencyInjectionDemo.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 使用.net core自带的依赖注入容器实现
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();
        //    // 使用单例模式进行注入 注入MSSQL实现
        //    //services.AddSingleton<IUserRepository, UserMSSQLRepository>();
        //    // 使用单例模式进行注入 注入Oracle实现
        //    services.AddSingleton<IUserRepository, UserOracleRepository>();
        //}


        /// <summary>
        /// 使用第三方依赖注入容器AutoFac实现依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(60);
            });
            var containerBuilder = new ContainerBuilder();
            // 使用单例模式进行注入 注入MSSQL实现
            //containerBuilder.RegisterType<UserMSSQLRepository>().As<IUserRepository>().SingleInstance();
            // 使用单例模式进行注入 注入Oracle实现
            containerBuilder.RegisterType<UserOracleRepository>().As<IUserRepository>().SingleInstance();

            containerBuilder.Populate(services);

            IContainer container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
