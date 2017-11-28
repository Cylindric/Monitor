using System;

namespace Utils
{
    public class CounterValue
    {
        public int CounterValueId { get; set; }
        public DateTime TimeLogged { get; set; }
        public string HostName { get; set; }
        public string Category { get; set; }
        public string Counter { get; set; }
        public string Instance { get; set; }
        public float Value { get; set; }
    }
}
