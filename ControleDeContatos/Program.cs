using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.repositorio;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();  /*liberação do FILHO DA PUTA DO CORS*/
            builder.Services.AddControllers(); /*para  a API funcionar, inserir o LaunchURL*/
            builder.Services.AddEndpointsApiExplorer();/*para  a API funcionar, inserir o LaunchURL*/
            builder.Services.AddSwaggerGen();/*para o swagger funcionar, inserir o LaunchURL*/
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEntityFrameworkSqlServer()/*Vou usar SQL seer*/
                .AddDbContext<BancoContext>(o =>
                {
                    {
                        o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"));
                    };
                });/*e o contexto é esse!*/

            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<ISessao, Sessao>();
            builder.Services.AddSingleton<IEmail, Email>();


            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            }
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

            }
            app.UseStaticFiles();

            /*Para o swagger funcionar*/
            app.UseSwagger();

            app.UseSwaggerUI();

            app.MapControllers();

            /*FIM SWAGGER*/

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            /*liberação do FILHO DA PUTA DO CORS*/
            app.UseCors(x => x.AllowAnyMethod()
                              .AllowAnyHeader()
                              .SetIsOriginAllowed(origin => true)
                              //.WithOrigins("https://localhost:44351"));
                              .AllowCredentials());

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();

        }
    }
}