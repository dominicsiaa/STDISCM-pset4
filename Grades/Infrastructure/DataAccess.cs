using Grades.Model;

namespace Grades.Infrastructure
{
    public class DataAccess : IDisposable
    {
        private readonly DataContext _context;

        public DataAccess(DataContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); 
        }

        public void Dispose() => _context.Dispose();

        public List<Grade> GetGrades()
        {
            return _context.Grades.ToList();
        }

        public Grade? GetGradeById(int id)
        {
            return _context.Grades.FirstOrDefault(g => g.Id == id);
        }

        public bool InsertGrade(GradeRequest gradeRequest)
        {
            var grade = new Grade
            {
                Name = gradeRequest.Name,
                StudentId = gradeRequest.StudentId,
                TeacherId = gradeRequest.TeacherId,
                Score = gradeRequest.Score,
                DateRecorded = DateTime.Now
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();

            return true;
        }

    }
}
