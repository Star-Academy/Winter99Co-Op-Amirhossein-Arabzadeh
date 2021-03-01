namespace Phase04
{
    public class StudentInfo
    {
        public StudentInfo(double averageScore, string firstName = null, string lastName = null)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AverageScore = averageScore;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public double AverageScore { get; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}: {this.AverageScore}";
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                   (obj.GetType() == typeof(StudentInfo)) &&
                   ((StudentInfo) obj).FirstName.Equals(this.FirstName) &&
                   ((StudentInfo) obj).LastName.Equals(this.LastName) &&
                   ((StudentInfo) obj).AverageScore.Equals(this.AverageScore);
        }
    }
}