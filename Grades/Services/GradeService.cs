using Grades.Infrastructure;
using Grades.Model;

namespace Grades.Services
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetStudentGrades(int instructorId);
        IEnumerable<Grade> GetGradesOfStudent(int studentId);
        bool InsertGrade(Grade grade);
    }
    public class GradeService : IGradeService
    {
        private readonly DataAccess _dataAccess;
        public GradeService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public IEnumerable<Grade> GetStudentGrades(int instructorId)
        {
            return _dataAccess.GetStudentGrades(instructorId);
        }
        public IEnumerable<Grade> GetGradesOfStudent(int studentId)
        {
            return _dataAccess.GetGradesOfStudent(studentId);
        }
        public bool InsertGrade(Grade grade)
        {
            return _dataAccess.InsertGrade(grade);
        }
    }
}
