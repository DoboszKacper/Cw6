using APBD5.Models;
using APBD5.DTOs.RequestModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APBD5.Services
{
    public interface IStudentsDbService
    {
        void CreateStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, int idEnrollment, SqlConnection sqlConnection = null, SqlTransaction transaction = null);
        Enrollment CreateStudentWithStudies(StudentWithStudiesRequest request);
        Student GetStudent(string indexNumber);
        IEnumerable<Student> GetStudents();
        bool CheckIfEnrollmentExists(string studies, int semester);
        Enrollment PromoteStudents(string studies, int semester);
    }
}
