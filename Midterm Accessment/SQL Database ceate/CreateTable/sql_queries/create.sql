CREATE TABLE Country (
  countryID INT PRIMARY KEY,
  countryName VARCHAR(255) NOT NULL
);

CREATE TABLE City (
  cityID INT PRIMARY KEY,
  countryID INT NOT NULL,
  cityName VARCHAR(255) NOT NULL,
  FOREIGN KEY (countryID) REFERENCES Country(countryID)
);

CREATE TABLE Address (
  addressID INT PRIMARY KEY,
  cityID INT NOT NULL,
  addressName VARCHAR(255) NOT NULL,
  FOREIGN KEY (cityID) REFERENCES City(cityID)
);

CREATE TABLE Shop (
  shopID INT PRIMARY KEY,
  addressID INT NOT NULL,
  cityID INT NOT NULL,
  shopName VARCHAR(255) NOT NULL,
  FOREIGN KEY (addressID) REFERENCES Address(addressID),
  FOREIGN KEY (cityID) REFERENCES City(cityID)
);

CREATE TABLE Positions (
  positionID INT PRIMARY KEY,
  positionName VARCHAR(255) NOT NULL
);

CREATE TABLE Employee (
  employeeID INT PRIMARY KEY,
  firstName VARCHAR(255) NOT NULL,
  lastName VARCHAR(255) NOT NULL,
  positionID INT NOT NULL,
  shopID INT NOT NULL,
  FOREIGN KEY (positionID) REFERENCES Positions(positionID),
  FOREIGN KEY (shopID) REFERENCES Shop(shopID)
);

CREATE TABLE Manufacturers (
  manufacturerID INT PRIMARY KEY,
  manufacturerName VARCHAR(255) NOT NULL
);

CREATE TABLE Brands (
  brandID INT PRIMARY KEY,
  manufacturerID INT NOT NULL,
  brandName VARCHAR(255) NOT NULL,
  FOREIGN KEY (manufacturerID) REFERENCES Manufacturers(manufacturerID)
);

CREATE TABLE Model (
  modelID INT PRIMARY KEY,
  brandID INT NOT NULL,
  modelName VARCHAR(255) NOT NULL,
  FOREIGN KEY (brandID) REFERENCES Brands(brandID)
);

CREATE TABLE PhoneOwner (
  phoneOwnerID INT PRIMARY KEY,
  firstName VARCHAR(255) NOT NULL,
  lastName VARCHAR(255) NOT NULL,
  phoneNumber VARCHAR(255) NOT NULL
);

CREATE TABLE ProblemTypes (
  problemTypeID INT PRIMARY KEY,
  problemName VARCHAR(255) NOT NULL
);

CREATE TABLE ScreenResolution (
  screenResolutionID INT PRIMARY KEY,
  width INT NOT NULL,
  height INT NOT NULL
);

CREATE TABLE OrderState (
  orderStateID INT PRIMARY KEY,
  orderStateName VARCHAR(40) NOT NULL
);

CREATE TABLE Phone (
  phoneID INT PRIMARY KEY,
  ownerID INT NOT NULL,
  modelID INT NOT NULL,
  acceptedByEmployeeID INT NOT NULL,
  ROM VARCHAR(20) NOT NULL,
  RAM VARCHAR(20) NOT NULL,
  yearOfManufacture INT NOT NULL CHECK (yearOfManufacture > 1900 AND yearOfManufacture < 2100),
  screenResolutionID INT NOT NULL,
  FOREIGN KEY (ownerID) REFERENCES PhoneOwner(phoneOwnerID),
  FOREIGN KEY (modelID) REFERENCES Model(modelID),
  FOREIGN KEY (acceptedByEmployeeID) REFERENCES Employee(employeeID),
  FOREIGN KEY (screenResolutionID) REFERENCES ScreenResolution(screenResolutionID)
);

CREATE TABLE PhoneIMEI (
  phoneImeiID INT PRIMARY KEY,
  phoneID INT NOT NULL,
  modelID INT NOT NULL,
  imeiNumber CHAR(15) NOT NULL,
  FOREIGN KEY (phoneID) REFERENCES Phone(phoneID),
  FOREIGN KEY (modelID) REFERENCES Model(modelID)
);

CREATE TABLE RepairReceipt (
  repairReceiptID INT PRIMARY KEY,
  phoneID INT NOT NULL,
  phoneProvidedDate DATE NOT NULL,
  phoneFixedDate DATE,
  phoneReturnedDate DATE,
  repairedByEmployeeID INT NOT NULL,
  orderStateID INT NOT NULL,
  problemID INT NOT NULL,
  repairCost INT NOT NULL CHECK (repairCost >= 0),
  FOREIGN KEY (phoneID) REFERENCES Phone(phoneID),
  FOREIGN KEY (repairedByEmployeeID) REFERENCES Employee(employeeID),
  FOREIGN KEY (orderStateID) REFERENCES OrderState(orderStateID),
  FOREIGN KEY (problemID) REFERENCES ProblemTypes(problemTypeID)
);