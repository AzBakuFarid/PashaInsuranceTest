using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PashaInsuranceTest.Data;
using PashaInsuranceTest.DbEntities.Models;
using PashaInsuranceTest.Filters;
using PashaInsuranceTest.Repository;
using PashaInsuranceTest.Services;

namespace PashaInsuranceTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMvc(mvc => mvc.Filters.Add(new AppExceptionFilter())).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "P_I_Test"));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddControllers().AddNewtonsoftJson(); 
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IClientService, ClientService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
