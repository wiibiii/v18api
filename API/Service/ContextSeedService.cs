﻿using API.Data;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Service
{
    public class ContextSeedService
    {
        private readonly Context context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly BloggieDbContext bloggieDbContext;

        public ContextSeedService(Context context,
            BloggieDbContext bloggieDbContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task InitializeContextAsync()
        {
            if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                //applies pending migrations
                await context.Database.MigrateAsync();
            }

            if(bloggieDbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                await bloggieDbContext.Database.MigrateAsync();
            }

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = SD.AdminRole });
                await roleManager.CreateAsync(new IdentityRole { Name = SD.ManagerRole });
                await roleManager.CreateAsync(new IdentityRole { Name = SD.PlayerRole });
            }

            if (!userManager.Users.AnyAsync().GetAwaiter().GetResult())
            {
                var admin = new User { FirstName = "admin", LastName = "jackson", UserName = SD.AdminUserName, Email = SD.AdminUserName, EmailConfirmed = true };
                await userManager.CreateAsync(admin, "123456");
                await userManager.AddToRolesAsync(admin, new[] { SD.AdminRole, SD.ManagerRole, SD.PlayerRole });
                await userManager.AddClaimsAsync(admin,
                    new Claim[] {
                        new Claim(ClaimTypes.Email, admin.Email),
                        new Claim(ClaimTypes.Surname, admin.LastName),
                        new Claim(ClaimTypes.NameIdentifier, admin.Id),
                        new Claim(ClaimTypes.GivenName, admin.FirstName)
                });

                var manager = new User { FirstName = "manager", LastName = "wilson", UserName = "manager@example.com", Email = "manager@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(manager, "123456");
                await userManager.AddToRoleAsync(manager, SD.ManagerRole);
                await userManager.AddClaimsAsync(manager,
                    new Claim[] {
                        new Claim(ClaimTypes.Email, manager.Email),
                        new Claim(ClaimTypes.Surname, manager.LastName),
                        new Claim(ClaimTypes.NameIdentifier, manager.Id),
                        new Claim(ClaimTypes.GivenName, manager.FirstName)
                });

                var player = new User { FirstName = "player", LastName = "bi", UserName = "player@example.com", Email = "player@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(player, "123456");
                await userManager.AddToRoleAsync(player, SD.PlayerRole);
                await userManager.AddClaimsAsync(player,
                    new Claim[] {
                        new Claim(ClaimTypes.Email, player.Email),
                        new Claim(ClaimTypes.Surname, player.LastName),
                        new Claim(ClaimTypes.NameIdentifier, player.Id),
                        new Claim(ClaimTypes.GivenName, player.FirstName)
                });

                var vipplayer = new User { FirstName = "vipplayer", LastName = "vip", UserName = "vipplayer@example.com", Email = "vipplayer@example.com", EmailConfirmed = true };
                await userManager.CreateAsync(vipplayer, "123456");
                await userManager.AddToRoleAsync(vipplayer, SD.PlayerRole);
                await userManager.AddClaimsAsync(vipplayer,
                    new Claim[] {
                        new Claim(ClaimTypes.Email, vipplayer.Email),
                        new Claim(ClaimTypes.Surname, vipplayer.LastName),
                        new Claim(ClaimTypes.NameIdentifier, vipplayer.Id),
                        new Claim(ClaimTypes.GivenName, vipplayer.FirstName)
                });
            }

        }

    }
}
