namespace CarreraDeAutos.Models;

public class Pista
{
    public string Nombre { get; }
    public int Longitud { get; }
    public string TipoTerreno { get; }
    public int TiempoMaximo { get; }
    public int ReduccionVelocidad { get; }

    public Pista(string nombre, int longitud, string tipoTerreno, int tiempoMaximo, int reduccionVelocidad)
    {
        Nombre = nombre;
        Longitud = longitud;
        TipoTerreno = tipoTerreno;
        TiempoMaximo = tiempoMaximo;
        ReduccionVelocidad = reduccionVelocidad;
    }
}
