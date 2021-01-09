# Monster Trading Card Game

## Technical steps:
* The Game is based on Http/Rest Server Client Model to make the Multithreading Connection, and the steps used to get to the final version are the following:
1. Designing Server-Client Sockets Based on TCP listeners in C# and testing its functionality.
2. Designing the Classes of the Game and implementing a Class Diagram.
3. Database in Postgress with 6 Tables to Save the Data of the Users, and the Cards ohf the Game, Transactions and Scores
4. Connect the Game with the Database and with th Server to be able to connect to Multiple Users.
5. Writing Unit Tests that tests the functionality of the battles
6. Integration Test using Postman in a Collection that can run Curls one after another.
7. Github Documentation 

The Design simply Connects the Server with the Managers (The Control Components), and they are connected to the Method in the Game.
I kept the logic Simple and focused on achieving the requirements, and the curls results

Concentrating on the Methods construction and adding interfaces was one of the Challenges that I faced.
I was able to implement all the functionalities and save them in the Database, but i am not satisfied with the Design.

On the Other hand, the Game goes exactly as it is already asked with only one thing different.
I have changes the word Base in th eToken to Bearer type. It is the only thing that Postman accept with Token.
The Token is the user-name with MSTG-token added to it. 
Authorization goes normally with this token. the user that logs in needs to have a token.
Admin can do much more and get the information of the users easily using admin token.

Libraries and languages used in the Project:
* C#
  * Microsoft.NetCore
* Sql
  * Postgress / Pgadmin 4
* Json
* Packages
   * Npgsql (5.0.1.1)
    * NUnit (3.12.0)
    * Newtonsoft (12.0.3)
    * System Collections
    
* The project started bei (1.0.0) version then (1.1.0) then (2.0.0) before the submission 
* Visual Studio was used as the main tool
* Postman

## Unit tests 

I have included 23 tests, they vary between battle Methods to testing the responses. Coverage is about 30% which I could accept because I did the testing using Postman (integration):
* Battle Methods were tested: Monster vs Monster , Monster vs Spell , Spell vs Spell
* Battle element: water vs fire , water vs water , water vs normal
* Responses specially when I expect errors , I tried to cover as many as possible of errors and failure cases
* Json file of all the tests in MTCG collection is added, and you could add it to your account easily.
* in the Database there are already saved values , and we can use it or start over if it is asked.

## The time spent
It was a journey! I could say before giving the assignment , I spent at least three to four days just going through every detail.
But I would say that I worked for more than 20 days to get it done, and I think if I had more time ii would have optimized it.
I gave all my 100% to get it done!
It made me appreciate the designers of the frameworks so much, they were able to but such  code all together and implement it for us!.

## Git documentation
SWE-MTCG repository with the following Parts:
* Rest Http Server Project
* MTCG Project
* Protocol
* Releases
* Wiki Pages
* Database SQL
* Json Collection 
* 4 different features branches used in development

My Github repository is now public, and the Link is:

https://github.com/Shahed-RAJOUB/SWE1-MTCG

## Maintainer

* Chahed Rajoub - BIF3C - If19b166
