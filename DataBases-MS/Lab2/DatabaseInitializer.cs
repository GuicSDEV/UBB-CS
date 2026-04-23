using System.Data.SQLite;
using System.IO;
using System;

public static class DatabaseInitializer
{
    public static string DbPath =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.db");

    public static string ConnectionString =>
        $"Data Source={DbPath}";

    public static void Initialize()
    {
        if (!File.Exists(DbPath))
        {
            SQLiteConnection.CreateFile(DbPath);

            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = @"
            CREATE TABLE Students (
                StudentId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT
            );

            CREATE TABLE Grades (
                GradeId INTEGER PRIMARY KEY AUTOINCREMENT,
                StudentId INTEGER,
                Subject TEXT,
                Grade INTEGER,
                FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
            );

            INSERT INTO Students (Name) VALUES 
            ('Guilherme'), ('Ana'), ('Carlos');

            INSERT INTO Grades (StudentId, Subject, Grade) VALUES
            (1, 'Math', 10),
            (1, 'Physics', 9),
            (2, 'Biology', 8),
            (3, 'Programming', 10);
            ";

            cmd.ExecuteNonQuery();
        }
    }
}