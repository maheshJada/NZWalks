var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// Azure deployemnt Notes
//1. Create resource grouo and create the app services 
//3. create sql servcue data base
//need to write ip and address in sql azure side
//after that u will able to see azure sql in sql serer side


//open vs
//1.add account creat azure servide
//2.Right click on project go to publish window 
//3. clecin on specfic target , so cliecl on azure appservces
//4.it will shoow resource griop
//5. api management 
//6. deployemnt  notes publish finish
//7.go to prop and go to publisj
//8..connect dependency clickc on sql connect
//9.
//cleick on next click on databse amnd password
//10.azure app sertting 
//11.finish
//12.publich connect on bottom of 3 dots
//connect sql
//13.
//then publish on top of the windo
//14.i
//opn azure app servicwe insice app service go to configuration 
//15
//rihnt clieck on project go to publish and edit
//16.
//click on apply migraton on publish  in tha entity frmework migtation
//17.
//go to app servcie click on app servcie 
//18. advamce dtools chcke tools
//18. exntibel app servcie go to debug consule\

//19. write a command 
//wwwroot
//wwwroot
//Dotnet nzwalks.api.dll

//20. rihnt click on publish and clcik on imagesg
//21.
//need to check jwt before publish 
