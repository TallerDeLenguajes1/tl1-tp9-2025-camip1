string Directorio;

do
{
    Console.WriteLine("Ingrese la ruta del Directorio que desea analizar:");
    Directorio = Console.ReadLine();
    if (!Directory.Exists(Directorio))
    {
        Console.WriteLine("Inavlido: Directorio inexistente. Intente nuevamente.");
    }
} while (!Directory.Exists(Directorio));

List<string> ListadoCarpetas = Directory.GetDirectories(Directorio).ToList();
List<string> ListadoArchivos = Directory.GetFiles(Directorio).ToList();

Console.WriteLine("---------------------");
Console.WriteLine("Listado de carpetas en \"" + Directorio + "\"");

foreach (string Carpeta in ListadoCarpetas)
{
    Console.WriteLine(Carpeta);
}

string NuevoArchivo = Directorio + @"\reporte_archivos.csv";
FileInfo fileInfo;

Console.WriteLine("---------------------");
Console.WriteLine("Listado de archivos en \"" + Directorio + "\"");

if (!File.Exists(NuevoArchivo))
{
    File.Create(NuevoArchivo).Close();
}

string[] Lineas = new string[ListadoArchivos.Count + 1];
Lineas[0] = "Ruta ; Tamaño (KB) ; Última modificación";
int i = 1;

foreach (string Archivo in ListadoArchivos)
{
    Console.WriteLine(Archivo);
    fileInfo = new FileInfo(Archivo);
    Console.WriteLine("Tamaño: {0} KB", ((fileInfo.Length) / 1024));
    string nombre = fileInfo.FullName;
    double tamaño = fileInfo.Length / 1024;
    DateTime ultimaModificacion = fileInfo.LastWriteTimeUtc;
    Lineas[i] = $"{nombre}; {tamaño:F2}; {ultimaModificacion}";
    i++;
}

File.WriteAllLines(NuevoArchivo, Lineas);
Console.WriteLine($"Reporte guardado en {NuevoArchivo}");

