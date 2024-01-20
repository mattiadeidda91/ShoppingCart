USE ShoppingCart;

IF OBJECT_ID('dbo.UserProducts', 'U') IS NOT NULL
    DROP TABLE dbo.UserProducts;

IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
    DROP TABLE dbo.Users;

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
    DROP TABLE dbo.Products;

CREATE TABLE dbo.Products (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(50),
    Price DECIMAL(18, 2) NOT NULL
);

INSERT INTO dbo.Products (Name, Description, Category, Price)
VALUES
    ('Laptop', 'Powerful laptop for work and entertainment', 'Electronics', 999.99),
    ('Headphones', 'High-quality over-ear headphones', 'Audio', 149.99),
    ('Smartphone', 'Latest smartphone with advanced features', 'Electronics', 699.99),
    ('Coffee Maker', 'Automatic coffee maker for your kitchen', 'Appliances', 79.99),
    ('Running Shoes', 'Comfortable running shoes for exercise', 'Footwear', 59.99),
    ('Wireless Mouse', 'Ergonomic wireless mouse for your computer', 'Electronics', 29.99),
    ('Backpack', 'Durable backpack for everyday use', 'Fashion', 49.99),
    ('Smartwatch', 'Fitness tracker and smartwatch in one', 'Wearables', 129.99),
    ('Digital Camera', 'High-resolution digital camera for photography', 'Electronics', 399.99),
    ('Portable Speaker', 'Compact portable speaker for on-the-go music', 'Audio', 89.99);

CREATE TABLE dbo.Users (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL,
    Lastname NVARCHAR(50) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20),
    City NVARCHAR(50)
);

INSERT INTO dbo.Users (Name, Lastname, Email, Phone, City)
VALUES
    ('John', 'Doe', 'john.doe@example.com', '1234567890', 'New York'),
    ('Jane', 'Smith', 'jane.smith@example.com', '9876543210', 'Los Angeles'),
    ('Alice', 'Johnson', 'alice.johnson@example.com', '5551234567', 'Chicago');

CREATE TABLE dbo.UserProducts (
    UserID INT,
    ProductID INT,
    PRIMARY KEY (UserID, ProductID),
    FOREIGN KEY (UserID) REFERENCES dbo.Users(ID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES dbo.Products(ID)
);

INSERT INTO dbo.UserProducts (UserID, ProductID)
VALUES
    (1, 1),
    (1, 2),
    (2, 2),
    (2, 3),
    (3, 1),
    (3, 3),
    (1, 4),
    (2, 5),
    (3, 6),
    (1, 7);

CREATE TABLE dbo.Carts (
    ID INT PRIMARY KEY IDENTITY,
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES dbo.Users(ID) ON DELETE CASCADE
);

INSERT INTO dbo.Carts (UserID)
VALUES
    (1),
    (2),
    (3);
