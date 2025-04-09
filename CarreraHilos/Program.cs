using System;
using System.Threading;

class CarreraDeHilos
{
    static int meta = 100; // Distancia total a recorrer
    static object bloqueo = new object(); // Para sincronizar acceso a la consola

    static void Corredor(object obj)
    {
        string nombre = (string)obj;
        int distancia = 0;
        Random rand = new Random(Guid.NewGuid().GetHashCode()); // Semilla aleatoria única

        while (distancia < meta)
        {
            int paso = rand.Next(1, 10); // El corredor avanza entre 1 y 9 pasos
            distancia += paso;
            lock (bloqueo)
            {
                Console.WriteLine($"{nombre} ha avanzado a {Math.Min(distancia, meta)} metros.");
            }
            Thread.Sleep(rand.Next(100, 300)); // Simula el tiempo entre pasos
        }

        lock (bloqueo)
        {
            Console.WriteLine($" {nombre} ha terminado la carrera!");
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando carrera...\n");

        Thread[] corredores = new Thread[3];
        string[] nombres = { "Hilo 1", "Hilo 2", "Hilo 3" };

        for (int i = 0; i < corredores.Length; i++)
        {
            corredores[i] = new Thread(Corredor);
            corredores[i].Start(nombres[i]);
        }

        // Esperar que todos terminen
        foreach (Thread corredor in corredores)
        {
            corredor.Join();
        }

        Console.WriteLine("\n Carrera finalizada.");
    }
}
