string? Directorio;

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

Console.WriteLine("---------------------");
Console.WriteLine("Listado de archivos en \"" + Directorio + "\"");

foreach (string Archivo in ListadoArchivos)
{
    if (Archivo != ".gitignore")
    {
        Console.WriteLine(Archivo);
        FileInfo fileOp = new FileInfo(Archivo);
        Console.WriteLine("Tamaño: {0} KB" , ((fileOp.Length)/1024));
    }
}


