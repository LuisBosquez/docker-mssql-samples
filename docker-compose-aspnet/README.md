# Quickstart Compose and .NET Core with SQL Server
This quick-start guide demonstrates how to use Docker Compose to set up and run a simple .NET Core application with SQL Server in Docker. Before starting, you’ll need to have Compose installed.

# Define the project
For this project, you will need a few code files, a Dockerfile and a docker-compose.yml YAML file.

1. Create an empty project directory.

    This directory will be the context of you Docker image. It should only contain the resources necessary to build it.    

1. Create a folder to host your code. 

    ```{r, engine='bash', count_lines}
    mkdir code
    ```
1. Create a new file called `project.json` under your `code` directory and paste the following code:

    ```json
    {
        "version": "1.0.0-*",
        "buildOptions": {
            "debugType": "portable",
            "emitEntryPoint": true
        },
        "dependencies": {
            "System.Data.SqlClient": "4.3.0"
        },
        "frameworks": {
            "netcoreapp1.0": {
            "dependencies": {
                "Microsoft.NETCore.App": {
                "type": "platform",
                "version": "1.0.1"
                }
            },
            "imports": "dnxcore50"
            }
        }
    }
    ```

    This will add the necessary dependencies to dotnet core and the SqlClient library.

1. Create a new file called `Program.cs` under your `code` directory and paste the following code. **Make sure to change the password** in the `builder.Password` variable below:

    ```csharp
    using System;
    using System.Text;
    using System.Data.SqlClient;

    namespace SqlServerSample
    {
        class Program
        {
            static void Main(string[] args)
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "db";  // This name is defined in docker-compose.yml
                builder.UserID = "sa";  // SysAdmin user for SQL Server in Docker
                builder.Password = "your_password"; // Update with selected password
                builder.InitialCatalog = "master";

                bool connected = false;
                while (!connected)
                {
                    try
                    {
                        // Connect to SQL
                        Console.Write("Connecting to SQL Server ... ");
                        using (SqlConnection connection = 
                                    new SqlConnection(builder.ConnectionString))
                        {
                            connection.Open();
                            Console.WriteLine("Done.");
                            connected = true;
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                
                Console.WriteLine("Successfully connected to SQL Server in Docker.");
            }
        }
    }
    ```

    In this code, an attempt to connect to SQL Server running in Docker is made with retry logic. 

1. Create a new Dockerfile in your working directory.

    This file will define the steps necessary to create the continerized ASP.NET application. Once it is built, it can be run as a container.

1. Add the following content to the Dockerfile
    
    ```
    FROM ubuntu:16.04

    RUN apt-get update && apt-get install -y \
            apt-transport-https

    RUN sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
    RUN apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
    RUN apt-get update && apt-get install -y \
            dotnet-dev-1.0.0-preview2-003131

    RUN mkdir ~/aspnetcore
    WORKDIR ~/aspnetcore

    ADD ./code/ ./
    RUN dotnet restore
    ```

1. Save and close the Dockerfile.

1. Create a `docker-compose.yml` file in your project directory. 

    This file describes all the services that make up your application, how they're configured and how they interact. Paste the following code into your `docker-compose.yml` file and **update the password field** under `SA_PASSWORD` below:

    ```YAML
    version: '2'

    services:
        web:
            build: .
            command: dotnet run
            ports: 
                - "5000:5000"
            links:
                - db
            depends_on:
                - db
        db:
            image: "microsoft/mssql-server-linux"
            ports: 
                - "1433:1433"
            environment:
                SA_PASSWORD: "your_password"
                ACCEPT_EULA: "Y"
    ```

1. Run the `docker-compose build` command.

1. Run the `docker-compose up` command. After a few seconds, you should be able to see a connection attempt and the message "Successfully connected to SQL Server in Docker.".

Ready! You now have a .NET Core application running against SQL Server in Docker!