using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace TrackerP3
{
    public class Tracker
    {
        #region Singleton

        public static Tracker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Tracker();
                }
                return instance;
            }
        }
        private static Tracker instance = null;

        private Tracker()
        {
        }

        #endregion Singleton

        //Atributos----------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ID único de sesion
        /// </summary>
        public static string ID = System.DateTime.Now.Ticks.ToString();

        private ISerializer serializer;
        private IPersistence persistance;

        private Thread flushThread;

        static Object dummyCola, dummyPend;
        static Queue<Event> cola;
        static Queue<Event> pendientes;

        static bool exit;
        static bool flushing;
        private float _flushRate; //Tiempo entre cada flush TODO: AL EDITOR

        /// <summary>
        /// Inicialización del tracker. Es necesario una inicialización explicita
        /// Genera evento de inicio de juego
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="serializerType"></param>
        /// <param name="persistenceType"></param>
        public void Init(string gameName, SerializerType serializerType, PersistenceType persistenceType, float flushRate)
        {
            switch (serializerType)
            {
                case SerializerType.CSV:
                    serializer = new CSVSerializer();
                    break;
            }

            switch (persistenceType)
            {
                case PersistenceType.FILE:
                    persistance = new FilePersistence(serializerType, gameName);
                    break;
            }

            _flushRate = flushRate;

            cola = new Queue<Event>();
            pendientes = new Queue<Event>();
            flushing = false;
            exit = false;
            dummyCola = new object();
            dummyPend = new object();

            flushThread = new Thread(FlushRoutine);
            flushThread.Start();

            AddEvent(EventCreator.Init(ActorSubjectType.None, ActorSubjectType.None, ID));
        }

        /// <summary>
        /// Desconecta el tracker
        /// Genera evento de fin de juego
        /// </summary>
        public void End()
        {
            exit = true;
            flushThread.Abort();

            AddEvent(EventCreator.Final(ActorSubjectType.Game, ActorSubjectType.Game, ID));

            Flush();
        }

        public void AddEvent(Event e)
        {
            if (!flushing)
            {
                lock (dummyCola)
                {
                    if (pendientes.Count > 0)
                    {
                        lock (dummyPend)
                        {
                            cola = new Queue<Event>(pendientes);
                            pendientes.Clear();
                        }
                    }
                    cola.Enqueue(e);
                }
            }
            else
            {
                lock (dummyPend)
                {
                    pendientes.Enqueue(e);
                }
            }
        }

        private void FlushRoutine()
        {
            Thread.Sleep((int)(_flushRate * 1000));
            Console.WriteLine("Starting Flush Thread");

            while (!exit)
            {
                Flush();
                Thread.Sleep((int)(_flushRate * 1000));
            }
        }

        public void Flush()
        {
            lock (dummyCola)
            {
                flushing = true;
                if (cola.Count > 0)
                {
                    string flushString = "";

                    Queue<Event> copyQueue = new Queue<Event>(cola);
                    cola.Clear();

                    while (copyQueue.Count > 0)
                    {
                        Event e = copyQueue.Dequeue();
                        Console.WriteLine(e);
                        flushString += serializer.Serialize(e);
                    }
                    persistance.Send(flushString);
                }
                flushing = false;
            }
        }

    }
}
