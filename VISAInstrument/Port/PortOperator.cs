using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace VISAInstrument.Port
{


    enum FlowControl
    {
        None, XOnXOff,RtsCts,DtrDst
    }

    class RS232PortOperator:PortOperatorBase,IPortType
    {
        public Parity Parity { private set; get; }

        public StopBits StopBits { private set; get; }

        public int DataBits { private set; get; }

        public PortType PortType { get => PortType.RS232; }

        public FlowControl FlowControl { set; get; } = FlowControl.None;

        public RS232PortOperator(string address,Parity parity,StopBits stopBits,int dataBits) : base(address)
        {
            if (!address.ToUpper().Contains("ASRL"))
                throw new ArgumentException($"该地址不含ASRL字样");
            Parity = parity;
            if (stopBits == StopBits.None) throw new NotSupportedException($"不支持停止位为：{stopBits.ToString()}");
            StopBits = stopBits;
            if(dataBits<5 || dataBits>8) throw new NotSupportedException($"不支持数据位为：{dataBits.ToString()}");
            DataBits = dataBits;
        }

        public override void Open()
        {
            base.Open();
            int result = 0;
            switch (Parity)
            {
                case Parity.None:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_PARITY, VISA32.VI_ASRL_PAR_NONE);break;
                case Parity.Odd:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_PARITY, VISA32.VI_ASRL_PAR_ODD); break;
                case Parity.Even:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_PARITY, VISA32.VI_ASRL_PAR_EVEN); break;
                case Parity.Mark:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_PARITY, VISA32.VI_ASRL_PAR_MARK); break;
                case Parity.Space:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_PARITY, VISA32.VI_ASRL_PAR_SPACE); break;
            }
            PortUltility.ThrowIfResultExcepiton(result);
            switch(StopBits)
            {
                case StopBits.One:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_STOP_BITS, VISA32.VI_ASRL_STOP_ONE); break;
                case StopBits.OnePointFive:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_STOP_BITS, VISA32.VI_ASRL_STOP_ONE5); break;
                case StopBits.Two:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_STOP_BITS, VISA32.VI_ASRL_STOP_TWO); break;
            }
            PortUltility.ThrowIfResultExcepiton(result);
            result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_DATA_BITS, DataBits);
            PortUltility.ThrowIfResultExcepiton(result);
            switch (FlowControl)
            {
                case FlowControl.None:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_FLOW_CNTRL, VISA32.VI_ASRL_FLOW_NONE); break;
                case FlowControl.XOnXOff:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_FLOW_CNTRL, VISA32.VI_ASRL_FLOW_XON_XOFF); break;
                case FlowControl.RtsCts:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_FLOW_CNTRL, VISA32.VI_ASRL_FLOW_RTS_CTS); break;
                case FlowControl.DtrDst:
                    result = VISA32.viSetAttribute(VI, VISA32.VI_ATTR_ASRL_FLOW_CNTRL, VISA32.VI_ASRL_FLOW_DTR_DSR); break;
            }
            PortUltility.ThrowIfResultExcepiton(result);
        }
    }

    class USBPortOperator : PortOperatorBase, IPortType
    {
        public USBPortOperator(string address) : base(address)
        {
            if (!address.ToUpper().Contains("USB"))
                throw new ArgumentException($"该地址不含USB字样");
        }
        public PortType PortType { get => PortType.USB; }
    }

    class GPIBPortOperator : PortOperatorBase, IPortType
    {
        public GPIBPortOperator(string address) : base(address)
        {
            if (!address.ToUpper().Contains("GPIB"))
                throw new ArgumentException($"该地址不含GPIB字样");
        }
        public PortType PortType { get => PortType.GPIB; }
    }

    class LANPortOperator : PortOperatorBase, IPortType
    {
        public LANPortOperator(string address) : base(address)
        {
            if (!address.ToUpper().Contains("TCPIP"))
                throw new ArgumentException($"该地址不含TCPIP字样");
        }
        public PortType PortType { get => PortType.LAN; }
    }

    class PortEventArgs:EventArgs
    {
        public string Address { private set; get; }
        public bool Cancel { set; get; }
        public PortEventArgs(string address)
        {
            Address = address;
        }
    }

    abstract class PortOperatorBase:IPortOperator
    {
        public string Address { private set; get; }

        public PortOperatorBase(string address) => Address = address;

        public int Timeout { set; get; } = 2000;


        public event EventHandler<PortEventArgs> PortOpenning;
        public event EventHandler<PortEventArgs> PortClosing;

        protected virtual void OnPortOpenning(PortEventArgs e)
        {
            PortOpenning?.Invoke(this, e);
        }

        protected virtual void OnPortClosing(PortEventArgs e)
        {
            PortClosing?.Invoke(this, e);
        }

        int result;
        int sesn;
        protected int vi;

        protected int VI { private set; get; }

        public bool IsPortOpen { private set; get; } = false;
        public virtual void Open()
        {
            PortEventArgs e = new PortEventArgs(Address);
            OnPortOpenning(e);
            if(!e.Cancel)
            {
                result = VISA32.viOpenDefaultRM(out sesn);
                result = VISA32.viOpen(sesn, Address, 0, Timeout, out vi);
                VI = vi;
                PortUltility.ThrowIfResultExcepiton(result);
                this.IsPortOpen = true;
            }
        }

        public virtual void Close()
        {
            PortEventArgs e = new PortEventArgs(Address);
            OnPortClosing(e);
            if (!e.Cancel)
            {
                result = VISA32.viClose(vi);
                PortUltility.ThrowIfResultExcepiton(result);
                this.IsPortOpen = false;
            }
        }

        public virtual void Write(string command)
        {
            byte[] commandBytes = Encoding.ASCII.GetBytes(command);
            result = VISA32.viWrite(vi, commandBytes, commandBytes.Length, out int retCount);
            PortUltility.ThrowIfResultExcepiton(result);
        }

        public virtual void WriteLine(string command)
        {
            Write($"{command}\n");
        }

        public const int READ_BUFFER_COUNT = 1024;

        public virtual string Read()
        {
            byte[] resultBytes = new byte[READ_BUFFER_COUNT];
            result = VISA32.viRead(vi, resultBytes, READ_BUFFER_COUNT, out int retCount);
            PortUltility.ThrowIfResultExcepiton(result);
            return retCount != 0 ? Encoding.ASCII.GetString(resultBytes.Take(retCount).ToArray()):null;
        }

        public virtual string ReadLine()
        {
            string result = Read();
            return result.EndsWith("\n") ? result.TrimEnd(new char[] { '\n' }) : result;
        }
    }

    interface IPortOperator
    {
        void Open();
        void Close();
        void Write(string command);
        string Read();
    }

    interface IPortType
    {
        PortType PortType {  get; }
    }


}
