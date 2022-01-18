using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// configuring ELSA's service
builder.Services.AddElsa(elsa =>
        // using SQLite persistence via Entity Framework
        elsa.UseEntityFrameworkPersistence(ef => ef.UseSqlite())
        .AddConsoleActivities()
        .AddHttpActivities(builder.Configuration.GetSection("Elsa").Bind)
        .AddEmailActivities(builder.Configuration.GetSection("Smtp").Bind)
        .AddQuartzTemporalActivities()
        // this will use reflection to load all workflows (i.e. classes which inherits from IWorkflow interface)
        .AddWorkflowsFrom(typeof(Program).Assembly)
    );

builder.Services.AddElsaApiEndpoints();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseHttpActivities();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(ep =>
{
    ep.MapControllers();
    ep.MapFallbackToPage("/_Host");
});

app.MapRazorPages();
app.Run();