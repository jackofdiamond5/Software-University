namespace BashSoft.Static_data
{
    public static class ExceptionMessages
    {
        public const string DataAlreadyInitializedException = "Data is already initialized!";

        public const string DataNotInitializedException =
            "The data structure must be initialized first in order to make any operations with it.";

        public const string InexistingCourseInDataBase =
            "The course you are trying to get does not exist in the database!";

        public const string IndexistingStudentInDataBase =
            "The username for this student you are trying to get does not exist!";

        public const string InvalidPath =
            "The folder/file you are trying to access at the current address, does not exist";

        public const string UnauthorizedAccessExceptionMessage =
            "The folder/file you are trying to get access needs a higher level of rights than you currently have.";

        public const string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain mismatch.";

        public const string ForbiddenSymbolsContainedInName = 
            "The given name contains symbols that are not allowed to be used in names of files and folders.";

        public const string UnableToGetHigherPartitionHierarchy = "Cannot move higher in the partition hierarchy!";

        public const string UnableToParseNumber = "The sequence you've written is not a valid number.";

        public const string InvalidStudentsFilter = "Invalid students' filter!";

        public const string InvalidComparisonQuery = "Invalid comparison query!";

        public const string InvalidTakeCommand = "The take command expected does not match the format wanted!";

        public const string InvalidTakeQuantityParameter = "The value provided for parameter quantity was invalid!";

        public const string InvalidOrderCommand = "Invalid order command!";
    }
}
