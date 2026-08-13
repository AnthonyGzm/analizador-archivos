using AnalizadorArchivos;
using System.Diagnostics;

string carpeta = @"C:\Users\maria\OneDrive\Desktop\Paralela\Finalfinal\DocumentosAnalizar";

// Verificacion de carpeta
if (!Directory.Exists(carpeta))
{
    Console.WriteLine("La carpeta no existe: " + carpeta);
    Console.WriteLine("Verifica la ruta en Program.cs y vuelve a ejecutar.");
    Console.WriteLine("\nPresiona ENTER para salir...");
    Console.ReadLine();
    return;
}

List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
if (archivos.Count == 0)
{
    Console.WriteLine("La carpeta no se puede analizar porque no contiene archivos .txt:");
    Console.WriteLine("  " + carpeta);
    Console.WriteLine("Coloca archivos de texto (.txt) en esa carpeta y vuelve a ejecutar.");
    Console.WriteLine("\nPresiona ENTER para salir...");
    Console.ReadLine();
    return;
}
    Console.WriteLine($"Analizando {archivos.Count:N0} archivos de: {carpeta}\n");

// Palabras frecuentes
string[] palabrasClave = ClavesFrecuentes.Extraer(archivos, 800);
Analizador analizador = new Analizador(palabrasClave);
int grupos = Environment.ProcessorCount;

analizador.Secuencial(archivos);

// Stopwatch de modos
Stopwatch reloj = new Stopwatch();

reloj.Restart();
Reporte r1 = analizador.Secuencial(archivos);
reloj.Stop();
double t1 = reloj.Elapsed.TotalMilliseconds;

reloj.Restart();
Reporte r2 = analizador.ParaleloPorArchivo(archivos);
reloj.Stop();
double t2 = reloj.Elapsed.TotalMilliseconds;
