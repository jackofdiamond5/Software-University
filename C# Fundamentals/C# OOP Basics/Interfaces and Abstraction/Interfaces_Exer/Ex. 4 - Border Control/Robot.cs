public class Robot : ICitizen
{
    public Robot(string robotModel, string robotId)
    {
        this.Model = robotModel;
        this.Id = robotId;
    }

    public string Id { get; }

    public string Model { get; }

    public override string ToString()
    {
        return this.Id;
    }
}