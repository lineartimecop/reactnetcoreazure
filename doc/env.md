WebApp
======

# Environment

Visual Studio Code + .NET SDK + C# Dev Kit + Node.js

# Creating a new .NET Core Web API project

## Command line

dotnet new webapi -n NewWebApi
dotnet run

## Visual Studio

To have access to .NET Framework Project Templates: install .NET Framework project and item templates module from the ASP.NET and web development workload

Creating a new web api project:
- File -> New -> Project -> ASP.NET Core Web API
- Make sure Configure for HTTPS is checked
- Uncheck Enable OpenAPI support. Make sure that Use controllers is checked

## Visual Studio Code

VS Code -> Explorer (top left) -> Create .NET Project -> ASP.NET Core Web API

# Create a new React.JS project

npx create-react-app react-app-1

If you get an error message saying that C:\Users\<USER>\AppData\Roaming\npm is missing, then create that folder.

cd react-app-1
npm install
npm start

To deploy the project to a web server:
- npm run build
- An optimized production build will be created in the build folder

If something doesn't work but it should:
- delete package-lock.json, node_modules
- npm install

# Azure

It's a best practice to prefix Azure resources. E.g. "rg-" for resource groups.

## Virtual Machine

Create a web server:
- From availability options, select no infrastructure redundancy required
- Select standard for security type
- For OS, select Windows Server 2022 Datacenter - x64 Gen2
- Select Standard_B2s - 2 vcpus, 4 GiB memory as size
- Select http, https and ssh for inbound ports in addition to rdp which is already selected by default
- On the disks tab, select standard ssd as a locally redundant storage
- Make sure the delete with vm checkbox is checked
- On the networking tab, make sure that allow selected ports is selected as public inbound ports and select http, https, ssh and rdp as inbound ports
- Make sure the delete public ip and nic when vm is deleted checkbox is checked
- Shut down the server when you don't need it to save money

Configure the server:
- When the server starts up allow it to be discoverable by other devices on the same network (it's the recommended setting for home/work networks)
- Do a Windows update and restart if necessary: Start -> Settings -> Update & Security -> Check for updates
- To allow network sharing click on the server on portal.azure.com, go Networking -> Network Settings and create a new inbound port rule for destination TCP port 445. Name the port rule NetworkShare for example
- Share a folder on the server and connect to it (from Mac Finder -> Go -> Connect to server -> smb://<IP address> -> Connect then navigate to the folder, hover the mouse over the folder name in the upper-left part and drag and drop it to Locations on the left to have it easily accessible)
- If the shared folder is not accessible on Windows, try switching off the firewall in the anti-virus software

Configure IIS:
- Add IIS: Start -> Server Manager -> Manage -> Add roles and features -> Nexts -> Add Web Server (IIS) -> Nexts -> On Select Role Services add Web Server/Application Development/ASP.NET 4.8 -> Next -> Install -> Close
- Verify the installation by opening localhost on the server and visiting the IP address of the server in the browser from another machine
- Install the .NET Core Hosting Bundle
- Create an IISApplications folder on the C drive for the apps

Publishing a web api on IIS:
- Publish your app (web api) and copy it over the server into the IISApplications folder
- Right click the default website and add a new application (a virtual directory won't do either for a .NET Core Web Api or for an ASP.NET Web Forms App)

Publishing a react app on IIS:
- Add "homepage": "." to the root of package.json otherwise the resources might not be found on the server if the app is deployed as an application under a website
- To create a production build, run: npm run build
- Copy the build folder into the IISApplications folder and rename it to the name of the application
- Right click the default website and add a new application
- If IIS is loading some cached version of some files instead of what is actually in the IISApplication folder, then restart the server

## SQL Server

Creating an SQL database:
- Under Compute + storage click Configure database make sure that under the vCore model, General Purpose (Most budget friendly) is selected, then make sure that the Serverless option is selected. Make sure that the Enable auto-pause checkbox is checked and set the auto-pause delay to 15 mins. Set the maximum size of the database to 4 GB. Make sure that the database zone redundancy is set to No
- To avoid legal troubles concerning data residency, select Locally-redundant backup storage
- Go to the database and click Set server firewall. Under the Public access tab, select Selected networks and under Firewall rules, click Add your client IPv4 address. Optionally, change the name of the rule to the computer name you're connecting to the database. Also, add the IP address of the web server. Click Save
- Connect to the database from Microsoft SQL Server Management Studio or Azure Data Studio
- Install the Microsoft.Data.SqlClient (it has replaced System.Data.SqlClient) NuGet package in the backend

## Azure Web App

Creating an Azure Web App for web api:
- Make sure that Try a unique hostname is set to off
- Choose the Free F1 (Shared infrastructure) Pricing plan
- Go to the resource and click Browse to see what is there by default
- In Visual Studio right-click the project -> Publish -> New profile -> Azure -> Azure App Service (Windows)
- Log in with your Microsoft account, select your Azure subscription -> Select the App Service created above -> Finish -> Close
- Rename your profile AzureWebAppProfile
- Right-click the project -> Publish -> Select the AzureWebAppProfile
- Show all settings -> Check Remove additional files at destination, uncheck Install ASP.NET Core Logging Integration Site Extension -> Save -> Publish
- Go to the database and under Security -> Networking check the Allow Azure services and resources to access this server. Click Save

Creating an Azure Static Web App for react app:
- portal.azure.com -> Create -> Web -> Static Web App
- For Plan type, select Free
- Under Deployment details, select Other

## Azure Container
- Install Docker Desktop to be able to debug the app
- Under Settings uncheck Send user statistics
- When Windows Defender asks whether to allow Docker to communicate on private or public networks, allow both
- Visual Studio -> Right click project -> Add -> Docker support
- Select the project level folder (as opposed to the solution level folder) for Docker Build Context
- You can delete the container, the image and the build from Docker Desktop. Then running (debugging) the project from Visual Studio will start without a container. To run the project in a container again, right-click the Dockerfile in Visual Studio -> Build Docker Image. Then go to Docker Desktop -> Images -> select the image just created -> Run. Under Optional settings, add the container a name. The host port for http (8080) and https (8081) can be specified. Set 5001 and 5002 respectively and click Run. Now, you can open the app in the browser under localhost:5001

# Misc

## Firefox

To get the path to the cache:
- Type about:cache in the address bar
- Hit enter

## Mac

To list access priviledges for all files in a directory: ls -la
To give a file execute privilege: chmod +x <filename>
To run a .command file downloaded from the Internet, double click it then Settings -> Privacy and Security -> Open Anyway

Window App can be used to rdp into a Windows VM
Azure Data Studio can be used to connect to ms sql databases
