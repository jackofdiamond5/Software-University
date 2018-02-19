public class Cargo
{
    private int cargoWeight;
    private string cargoType;

    public Cargo(int cargoWeight, string cargoType)
    {
        this.CargoWeight = cargoWeight;
        this.CargoType = cargoType;
    }

    public int CargoWeight { get => cargoWeight; set => cargoWeight = value; }

    public string CargoType { get => cargoType; set => cargoType = value; }
}