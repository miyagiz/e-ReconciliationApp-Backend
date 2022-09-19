﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Reconciliation.Core.CrossCuttingConcerns.Caching;
using Reconciliation.Core.CrossCuttingConcerns.Caching.Microsoft;
using Reconciliation.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
