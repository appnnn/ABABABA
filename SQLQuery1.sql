CREATE TABLE Employee (
    [EmployeeId] INT IDENTITY(1,1) PRIMARY KEY, --auto incrementing

    [FirstName] NVARCHAR(50) NOT NULL,           
    [LastName] NVARCHAR(50) NOT NULL,           

    [Email] NVARCHAR(100) NOT NULL UNIQUE,    

    [PhoneNumber] NVARCHAR(20),                

    [Department] NVARCHAR(50),                  
    [Position] NVARCHAR(50),                    

    [Salary] DECIMAL(10,2),                     

    [DateOfBirth] DATE,                       
    [JoiningDate] DATE,                         

    [ProfilePicturePath] NVARCHAR(255),          

    [Username] NVARCHAR(50) NOT NULL UNIQUE,    

    [Password] NVARCHAR(255) NOT NULL,          

    [Role] NVARCHAR(50),                        
    [Gender] NVARCHAR(10),                       
    [Address] NVARCHAR(255)                      
);