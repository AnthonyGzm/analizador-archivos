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

    // Metodo para contar las ocurrencias de una palabra en un texto
    private int ContarOcurrencias(string texto, string palabra)
    {
        int cuenta = 0;
        int i = 0;
        while ((i = texto.IndexOf(palabra, i, StringComparison.Ordinal)) != -1)
        {
            cuenta++;
            i += palabra.Length;
        }
        return cuenta;
    }

    // modo secuencial
    public Reporte Secuencial(List<string> archivos)
    {
        Reporte general = new Reporte();
        foreach (string archivo in archivos)
            general.Combinar(AnalizarArchivo(archivo));
        return general;
    }

    // modo paralelo por archivos
    public Reporte ParaleloPorArchivo(List<string> archivos)
    {
        Reporte general = new Reporte();
        List<Task> tareas = new List<Task>();
        foreach (string archivo in archivos)
        {
            string ruta = archivo;
            tareas.Add(Task.Factory.StartNew(() =>
            {
                Reporte parcial = AnalizarArchivo(ruta);
                lock (candado) { general.Combinar(parcial); }
            }));
        }
        Task.WhenAll(tareas).Wait();
        return general;
    }

    // modo paralelo por grupos
    public Reporte ParaleloPorGrupos(List<string> archivos, int grupos)
    {
        if (grupos > archivos.Count) grupos = archivos.Count;
        if (grupos < 1) grupos = 1;

        Reporte general = new Reporte();
        List<Task> tareas = new List<Task>();
        int tam = archivos.Count / grupos;
        for (int g = 0; g < grupos; g++)
        {
            int inicio = g * tam;
            int cuenta = (g == grupos - 1) ? archivos.Count - inicio : tam;
            List<string> grupo = archivos.GetRange(inicio, cuenta);
            tareas.Add(Task.Factory.StartNew(() =>
            {
                Reporte parcial = new Reporte();
                foreach (string archivo in grupo)
                    parcial.Combinar(AnalizarArchivo(archivo));
                lock (candado) { general.Combinar(parcial); }
            }));
        }
        Task.WhenAll(tareas).Wait();
        return general;
    }
}
