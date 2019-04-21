
namespace TrackerP3
{
    /// <summary>
    /// Clase encargada de serializar una cadena al formato CSV
    /// </summary>
    class CSVSerializer : ISerializer
    {
        /// <summary>
        /// Serializa un evento en CSV
        /// </summary>
        /// <param name="trackerEvent"></param>
        /// <returns></returns>
        public string Serialize(Event trackerEvent)
        {
            return trackerEvent.ToCSV();
        }
    }
}
