# Assignment Management App

The purpose of this project is to provide a means of assignment management in both local projects and remote projects. (Via the WebAPI)

## Includes
**AssignmentLibrary** - Provides a template and concretion for an assignment and assignment service.
**UI** - Provides built in UI methods for interaction between the user and the service.
**WebAPI** - Provides Create, Read, Update, and Delete endpoints for pinging across a network.
**Console** - Is a wrapper for the ConsoleUI class in the UI project.
**Tests** - Contains testing for the projects.

## Instructions
1. Install external dependancies:
    - [Git](https://git-scm.com/downloads)
    - [Dotnet SDK](https://learn.microsoft.com/en-us/dotnet/core/install/)
        - If you are using visual studio, make sure the _.NET desktop development_ workload is installed.

2. Clone the repository using the command: 
```git clone https://github.com/GiftedR/AssignmentManagementApp-TDD.git```

3. Move into the newly cloned folder and open the Solution file.
    - In vscode you can either use the command ```code .``` in the terminal, or right clicking and opening folder with vscode
    - In visual studio, double clicking the **.sln** file should open it up.

4. Run the app.
    - In visual studio, there should be a green arrow at the top of the ui, click that to run.
    - In vscode, you can use the integrated terminal to run the command ```dotnet run``` or click the bug and arrow icon and selecting _Run and Debug_ and selecting the project you want to run from the popup if it asks

### Running the tests

1. Do steps 1 - 3 listed in [Instructions](#Instructions)

2. Instead of running the app, you can insead run tests.
    - In visual studio, you click the button labeled _Test_ at the top, and click _Run All Tests_
    - In vscode you need to open the integrated terminal and run ```dotnet test``` or install the [**C# Dev Kit** extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

## Contributing
Anyone is free to contribute, just make a fork and pull request with the changes.

## Bug Reports
Bug reports can be submitted within the _Issues_ tab labeled in github.

## Planned Maintenance
- Looking into reducing complexity of ConsoleUI.Run() as it has the highest complexity.
- Better Organization of tests.
- File saving for both logs and assignments.
    - Currently assignments are removed when the application closes.
- Support for differing types of assignments including those that allow communication between assignment provider and assignment consumer.

### Not Planned
- Test Documentation
    - Tests should be self explaining. I.E. The test name should explain exactly what the test is for.