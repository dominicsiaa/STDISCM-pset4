namespace Classify.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public double Score { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
