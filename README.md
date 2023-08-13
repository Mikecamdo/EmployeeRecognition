
![cropped-laudatio-logo](https://github.com/Mikecamdo/Laudatio/assets/71799695/367893a2-37dc-44b4-82aa-f6b6457ec9ca)

#### A web application for employees to recognize and praise each other, improving employee morale in a virtual work environment.

#### You can view the website [here](https://laudatio-production.up.railway.app/).

# Project Background
This web app was inspired by remote internship during Summer 2023. A little peer encouragement goes a long way towards boosting company culture and team morale. A "Thank You" by the coffee machine or stopping by someone's desk to give praise can be encouraging, but how does a company support this in a remote work environment? I wanted to develop a digital application that allows a company's employees to recognize each other, with the aim of promoting peer engagement and cross-team connections.

# Project Details
This web app includes all the essential features, including:
- Account Creation (with unique names for every user)
- Sending "Kudos" to other Employees
- A Homepage displaying the most recently sent Kudos
- Commenting on sent Kudos
- User Authentication using JSON Web Tokens
- Secure API Endpoints
- Hashed Passwords
- Data Validation in the Backend
- Unit Tests using xUnit
- Dockerfiles for easy deployment
- And much more!

# Project Implementation
This project was made using the following technologies:

- Frontend: HTML, CSS, TypeScript, Angular, Bootstrap
- Backend: C#, ASP.NET Core, Entity Framework Core
- Database: MySQL

The frontend, backend, and database were deployed on [Railway](https://railway.app).

# Build Instructions
Follow these steps to build this project locally:
1. Clone the repo using GitHub Desktop or the following git command:

        git clone https://github.com/Mikecamdo/Laudatio

2. Navigate to the cloned repository and add a .env file with connection details for your MySQL server. Use example.env as a reference for what you need.
3. Make sure you have [Docker](https://www.docker.com/) downloaded, and run:

         docker-compose build

4. Once the Docker images have been built, run:

         docker-compose up

5. Now navigate to localhost:5000, where the project should be running!
6. When done, run:

         docker-compose down

   to end the session and stop the Docker containers.
