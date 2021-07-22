
/***************************************ცხრილების შექმნა*************************************************/

IF OBJECT_ID('dbo.Teacher', 'U') IS NOT NULL  
   DROP TABLE dbo.Teacher; 
GO
CREATE TABLE Teacher (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Firstname NVARCHAR(20) NOT NULL,
	Lastname NVARCHAR(20) NOT NULL,
	Gender NVARCHAR(20) NOT NULL,
	Subject NVARCHAR(20) NOT NULL
);

IF OBJECT_ID('dbo.Pupil', 'U') IS NOT NULL  
   DROP TABLE dbo.Pupil; 
GO
CREATE TABLE Pupil (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Firstname NVARCHAR(20) NOT NULL,
	Lastname NVARCHAR(20) NOT NULL,
	Gender NVARCHAR(20) NOT NULL,
	Class NVARCHAR(20) NOT NULL
);

IF OBJECT_ID('dbo.Teacher_Pupil', 'U') IS NOT NULL  
   DROP TABLE dbo.Teacher_Pupil; 
GO
CREATE TABLE Teacher_Pupil(
	TeacherId INT NOT NULL FOREIGN KEY REFERENCES dbo.Teacher(Id),
	PupilId INT NOT NULL FOREIGN KEY REFERENCES dbo.Pupil(Id),
	UNIQUE (TeacherId, PupilId)
);

/***************************************ცხრილების შევსება*************************************************/


-- ჩვენ გვინდა რომ გიორგი სწავლობდეს იასთან

-- ველები Teacher ცხრილისთვის
DECLARE @TeacherFirstName NVARCHAR(20) =N'ია'
DECLARE @TeacherLastName NVARCHAR(20) = N'ბაქრაძე'
DECLARE @TeacherGender NVARCHAR(20) = N'მდედრობითი'
DECLARE @Subject NVARCHAR(20) = N'ქართული'

-- ველები Pupil ცხრილისთვის
DECLARE @PupilFirstName NVARCHAR(20) =N'გიორგი'
DECLARE @PupilLastName NVARCHAR(20) = N'გიორგაძე'
DECLARE @PupilGender NVARCHAR(20) = N'მამრობითი'
DECLARE @Class int = 11

-- ველები N:M კავშირ ცხრილისთვის
DECLARE @TeacherId INT
DECLARE @PupilId INT

SELECT @TeacherId= Id FROM dbo.Teacher
WHERE Firstname=@TeacherFirstName AND Lastname=@TeacherLastName -- დავუშვათ სახელი და გვარი ერთად არის უნიკალური და სხვა მასწავლებელი ამ სახელი და გვარით არ არსებობს

IF (@TeacherId IS NULL)
BEGIN
INSERT INTO dbo.Teacher
(
    Firstname,
    Lastname,
    Gender,
    Subject
)
VALUES
(  @TeacherFirstName, -- Firstname - nvarchar(20)
   @TeacherLastName, -- Lastname - nvarchar(20)
   @TeacherGender, -- Gender - nvarchar(20)
   @Subject  -- Subject - nvarchar(20)
    )
SELECT @TeacherId=SCOPE_IDENTITY()
END


SELECT @PupilId = Id FROM dbo.Pupil
WHERE Firstname=@PupilFirstName AND Lastname=@PupilLastName -- დავუშვათ სახელი და გვარი ერთად არის უნიკალური და სხვა მოსწავლე ამ სახელი და გვარით არ არსებობს

IF (@PupilId IS NULL)
BEGIN
INSERT INTO dbo.Pupil
(
    Firstname,
    Lastname,
    Gender,
    Class
)
VALUES
(   @PupilFirstName, -- Firstname - nvarchar(20)
    @PupilLastName, -- Lastname - nvarchar(20)
    @PupilGender, -- Gender - nvarchar(20)
    @Class -- Class - nvarchar(20)
)
SELECT @PupilId=SCOPE_IDENTITY()
END

INSERT INTO dbo.Teacher_Pupil
(
    TeacherId,
    PupilId
)
VALUES
(   @TeacherId, -- TeacherId - int
    @PupilId  -- PupilId - int
)

/************************************ინფორმაციის წამოღება*************************************/

SELECT * FROM dbo.Teacher
SELECT * FROM dbo.Pupil
SELECT * FROM dbo.Teacher_Pupil


SELECT t.* FROM dbo.Teacher_Pupil tp
JOIN dbo.Pupil p ON p.Id = tp.PupilId
JOIN dbo.Teacher t ON t.Id = tp.TeacherId
WHERE p.Firstname=N'გიორგი'