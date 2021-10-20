This is a .NET Core 3.1 Web API application that exposes an api.


#IN ORDER TO INSTALL AND RUN#\
--Before Running--\
Download the repo from github and extract to desired location.\
You must have the docker Desktop client installed on your machine.\
This was tested using a Windows 10 machine.

\
--With Docker--\
This application has been dockerized, and can be built and run through Visual Studio.

1. Open the solution for the project.
2. At the top of the window, be sure that "Docker" is selected instead of "IIS" or "LightfeatherBackendChallenge"
3. Click on "Docker" to build and run. Visual Studio should build, and a new docker container should appear in your Docker client.
4. A new Browser should automatically open. You can view the console by clicking on the container in the Docker client.

\
--Other--\
It is also possible to run the application locally.

1. Open the solution for the project, and select "LightfeatherBackendChallenge" instead of "IIS" or "Docker".
2. Run the project, it should build successfully and a browser and console window should appear.

\
--Testing--\
In order to fully test this API, it would be best to use a REST client such as Postman.