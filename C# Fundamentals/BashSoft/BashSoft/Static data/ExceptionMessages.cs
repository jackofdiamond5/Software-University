namespace BashSoft.Static_data
{
    public static class ExceptionMessages
    {
        public const string InexistingCourseInDataBase =
            "The course you are trying to get does not exist in the database!";

        public const string IndexistingStudentInDataBase =
            "The username for this student you are trying to get does not exist!";

        public const string UnauthorizedAccessExceptionMessage =
            "The folder/file you are trying to get access needs a higher level of rights than you currently have.";

        public const string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain mismatch.";
        
        public const string UnableToGetHigherPartitionHierarchy = "Cannot move higher in the partition hierarchy!";

        public const string UnableToParseNumber = "The sequence you've written is not a valid number.";

        public const string InvalidStudentsFilter = "Invalid students' filter!";

        public const string InvalidComparisonQuery = "Invalid comparison query!";

        public const string InvalidTakeCommand = "The take command expected does not match the format wanted!";

        public const string InvalidTakeQuantityParameter = "The value provided for parameter quantity was invalid!";

        public const string InvalidOrderCommand = "Invalid order command!";
        
        public const string InvalidNumberOfScores = "The number of scores for the given course is greater than the possible.";

        public const string InvalidScore = "Invalid score.";
        
        public const string InvalidDestination = "Invalid destination!";
    }
}
