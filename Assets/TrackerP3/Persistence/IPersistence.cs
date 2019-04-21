
namespace TrackerP3
{
    // Enumerado con los diferentes modos de guardado de datos
    public enum PersistenceType { FILE }

    /// <summary>
    /// Interfaz de la clase Persistence
    /// </summary>
    public interface IPersistence
    {
        /// <summary>
        /// Guarda las trazas en la forma de guardado de datos implementada (disco o server)
        /// </summary>
        /// <param name="eventString"></param>
        void Send(string eventString);
    }
}
