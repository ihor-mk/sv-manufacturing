﻿using SunVita.Core.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace SunVita.Core.WebApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCodiCoreContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<SunVitaCoreContext>();
            context?.Database.Migrate();
        }
    }
}
