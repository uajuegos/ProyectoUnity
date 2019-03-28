using UajTracker;
using System;
using System.Collections;
public class Tracker{
    
    private static Tracker instance = null;

    public Alternative Alternative;
    private Queue cola = new Queue<Event>();

    public bool AddEvnt(Event e)
    {
        cola.Enqueue(e);
        return true;
    }

    public void Flush()
    {
        //disco o servidor
        p.Flush();
    }
    FileStyleUriParser p;
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
