﻿using System;

namespace BashSoft.Exceptions
{
    class DuplicateEntryInStructureException : Exception
    {
        public const string DuplicateEntry = "The {0} already exists in {1}.";
        //StudentAlreadyEnrolledInGivenCourse
        public DuplicateEntryInStructureException(string message)
            : base(message) { }

        public DuplicateEntryInStructureException(string entry, string structure)
            : base(string.Format(DuplicateEntry, entry, structure)) { }
    }
}
