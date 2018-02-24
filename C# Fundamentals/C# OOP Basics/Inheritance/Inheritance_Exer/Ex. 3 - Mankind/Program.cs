using System;

public class Program
{
    static void Main()
    {
        try
        {
            var studentData = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var studentFirstName = studentData[0];
            var studentLastName = studentData[1];
            var studentFacultyNumber = studentData[2];
            var student = new Student(studentFirstName, studentLastName, studentFacultyNumber);

            var workerData = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var workerFirstName = workerData[0];
            var workerLastName = workerData[1];
            var workerWeekSalary = decimal.Parse(workerData[2]);
            var workerDailyWorkHours = decimal.Parse(workerData[3]);
            var worker = new Worker(workerFirstName, workerLastName, workerWeekSalary, workerDailyWorkHours);

            Console.WriteLine(student);
            Console.WriteLine(worker);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

