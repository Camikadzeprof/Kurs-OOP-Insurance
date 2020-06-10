use InsuranceComp;
Create Table Users(
Username nvarchar(15) constraint PK_Users primary key,
[Name] nvarchar(15) NOT NULL,
Surname nvarchar(20) NOT NULL,
[Password] nvarchar(100) NOT NULL,
SecretWord nvarchar(25) NOT NULL,
[Role] int default 1 check([Role] between 1 and 3)
);
Create Table InsTypes(
[Type] nvarchar(25) constraint PK_InsuranceTypes primary key,
Fee int not null,
MaxPayout int not null
);
Create Table Insurances(
Num int identity(1,1) constraint PK_Insurances primary key,
[Type] nvarchar(25) NOT NULL constraint FK_Insurances_Type foreign key references InsTypes([Type]),
Username nvarchar(15) constraint FK_Insurances_Client foreign key references Users(Username)
);
Create Table Incidents(
IdIncident int identity(1,1) constraint PK_Incidents primary key,
Num int NOT NULL constraint FK_Incidents_InsNum foreign key references Insurances(Num),
Explain nvarchar(Max),
[File] nvarchar(Max) NOT NULL,
[Status] nvarchar(20) default 'На рассмотрении' check([Status]='На рассмотрении' or [Status]='Отказано' or [Status]='Одобрено')
);
Create Table Payouts(
IdPayout int identity(1,1) constraint PK_Payouts primary key,
IdIncident int NOT NULL constraint FK_Payouts_IdIncident foreign key references Incidents(IdIncident),
[Sum] int NOT NULL
);