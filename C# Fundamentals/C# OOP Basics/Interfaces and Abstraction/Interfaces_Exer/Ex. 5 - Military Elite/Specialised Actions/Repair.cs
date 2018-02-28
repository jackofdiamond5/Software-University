public class Repair
{
    public Repair(string name, int hoursWorked)
    {
        this.Name = name;
        this.HoursWorked = hoursWorked;
    }

    public string Name { get; }

    public int HoursWorked { get; }

    public override string ToString()
    {
        return $"Part Name: {this.Name} Hours Worked: {this.HoursWorked}";
    }
}