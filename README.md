# Portfolio Site for CITB517

Createdy by Ivan Stoyanov F90980

## Prerequisites

- .NET 5.0

- npm v6.14.9

- NodeJS v14.15.3

- Visual Studio 2019


## Description
This project is designed to showcase who I am, my work experience, project's I've worked on and technologies that I'm familiar with.

## Technologies
.NET 5.0

EFCore

ReactJS

HTML & CSS

JavaScript

Axios

## Development stages
### BackEnd
- Figure out DB design
- Initial setup of .NET project
- Authentication using JWT and Identity
- Implementation of Services, Models and Controllers

### FrontEnd
#### Design the components of the main page
    -Navbar
    -HeroSection
    -Experience
    -CardsSection
#### Design Login and Register pages
#### Implement requests for
     -CardsSection (get)
     -Login (post)
     -Register (post)
     -Projects (get)
     -Comments (get, delete and create)

## Initial setup
### BackEnd
Using Visual Studio 2019 when you first build the application it automatically applies the initial migration.

The Initial migration creates 5 Projects, 1 User and 1 comment for the first project.

#### Initial user data: 
     -UserName: initialUser
     -Password: initialPassword
     
### FrontEnd
Navigate to *porfolio-app* and create **.env** file using the **.env.example** for reference.

After your first BackEnd build copy the URL and paste it inside your **.env** file.

#### Install inital dependencies
    -npm install

#### Build
    -npm start

