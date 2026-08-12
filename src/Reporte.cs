namespace AnalizadorArchivos;

public class Reporte
{
    public int TotalArchivos = 0;
    public long TotalLineas = 0;
    public long TotalPalabras = 0;
    public long TotalCaracteres = 0;
    public long TotalCoincidencias = 0;

    public Dictionary<string, int> Frecuencias = new Dictionary<string, int>();

    public void Combinar(Reporte otro)
    {
        TotalArchivos += otro.TotalArchivos;
        TotalLineas += otro.TotalLineas;
        TotalPalabras += otro.TotalPalabras;
        TotalCaracteres += otro.TotalCaracteres;
        TotalCoincidencias += otro.TotalCoincidencias;

        foreach (KeyValuePair<string, int> par in otro.Frecuencias)
        {
            if (Frecuencias.ContainsKey(par.Key))
                Frecuencias[par.Key] += par.Value;
            else
                Frecuencias[par.Key] = par.Value;
        }
    }
}
