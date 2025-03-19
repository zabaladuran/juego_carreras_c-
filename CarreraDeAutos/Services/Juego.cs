using CarreraDeAutos.Models;
using System;

namespace CarreraDeAutos.Services;

public class Juego
{
    public List<Jugador> Jugadores { get; private set; } = new();
    public List<Auto> AutosDisponibles { get; private set; } = new();
    public List<Pista> PistasDisponibles { get; private set; } = new();

    public Juego()
    {
        InicializarAutos();
        InicializarPistas();
    }

    private void InicializarAutos()
    {
        AutosDisponibles.Add(new Auto("Ferrari", "Rojo", 10, 4, "🏎️"));
        AutosDisponibles.Add(new Auto("Lamborghini", "Amarillo", 12, 4, "🚗"));
        AutosDisponibles.Add(new Auto("Tesla", "Blanco", 9, 4, "🚙"));
        AutosDisponibles.Add(new Auto("Bugatti", "Azul", 13, 4, "🚘"));
        AutosDisponibles.Add(new Auto("Ford", "Negro", 8, 4, "🚖"));
        AutosDisponibles.Add(new Auto("Chevrolet", "Gris", 7, 4, "🚔"));
        AutosDisponibles.Add(new Auto("McLaren", "Naranja", 11, 4, "🚕"));
    }

    private void InicializarPistas()
    {
        PistasDisponibles.Add(new Pista("Ciudad Nocturna", 50, "Asfalto", 15, 0));
        PistasDisponibles.Add(new Pista("Montaña", 40, "Tierra", 13, 2));
        PistasDisponibles.Add(new Pista("Desierto", 45, "Arena", 14, 3));
        PistasDisponibles.Add(new Pista("Bosque", 35, "Caminos de Tierra", 12, 4));
    }

    public void IniciarJuego()
    {
        Console.Clear();
        Console.WriteLine("🏎️ BIENVENIDO A LA CARRERA DE AUTOS 🏎️");
        SeleccionarJugadores();
        Pista pistaElegida = SeleccionarPista();
        Carrera carrera = new Carrera(Jugadores, pistaElegida);
        carrera.Empezar();
    }

    private void SeleccionarJugadores()
    {
        Console.Write("\n👉 Ingresa la cantidad de jugadores (2-5): ");
        int cantidadJugadores;
        while (!int.TryParse(Console.ReadLine(), out cantidadJugadores) || cantidadJugadores < 2 || cantidadJugadores > 5)
        {
            Console.Write("❌ Entrada no válida. Ingresa un número entre 2 y 5: ");
        }

        for (int i = 0; i < cantidadJugadores; i++)
        {
            Console.Write($"\n👤 Ingresa el nombre del jugador {i + 1}: ");
            string nombre = Console.ReadLine() ?? $"Jugador{i + 1}";
            Auto autoElegido = SeleccionarAuto(i + 1);
            Jugadores.Add(new Jugador(nombre, autoElegido));
        }
    }

    private Auto SeleccionarAuto(int numeroJugador)
    {
        Console.WriteLine($"\n🚗 Selección de Auto para Jugador {numeroJugador}:");
        for (int i = 0; i < AutosDisponibles.Count; i++)
        {
            var auto = AutosDisponibles[i];
            Console.WriteLine($"[{i + 1}] {auto.Emoji} {auto.Marca} - Color: {auto.Color}, Velocidad: {auto.VelocidadBase}, Ruedas: {auto.Ruedas}");
        }

        Console.Write("👉 Elige un auto (1-7): ");
        int seleccion;
        while (!int.TryParse(Console.ReadLine(), out seleccion) || seleccion < 1 || seleccion > AutosDisponibles.Count)
        {
            Console.Write("❌ Entrada no válida. Ingresa un número entre 1 y 7: ");
        }

        return AutosDisponibles[seleccion - 1];
    }

    private Pista SeleccionarPista()
    {
        Console.WriteLine("\n🏁 Selección de Pista:");
        for (int i = 0; i < PistasDisponibles.Count; i++)
        {
            var pista = PistasDisponibles[i];
            Console.WriteLine($"[{i + 1}] {pista.Nombre} - Terreno: {pista.TipoTerreno}, Longitud: {pista.Longitud}m, Tiempo Máx: {pista.TiempoMaximo}s, Reducción Velocidad: {pista.ReduccionVelocidad}");
        }

        Console.Write("👉 Elige una pista (1-4): ");
        int seleccion;
        while (!int.TryParse(Console.ReadLine(), out seleccion) || seleccion < 1 || seleccion > PistasDisponibles.Count)
        {
            Console.Write("❌ Entrada no válida. Ingresa un número entre 1 y 4: ");
        }

        return PistasDisponibles[seleccion - 1];
    }
}
