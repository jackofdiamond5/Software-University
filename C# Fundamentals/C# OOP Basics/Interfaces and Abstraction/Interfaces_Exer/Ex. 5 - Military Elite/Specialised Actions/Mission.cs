using System;

public class Mission
{
    private string state;

    public Mission(string codeName, string state)
    {
        this.CodeName = codeName;
        this.State = state;
    }

    public string CodeName { get; }

    public string State
    {
        get => this.state;
        set
        {
            if (this.state == "Finished") return;

            if (value.Trim() != "inProgress" && value.Trim() != "Finished")
            {
                throw new ArgumentException();
            }

            this.state = value;
        }
    }

    //public void CompleteMission()
    //{
    //    if (this.State == "inProgress")
    //    {
    //        this.State = "Finished";
    //    }
    //}

    public override string ToString()
    {
        return $"Code Name: {this.CodeName} State: {this.State}";
    }
}