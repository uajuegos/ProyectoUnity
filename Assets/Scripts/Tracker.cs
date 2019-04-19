using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Tracker
{
    public class Tracker
    {
        //Atributos----------------------------------------------------------------------------------------------------------------------------------------------
        private static Tracker instance = null;
        static Object dummyCola, dummyPend;
        static Queue<Event> cola;
        static Queue<Event> pendientes;
        static ISerializer serializer;
        static IPersistence persistance;

        static bool exit;
        static bool flushing;
        private float FlushRate; //Tiempo entre cada flush TODO: AL EDITOR


        //Metodos------------------------------------------------------------------------------------------------------------------------------------------------
        private Tracker()
        {
            cola = new Queue<Event>();
            pendientes = new Queue<Event>();
            flushing = false;
            exit = false;
            dummyCola = new object();
            dummyPend = new object();


            serializer = new CSVSerializer();
            persistance = new FilePersistence(SerializerType.CSV, "Beerkings");
            FlushRate = 5.0f;

            Start();//Comienzo
        }

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

        void Flush()
        {
            Thread.Sleep((int)(FlushRate * 1000));
            Console.WriteLine("Starting Flush Thread");
            int i = 0;
            while (!exit)
            {
                lock (dummyCola)
                {
                    flushing = true;
                    ProcessQueue();
                    flushing = false;
                    //No se hace flush hasta pasados 5 segundos
                }

                Thread.Sleep((int)(FlushRate * 100));
            }
        }
        void ProcessQueue()
        {
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
        }

        public void Start()
        {
            Thread t = new Thread(Flush);
            t.Start();
        }
        void Exit() { exit = true; }
    }
}
