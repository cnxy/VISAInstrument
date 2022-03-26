using System;

namespace VISAInstrument.Ports
{
    public class SerialDataReceivedEventArgs:EventArgs
    {
        public int BytesToRead { get; }
        public SerialDataReceivedEventArgs(int bytesToRead)
        {
            BytesToRead = bytesToRead;
        }
    }
}
