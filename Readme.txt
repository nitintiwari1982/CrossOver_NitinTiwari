Project Assessment:
    Cross Exchange is an arbitrarily trading game developed by a startup in a very short span of time called “Super Traders” . The purpose of the application is to educate users on the terminology used in trading of shares.

Notes:
    - The project registers share and and allow users to updated price of the share on an hourly basis; the share registered should have unique Symbol to identify it and should be all capital letters with 3 characters. The rate of the share should be exactly 2 decimal digits. 
    - Also, the users should have a portfolio before they can start trading in the shares. 
    - The frontend application is excluded from the current scope. It is a separate, fully-functional application handled by another team, and we do not want to modify it.

Tasks:

    1) For a given portfolio, with all the registered shares you need to do a trade which could be either a BUY or SELL trade. For a particular trade keep following conditions in mind:
                BUY:
                a) The rate at which the shares will be bought will be the latest price in the database.
                b) The share specified should be a registered one otherwise it should be considered a bad request. 
                c) The Portfolio of the user should also be registered otherwise it should be considered a bad request. 
                
                SELL:
                a) The share should be there in the portfolio of the customer.
                b) The Portfolio of the user should be registered otherwise it should be considered a bad request. 
                c) The rate at which the shares will be sold will be the latest price in the database.
                d) The number of shares should be sufficient so that it can be sold. 
        Hint: You need to group the total shares bought and sold of a particular share and see the difference to figure out if there are sufficient quantities available for SELL. 

    Your goal is to complete the above task. The API specifications are already there in the code as agreed with the frontend team. 

    2) There are a few bugs in the application that we would like you to fix. Even though the project might not be in a great structure, please do not spend your valuable time on structural modifications - just focus on fixing the bugs.

    3) Increase unit test coverage to reach code coverage up to 60%, achieving more than 60% will only consume your valuable time without any extra score.

    PLEASE NOTE THAT ALL OF THE TASKS LISTED ABOVE ARE MANDATORY.

    We will evaluate your submission on the following parameters:
        - Implementation of new feature
        - Bug fixes
        - Unit Tests

    Prerequisites:
       	- GIT
 		- VS2017
 		- .NET Core 2.0
 		- SQL Server 2012+


   Development Environment:
        Cross Exchange application:
        
        - Modify the database connection string as per your instance and authentication.
        - On any terminal move to the "CrossExchange" folder (the folder containing the "CrossExchange.csproj" file) and execute these commands:

        dotnet restore
        dotnet build
        dotnet ef database update

        - Now you can call the API using any tool, like Postman, Curl, etc 
        
        - To check code coverage, execute the batch script:
        coverage.bat

   Production Environment:
        This is how we are going to run and evaluate your submission, so please make sure to run below steps before submitting your answer.

        1) Make sure to run unit tests, check code coverage, ensure the application is compiling and all dependencies are included.
        2) Commit everything to use.
                (git add --all && git commit -m "My submission")
        3) Create patch file with the name without spaces 'cross-exchange-dotnet_<YourNameHere>.patch', using the specified tag as the starting point.
                (git format-patch initial-commit --stdout > cross-exchange-dotnet_<yournamehere>.patch)
        4) Store your file in a shared location where Crossover team can access and download it for evaluation. and add your sharable link in the answer field of this question.

