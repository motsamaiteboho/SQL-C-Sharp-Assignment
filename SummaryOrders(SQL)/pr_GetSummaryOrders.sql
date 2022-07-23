USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetOrderSummary]    Script Date: 23 Jul 2022 13:42:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pr_GetOrderSummary] @StartDate datetime ,@EndDate datetime, @CustomerID nchar(5), @EmployeeID int
AS
BEGIN
SELECT 
    CONCAT(' ', TitleOfCourtesy, FirstName, LastName) AS 'EmployeeFullName', 
	s.CompanyName as 'Shipper CompanyName', 
	c.CompanyName as 'Customer CompanyName',
	COUNT(o.OrderID) as 'NumberOfOrders',
	o.OrderDate as 'Date',
	SUM(o.Freight) as 'TotalFreightCost',
	COUNT(DISTINCT p.ProductID) as 'NumberOfDifferentProducts',
	SUM(od.UnitPrice * Quantity) as 'TotalOrderValue'
FROM Employees As e
INNER JOIN Orders As o
ON e.EmployeeID = o.EmployeeID
INNER JOIN Customers AS c
ON c.CustomerID = o.CustomerID
INNER JOIN [Order Details] As  od
ON od.OrderID = o.OrderID
INNER JOIN Products as p 
ON p.ProductID = od.ProductID
INNER JOIN Shippers as s
ON s.ShipperID = o.ShipVia
WHERE   (OrderDate BETWEEN @StartDate AND @EndDate)
		AND c.CustomerID = @CustomerID
		AND e.EmployeeID = @EmployeeID
		
GROUP BY e.TitleOfCourtesy, e.FirstName,e.LastName,
c.CompanyName, o.OrderDate, s.CompanyName
END