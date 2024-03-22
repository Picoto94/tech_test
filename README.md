CDR API JM - README
Technologies Used
This project was developed using the following technologies:

ASP.NET Core (v8.0)
C# (v12)
xUnit.NET (v2.7.0)
Moq (v4.20.70)
Microsoft.NET.Test.Sdk(v17.10.0)
Other relevant libraries or tools
Assumptions and Justifications
During the development of this project, some assumptions were made to facilitate implementation and meet requirements. Here are some of the assumptions made and their justifications:

Date Format: It was assumed that all dates provided as input to the API endpoints will follow the ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ) to ensure consistency and ease of manipulation.

Data Validation: It was assumed that the data provided to the API endpoints will be validated before processing to ensure data integrity and security.

Running Instructions
To run the application locally and execute the automated test suite, follow the instructions below:

Running the Application Locally
Clone the repository to your local machine using the command:

bash
Copy code
git clone <REPOSITORY_URL>
Navigate to the root directory of the project:

bash
Copy code
cd CDR_API
Open the project in your preferred IDE and run it. Make sure to install necessary dependencies.

Access the API in your browser or favorite REST client using the URL:

bash
Copy code
http://localhost:5000/api/...
Or use Swagger!

Running the Automated Test Suite
In the root directory of the project, execute the following command:

bash
Copy code
dotnet test
Wait until all tests have been executed and check the console output for any errors or failures.

<<<<<<< HEAD

Add new CSV files
You can add new csv files with new data by adding the file to the folder: ...\CDR_API_JM\Infrastructure

=======
>>>>>>> ecdb5d00c02d241b17cf0145b0f3f14a717a6130
Considerations for Future Enhancements
Some considerations and enhancements that can be made for this project include:

Addition of additional validations to ensure data integrity.
Improvements in API documentation.
Add response classes.
Optimize the code.
<<<<<<< HEAD
=======
In this version, to add a file, you have to replace the other. A function must be implemented that reads all existing files in a specific folder that contains all CSV files, which have the desired format
>>>>>>> ecdb5d00c02d241b17cf0145b0f3f14a717a6130
