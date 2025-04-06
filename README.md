# Conway Game of Life API

This project is an implementation of the Conway Game of Life API using C# & Net 7.0. It includes endpoints to manage game boards and simulate the game.

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Running the Project

### Running with Visual Studio

1. **Open the Solution**:
   - Open the solution file (`.sln`) in Visual Studio 2022.

2. **Set the Startup Project**:
   - In the Solution Explorer, right-click on the `Game.API` project and select `Set as Startup Project`.

3. **Run the Project**:
   - Press `F5` to run the project in debug mode, or `Ctrl + F5` to run without debugging.
   - The API should be accessible at `http://localhost:5125` or `https://localhost:7043`.

4. **Access Swagger**:
   - Open a web browser and navigate to `http://localhost:5125/swagger` or `https://localhost:7043/swagger` to access the Swagger UI.

### Running with Docker

1. **Build the Docker Image**:
   - Open a terminal and navigate to the root directory of the project (where the `Dockerfile` is located).
   - Run the following command to build the Docker image:
     
``` docker build -t game-api . ```

2. **Run the Docker Container**:
   - Run the following command to start the Docker container:

``` docker run -d -p 8080:80 --name game-api-container game-api ```     

3. **Access the API**:
   - Open a web browser and navigate to `http://localhost:8080/swagger` to access the Swagger UI.

### Running with Docker Compose

1. **Create `docker-compose.yml`**:
   - Ensure you have a `docker-compose.yml` file in the root directory with the following content:
     
``` 
 version: '3.4'

 services:
   game-api:
     image: game-api
     build:
       context: .
       dockerfile: Dockerfile
     ports:
       - "8080:80"
     environment:
       - ASPNETCORE_ENVIRONMENT=Development
```

2. **Build and Run with Docker Compose**:
   - Run the following command to build and start the services defined in `docker-compose.yml`:
   
``` docker-compose up --build ```    

3. **Access the API**:
   - Open a web browser and navigate to `http://localhost:8080/swagger` to access the Swagger UI.

## Running Tests

### Running Tests with Visual Studio

1. **Open the Solution**:
   - Open the solution file (`.sln`) in Visual Studio 2022.

2. **Open Test Explorer**:
   - Go to `View` > `Test Explorer` to open the Test Explorer.

3. **Run All Tests**:
   - In the Test Explorer, click on `Run All` to execute all the tests in the solution.

### Running Tests with .NET CLI

1. **Open a Terminal**:
   - Open a terminal and navigate to the root directory of the solution.

2. **Run Tests**:
   - Run the following command to execute all the tests:
   
``` dotnet test ```
	 