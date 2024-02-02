﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SuggestionAppUI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddMemoryCache();

        //the DBConnection which has the connection to the mongo database as a singleton
        //One database connection for everybody to use instead of AddScoped which is a singleton per user
        builder.Services.AddSingleton<IDBConnection, DBConnection>();
        //AddSingleton used same as DBConnection, instead of AddTransient which creates new instance every time there is a call.
        //Because CategoryData has access to the cache and collection which are not unique and can be shared accross users
        //All the ones below rely on DBConnection and MemoryCache
        builder.Services.AddTransient<ICategoryData, MongoCategoryData>();
        builder.Services.AddSingleton<IStatusData, MongoStatusData>();
        builder.Services.AddSingleton<IUserData, MongoUserData>();
        builder.Services.AddSingleton<ISuggestionData, MongoSuggestionData>();
        builder.Services.AddSingleton<IUserData, MongoUserData>();
    }
}