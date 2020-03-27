using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Northwind.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CachingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CachingOptions options;

        public CachingMiddleware(RequestDelegate next, CachingOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var absolutePathCacheDirectory = Path.Combine(Environment.CurrentDirectory, options.CacheDirectory);
            Directory.CreateDirectory(absolutePathCacheDirectory);
            var pathToCachedFile = Path.Combine(absolutePathCacheDirectory,
                httpContext.Request.Path.Value.Remove(0, 1).Replace('/', Path.DirectorySeparatorChar) + ".cache");
            // skip further processing if cache exists and not expired
            if (File.Exists(pathToCachedFile))
            {
                if (DateTime.Now.AddMinutes(-options.ExpirationTime) < File.GetCreationTime(pathToCachedFile))
                {
                    using (var fileStream = File.OpenRead(pathToCachedFile))
                    {
                        fileStream.Seek(0, SeekOrigin.Begin);
                        fileStream.CopyTo(httpContext.Response.Body);
                        return;
                    }
                }

                File.Delete(pathToCachedFile);
            }

            //proceed with usual request if cached image was not found or was cleared

            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;
                await next(httpContext);
                httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                if (httpContext.Response.ContentType == "image/png" &&
                    Directory.GetFiles(absolutePathCacheDirectory, "*", SearchOption.AllDirectories).Length <
                    options.MaxCacheSize)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(pathToCachedFile));
                    using (var fileStream = File.Create(pathToCachedFile))
                    {
                        httpContext.Response.Body.CopyTo(fileStream);
                        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    }
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }

    public class CachingOptions
    {
        public string CacheDirectory { get; set; }

        /// <summary>
        ///     Expiration time in minutes.
        /// </summary>
        public int ExpirationTime { get; set; }

        /// <summary>
        ///     Max count of cached images.
        /// </summary>
        public int MaxCacheSize { get; set; }
    }

    public static class CachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCachingMiddleware(this IApplicationBuilder builder, CachingOptions options)
        {
            return builder.UseMiddleware<CachingMiddleware>(options);
        }
    }
}