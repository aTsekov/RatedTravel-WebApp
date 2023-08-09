# Introduction
- This is a Project Defense for the ASP.NET Advanced course at SoftUni - August 2023.
- "Rated Travel" is a user-friendly web application designed for travelers who want to plan their trips better. It allows users to explore different cities and gather essential information about each one. The application provides a comprehensive list of restaurants and bars in each city. Users can read reviews about these establishments and even contribute their own opinions. This feature enables travelers to make informed decisions about where to eat and drink during their trips.

In essence, "Rated Travel" simplifies travel planning by offering insights into cities and helping users find great places to dine and unwind.


## :ledger: Index

- [About](#beginner-about)
- [Installation](#electric_plug-installation)
- [Credentials](#key-credentials)
- [Build With](#hammer-build-with)
- [Gallery](#camera-gallery)


##  :beginner: About
"Rated Travel" caters to four user types: three registered and one unregistered.

1. **Admin**
    - Admins can Create, Read, Edit, and Delete all pages, comments, and users on the site. Only admins can create new cities.
2. **Emloyee**
    - Employees can Create, Read, Edit, and Delete all pages except for cities. Employees can only Edit a city
3. **Normal User**
    - Normal users can Add Restaurants and Bars. They can also add reviews and become employees if they have created at least 3 bars or restaurants.
4. **Unkown User**
    - Unregistered users can view cities, restaurants, and bars. They can also leave reviews.

##  :electric_plug: Installation
To access the project, download the project's zip file and open it with Visual Studio or another IDE. Ensure you have SQL Server Management Studio (SMSS) installed. Add a connection string to the "Manage Secrets JSON". This step will change once the application is deployed. Create initial migration via the "package manager Control" to see the test data. 
You can use the following commands:

**Add-Migration initial**

**Update-Database**

##  :key: Credentials
Those users are seeded into the DB once the initial migration is applied. 
 - Admin User:

   -- UserName: admin@abv.bg
   
   -- Password: 123456
   
- Employee User:

   -- UserName: antk@abv.bg
  
   -- Password: 123456
  
- Admin User:

   -- UserName: pesho@abv.bg
  
   -- Password: 123456

## :hammer: Build With
- ASP.NET Core 6
  -- Database layer with 8 entity models. The "Attractions" entity is implemented on the DB level, but it has to be added to the service and the UI layers.
  -- UI layer with 6 controllers + 1 more in the “Admin” area.
  -- Service layer with 5 services.
  -- Test layer for services and controllers with 80 tests
  -- 25+ Views.
- Entity Framework Core
- Microsoft SQL Server
- Vanila JS
- HTML&CSS
- TempData messages
- NUnit  

##  :camera: Gallery
Unknown User:  Home View
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/e6c34927-1669-4d63-b294-ce85094e1f95)

The Unknown User  can see City Details and directions towards All Restaurants and All Bars in the selected city.
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/a1090b1b-b36e-45e3-b88a-e3b5925fc048)

Unknown User can see All Restaurants in the city and drill into the details of each restaurant. 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/f7053199-968a-4f23-83a5-ccadd4b7ddfd)

Unknown User can see All Bars in the city and drill into the details of each bar. 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/07f0ca21-d4ff-4502-ada5-d049a01e4a47)

Unknown User: Restaurant Details >> Add and See All Reviews

![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/24b002b4-9868-4723-b3f9-513b806ad558)

Unknown User: Bar Details >> Add and See All Reviews
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/0314e6ea-c803-46b0-bdf8-1a1bbb668cbc)

Register View: 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/5f1b6f95-263d-4574-9df6-90ff3261b6b7)

Login View:
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/23a4bf59-1078-4ab1-b667-7d90549e2850)

People have access to different pages and actions depending on the type of the user. 
The 3 type of users are Normal, Employee and Admin. 

Normal User Home view: 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/4be11a8c-c625-4d23-b7fb-e985f550b4a9)

The normal user can see All Cities, Bars and Restaurants under “Our Places”. 
The normal user can add a Bar or a Restaurant under “Add a Place” 

![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/80b1cf60-88d3-4a79-ae13-4feed623c7a3)



The Normal User can also “Join the team” if he has created more than 3 bars or restaurants combined.
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/f9bd284b-fe67-498f-8ebf-22e5b0e0ee8f)

If the user does not meet the requirements they would get an error message. 

Employee User: Apart from adding restaurants and bars the employees can also add a city. 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/3860d8b4-970b-4a5f-95c1-4c08cdf13e70)
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/48721b8c-0ffc-484d-a3b0-df3144e1256f)

The Employees can Edit Cities
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/aca3c7f7-c571-463c-83d5-200914f4ef12)


The Employees can also Edit and Delete Restaurants and Bars
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/bff2de6a-7fcb-4f24-9656-a1b6d3f339cd)

The Employees can also delete Customer’s reviews. 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/fff13957-6d13-40ab-9ed9-b3fdf50bfbbb)
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/bb823d6c-1cd4-4c50-9389-f0c3b16c8a09)
The Employees can also delete Customer’s reviews. 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/dd3272dc-cdf7-4efb-ba5f-1b34f0e2863c)

Admin user: the admin user can execute all actions on the website. The main difference with the employee is that only the Admin is allowed to delete a city.
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/ebe18182-55ce-49dc-b2f0-f122590af102)
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/ea1f8672-bbbf-4a83-a856-90757cf112b7)
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/d36bfafb-d1f2-48fa-aa1c-6f988df82c94)

Databse Diagram 
![image](https://github.com/aTsekov/RatedTravel-WebApp/assets/102099768/fa997544-d021-4799-89cb-8cc26b5bb775)
