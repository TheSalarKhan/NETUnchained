using System.Text;
using Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DataSeeder
{
    // TODO: Move this code when seed data is implemented in EF 7

    /// <summary>
    /// This is a workaround for missing seed data functionality in EF 7.0-rc1
    /// More info: https://github.com/aspnet/EntityFramework/issues/629
    /// </summary>
    /// <param name="app">
    /// An instance that provides the mechanisms to get instance of the database context.
    /// </param>
    

    public static void CreateDummyUsers(ApplicationDbContext db) {
        db.Users.Add(new Users(db)
        {
            Email = "user.email",
            Phone = "user.phone",
            Password = "kasd",
            TokenActivate = "token",
            EmailVerified = 0,
            PushId = "No Push",
            Name = "ali",
            // the user.profileImage is a base64, we can't insert it into the db
            // its a huge string..... its bad! 
            ProfileImage = new BinaryData(Encoding.UTF8.GetBytes(""))
        });

        db.Users.Add(new Users(db)
        {
            Email = "user.email",
            Phone = "user.phone",
            Password = "kasd",
            TokenActivate = "token",
            EmailVerified = 0,
            PushId = "No Push",
            Name = "ali",
            // the user.profileImage is a base64, we can't insert it into the db
            // its a huge string..... its bad! 
            ProfileImage = new BinaryData(Encoding.UTF8.GetBytes(""))
        });

        db.SaveChanges();
    }


    public static void CreateAdminLogin(ApplicationDbContext db) {
        db.AdminLogins.Add(new AdminLogin()
        {
            UserName = "admin",
            Password = Utils.EncryptionUtils.CreateMD5("admin@123")
        });

        db.SaveChanges();
    }
    public static void SeedData(this IApplicationBuilder app)
    {
        var db = app.ApplicationServices.GetService<Application.Models.ApplicationDbContext>();

        //PopulateAuditFormTemplates(db);

        //CreateDummyUsers(db);

        //CreateAdminLogin(db);

        db.SaveChanges();
    }
}