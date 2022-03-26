using Ivi.Visa;
using NationalInstruments.Visa;
using System;
using VISAInstrument.Utility;

namespace VISAInstrument.Ports
{
    internal class Rs232PortOperator : PortOperatorBase, IPortType
    {
        public int BaudRate { get; }

        public SerialParity Parity { get; }

        public SerialStopBitsMode StopBits { get; }

        public int DataBits { get; }

        public PortType PortType => PortType.Rs232;

        public SerialFlowControlModes FlowControl { set; get; } = SerialFlowControlModes.None;

        private readonly SerialSession _serialSession;


        private EventHandler<SerialDataReceivedEventArgs> _dataReceived;

        public event EventHandler<SerialDataReceivedEventArgs> DataReceived
        {
            add
            {
                _serialSession.AnyCharacterReceived += SerialSession_AnyCharacterReceived;
                _dataReceived += value;
            }
            remove
            {
                _serialSession.AnyCharacterReceived -= SerialSession_AnyCharacterReceived;
                _dataReceived -= value;
            }
        }

        private void SerialSession_AnyCharacterReceived(object sender, VisaEventArgs e)
        {
            OnDataReceived(new SerialDataReceivedEventArgs(_serialSession.BytesAvailable));
        }

        protected virtual void OnDataReceived(SerialDataReceivedEventArgs e)
        {
            _dataReceived?.Invoke(this, e);
        }

        public Rs232PortOperator(string address, int baudRate, SerialParity parity, SerialStopBitsMode stopBits, int dataBits) : base(new SerialSession(address), address)
        {
            if (!address.ToUpper().Contains("ASRL")) throw new ArgumentException($"该地址不含ASRL字样");
            BaudRate = baudRate;
            Parity = parity;
            StopBits = stopBits;
            if (dataBits < 5 || dataBits > 8) throw new NotSupportedException($"不支持数据位为：{dataBits.ToString()}");
            DataBits = dataBits;
            _serialSession = (SerialSession)Session;
        }

        public void SetReadTerminationCharacterEnabled(bool enabled)
        {
            _serialSession.ReadTermination = enabled ? SerialTerminationMethod.TerminationCharacter : SerialTerminationMethod.None;
        }

        public override void Open()
        {
            base.Open();
            _serialSession.BaudRate = BaudRate;
            switch (Parity)
            {
                case SerialParity.None:
                    _serialSession.Parity = SerialParity.None; break;
                case SerialParity.Odd:
                    _serialSession.Parity = SerialParity.Odd; break;
                case SerialParity.Even:
                    _serialSession.Parity = SerialParity.Even; break;
                case SerialParity.Mark:
                    _serialSession.Parity = SerialParity.Mark; break;
                case SerialParity.Space:
                    _serialSession.Parity = SerialParity.Space; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (StopBits)
            {
                case SerialStopBitsMode.One:
                    _serialSession.StopBits = SerialStopBitsMode.One; break;
                case SerialStopBitsMode.OneAndOneHalf:
                    _serialSession.StopBits = SerialStopBitsMode.OneAndOneHalf; break;
                case SerialStopBitsMode.Two:
                    _serialSession.StopBits = SerialStopBitsMode.Two; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _serialSession.DataBits = (short)DataBits;
            switch (FlowControl)
            {
                case SerialFlowControlModes.None:
                    _serialSession.FlowControl = SerialFlowControlModes.None; break;
                case SerialFlowControlModes.XOnXOff:
                    _serialSession.FlowControl = SerialFlowControlModes.XOnXOff; break;
                case SerialFlowControlModes.RtsCts:
                    _serialSession.FlowControl = SerialFlowControlModes.RtsCts; break;
                case SerialFlowControlModes.DtrDsr:
                    _serialSession.FlowControl = SerialFlowControlModes.DtrDsr; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
