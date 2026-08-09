namespace AnalizadorArchivos;

public class Analizador
{
    private readonly object candado = new object();

    // Aqui se buscan dentro las palabras claves de cada documento.
    private readonly string[] palabrasClave;

    public Analizador(string[] palabrasClave)
    {
        this.palabrasClave = palabrasClave;
    }

    public Reporte AnalizarArchivo(string ruta)
    {
        Reporte r = new Reporte();
        r.TotalArchivos = 1;

        string texto = File.ReadAllText(ruta);
        r.TotalCaracteres = texto.Length;

        string[] palabras = texto.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        r.TotalPalabras = palabras.Length;
        foreach (string palabra in palabras)
        {
            if (r.Frecuencias.ContainsKey(palabra)) r.Frecuencias[palabra]++;
            else r.Frecuencias[palabra] = 1;
        }

        foreach (char c in texto)
            if (c == '\n') r.TotalLineas++;

        foreach (string clave in palabrasClave)
            r.TotalCoincidencias += ContarOcurrencias(texto, clave);

        return r;
    }

}