# marsRover
Mars Rover Code Challenge on .netCore

Problem is explained here https://code.google.com/archive/p/marsrovertechchallenge/

Applied Principles are SOLID, OOP, DRY, Clean Code, Unit Test (xUnit), Design Patterns


ConsoleApp execution with given input parameters and expected output screenshot is below.

![ConsoleApp execution Screenshot](https://raw.githubusercontent.com/gurcag/marsRover/master/marsRoverCmdSS.PNG)


All unit tests are passed. Screenshot is below.

![ConsoleApp execution Screenshot](https://raw.githubusercontent.com/gurcag/marsRover/master/marsRoverTestResult.PNG)


Important Note : Couldn't apply the dependency injection (already added comment lines and explained on program.cs).
MS DependencyInjection library can not resolve classic approach of singleton pattern. 
I've found an article about this problem : https://espressocoder.com/2019/01/03/implementing-the-singleton-pattern-in-asp-net-core/
I've already added to TODO item for applying dependency injection soon.
