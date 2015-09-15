# db-project-2013
Automatically exported from code.google.com/p/db-project-2013

Telerik Academy team project.

[Download full assignment](https://github.com/VelizarIT/db-project-2013/blob/master/Databases-Teamwork-Practical-Project.docx?raw=true)

The assignment was to design, develop and test a C# application for importing Excel sales reports from ZIP file
and product data from MySQL into SQL Server, generate PDF aggregated reports and XML sales reports, create product 
reports as JSON documents and load them into MongoDB, load vendor expenses from XML file, read taxes from SQLite 
and calculate vendor's total results and write them into Excel file.

Additional requirements:
* Your main program logic should be a C# application (a set of modules, executed sequentially one after another).
* Use non-commercial library to read the ZIP file.
* For reading the Excel 2003 files (.xls) use ADO.NET (without ORM or third-party libraries).
* MySQL should be accessed through OpenAccess ORM.
* SQL Server should be accessed through Entity Framework.
*	You are free to use "code first" or "database first" approach or both for the ORM frameworks.
* For the PDF export use a non-commercial third party framework.
* The XML files should be read / written through the standard .NET parsers (by your choice).
* For JSON serialization you a non-commercial library / framework of your choice.
* MongoDB should be accessed through the Official MongoDB C# Driver.
* The SQLite embedded database should be accesses though its Entity Framework provider.
* For creating the Excel 2007 files (.xlsx) use a third-party non-commercial library.
