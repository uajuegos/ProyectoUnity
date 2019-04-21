namespace TrackerP3
{
    // Enumerado con los tipos de formatos disponibles para serializar
    public enum SerializerType { CSV }

    /// <summary>
    /// Interfaz de la clase Serializer
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializa un evento en el formato correspondiente
        /// </summary>
        /// <param name="trackerEvent"></param>
        /// <returns></returns>
        string Serialize(Event trackerEvent);
    }
}
