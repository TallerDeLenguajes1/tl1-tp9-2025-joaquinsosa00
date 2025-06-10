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


            if (subdirectorios.Length == 0)
            {
                Console.WriteLine("No se encontraron archivos.");
            }
            else
            {
                Console.WriteLine("***Se encontraron archivos***\n");
                foreach (string files in archivos)
                {
                    
                    FileInfo fileInfo = new FileInfo(files);
                    long tamañoArchivo = files.Length;
                    double tamañoKB = tamañoArchivo / 1024.0;
                    Console.WriteLine(files+"  Tamaño: "+tamañoKB.ToString("F2")+" KB");
                }
            }

File.Create(path + "/reporte_archivoss.csv");
string rutaCompleta = Path.Combine(path, "reporte_archivos.csv");
File.Create(rutaCompleta);

