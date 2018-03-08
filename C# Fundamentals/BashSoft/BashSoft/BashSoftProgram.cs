using BashSoft.IO;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main()
        {
            var tester = new Tester();
            var ioManager = new IoManager();
            var repository = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            var currentInterpreter = new CommandInterpreter(tester, repository, ioManager);
            var reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
