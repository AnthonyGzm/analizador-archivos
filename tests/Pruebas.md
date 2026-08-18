# Pruebas del Analizador de Archivos

Se realizaron diferentes pruebas para comprobar que las funciones principales del Analizador de Archivos funcionan correctamente.

## Resultados generales

* **Pruebas ejecutadas:** 5
* **Pruebas exitosas:** 5
* **Pruebas con errores:** 0
* **Pruebas omitidas:** 0
* **Tiempo total:** 257 ms aproximadamente

## Pruebas realizadas

### 1. BuscadorEncuentraTodosLosArchivos

Esta prueba verifica que el sistema pueda encontrar correctamente todos los archivos que se encuentran en la ubicación indicada.

**Resultado:** Correcta.

### 2. ExtraeTerminosClaveDeLosDocumentos

Esta prueba verifica que el sistema pueda extraer correctamente los términos clave encontrados dentro de los documentos.

**Resultado:** Correcta.

### 3. LosTresModosDanElMismoTotal

Esta prueba comprueba que los tres modos de procesamiento del analizador produzcan el mismo resultado.

Esto permite verificar que, aunque se utilicen diferentes formas de procesamiento, los datos obtenidos sean consistentes.

**Resultado:** Correcta.

### 4. LosTresModosDanElMismoTotal...

Esta prueba realiza otra comprobación de los resultados obtenidos mediante los diferentes modos de procesamiento, verificando que los totales coincidan.

**Resultado:** Correcta.

### 5. ParaleloPorGruposDaLosMismos...

Esta prueba verifica que el procesamiento paralelo por grupos produzca los mismos resultados que los demás métodos.

Su objetivo es comprobar que dividir los archivos en grupos y procesarlos de manera paralela no altere los resultados del análisis.

**Resultado:** Correcta.

## Conclusión

Las pruebas realizadas fueron exitosas, ya que las **5 pruebas terminaron correctamente y no se encontraron errores**.