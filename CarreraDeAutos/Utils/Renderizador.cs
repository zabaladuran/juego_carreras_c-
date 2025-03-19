using CarreraDeAutos.Models;

namespace CarreraDeAutos.Utils;

public static class Renderizador
{
    public static void MostrarPista(List<Jugador> jugadores, Pista pista)
    {
        Console.Clear();
        Console.WriteLine($"🚦 Carrera en {pista.Nombre} ({pista.TipoTerreno})");

        foreach (var jugador in jugadores)
        {
            string pistaGrafica = new string('-', jugador.Posicion) + jugador.Auto.Emoji;
            Console.WriteLine($"{jugador.Nombre}: {pistaGrafica}");
        }
    }
}
