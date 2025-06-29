using System.Text;
using System.IO;
using TagMp3;

string nombreArchivo; // = "c:\taller-de-lenguajes\tl1-tp9-2025-camip1\LectorTagMP3\12 al vacío.mp3";

do
{
    Console.WriteLine("Ingrese la ruta de un archivo MP3:");
    nombreArchivo = Console.ReadLine();
} while (!File.Exists(nombreArchivo));

using (FileStream fstreamr = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read))
{
    if (fstreamr.Length < 128)
    {
        Console.WriteLine("Archivo invalido. No tiene informacion suficiente.");
    }
    else
    {
        Id3v1Tag TagInfo = new Id3v1Tag();
        fstreamr.Seek(-128, SeekOrigin.End);

        byte[] buffer = new byte[128];
        //fstreamr.Read(buffer, 0, 128);
        fstreamr.ReadExactly(buffer, 0, 128);

        string header = Encoding.GetEncoding("latin1").GetString(buffer, 0, 3);
        if (header != "TAG")
        {
            Console.WriteLine("El archivo no contiene una etiqueta ID3v1.");
        }
        else
        {
            TagInfo.Header = header;
            TagInfo.Titulo = Encoding.GetEncoding("latin1").GetString(buffer, 3, 30).TrimEnd('\0');
            TagInfo.Artista = Encoding.GetEncoding("latin1").GetString(buffer, 33, 30).TrimEnd('\0');
            TagInfo.Album = Encoding.GetEncoding("latin1").GetString(buffer, 63, 30).TrimEnd('\0');
            string anioString = Encoding.GetEncoding("latin1").GetString(buffer, 93, 4).TrimEnd('\0');
            if (int.TryParse(anioString, out int anio))
            {
                TagInfo.Anio = anio;
            }
            else
            {
                TagInfo.Anio = 0; //por defecto
            }

            TagInfo.Comentario = Encoding.GetEncoding("latin1").GetString(buffer, 97, 30).TrimEnd('\0');
            TagInfo.Genero = TagInfo.Genero = buffer[127].ToString();

            TagInfo.MostrarInfo();
        }
    }
}
