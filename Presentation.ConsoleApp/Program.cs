﻿using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    //services.AddDbContext<UserContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Education\datalagring\assignment\Datalagring_Assignment\Infrastructure\Data\user_database_cf.mdf;Integrated Security=True;Connect Timeout=30"));
    //services.AddDbContext<ProductCatalogContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Education\datalagring\assignment\Datalagring_Assignment\Infrastructure\Data\productcatalog_database_df.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAuthRepository, AuthRepository>();
    services.AddScoped<IProfileRepository, ProfileRepository>();
    services.AddScoped<IAddressRepository, AddressRepository>();

    services.AddScoped<IUserService, UserService>();
}).Build();