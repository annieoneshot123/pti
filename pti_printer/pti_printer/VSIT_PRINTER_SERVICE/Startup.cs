using Cloud_Net_Sdk_Hmac_Lib.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using VsitPrinter;
using VsitPrinter.Model;
using Serilog;
using System;

// INJECTION ZIPKIN LIBS START
using zipkin4net;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;
using zipkin4net.Middleware;
using VsitPrinter.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VsitPrinter.Infrastructure.Service;
// INJECTION ZIPKIN LIBS ENDs

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // INJECTION ZIPKIN SETTINGS VARIABLE
            // START
            AppSetting.ZipkinType = Configuration["Zipkin:Type"];
            AppSetting.ZipkinLoggerName = Configuration["Zipkin:LoggerName"];
            AppSetting.ZipkinUrl = Configuration["Zipkin:Url"];
            AppSetting.ZipkipServiceName = Configuration["Zipkin:ServiceName"];
            AppSetting.ConnectionString = Configuration["ConnectionString"];
            // END
            // INJECTION ZIPKIN SETTINGS VARIABLE
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(provider => (Microsoft.Extensions.Configuration.IConfigurationRoot)Configuration);
            services.AddMvc();
            services.AddDbContext<PrinterDbContext>(opt => opt.UseNpgsql(AppSetting.ConnectionString));
            services.AddTransient<IPrinterService, PrinterService>();

            services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ApiDocument", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "Api document",
                    Description = "Api document",
                    TermsOfService = "None"
                });
            });

            services.AddAuthentication(HMACAuthOptions.Scheme).AddScheme<HMACAuthOptions, CustomAuthHandler>(HMACAuthOptions.Scheme, null);
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder =>
               builder.SetIsOriginAllowedToAllowWildcardSubdomains().WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // INJECTION ZIPKIN PUSH MESSAGE
            // START
            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            lifetime.ApplicationStarted.Register(() => {
                TraceManager.SamplingRate = 1.0f;
                var logger = new TracingLogger(loggerFactory, AppSetting.ZipkinLoggerName);
                var httpSender = new HttpZipkinSender(AppSetting.ZipkinUrl, AppSetting.ZipkinType);
                var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
                TraceManager.RegisterTracer(tracer);
                TraceManager.Start(logger);
            });
            lifetime.ApplicationStopped.Register(() => TraceManager.Stop());
            app.UseTracing(AppSetting.ZipkipServiceName);
            // END
            // INJECTION ZIPKIN PUSH MESSAGE

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // loggerFactory.AddFile(Configuration.GetSection("Logging"));
            // loggerFactory.AddFile("Logs/VSITPRO-{Date}.txt");

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/ApiDocument/swagger.json", "ApiDocument");
                c.RoutePrefix = "api-docs";
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            AppSetting.A4_PRINTER_ID = Configuration["AppSettings:A4_PRINTER_ID"];
            AppSetting.A5_PRINTER_ID = Configuration["AppSettings:A5_PRINTER_ID"];
            AppSetting.A4_NORMAL_PRINTER_ID = Configuration["AppSettings:A4_NORMAL_PRINTER_ID"];
            AppSetting.ApiProxyUrl = Configuration["AppSettings:ApiProxyUrl"];
            AppSetting.ContractSlugName = Configuration["AppSettings:ContractSlugName"];
            AppSetting.ClientAppIdSecret.ClientId = Configuration["HMAC_KEY_CONFIG:ClientId"];
            AppSetting.ClientAppIdSecret.SecretKey = Configuration["HMAC_KEY_CONFIG:SecretKey"];

            // Serilog - START
            var appConfig = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json")
                .Build();
            var logConfig = new LoggerConfiguration();
            logConfig.ReadFrom.Configuration(appConfig);
            Log.Logger = logConfig.CreateLogger();
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            // Serilog - END
        }
    }
}
