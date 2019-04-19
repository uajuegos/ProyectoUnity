using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tracker
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
        string ISerializer.Serialize(Event trackerEvent)
        {         
            return trackerEvent.ToCSV(); 
        }
    }
}
