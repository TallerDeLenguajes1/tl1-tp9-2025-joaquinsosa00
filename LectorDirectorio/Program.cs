// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine("Ingrese el path del directorio a analizar: ");
string path = Console.ReadLine();
bool existe = Directory.Exists(path);

if (!existe)
{
        do
        {
            Console.WriteLine("No existe ese directorio - Ingrese un path existente: ");
            path = Console.ReadLine();
            existe = Directory.Exists(path);
            if (existe)
            {

                Console.WriteLine("....Si existe ese directorio....");

            }
        } while (!existe);
}


Console.WriteLine("\n********Subdirectorios encontrados*******\n");

            string[] subdirectorios = Directory.GetDirectories(path);

            if (subdirectorios.Length == 0)
            {
                Console.WriteLine("No se encontraron subdirectorios.");
            }
            else
            {
                Console.WriteLine("Se encontraron subdirectorios.");
                foreach (string dir in subdirectorios)
                {
                    Console.WriteLine(dir);
                }
            }

            string[] archivos = Directory.GetFiles(path);

        if (archivos.Length == 0)
        {
            Console.WriteLine("No se encontraron archivos.");
        }
        else
        {
            Console.WriteLine("***Se encontraron archivos***\n");

            string rutaCsv = Path.Combine(path, "reporte_archivos.csv");

            // Crear y escribir encabezado una sola vez
            using (StreamWriter writer = new StreamWriter(rutaCsv))
            {
                writer.WriteLine("NombreArchivo;Tamanio(KB);UltimaModificacion");

                foreach (string file in archivos)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    long tamañoArchivo = fileInfo.Length;
                    double tamañoKB = tamañoArchivo / 1024.0;
                    string ultimaMod = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                    Console.WriteLine($"{file}  Tamaño: {tamañoKB:F2} KB");

                    writer.WriteLine($"\"{fileInfo.Name}\";{tamañoKB:F2};{ultimaMod}");
                }
            }

            Console.WriteLine($"\n📁 Reporte CSV generado en: {rutaCsv}");
        }
    
