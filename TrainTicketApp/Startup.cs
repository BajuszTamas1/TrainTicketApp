using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainTicketApp.Data;
using TrainTicketApp.Services;


public class Startup
{
    public void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite("Data Source=trains.db"));
    services.AddScoped<ITrainService, TrainService>();
    services.AddScoped<IOrderService, OrderService>();
    services.AddScoped<ITrainDepartureTimeService, TrainDepartureTimeService>();
    services.AddRazorPages();
}

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceCollection services)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
    }
}