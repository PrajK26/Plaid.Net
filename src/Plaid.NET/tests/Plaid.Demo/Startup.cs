using Acklann.Plaid.Demo.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acklann.Plaid.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddSingleton<PlaidCredentials>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseNodeModules(env.ContentRootPath);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{Controller=Home}/{Action=Index}");
            });
        }

        public class CustomContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);

                // Check if the member has JsonProperty attribute
                if (member is System.Reflection.PropertyInfo propertyInfo)
                {
                    var attributes = propertyInfo.GetCustomAttributes(typeof(JsonPropertyAttribute), true);
                    if (attributes.Length > 0)
                    {
                        var jsonPropertyAttribute = attributes[0] as JsonPropertyAttribute;
                        property.PropertyName = jsonPropertyAttribute.PropertyName; // Use JsonProperty name instead of member name
                    }
                }

                return property;
            }
        }
    }
}