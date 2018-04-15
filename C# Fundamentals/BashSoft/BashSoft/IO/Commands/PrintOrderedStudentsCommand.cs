using BashSoft.Contracts;
using BashSoft.Attributes;
using BashSoft.StaticData;

namespace BashSoft.IO.Commands
{
    [Alias("order")]
    internal class PrintOrderedStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintOrderedStudentsCommand(string input, string[] data)
            : base(input, data) { }

        public override void Execute()
        {
            if (this.Data.Length == 5)
            {
                var courseName = this.Data[1];
                var filter = this.Data[2].ToLower();
                var orderCommand = this.Data[3].ToLower();
                var takeQuantity = this.Data[4].ToLower();

                TryParseParemetersForOrderAndTake(orderCommand, takeQuantity, courseName, filter);
            }
        }

        private void TryParseParemetersForOrderAndTake(
            string orderCommand, string takeQuantity, string courseName, string filter)
        {
            if (orderCommand == "order")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, filter);
                }
                else
                {
                    var wasParsed = int.TryParse(takeQuantity, out int studentsToTake);

                    if (wasParsed)
                    {
                        this.repository.OrderAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidOrderCommand);
            }
        }
    }
}
