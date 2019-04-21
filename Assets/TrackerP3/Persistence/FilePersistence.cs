using System;
using System.Text;
using System.IO;

namespace TrackerP3
{
    /// <summary>
    /// Clase encargada de escribir en un fichero local los datos de los eventos
    /// </summary>
    class FilePersistence : IPersistence
    {
        //Ruta
        private string filePath;

        /// <summary>
        /// Según el tipo de formato, crea el directorio y obtiene la ruta del fichero en el que guardará los eventos de la ejecución
        /// </summary>
        /// <param name="serializerType"></param>
        /// <param name="gameName"></param>
        public FilePersistence(SerializerType serializerType, string gameName)
        {
            //Obtenemos la ruta del directorio en función del nombre de juego
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName);

            //Comprobamos si existe y lo creamos
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameName));

            //Obtenemos la ruta del fichero
            switch (serializerType)
            {
                case SerializerType.CSV:
                    filePath = directoryPath + Path.DirectorySeparatorChar + Tracker.ID + ".csv";
                    break;
            }
        }

        /// <summary>
        /// Guarda las trazas en disco
        /// </summary>
        /// <param name="eventString"></param>
        public void Send(string eventString)
        {
            //Apertura/creación del fichero
            FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);

            //Buffer que guarda el string convertido en bytes
            byte[] streamInfo = new UTF8Encoding(true).GetBytes(eventString);

            //Mueve el puntero
            stream.Position = stream.Length;

            //Escribe en fichero
            stream.Write(streamInfo, 0, streamInfo.Length);

            //Se cierra el fichero
            stream.Close();
        }
    }
}