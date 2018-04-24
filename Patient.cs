namespace Symulacja_Cyfrowa
{
    public class Patient
    {

        private int _bloodUnits;
        public Patient(int bloodUnit)
        {
            _bloodUnits = bloodUnit;
        }
        public int BloodUnits
        {
            get { return _bloodUnits; }
            set
            {
                _bloodUnits = value;
                _bloodUnits = BloodUnits;
            }
        }
    }
}
