﻿using System;

using BashSoft.Judge;
using BashSoft.Contracts;
using BashSoft.Repository;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        private Tester judge;
        private StudentsRepository repository;
        private IDirectoryManager inputOutputManager;

        public Command(string input, string[] data, Tester judge, 
            StudentsRepository repository, IDirectoryManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        protected string Input
        {
            get
            {
                return this.input;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        protected string[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        protected Tester Judge
        {
            get
            {
                return this.judge;
            }
        }

        protected StudentsRepository Repository
        {
            get
            {
                return this.repository;
            }
        }

        protected IDirectoryManager InputOutputManager
        {
            get
            {
                return this.inputOutputManager;
            }
        }

        public abstract void Execute();
    }
}
