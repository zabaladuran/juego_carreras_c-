namespace CarreraDeAutos.Models;

public class Potenciador
{
    public string Nombre { get; }
    public int AumentoVelocidad { get; }

    public Potenciador(string nombre, int aumentoVelocidad)
    {
        Nombre = nombre;
        AumentoVelocidad = aumentoVelocidad;
    }
}
