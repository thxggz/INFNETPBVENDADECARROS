using INFNETPBVENDADECARROS.Data;
using INFNETPBVENDADECARROS.Services;
using INFNETPBVENDADECARROS.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace INFNETPBVENDADECARROS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona Razor Pages com NToastNotify
            builder.Services.AddRazorPages()
                .AddNToastNotifyToastr(new ToastrOptions
                {
                    ProgressBar = true,
                    PositionClass = ToastPositions.BottomRight,
                    TimeOut = 5000,
                    PreventDuplicates = true
                });

            // Configura o contexto do banco com a connection string "MyConn"
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")
                    ?? throw new InvalidOperationException("Connection string 'MyConn' not found.")));

            // Configura Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Configurações extras do Identity
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;

                options.Lockout.MaxFailedAccessAttempts = 30;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            // Protege pastas específicas
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Veiculos");
                options.Conventions.AuthorizeFolder("/Marcas");
            });

            // ✅ REGISTRO DO SERVIÇO PERSONALIZADO
            builder.Services.AddScoped<VeiculoService>();

            var app = builder.Build();


            // Pipeline de requisição HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNToastNotify();

            app.MapRazorPages();

            app.Run();
        }
    }
}
