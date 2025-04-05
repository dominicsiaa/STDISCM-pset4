using RateProfs.Infrastructure;
using RateProfs.Model;


namespace RateProfs.Services
{
    public interface IRateProfService
    {
        IEnumerable<RateProf> GetRatingsOfStudent(int studentId);
        void SubmitRating(RateProf rating);

        IEnumerable<RateProf> GetRatingsOfProfessor(int professorId);

    }

    public class RateProfService : IRateProfService
    {
        private readonly DataAccess _dataAccess;

        public RateProfService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<RateProf> GetRatingsOfStudent(int studentId)
        {
            return _dataAccess.GetRatingsByStudentId(studentId);
        }

        public void SubmitRating(RateProf rating)
        {
            _dataAccess.SubmitRating(rating);
        }

        public IEnumerable<RateProf> GetRatingsOfProfessor(int professorId)
        {
            return _dataAccess.GetRatingsByProfessorId(professorId);
        }
    }

}
