using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TextMessenger.DataAccessLayer;
using TextMessenger.DataAccessLayer.Interfaces;
using TextMessenger.DataAccessLayer.Repositories;
using TextMessenger.Hubs;
using TextMessenger.MapperProfiles.AuthorizationProfiles;
using TextMessenger.MapperProfiles.ChatProfiles;
using TextMessenger.MapperProfiles.MessageProfiles;
using TextMessenger.MapperProfiles.UserProfiles;
using TextMessenger.Services.Interfaces;
using TextMessenger.Services.Services;

namespace TextMessenger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connection));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            builder.Services.AddMvcCore().AddApiExplorer();

            builder.Services.AddSignalR();

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IContactsService, ContactsService>();
            builder.Services.AddTransient<IChatsService, ChatsService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IChatRepository, ChatRepository>();
            builder.Services.AddTransient<IMessageRepository, MessageRepository>();

            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            builder.Services.AddAutoMapper(
                typeof(LoginViewModelToLoginDTOMapperProfile),
                typeof(RegistrationDTOToUserMapperProfile),
                typeof(RegistrationDTOToUserMapperProfile),
                typeof(UserToUserMainInfoDTOMapperProfile),
                typeof(ChatToChatListElementMapperProfile),
                typeof(ChatToSelectedChatDTOMapperProfile),
                typeof(MessageToMessageDTOMapperProfile),
                typeof(ReceivedMessageDTOToMessageMapperProfile),
                typeof(UserMainInfoDTOListToNewChatViewModelMapperProfile),
                typeof(NewChatViewModelToNewChatDTOMapperProfile)
                );





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });

            app.Run();
        }
    }
}