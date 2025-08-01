using DevXpert.Store.Core.Application.App;
using DevXpert.Store.Core.Application.Helpers;
using DevXpert.Store.Core.Business.Interfaces.Repositories;
using DevXpert.Store.Core.Business.Interfaces.Services;
using DevXpert.Store.Core.Business.Services;
using DevXpert.Store.Core.Business.Services.Notificador;
using DevXpert.Store.Core.Data.Context;
using DevXpert.Store.Core.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Store.Core.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder ResolveDependecies(this WebApplicationBuilder builder)
        {
            #region APPServices
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IdentityDbContext, AppDbContext>();
            builder.Services.AddScoped<INotificador, Notificador>();
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            #endregion

            #region SERVICES
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IArquivoService, ArquivoService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IFavoritoService, FavoritoService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<ICategoriaHelperService, CategoriaHelperService>();
            #endregion

            #region REPOSITORIES
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            #endregion

            return builder;
        }
    }
}