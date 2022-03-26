using System;

namespace VISAInstrument.Ports
{
    internal class PortEventArgs : EventArgs
    {
        public string Address { get; }
        public bool Cancel { set; get; }
        public PortEventArgs(string address)
        {
            Address = address;
        }
    }
}
