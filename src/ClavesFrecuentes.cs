namespace AnalizadorArchivos;


//palabras mas frecuentes
public static class ClavesFrecuentes
{
    public static string[] Extraer(List<string> archivos, int n)
    {
        Dictionary<string, int> freq = new Dictionary<string, int>();
        foreach (string archivo in archivos)
        {
            string[] palabras = File.ReadAllText(archivo)
                .Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string p in palabras)
            {
                string limpia = Limpiar(p);
                if (limpia.Length < 3) continue;
                if (freq.ContainsKey(limpia)) freq[limpia]++;
                else freq[limpia] = 1;
            }
        }

        List<KeyValuePair<string, int>> lista = new List<KeyValuePair<string, int>>(freq);
        lista.Sort((a, b) => b.Value.CompareTo(a.Value));
        int cuenta = Math.Min(n, lista.Count);
        string[] claves = new string[cuenta];
        for (int i = 0; i < cuenta; i++)
            claves[i] = lista[i].Key;
        return claves;
    }

    private static string Limpiar(string p)
    {
        return p.Trim('.', ',', ';', ':', '!', '?', '"', '\'', '(', ')', '[', ']', '-', '_', '*')
                .ToLowerInvariant();
    }
}
