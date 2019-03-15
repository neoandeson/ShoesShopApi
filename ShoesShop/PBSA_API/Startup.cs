using AutoMapper;
using DataService.Infrastructure;
using DataService.Models;
using DataService.Repositories;
using DataService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace PBSA_API
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            //Add AutoMapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Add context and Transient for DI
            services.AddDbContext<ShoesShopContext>();

            //Repositories
            services.AddTransient<IShoesRepository, ShoesRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IShoesHasSizeRepository, ShoesHasSizeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();

            //Services
            services.AddTransient<IShoesService, ShoesService>();
            services.AddTransient<IShoesHasSizeService, ShoesHasSizeService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ISizeService, SizeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "PBSA");
            });

            app.UseCors("AllowMyOrigin");


            app.UseMvc();
        }
    }
}
