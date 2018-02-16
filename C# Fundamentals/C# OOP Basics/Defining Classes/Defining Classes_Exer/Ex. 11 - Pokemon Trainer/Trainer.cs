using System;
using System.Collections.Generic;

public class Trainer 
{
    private string name;
    private List<Pokemon> pokemon;
    private int badges;

    public Trainer(string name)
    {
        this.Name = name;
        this.Pokemon = new List<Pokemon>();
    }

    public string Name { get => name; set => name = value; }

    public List<Pokemon> Pokemon { get => pokemon; set => pokemon = value; }

    public int Badges { get => badges; set => badges = value; }
}

