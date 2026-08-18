using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AnalizadorArchivos.Tests;

public class AnalizadorTests
{
    // creamos una carpeta temporal con archivos de texto de prueba.
    private string CrearCarpetaDePrueba()
    {
        string carpeta = Path.Combine(Path.GetTempPath(), "prueba_" + Path.GetRandomFileName());
        string[] muestra = {

            "estamos testeando nuestro proyecto final de programacion paralela",
            "esta prueba verifica que el modo secuencial y el paralelo den el mismo resultado",
            "el analizador cuenta palabras lineas caracteres y coincidencias de terminos clave",
            "si la sincronizacion con lock funciona los tres modos deben coincidir siempre"

        };
        for (int i = 0; i < 40; i++)
        {
            string sub = Path.Combine(carpeta, "carpeta_" + (i % 4));
            Directory.CreateDirectory(sub);
            string texto = "";
            for (int k = 0; k < 50; k++)
                texto += muestra[(i + k) % muestra.Length] + "\n";
            File.WriteAllText(Path.Combine(sub, "doc_" + i + ".txt"), texto);
        }
        return carpeta;
    }

    [Fact]
    public void BuscadorEncuentraTodosLosArchivos()
    {
        string carpeta = CrearCarpetaDePrueba();
        try
        {
            List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
            Assert.Equal(40, archivos.Count);
        }
        finally { Directory.Delete(carpeta, true); }
    }

    [Fact]
    public void LosTresModosDanElMismoTotalDePalabras()
    {
        string carpeta = CrearCarpetaDePrueba();
        try
        {
            List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
            string[] claves = ClavesFrecuentes.Extraer(archivos, 20);
            Analizador a = new Analizador(claves);

            Reporte sec = a.Secuencial(archivos);
            Reporte porArchivo = a.ParaleloPorArchivo(archivos);
            Reporte porGrupos = a.ParaleloPorGrupos(archivos, 12);

            Assert.Equal(sec.TotalPalabras, porArchivo.TotalPalabras);
            Assert.Equal(sec.TotalPalabras, porGrupos.TotalPalabras);
        }
        finally { Directory.Delete(carpeta, true); }
    }

    [Fact]
    public void LosTresModosDanLasMismasCoincidencias()
    {
        string carpeta = CrearCarpetaDePrueba();
        try
        {
            List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
            string[] claves = ClavesFrecuentes.Extraer(archivos, 20);
            Analizador a = new Analizador(claves);

            Reporte sec = a.Secuencial(archivos);
            Reporte porArchivo = a.ParaleloPorArchivo(archivos);
            Reporte porGrupos = a.ParaleloPorGrupos(archivos, 12);

            Assert.Equal(sec.TotalCoincidencias, porArchivo.TotalCoincidencias);
            Assert.Equal(sec.TotalCoincidencias, porGrupos.TotalCoincidencias);
        }
        finally { Directory.Delete(carpeta, true); }
    }

    [Fact]
    public void ParaleloPorGruposDaLosMismosCaracteresYLineas()
    {
        string carpeta = CrearCarpetaDePrueba();
        try
        {
            List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
            string[] claves = ClavesFrecuentes.Extraer(archivos, 20);
            Analizador a = new Analizador(claves);

            Reporte sec = a.Secuencial(archivos);
            Reporte porGrupos = a.ParaleloPorGrupos(archivos, 12);

            Assert.Equal(sec.TotalCaracteres, porGrupos.TotalCaracteres);
            Assert.Equal(sec.TotalLineas, porGrupos.TotalLineas);
        }
        finally { Directory.Delete(carpeta, true); }
    }

    [Fact]
    public void ExtraeTerminosClaveDeLosDocumentos()
    {
        string carpeta = CrearCarpetaDePrueba();
        try
        {
            List<string> archivos = BuscadorArchivos.BuscarTxt(carpeta);
            string[] claves = ClavesFrecuentes.Extraer(archivos, 20);
            Assert.NotEmpty(claves);
        }
        finally { Directory.Delete(carpeta, true); }
    }
}