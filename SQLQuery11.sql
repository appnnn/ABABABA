
CREATE TABLE Departments (
  DepartmentID    INT           PRIMARY KEY,              
  DepartmentName  VARCHAR(100) NOT NULL,                
  DepartmentHead  INT           NULL                       
);


CREATE TABLE Employees (
  EmployeeID      INT            PRIMARY KEY,          
  FirstName       VARCHAR(50)   NOT NULL,                  
  LastName        VARCHAR(50)   NOT NULL,                 
  Address         VARCHAR(255)  NULL,                     
  Birthday        DATE          NULL,                   
  Gender          CHAR(1)       NULL,  
  PhoneNumber     VARCHAR(15)   NULL,                      
  DepartmentID    INT           NOT NULL,                 
  Position        VARCHAR(100)  NULL,                   
  JoinDate        DATE          NULL,                     
  Email           VARCHAR(100)  NOT NULL UNIQUE,           
  Username        VARCHAR(50)   NOT NULL UNIQUE,           
  PasswordHash    VARCHAR(255)  NOT NULL,                 
  BasicSalary     DECIMAL(10,2) NOT NULL,                
  CONSTRAINT FK_Employees_Departments 
    FOREIGN KEY (DepartmentID) 
    REFERENCES Departments(DepartmentID)                  
);


CREATE TABLE Salary (
  SalaryNo        INT            PRIMARY KEY,             
  EmployeeID      INT            NOT NULL,                
  EPFNo           VARCHAR(20)    NOT NULL,              
  Year            INT            NOT NULL,                 
  Month           INT            NOT NULL, 
  Basic           DECIMAL(10,2)  NOT NULL,                
  Allowances      DECIMAL(10,2)  NOT NULL DEFAULT 0,       
  OTHours         DECIMAL(5,2)   NOT NULL DEFAULT 0,      
  EarlyOut        DECIMAL(5,2)   NOT NULL DEFAULT 0,       
  Lateness        DECIMAL(5,2)   NOT NULL DEFAULT 0,       
  TimeOff         DECIMAL(5,2)   NOT NULL DEFAULT 0,      
  EPF             DECIMAL(10,2)  NOT NULL DEFAULT 0,       
  Total           DECIMAL(10,2)  NOT NULL,                 
  CONSTRAINT FK_Salary_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                    
);


CREATE TABLE Leaves (
  LeaveID         INT           PRIMARY KEY,            
  EmployeeID      INT           NOT NULL,                
  Year            INT           NOT NULL,                
  Month           INT           NOT NULL,  
  CasualLeaves    INT           NOT NULL DEFAULT 0,      
  SickLeaves      INT           NOT NULL DEFAULT 0,      
  AnnualLeaves    INT           NOT NULL DEFAULT 0,      
  PaternityLeaves INT           NOT NULL DEFAULT 0,      
  MaternityLeaves INT           NOT NULL DEFAULT 0,      
  BereavementLeaves INT         NOT NULL DEFAULT 0,       
  ShortLeaves     INT           NOT NULL DEFAULT 0,       
  CONSTRAINT FK_Leaves_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                   
);


CREATE TABLE Attendance (
  AttendanceID    INT            IDENTITY(1,1) PRIMARY KEY,  
  EmployeeID      INT            NOT NULL,                  
  AttendanceDate  DATE           NOT NULL,                   
  ArrivalTime     TIME           NULL,                      
  ExitTime        TIME           NULL,                      
  OTHours         DECIMAL(5,2)   NOT NULL DEFAULT 0,         
  LateMinutes     INT            NOT NULL DEFAULT 0,        
  CONSTRAINT FK_Attendance_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                      
);



