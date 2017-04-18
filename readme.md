TechSolutions Developer Assessment
----------------------------------
----------------------------------

Deployment Intructions
----------------------

I have used EF code first migrations so the SQL server database will need to be created. The connection string will need to be updated to either the Local DB instance of the machine it will be tested on or a SQL server instance and the *update-database* command run against the project *PressfordPublishingSystem.Models*. The database is seeded with sample users and articles.

Login instructions are provided on the login page.


Notes
-----

* Due to the tight timeline I have focused on implementing the basic features only but implementing them cleanly, demostrating testability and good OO design.

* Test coverage is by no means comprehensive due to time restrictions but I have demonstrated my testing approach for a number of components using self-contained and decoupled tests and using Moq where necessary to mock dependencies.

* I have used bootstrap css framework. However, examples of custom css can be found in either the site.css file if applicable site wide or at the top of each page if applicable to the page only.

* Examples of custom javascript and ajax can be found on the Index pages of the PublishArticles and ViewArticles pages.


Features
--------

* An area for publishers to view, create, edit and delete articles - accessible from 'Publish' on the navbar.

* An area for employees to view articles by month and like them. Each employee can like up to 5 articles and I've allowed an employee to like the same article more than once to allow for easier testing/demostration.