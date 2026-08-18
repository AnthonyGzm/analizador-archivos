using AnalizadorArchivos;
using System.Diagnostics;

string carpeta = @"C:\Users\antho\OneDrive\Documentos\DocumentosAnalizar";

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

 reloj.Restart();
Reporte r3 = analizador.ParaleloPorGrupos(archivos, grupos);
reloj.Stop();
double t3 = reloj.Elapsed.TotalMilliseconds;

//reporte
Console.WriteLine("------------------");
Console.WriteLine("------REPORTE-----");
Console.WriteLine("------------------");

Console.WriteLine($"Archivos analizados: {r1.TotalArchivos:N0}");
Console.WriteLine($"Total de lineas:     {r1.TotalLineas:N0}");
Console.WriteLine($"Total de palabras:   {r1.TotalPalabras:N0}");
Console.WriteLine($"Total de caracteres: {r1.TotalCaracteres:N0}");

Console.WriteLine("-------------------------------------------");
Console.WriteLine($"Coincidencias de terminos clave: {r1.TotalCoincidencias:N0}");
Console.WriteLine("-------------------------------------------");
Console.WriteLine("Top 3 palabras mas repetidas:");
foreach (KeyValuePair<string, int> par in r1.TopPalabras(3))
    Console.WriteLine($"   {par.Key} -> {par.Value:N0}");

//Analisis de rendimiento

Console.WriteLine("---------------------------------");
Console.WriteLine("-----Analisis de Rendimiento-----");
Console.WriteLine("---------------------------------");

Console.WriteLine($"Nucleos disponibles: {Environment.ProcessorCount}\n");

Console.WriteLine($"Modo Secuencial:        {t1,8:N1} ms   (speedup 1.00x, eficiencia 100%)");
Console.WriteLine($"Modo Paralelo x archivo:{t2,8:N1} ms  (speedup {t1 / t2:N2}x, eficiencia {(t1 / t2) / grupos * 100:N0}%)");
Console.WriteLine($"Modo Paralelo x grupos: {t3,8:N1} ms   (speedup {t1 / t3:N2}x, eficiencia {(t1 / t3) / grupos * 100:N0}%)");

//verificacion
Console.WriteLine("---------------------------------");
Console.WriteLine("Verificacion:                    ");
Console.WriteLine("---------------------------------");

Console.WriteLine($"  Secuencial:   {r1.TotalPalabras:N0} palabras");
Console.WriteLine($"  Por archivo:  {r2.TotalPalabras:N0} palabras");
Console.WriteLine($"  Por grupos:   {r3.TotalPalabras:N0} palabras");
Console.WriteLine($"  Coincidencias: {r1.TotalCoincidencias:N0} = {r2.TotalCoincidencias:N0} = {r3.TotalCoincidencias:N0}");

Console.WriteLine("\nPresiona ENTER para salir...");
Console.ReadLine();


