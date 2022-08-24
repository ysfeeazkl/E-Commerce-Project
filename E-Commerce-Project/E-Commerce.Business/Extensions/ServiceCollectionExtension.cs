using E_Commerce.Business.Abstract;
using E_Commerce.Business.AbstractUtilities;
using E_Commerce.Business.Concrete;
using E_Commerce.Business.Utilities;
using E_Commerce.Data.Concrete.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<CommerceContext>(contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerService, LoggerManager>();

            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<ICustomerPictureService, CustomerPictureManager>();
            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IBrandPicturesService, BrandPicturesManager>();
            services.AddScoped<ICategoryAndProductService, CategoryAndProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IFavoriteAndCustomerService, FavoriteAndCustomerManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductPictureService, ProductPictureManager>();
            services.AddScoped<IReportService, ReportManager>();
            services.AddScoped<IReportPictureService, ReportPictureManager>();
            services.AddScoped<ISellerService, SellerManager>();
            services.AddScoped<ISellerPictureService, SellerPictureManager>();
            services.AddScoped<IShoppingCartService, ShoppingCartManager>();

            return services;

        }
    }
}
