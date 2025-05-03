-- 🏢 DEPARTMENTS: each department has a unique ID and optional head
CREATE TABLE Departments (
  DepartmentID    INT           PRIMARY KEY,               -- 1) Type → INT; 2) declared PRIMARY KEY
  DepartmentName  VARCHAR(100) NOT NULL,                  -- 3) Type → VARCHAR(100); 4) set NOT NULL
  DepartmentHead  INT           NULL                       -- 5) Type → INT; allows NULL if no head yet
);

-- 👤 EMPLOYEES: each employee belongs to exactly one department
CREATE TABLE Employees (
  EmployeeID      INT            PRIMARY KEY,              -- 1) Type → INT; declared PRIMARY KEY
  FirstName       VARCHAR(50)   NOT NULL,                  -- 2) Type → VARCHAR(50); set NOT NULL
  LastName        VARCHAR(50)   NOT NULL,                  -- 3) Type → VARCHAR(50); set NOT NULL
  Address         VARCHAR(255)  NULL,                      -- 4) Type → VARCHAR(255); allows NULL
  Birthday        DATE          NULL,                      -- 5) Type → DATE; allows NULL
  Gender          CHAR(1)       NULL  CHECK (Gender IN ('M','F')),  -- 6) Type → CHAR(1); added CHECK constraint
  PhoneNumber     VARCHAR(15)   NULL,                      -- 7) Type → VARCHAR(15); allows NULL
  DepartmentID    INT           NOT NULL,                  -- 8) Fixed typo; Type → INT; set NOT NULL
  Position        VARCHAR(100)  NULL,                      -- 9) Type → VARCHAR(100); allows NULL
  JoinDate        DATE          NULL,                      -- 10) Type → DATE; allows NULL
  Email           VARCHAR(100)  NOT NULL UNIQUE,           -- 11) Type → VARCHAR(100); set NOT NULL & UNIQUE
  Username        VARCHAR(50)   NOT NULL UNIQUE,           -- 12) Type → VARCHAR(50); set NOT NULL & UNIQUE
  PasswordHash    VARCHAR(255)  NOT NULL,                  -- 13) Type → VARCHAR(255); renamed to PasswordHash
  BasicSalary     DECIMAL(10,2) NOT NULL,                  -- 14) Type → DECIMAL(10,2); set NOT NULL
  CONSTRAINT FK_Employees_Departments 
    FOREIGN KEY (DepartmentID) 
    REFERENCES Departments(DepartmentID)                   -- 15) Added FK to Departments
);

-- 💰 SALARY: one record per pay cycle per employee
CREATE TABLE Salary (
  SalaryNo        INT            PRIMARY KEY,             -- 1) Type → INT; declared PRIMARY KEY
  EmployeeID      INT            NOT NULL,                 -- 2) Type → INT; set NOT NULL
  EPFNo           VARCHAR(20)    NOT NULL,                 -- 3) Type → VARCHAR(20); set NOT NULL
  Year            INT            NOT NULL,                 -- 4) Type → INT; set NOT NULL
  Month           INT            NOT NULL CHECK (Month BETWEEN 1 AND 12),  -- 5) Type → INT; added CHECK
  Basic           DECIMAL(10,2)  NOT NULL,                 -- 6) Type → DECIMAL(10,2); set NOT NULL
  Allowances      DECIMAL(10,2)  NOT NULL DEFAULT 0,       -- 7) Corrected spelling; set default 0
  OTHours         DECIMAL(5,2)   NOT NULL DEFAULT 0,       -- 8) Type → DECIMAL(5,2); set default 0
  EarlyOut        DECIMAL(5,2)   NOT NULL DEFAULT 0,       -- 9) Type → DECIMAL(5,2); set default 0
  Lateness        DECIMAL(5,2)   NOT NULL DEFAULT 0,       -- 10) Type → DECIMAL(5,2); set default 0
  TimeOff         DECIMAL(5,2)   NOT NULL DEFAULT 0,       -- 11) Type → DECIMAL(5,2); set default 0
  EPF             DECIMAL(10,2)  NOT NULL DEFAULT 0,       -- 12) Type → DECIMAL(10,2); set default 0
  Total           DECIMAL(10,2)  NOT NULL,                 -- 13) Type → DECIMAL(10,2); set NOT NULL
  CONSTRAINT FK_Salary_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                      -- 14) Added FK to Employees
);

-- 📅 LEAVES: track leave balances per employee per month
CREATE TABLE Leaves (
  LeaveID         INT           PRIMARY KEY,              -- 1) Type → INT; declared PRIMARY KEY
  EmployeeID      INT           NOT NULL,                 -- 2) Type → INT; set NOT NULL
  Year            INT           NOT NULL,                 -- 3) Type → INT; set NOT NULL
  Month           INT           NOT NULL CHECK (Month BETWEEN 1 AND 12),  -- 4) Type → INT; added CHECK
  CasualLeaves    INT           NOT NULL DEFAULT 0,       -- 5) Type → INT; set default 0
  SickLeaves      INT           NOT NULL DEFAULT 0,       -- 6) Type → INT; set default 0
  AnnualLeaves    INT           NOT NULL DEFAULT 0,       -- 7) Type → INT; set default 0
  PaternityLeaves INT           NOT NULL DEFAULT 0,       -- 8) Type → INT; set default 0
  MaternityLeaves INT           NOT NULL DEFAULT 0,       -- 9) Type → INT; set default 0
  BereavementLeaves INT         NOT NULL DEFAULT 0,       -- 10) Type → INT; set default 0
  ShortLeaves     INT           NOT NULL DEFAULT 0,       -- 11) Type → INT; set default 0
  CONSTRAINT FK_Leaves_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                     -- 12) Added FK to Employees
);

-- ⏱️ ATTENDANCE: one row per employee per date
CREATE TABLE Attendance (
  AttendanceID    INT            IDENTITY(1,1) PRIMARY KEY,  -- 1) Added surrogate PK
  EmployeeID      INT            NOT NULL,                   -- 2) Added EmployeeID; Type → INT; set NOT NULL
  AttendanceDate  DATE           NOT NULL,                   -- 3) Renamed; Type → DATE; set NOT NULL
  ArrivalTime     TIME           NULL,                       -- 4) Type → TIME; allows NULL if absent
  ExitTime        TIME           NULL,                       -- 5) Type → TIME; allows NULL
  OTHours         DECIMAL(5,2)   NOT NULL DEFAULT 0,         -- 6) Type → DECIMAL(5,2); default 0
  LateMinutes     INT            NOT NULL DEFAULT 0,         -- 7) Renamed from late; Type → INT; default 0
  CONSTRAINT FK_Attendance_Employees 
    FOREIGN KEY (EmployeeID) 
    REFERENCES Employees(EmployeeID)                       -- 8) Added FK to Employees
);

-- 🔍 INDEXES for performance
CREATE INDEX IDX_Salary_Emp_Year_Month ON Salary(EmployeeID, Year, Month);
CREATE INDEX IDX_Leaves_Emp_Year_Month  ON Leaves(EmployeeID, Year, Month);
CREATE INDEX IDX_Attendance_Emp_Date    ON Attendance(EmployeeID, AttendanceDate);
