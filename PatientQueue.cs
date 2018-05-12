using System;
using System.Collections.Generic;
using System.Linq;

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
        public int BloodNeededFirst()
        {
            var temp = PatientQ.ElementAt(0);
            return temp.BloodUnits;
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
