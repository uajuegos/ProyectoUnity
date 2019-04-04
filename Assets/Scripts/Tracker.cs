using UajTracker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Tracker {
    
    private static Tracker instance = null;

    public Alternative Alternative;
    private Queue<Event> cola = new Queue<Event>();
    private Queue<Event> pendientes = new Queue<Event>();


    public bool AddEvnt(Event e)
    {
        if (!flushing)
        {
            lock (cola)
            {
                if (pendientes.Count > 0)
                {

                    cola = new Queue<Event>(pendientes);
                    pendientes.Clear();
                }
                cola.Enqueue(e);
            }
        }
        else
            pendientes.Enqueue(e);

        return true;
    }
    readonly object flushLockObject = new object();
    bool exit;
    bool flushing;
    public void Flush()
    {
        //disco o servidor
        
            while (!exit)
            {
                lock (cola)
                {
                    flushing = true;
                    ProcessQueue();
                    flushing = false;
                }
            }
    }
    FileStyleUriParser p;

    private void ProcessQueue() {
        while (cola.Count > 0) {
            Event e = cola.Dequeue();
            //serializer.Serialize(e)
        }

        //persintance.Write()

    }
    private Tracker()
    {

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
}
