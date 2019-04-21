using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace TrackerP3
{
    public static class EventCreator
    {

        //Podría ser interesante comprobar la conexion a internet y proporcionar la hora del equipo solo si no hay acceso a la red
        /// <summary>
        /// Muerte actor
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static Event Dead(ActorSubjectType actor, ActorSubjectType subject)
        {
            return new Event(DateTime.Now, EvenType.Dead, actor, subject, "");
        }
        public static Event Dead(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.Dead, actor, subject, extra);
        }
        /// <summary>
        /// Daño actor
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static Event Damage(ActorSubjectType actor, ActorSubjectType subject)
        {
            return new Event(DateTime.Now, EvenType.Damage, actor, subject, "");
        }
        public static Event Damage(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.Damage, actor, subject, extra);
        }
        /// <summary>
        /// Evento de interacción de actor y sujeto
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static Event Interact(ActorSubjectType actor, ActorSubjectType subject)
        {
            return new Event(DateTime.Now, EvenType.Interact, actor, subject, "");
        }
        public static Event Interact(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.Interact, actor, subject, extra);
        }
        /// <summary>
        /// Posición actor/sujeto
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="scene"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Event Position(ActorSubjectType actor, string scene, int x, int y, int z)
        {
            string xtra = scene + ": " + x.ToString() + " " + y.ToString() + " " + z.ToString();
            return new Event(DateTime.Now, EvenType.Position, actor, ActorSubjectType.None, xtra);
        }
        /// <summary>
        /// Inicio
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static Event Init(ActorSubjectType actor, ActorSubjectType subject)
        {
            return new Event(DateTime.Now, EvenType.Init, actor, subject, "");
        }
        public static Event Init(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.Init, actor, subject, extra);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static Event Final(ActorSubjectType actor, ActorSubjectType subject)
        {
            return new Event(DateTime.Now, EvenType.Final, actor, subject, "");
        }
        public static Event Final(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.Final, actor, subject, extra);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="subject"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public static Event UserDefinedEvent(ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            return new Event(DateTime.Now, EvenType.UserDefinedEvent, actor, subject, extra);
        }
    }

    public enum EvenType { Dead, Damage, Interact, Position, Init, Final, UserDefinedEvent, None }
    public enum ActorSubjectType {Player, NPC, Enemy, Boss, Item, PowerUp, DeathZone, Trigger, Scene, Game, Other, None}
    public class Event
    {
        DateTime _timeStamp;
        EvenType _verb;
        ActorSubjectType _actor;
        ActorSubjectType _subject;
        string _extra;


        public Event(DateTime timeStamp, EvenType verb, ActorSubjectType actor, ActorSubjectType subject, string extra)
        {
            _timeStamp = timeStamp;
            _verb = verb;
            _actor = actor;
            _subject = subject;
            _extra = extra;
        }

        /// <summary>
        /// Método encargado de serializar los parámetros del evento a CSV
        /// </summary>
        /// <returns></returns>
        public string ToCSV()
        {
            string CSVString;
            CSVString = _timeStamp.ToString() + "," + _verb.ToString() + "," + _actor.ToString() + "," + _subject.ToString() + "," + _subject.ToString() + "," + _extra + "\n";
            return CSVString;
        }

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
        }
        public EvenType Verb
        {
            get { return _verb; }
        }
        public ActorSubjectType Actor
        {
            get { return _actor; }
        }
        public ActorSubjectType Subject
        {
            get { return _subject; }
        }
        public string Extra
        {
            get { return _extra; }
        }
    }
}
