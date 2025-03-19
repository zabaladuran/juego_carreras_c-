namespace CarreraDeAutos.Models;

public class Auto
{
    public string Marca { get; }
    public string Color { get; }
    public int VelocidadBase { get; }
    public int Ruedas { get; }
    public string Emoji { get; }

    public Auto(string marca, string color, int velocidadBase, int ruedas, string emoji)
    {
        Marca = marca;
        Color = color;
        VelocidadBase = velocidadBase;
        Ruedas = ruedas;
        Emoji = emoji;
    }
}
