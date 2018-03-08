using System;

namespace BashSoft.Exceptions
{
    class CourseNotFoundException : Exception
    {
        public const string NotEnrolledInCourse =
            "Student must be enrolled in a course before you can set their mark.";

        public CourseNotFoundException() 
            : base(NotEnrolledInCourse) { }

        public CourseNotFoundException(string message) 
            : base(message) { }
    }
}
