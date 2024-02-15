USE SQL2017_TESTDB2
GO
Truncate table tbl_Student2;
GO
INSERT INTO tbl_Standards VALUES('1st', 'First-Standard');
INSERT INTO tbl_Standards VALUES('2nd', 'Second-Standard');
INSERT INTO tbl_Standards VALUES('3rd', 'Third-Standard');
GO
INSERT INTO tbl_Student2 VALUES('Pranaya', 'Rout', '1988-02-29', 5.10, 72, 1);
INSERT INTO tbl_Student2 VALUES('Mahesh', 'Kumar', '1992-12-15', 5.11, 75, 2);
INSERT INTO tbl_Student2 VALUES('Hina', 'Sharma', '1986-10-20', 5.5, 65, 3);
GO

ALTER TABLE tbl_Student2
ADD FOREIGN KEY (StandardId) REFERENCES tbl_Standards(StandardId); 

--eg
CREATE TABLE Orders (
    OrderID int NOT NULL PRIMARY KEY,
    OrderNumber int NOT NULL,
    PersonID int FOREIGN KEY REFERENCES Persons(PersonID)
); 


--
-- Standards table data
INSERT INTO Standards VALUES('STD1', 'Outstanding');
INSERT INTO Standards VALUES('STD2', 'Good');
INSERT INTO Standards VALUES('STD3', 'Average');
INSERT INTO Standards VALUES('STD4', 'Below Average');
GO
-- Teachers table data
INSERT INTO Teachers VALUES('Anurag', 'Mohanty', 1);
INSERT INTO Teachers VALUES('Preety', 'Tiwary', 2);
INSERT INTO Teachers VALUES('Priyanka', 'Dewangan', 3);
INSERT INTO Teachers VALUES('Sambit', 'Satapathy', 3);
INSERT INTO Teachers VALUES('Hina', 'Sharma', 2);
INSERT INTO Teachers VALUES('Sushanta', 'Jena', 1);
GO
-- Courses table data
INSERT INTO Courses VALUES('.NET', 1);
INSERT INTO Courses VALUES('Java', 2);
INSERT INTO Courses VALUES('PHP', 3);
INSERT INTO Courses VALUES('Oracle', 4);
INSERT INTO Courses VALUES('Android', 5);
INSERT INTO Courses VALUES('Python', 6);
GO
-- Students table data
INSERT INTO Students VALUES('Pranaya', 'Rout', 1);
INSERT INTO Students VALUES('Prateek', 'Sahu', 2);
INSERT INTO Students VALUES('Anurag', 'Mohanty', 3);
INSERT INTO Students VALUES('Hina', 'Sharma', 4);
GO
-- StudentAddresses table data
INSERT INTO StudentAddresses VALUES(1, 'Lane1', 'Lane2', '1111111111', '1@dotnettutorials.net');
INSERT INTO StudentAddresses VALUES(2, 'Lane3', 'Lane4', '2222222222', '2@dotnettutorials.net');
INSERT INTO StudentAddresses VALUES(3, 'Lane5', 'Lane6', '3333333333', '3@dotnettutorials.net');
INSERT INTO StudentAddresses VALUES(4, 'Lane7', 'Lane8', '4444444444', '4@dotnettutorials.net');
GO
-- StudentCourse table data
INSERT INTO CourseStudent VALUES(1,1);
INSERT INTO CourseStudent VALUES(2,1);
INSERT INTO CourseStudent VALUES(3,2);
INSERT INTO CourseStudent VALUES(4,2);
INSERT INTO CourseStudent VALUES(1,3);
INSERT INTO CourseStudent VALUES(6,3);
INSERT INTO CourseStudent VALUES(5,4);
INSERT INTO CourseStudent VALUES(6,4);
GO