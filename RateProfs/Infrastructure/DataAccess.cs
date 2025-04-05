using RateProfs.Model;

namespace RateProfs.Infrastructure
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

        public IEnumerable<RateProf> GetRatingsByStudentId(int studentId)
        {
            return _context.Ratings.Where(r => r.StudentId == studentId).ToList();
        }

        public void SubmitRating(RateProf rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public IEnumerable<RateProf> GetRatingsByProfessorId(int professorId)
        {
            return _context.Ratings
                .Where(r => r.InstructorId == professorId)
                .ToList();
        }


    }
}
