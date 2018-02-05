namespace BashSoft
{
    public class BashSoftProgram
    {
        public static void Main()
        {
            IoManager.ChangeCurrentDirectoryAbsolute(@"C:\Windows");
            IoManager.TraverseDirectory(20);
        }
    }
}
