using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symulacja_Cyfrowa
{
    class NewPatient : Event
    {
        private int _eventTime;
        private string _type;
        public NewPatient(int eventTime,string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }
        public override void Execute(List<Event> scheduler, BloodStore bloodStorage, PatientQueue patientQ)
        {
            _type = "NP";
            int timeExp = Rnd.Next(5,16);
            Console.WriteLine("\n### NEW PATIENT ###");
            var i = BloodStore.Rnd.Next(1, 6); // Patient needs 1-5 units of Blood
            patientQ.PatientQ.Enqueue(new Patient(i)); // new Patient who needs X blood units
            patientQ.BloodNeeded = patientQ.BloodNeeded + i;

            // Planning New Event
            // TIME_EXP comes from generator Exponential and Average P
            scheduler.Add(new NewPatient(SystemTime+timeExp, "NP"));
            Sorted(scheduler);
        }
    }
}
