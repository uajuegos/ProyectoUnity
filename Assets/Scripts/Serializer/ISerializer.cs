using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tracker
{

    // Enumerado con los tipos de formato para serializar
    public enum SerializerType { CSV }

    /// <summary>
    /// Interfaz de la clase Serializer
    /// </summary>
    interface ISerializer
    {
        string Serialize(Event trackerEvent);
    }
}
