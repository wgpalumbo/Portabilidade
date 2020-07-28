using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portabilidade.Domain.Entities;
using Portabilidade.Domain.Repositories;
using Portabilidade.Infra.Repository;
using Portabilidade.Service.Util;

namespace Portabilidade.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.KnownProxies.Add(System.Net.IPAddress.Parse("192.168.1.13"));
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // services.Add(new ServiceDescriptor(typeof(ISqliteRepository<Solicitacao>), typeof(SqliteSolicitacaoRepository), ServiceLifetime.Transient));
            // services.Add(new ServiceDescriptor(typeof(ISqliteRepository<Cliente>), typeof(SqliteClienteRepository), ServiceLifetime.Transient));
            // services.Add(new ServiceDescriptor(typeof(IValidarStrategy), typeof(ValidarCnpj), ServiceLifetime.Transient));
            // services.Add(new ServiceDescriptor(typeof(IValidarStrategy), typeof(ValidarCpf), ServiceLifetime.Transient));
            // services.Add(new ServiceDescriptor(typeof(IQualValidador), typeof(QualValidador), ServiceLifetime.Transient));

            services.AddScoped<ISqliteRepository<Solicitacao>, SqliteSolicitacaoRepository>();
            services.AddScoped<ISqliteRepository<Cliente>, SqliteClienteRepository>();

            //services.AddTransient<IClienteValidator, ClienteValidator>();

            services.AddTransient<IQualValidar, QualValidar>();

            services.AddTransient<IValidarStrategy, ValidarCnpj>();
            services.AddTransient<IValidarStrategy, ValidarCpf>();

            //services.AddTransient<IValidarStrategy, ValidarCnpj>(p => p.GetService<ValidarCnpj>());
            //services.AddTransient<IValidarStrategy, ValidarCpf>(p => p.GetService<ValidarCpf>());

            //services.AddTransient<IValidarStrategy, ValidarCpf>(p => p.GetService<ValidarCpf>());

            services.AddSwaggerGen(c => { c.EnableAnnotations(); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            //Obriga toda http ser https
            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            // // using Microsoft.Extensions.FileProviders;
            // // using System.IO;
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(env.ContentRootPath, "MyStaticFiles")),
            //     RequestPath = "/StaticFiles"
            // });



            //ATENÇÃO!!! Liberado
            app.UseCors(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
