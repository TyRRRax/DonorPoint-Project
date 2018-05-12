using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Symulacja_Cyfrowa
{
    public class Event
    {
        private int _eventTime;
        private string _type;
        private static int _systemTime = 0;
        private bool _bloodFromEmergency = false; // idk why resharper thinks it's never used. It's my flag to be sure that Event is comming from Emergency Order

        public Event(int eventTime, string type)
        {
            _eventTime = eventTime;
            _type = type;
        }
        public Event() { }
        public static readonly Random Rnd = new Random();
        public static void AddToScheduler(List<Event> scheduler, Event temp)
        {
            scheduler.Add(temp);
        }
        public static void ShowScheduler(List<Event> scheduler)
        {
            foreach (var schedul in scheduler)
            {
                Console.Write(schedul.EventTime + " ");
            }
            Console.Write("\n");
            foreach (var type in scheduler)
            {
                Console.Write(type._type + " ");
            } 
        }
        public static void Sorted(List<Event> scheduler) // Sorting scheduler
        {
            scheduler.Sort((x, y) => x.EventTime.CompareTo(y.EventTime)); // Sorted Scheduler
        }
        public static void SortedByType(List<Event> scheduler)
        {
            scheduler.Sort((emp1, emp2) => emp1.Type.CompareTo(emp2.Type)); // Changes scheduler Order to remove just UT elements
        }
        public int EventTime
        {
            get { return _eventTime; }
            set
            {
                _eventTime = value;
                _eventTime = EventTime;
            }
        }
        public static int SystemTime
        {
            get { return _systemTime; }
            set
            {
                _systemTime = value;
                _systemTime = SystemTime;
            }
        }
        public string Type
        {
            get { return _type; }
            set { _type = Type; }
        }
        public bool BloodFromEmergency { get; set; }
        public virtual void Execute(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {

        }
    }
}
