using System;
using System.Collections.Generic;

namespace Symulacja_Cyfrowa
{
    public class PatientQueue
    {
        private int _bloodNeeded;
        public Queue<Patient> PatientQ = new Queue<Patient>();
        public int BloodNeeded
        {
            get { return _bloodNeeded; }
            set
            {
                _bloodNeeded = value;
                _bloodNeeded = BloodNeeded;
            }
        }
        public void NewPatient()
        {
            var x = BloodStore.Rnd.Next(1, 6); // Patient needs 1-5 units of Blood
            PatientQ.Enqueue(new Patient(x)); // new Patient who needs X blood units
            BloodNeeded = BloodNeeded + x;
            // if bloodNeeded > bloodLevel -> make an EmergencyOrder on Q bloodUnits
        }
        public Patient RemovePatient()
        {
            var p1 = PatientQ.Dequeue();
            BloodNeeded = BloodNeeded - p1.BloodUnits;
            return p1;
        }
        public void ShowQueue()
        {
            int summ = 0;
            Patient[] queue = PatientQ.ToArray();
            Console.WriteLine("\nPatients in Queue: "+ PatientQ.Count);
            for (int i = 0; i < PatientQ.Count; i++)
            {
                Console.Write(i+": " + queue[i].BloodUnits + "\n");
                summ = queue[i].BloodUnits + summ;
            }
            Console.WriteLine("Blood Needed by Patients: "+ summ);
        }
    }
}
