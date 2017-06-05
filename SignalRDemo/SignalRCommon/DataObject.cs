using System;

namespace SignalRCommon
{
    [Serializable]
    public class DataObject : IDataObject
    {
        public double Amount { get; set; }
        
        public int Index { get; set; }

        public string Name { get; set; }
    }
}
