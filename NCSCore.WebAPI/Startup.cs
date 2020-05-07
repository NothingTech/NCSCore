using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NCSCore.Dao.Implements;
using NCSCore.Dao.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using NCSCore.Entity;

namespace NCSCore.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "AllowSpecificOrigins";//��˿������
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NCSContext>(options =>
            {

                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });


            //���ÿɿ������
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                     builder =>
                     {
                         builder.AllowAnyOrigin();
                         builder.AllowAnyHeader();
                         builder.AllowAnyMethod();

                     });


            });
            DIRegister(services);
            //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "ͨ�ð�1.0",
                    Title = "NCS��˽ӿ��ĵ�",
                    Description = "�ӿ�����"
                }) ;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddControllersAsServices().AddNewtonsoftJson
              (
                  json =>
                  {
                      //ͳһ����JsonResult
                      json.SerializerSettings.ContractResolver = new DefaultContractResolver();
                      json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                  }
              );

            //services.AddMvc(filter => filter.Filters.Add<ModelFilter>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);//UseCorsһ��Ҫ����UseRouting������UseEndpoints����֮��

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(p =>
            {
                p.SwaggerEndpoint("/swagger/v1/swagger.json", "Web App V1");
                p.RoutePrefix = string.Empty;//���ø��ڵ����
            });
            //�Զ����м��
            //app.UseMiddleware<ConfigMiddleware>();
        }
        // ��������ע��ӳ���ϵ
        public void DIRegister(IServiceCollection services)
        {

            services.AddTransient(typeof(IBaseDao<>), typeof(BaseDao<>));
            #region BLL��ע��

            #endregion
        }
    }
}
