using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CarreraDeAutos.Models
{
    public class Carrera
    {
        private List<Jugador> Jugadores;
        private Pista Pista;
        private Dictionary<Jugador, int> Posiciones;
        private Random Randomizador = new();
        private string Clima;

        public Carrera(List<Jugador> jugadores, Pista pista)
        {
            Jugadores = jugadores;
            Pista = pista;
        }

        private void DeterminarClima()
        {
            string[] climas = { "Soleado ☀️", "Lluvia 🌧️", "Nieve ❄️", "Viento fuerte 💨" };
            Clima = climas[Randomizador.Next(climas.Length)];
            Console.WriteLine($"🌦️ Clima actual: {Clima}");
        }

        public void Empezar()
        {
            do
            {
                ReiniciarCarrera();
                MostrarCuentaRegresiva();
                DeterminarClima();

                Console.WriteLine($"🏁 ¡La carrera en {Pista.Nombre} ha comenzado! 🏁\n");
                Console.WriteLine($"🌦️ Clima: {Clima}\n");

                int meta = 100; // Aumentamos la distancia de la carrera
                int tiempoMaximo = Pista.TiempoMaximo * 30; // Aumentamos la duración total de la carrera

                for (int tiempo = 0; tiempo < tiempoMaximo; tiempo++)
                {
                    Console.Clear();
                    Console.WriteLine($"⏳ Tiempo: {tiempo / 10.0:F1} seg / {Pista.TiempoMaximo} seg");
                    Console.WriteLine($"🌦️ Clima: {Clima}\n");
                    DibujarPista(meta);

                    foreach (var jugador in Jugadores)
                    {
                        int avanceBase = jugador.Auto.VelocidadBase - Pista.ReduccionVelocidad;
                        int avance = Math.Max(1, avanceBase / 5 + Randomizador.Next(0, 3));

                        if (Clima == "Lluvia 🌧️") avance -= 1;
                        if (Clima == "Nieve ❄️") avance -= 2;
                        if (Clima == "Viento fuerte 💨") avance += Randomizador.Next(-1, 2);

                        // Eventos aleatorios
                        int evento = Randomizador.Next(100);
                        if (evento < 5)
                        {
                            Console.WriteLine($"⚡ {jugador.Nombre} sufrió un fallo mecánico y su velocidad bajó!");
                            avance /= 2;
                        }
                        else if (evento < 10)
                        {
                            Console.WriteLine($"🛑 {jugador.Nombre} tuvo un pinchazo y perdió tiempo!");
                            Thread.Sleep(1000);
                        }
                        else if (evento < 15)
                        {
                            Console.WriteLine($"🚀 {jugador.Nombre} activó un Turbo Boost y aceleró!");
                            avance *= 2;
                        }

                        Posiciones[jugador] += avance;
                        if (Posiciones[jugador] >= meta)
                        {
                            Posiciones[jugador] = meta;
                        }
                    }

                    if (Posiciones.Values.Any(pos => pos >= meta))
                    {
                        break;
                    }

                    Thread.Sleep(800);
                }

                MostrarResultados();
                Console.Write("¿Quieres jugar otra carrera? (S/N): ");
            } while (Console.ReadLine().Trim().ToUpper() == "S");
        }

        private void ReiniciarCarrera()
        {
            Posiciones = new Dictionary<Jugador, int>();
            foreach (var jugador in Jugadores)
            {
                Posiciones[jugador] = 0;
            }
            Console.Clear();
        }

        private void MostrarCuentaRegresiva()
        {
            Console.Clear();
            Console.WriteLine("🏎️ ¡Prepárate para la carrera! 🏎️\n");

            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine($"⏳ {i}...");
                Console.Beep(800, 500);
                Thread.Sleep(700);
                Console.Clear();
            }

            Console.WriteLine("🚦 ¡ARRANCA! 🚦");
            Console.Write("🔥 ¡VROOOM!");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Console.Beep(1000 + (i * 200), 100);
                Thread.Sleep(300);
            }
            Thread.Sleep(700);
            Console.Clear();
        }

        private void DibujarPista(int meta)
        {
            Console.Clear();
            Console.WriteLine($"🚧 {Pista.Nombre} - Terreno: {Pista.TipoTerreno}");
            Console.WriteLine(new string('=', Console.WindowWidth - 2));

            foreach (var jugador in Jugadores)
            {
                int posicion = Posiciones[jugador];
                int anchoPista = Console.WindowWidth - 4;

                string pista = "|";
                for (int j = 0; j <= anchoPista; j++)
                {
                    if (j == (posicion * anchoPista / meta))
                    {
                        pista += jugador.Auto.Emoji;
                    }
                    else
                    {
                        pista += ".";
                    }
                }
                pista += "|";
                Console.WriteLine(pista);
            }

            Console.WriteLine(new string('=', Console.WindowWidth - 2));
        }

        private void MostrarResultados()
        {
            Console.Clear();
            Console.WriteLine("🏁 ¡Carrera Finalizada! 🏁\n");

            var resultados = Posiciones.OrderByDescending(kv => kv.Value).ToList();
            int maxPosicion = resultados[0].Value;

            var ganadores = resultados.Where(r => r.Value == maxPosicion).ToList();

            if (ganadores.Count > 1)
            {
                Console.WriteLine("⚠️ ¡Empate! ⚠️ Los siguientes vehículos llegaron al mismo tiempo:");
                foreach (var ganador in ganadores)
                {
                    MostrarDatosVehiculo(ganador.Key);
                }
            }
            else
            {
                Console.WriteLine($"🥇 ¡Ganador! {ganadores[0].Key.Nombre} con {ganadores[0].Key.Auto.Emoji}");
                MostrarDatosVehiculo(ganadores[0].Key);
            }

            Console.WriteLine("\n📊 Resultados Finales:");
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"🏎️ { resultado.Key.Nombre } con { resultado.Key.Auto.Emoji }");
                MostrarDatosVehiculo(resultado.Key);
                Console.WriteLine("-----------------------------");
            }
        }

        private void MostrarDatosVehiculo(Jugador jugador)
        {
            double kilometros = Posiciones[jugador] * 0.1;
            Console.WriteLine($"🚗 Vehículo: {jugador.Auto.Marca} - {jugador.Auto.Emoji}");
            Console.WriteLine($"⚡ Velocidad Final: {jugador.Auto.VelocidadBase} km/h");
            Console.WriteLine($"📏 Kilómetros Recorridos: {kilometros:F2} km");
        }
    }
}
