namespace AnalizadorArchivos;

public static class BuscadorArchivos
{
    public static List<string> BuscarTxt(string carpeta)
    {



        List<string> resultado = new List<string>();

        foreach (string archivo in Directory.GetFiles(carpeta, "*.txt"))
            resultado.Add(archivo);

        foreach (string sub in Directory.GetDirectories(carpeta))
            resultado.AddRange(BuscarTxt(sub));




        return resultado;
    }
}
