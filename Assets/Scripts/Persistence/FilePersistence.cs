using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Tracker
{
    /// <summary>
    /// Clase encargada de escribir en un fichero local los datos de los eventos
    /// </summary>
    class FilePersistence : IPersistence
    {
        //Ruta
        private string path;
        
        public FilePersistence(SerializerType serializerType)
        {
            switch (serializerType)
            {
                case SerializerType.CSV:
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Beerkings" + System.DateTime.Now.Ticks.ToString() + ".csv");
                    Console.WriteLine(path);
                    break;
            }
        }

        public FilePersistence(SerializerType serializerType, string gameName)
        {
            switch (serializerType)
            {
                case SerializerType.CSV:
                    string partialPath = path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName);
                    path = partialPath + Path.DirectorySeparatorChar + System.DateTime.Now.Ticks.ToString() + ".csv";
                    if (!Directory.Exists(partialPath))
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName));

                    Console.WriteLine(path);
                    break;
            }
        }

        public void Send(string eventString)
        {
            //Creación del fichero
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            //Buffer que guarda el string convertido en bytes
            byte[] info = new UTF8Encoding(true).GetBytes(eventString);

            stream.Write(info, 0, info.Length);

            //Se cierra el fichero
            stream.Close();
        }
    }
}