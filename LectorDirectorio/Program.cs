string? input;

do
{
    Console.WriteLine("Ingrese la ruta del Directorio que desea analizar:");
    input = Console.ReadLine();
    if (!Directory.Exists(input))
    {
        Console.WriteLine("Inavlido: Directorio inexistente. Intente nuevamente.");
    }
} while (!Directory.Exists(input));

Console.WriteLine("---------------");
Console.WriteLine("Listado de carpetas");


