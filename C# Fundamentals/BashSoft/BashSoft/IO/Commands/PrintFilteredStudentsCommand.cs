﻿using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.StaticData;

namespace BashSoft.IO.Commands
{
    [Alias("filter")]
    internal class PrintFilteredStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintFilteredStudentsCommand(string input, string[] data) 
            : base(input, data) { }
        
        public override void Execute()
        {
            if (this.Data.Length == 5)
            {
                var courseName = this.Data[1];
                var filter = this.Data[2].ToLower();
                var takeCommand = this.Data[3].ToLower();
                var takeQuantity = this.Data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
        }

        private void TryParseParametersForFilterAndTake(
           string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    var wasParsed = int.TryParse(takeQuantity, out int studentsToTake);

                    if (wasParsed)
                    {
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }
    }
}
