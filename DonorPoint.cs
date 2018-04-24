using System;

namespace Symulacja_Cyfrowa
{
    public class DonorPoint
    {
        private static void Main()
        {
            var scheduler = new Blood[10,50]; // scheduler as a matrix that contains 50 time units, one unit may has 10 bloodUnits in 1 expDate.
            var bloodStorage = new BloodStore(10); // object contains bloodStorage
            bloodStorage.FillBlood(scheduler); // initializes Blood into Storage
            var patientQ = new PatientQueue(); // object contains patientQueue
            Console.WriteLine("Przed sortowaniem: ");
            bloodStorage.ShowExpDate();
            bloodStorage.BloodStorageList.Sort((x, y) => x.ExpDate.CompareTo(y.ExpDate));
            Console.WriteLine("\nPo sortowaniu: ");
            bloodStorage.ShowExpDate();
            BloodStore.ShowScheduler(scheduler);

            for(int i=0;i<5;i++) bloodStorage.NewDonnor(scheduler); // Gives new 5 Donnors
            BloodStore.ShowScheduler(scheduler); 

            for(int i=0; i<5; i++) patientQ.NewPatient(); // creates 5 patients to the Queue
            patientQ.ShowQueue();
            bloodStorage.Transfusion(3,patientQ,scheduler); // Transfusion of 3 units of Blood
            patientQ.ShowQueue();
            BloodStore.ShowScheduler(scheduler);
        }
    }
}
