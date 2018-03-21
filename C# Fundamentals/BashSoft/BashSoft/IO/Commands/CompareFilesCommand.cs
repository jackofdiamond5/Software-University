﻿using BashSoft.IO.Commands;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO
{
    internal class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, Tester judge, 
            StudentsRepository repository, IoManager inputOutputManager) 
            : base(input, data, judge, repository, inputOutputManager) { }

        public override void Execute()
        {
            if (this.Data.Length == 3)
            {
                var firstPath = this.Data[1];
                var secondPath = this.Data[2];

                this.Judge.CompareContent(firstPath, secondPath);
            }
        }
    }
}