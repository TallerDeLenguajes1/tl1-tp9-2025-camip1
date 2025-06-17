using System.Text;
using TagMp3;

string nombreArchivo; // = "c:\taller-de-lenguajes\tl1-tp9-2025-camip1\LectorTagMP3\12 al vacío.mp3";

do
{
    Console.WriteLine("Ingrese la ruta de un archivo MP3:");
    nombreArchivo = Console.ReadLine();
} while (!File.Exists(nombreArchivo));

using (FileStream fstreamr = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read))
{
    Id3v1Tag TagInfo = new Id3v1Tag();
    fstreamr.Seek(-128, SeekOrigin.End);

    byte[] buffer = new byte[128];
    fstreamr.Read(buffer, 0, 128);

    if (buffer.Length < 128)
    {
        Console.WriteLine("Archivo invalido. No tengo informacion suficiente.");
    }
    else
    {
        TagInfo.Header = Encoding.Latin1.GetString(buffer, 0, 3);
        TagInfo.Titulo = Encoding.Latin1.GetString(buffer, 3, 30).TrimEnd('\0').Trim();
        TagInfo.Artista = Encoding.Latin1.GetString(buffer, 33, 30).TrimEnd('\0').Trim();
        TagInfo.Album = Encoding.Latin1.GetString(buffer, 63, 30).TrimEnd('\0').Trim();

        string anioString = Encoding.Latin1.GetString(buffer, 93, 4).TrimEnd('\0').Trim();
        if (int.TryParse(anioString, out int anio))
        {
            TagInfo.Anio = anio;
        }
        else
        {
            TagInfo.Anio = 0;
        }

        TagInfo.Comentario = Encoding.Latin1.GetString(buffer, 97, 30).TrimEnd('\0').Trim();
        TagInfo.Genero = Encoding.Latin1.GetString(buffer, 127, 1).TrimEnd('\0').Trim();

        TagInfo.MostrarInfo();
    }
}
