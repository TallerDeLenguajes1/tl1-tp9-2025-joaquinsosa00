using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Ingrese la ruta completa del archivo MP3:");
        string rutaArchivo = Console.ReadLine();

        if (!File.Exists(rutaArchivo))
        {
            Console.WriteLine("❌ El archivo no existe.");
            return;
        }

        try
        {
            using (FileStream fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length < 128)
                {
                    Console.WriteLine("❌ El archivo es muy pequeño para contener una etiqueta ID3v1.");
                    return;
                }

                // Leer últimos 128 bytes
                fs.Seek(-128, SeekOrigin.End);
                byte[] buffer = new byte[128];
                fs.Read(buffer, 0, 128);

                string tag = Encoding.Latin1.GetString(buffer, 0, 3);

                if (tag != "TAG")
                {
                    Console.WriteLine("❌ No se encontró el encabezado 'TAG'. Este archivo no tiene una etiqueta ID3v1.");
                    return;
                }

                Id3v1Tag id3 = new Id3v1Tag
                {
                    Titulo = Encoding.Latin1.GetString(buffer, 3, 30).TrimEnd('\0', ' '),
                    Artista = Encoding.ASCII.GetString(buffer, 33, 30).TrimEnd('\0', ' '),
                    Album = Encoding.ASCII.GetString(buffer, 63, 30).TrimEnd('\0', ' '),
                    Año = Encoding.ASCII.GetString(buffer, 93, 4).TrimEnd('\0', ' '),
                    
                };

                Console.WriteLine("\n✅ Información ID3v1 del archivo:");
                Console.WriteLine($"🎵 Título: {id3.Titulo}");
                Console.WriteLine($"🎤 Artista: {id3.Artista}");
                Console.WriteLine($"💿 Álbum: {id3.Album}");
                Console.WriteLine($"📅 Año: {id3.Año}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al leer el archivo: {ex.Message}");
        }
    }
}
