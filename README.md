# BR3WOK (Busol R3 Framework) Backend Template
Backend solution template used for speed up new development process on Regional 3 PT. Pertamina Hulu Indonesia built up on top of .Net Core 6.0.

The idea is to bring together the best and essential practices of .Net Core 6.0 with Clean Onion Architecture and bring on S-I-D Principles picked from SOLID Principles. Essentials feature used for Regional 3 environment application will be included on this template such as R3tina integration, Workflow integration, Oauth, R3 mailer, etc.

You can read more about **Onion Architecture** on **[here](https://medium.com/expedia-group-tech/onion-architecture-deed8a554423)** and about **SOLID Principles** on **[here](https://www.digitalocean.com/community/conceptual-articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)**

<details><summary>Pre-Requisites</summary>

- Visual Studio 2022, **or** Visual Studio Code
- [.Net Core 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (included if you install Visual Studio 2022)
- dotnet ef tools ([install instructions](http://10.18.0.57:8081/region3/r3application_template/r3backend_template_netcore6/-/blob/main/README.md#install-dotnet-ef-tools))
- if something error, try ([this link first](https://stackoverflow.com/questions/73247933/how-to-fix-no-net-sdks-were-found-error-vscode)) 
</details>

<details><summary>How To</summary>
<details><summary>New Development</summary>

1. Open Visual Studio.
2. Create New Project.
3. Search **class library** (personal preferences. Smallest project, quick initialization). Click Class Library, then click Next.
4. Leave **Project Name** as default but change the **Location** and **Solution name** to proper location and name.
5. Choose .Net 6.0.
6. Click Create and wait until VS Solution generated.
7. Right click on **ClassLibrary1** project and click Remove.
8. Go to File Explorer. Open R3 Framework Backend Template and copy all folder inside src folder.
9. Paste copied R3 Framework Backend Template to new project folder.
10. Back to Visual Studio.
11. Right click on Solution Name, then click **Add** then **Existing Project**.
12. Go to new project folder, and click Core Folder.
13. Double click ```Core.csproj``` file.
14. Do step 11 to 13 again for ```Data```, ```Domain```, ```Infrastructure```, and ```WebApi```.

    > Please add the project with correct order.    
    > Core --> Data --> Domain --> Infrastructure --> WebApi

15. Open ```Program.cs``` on **WebApi** Project.
16. Change all the ```[Application Name]``` value with correct name.
17. Right click on ```WebApi``` Project and click ```Set as Startup Project```
18. Run the project
</details>


<details><summary>Import Existing Table</summary>

1. Open project solution on Visual Studio 2022
2. Tools > NuGet Package Manager > Package Manager Console
3. paste this into the console
```
scaffold-dbcontext 'Server=phmsqldev01.pertamina.com\DEVSQL01;Database=ICTAssetMgmt;User Id=ictassetusr;Password=!ctAsset;TrustServerCertificate=True;' -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir test -schema dbo
```
4. makesure before running these script, you can clean and rebuild your solution
5. you can apply changes to db connection string, ef core provider, what to scaffold(entire schema or specific table/view) and output directory
6. put model output from schema (ex: BusProcess.cs, Organization.cs, WorkLoad.cs) into Core > Models > Entities > Tables / Views
7. create Interface Repository that inhert IBaseRepository per generated model
8. implement that interface in Data > Repositories > Tables / Views
9. create orm per table config in Data > EntityConfigs > Table / Views, contents can be copied from generated context on OnModelCreating method
10. move DBSet statement from generated application context to Data > ApplicationContext.cs
11. assign Interface of Repository into Core > IUnitOfWork.cs and then implement that in Data > UnitOfWork.cs
12. you can proceed to Development Flow

</details>

<details><summary>Create New Table</summary>

1. Open Visual Studio.
2. dotnet ef migrations add [MigrationName] -c ApplicationContext -p src\Data -s src\WebApi
3. dotnet ef database update -c ApplicationContext -p src\Data -s src\WebApi
4. to be continue...
</details>

<details><summary>Development Flow</summary>

1. After you finished Import Existing Table (because we use database first method), you can focus on business process on Domain Project
2. first you can observe these 2 subfolder Domain > Example and Domain > R3Example as an example
3. each of these subfolder consist of 3 elements(interface, dto and service/implementation)
4. interface: define your business needs using interface
5. dto: output data for your api based on frontend requirement
6. you can utilize AutoMapper to convert db Model <-> dto Model and vice versa, have a look at Domain > AutoMapProfile.cs
7. service: implementation of your business process here
8. after you finished the services in Domain Project, you can either create unit test for that service or you can create new api endpoint that will consume your newly created service
9. we highly recommend that you create unit test first before creating new api endpoint
10. when creating new api endpoint(controller), you must register your newly created service at \src\Infrastructure\AppServiceSetup.cs.
11. after that, you can call that service using dependency injection method, see \src\WebApi\Controllers\_Example\ExampleTableController.cs for pattern example.
9. don't forget to log your backend activity specifically when handling errors ;)
10. happy coding :)
</details>
</details>

<details><summary>Library & Packages</summary>

R3 Framework Backend Template have dependencies to some library packages.

|Packages|Link|Description|
|--------|------|----|
|AutoMapper|https://automapper.org/|Auto map property from database models to view models|
|ExcelDataReader|https://github.com/ExcelDataReader/ExcelDataReader|For read excel files|
|EF Core|https://learn.microsoft.com/en-us/ef/core/|ORM - Object Relational Mapper|
|FluentResults|https://github.com/altmann/FluentResults|Custom return response|
|Hangfire|https://www.hangfire.io/|For background jobs automation (replacing windows service)|
|NUnit|https://docs.nunit.org/index.html|Unit Test for .Net Core|
|Serilog|https://serilog.net/|Logging library|
|Swagger|https://swagger.io/|API Documentation|
</details>

<details><summary>Additional</summary>

### Install dotnet ef tools
1. Open Visual Studio 2022
2. Open Package Manager Console
3. Install dotnet ef tools
   ```
   dotnet tool install --global dotnet-ef
   ```
4. For more reference, you can check here: https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=vs
</details>

