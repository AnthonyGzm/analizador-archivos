using AnalizadorArchivos;

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