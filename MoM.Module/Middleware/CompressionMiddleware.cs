using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace MoM.Module.Middleware
{
    public class CompressionMiddleware
    {
        private readonly RequestDelegate _next;

        public CompressionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string acceptEncoding = httpContext.Request.Headers["Accept-Encoding"];
            if (acceptEncoding != string.Empty)
            {
                if (acceptEncoding.ToString().IndexOf("gzip", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var stream = httpContext.Response.Body;
                        httpContext.Response.Body = memoryStream;
                        await _next(httpContext);
                        using (var compressedStream = new GZipStream(stream, CompressionLevel.Optimal))
                        {
                            httpContext.Response.Headers.Add("Content-Encoding", new string[] { "gzip" });
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            await memoryStream.CopyToAsync(compressedStream);
                        }
                    }
                }
            }
        }

        public IEnumerable<string> FileExtensions()
        {
            return new string[] {  };
        }
    }
}
