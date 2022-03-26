using Ivi.Visa;
using System;
using System.Linq;
using System.Text;

namespace VISAInstrument.Ports
{
    internal abstract class PortOperatorBase : IPortOperator
    {
        public string Address { set; get; }

        protected PortOperatorBase(IMessageBasedSession session)
        {
            Session = session;
        }

        protected PortOperatorBase(IMessageBasedSession session, string address) : this(session)
        {
            Address = address;
        }

        public int Timeout { set; get; } = 2000;

        public event EventHandler<PortEventArgs> PortOpening;

        public event EventHandler<PortEventArgs> PortClosing;

        protected virtual void OnPortOpening(PortEventArgs e)
        {
            PortOpening?.Invoke(this, e);
        }

        protected virtual void OnPortClosing(PortEventArgs e)
        {
            PortClosing?.Invoke(this, e);
        }

        public bool IsPortOpen { private set; get; }

        protected IMessageBasedSession Session { get; }

        public virtual void Open()
        {
            PortEventArgs e = new PortEventArgs(Address);
            OnPortOpening(e);
            if (e.Cancel) return;
            Session.TimeoutMilliseconds = Timeout;
            this.IsPortOpen = true;
        }

        public virtual void Close()
        {
            PortEventArgs e = new PortEventArgs(Address);
            OnPortClosing(e);
            if (e.Cancel) return;
            Session.Dispose();
            this.IsPortOpen = false;
        }

        public virtual void Write(string command)
        {
            Session.RawIO.Write(command);
        }

        public virtual void Write(byte[] command)
        {
            Session.RawIO.Write(command);
        }

        public virtual void WriteLine(byte[] command)
        {
            Session.RawIO.Write(command.Concat(new byte[] { 0x0A }).ToArray());
        }

        public virtual void WriteLine(string command)
        {
            Write($"{command}\n");
        }

        public byte[] ReadToBytes()
        {
            return Session.RawIO.Read();
        }

        public byte[] ReadToBytes(int count)
        {
            return Session.RawIO.Read(count);
        }

        public string Read()
        {
            return Encoding.ASCII.GetString(Session.RawIO.Read());
        }

        public string Read(int count)
        {
            return Encoding.ASCII.GetString(Session.RawIO.Read(count));
        }

        public string ReadLine()
        {
            string result = Read();
            return result.EndsWith("\n") ? result.TrimEnd(new char[] { '\n' }) : result;
        }
    }
}
