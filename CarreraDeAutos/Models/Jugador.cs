namespace CarreraDeAutos.Models;

public class Jugador
{
    public string Nombre { get; }
    public Auto Auto { get; }
    public int Posicion { get; set; } = 0;

    public Jugador(string nombre, Auto auto)
    {
        Nombre = nombre;
        Auto = auto;
    }
}
