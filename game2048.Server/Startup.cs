using game2048.Data;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Mime;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace game2048.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.TryAddSingleton<IResponseCompressionProvider, ResponseCompressionProvider>();
            //ConnectionStringSettingsCollection settings =
            //ConfigurationManager.ConnectionStrings;

            //if (settings != null)
            //{
            //    foreach (ConnectionStringSettings cs in settings)
            //    {
            //        Console.WriteLine(cs.Name);
            //        Console.WriteLine(cs.ProviderName);
            //        Console.WriteLine(cs.ConnectionString);
            //    }
            //}
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddXmlFile("app.config").Build();
            var connectionString = configuration["connectionStrings:add:MyKey:connectionString"];
            //services.AddDbContext<Context>(
            //    opts => opts.UseNpgsql(connectionString.ConnectionString)
            //);
            services.AddEntityFrameworkNpgsql().AddDbContext<Context>(options => options.UseNpgsql(connectionString));
            //services.AddDbContext<Context>(options =>
            //options.UseSqlServer("(LocalDB)\\MSSQLLocalDB"));//System.Configuration.GetConnectionString("DefaultConnection")));

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Program>();
        }
    }
}
