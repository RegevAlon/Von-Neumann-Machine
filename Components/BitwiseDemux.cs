using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }

        private Demux m_gDemux;

        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);

            for (int i = 0; i < iSize; i++)
            {
                m_gDemux = new Demux();
                m_gDemux.ConnectControl(Control);
                m_gDemux.ConnectInput(Input[i]);
                Output1[i].ConnectInput(m_gDemux.Output1);
                Output2[i].ConnectInput(m_gDemux.Output2);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            Input[0].Value = 1;
            Input[1].Value = 0;
            Control.Value = 1;

            if (Output1[0].Value != 0 ^ Output2[0].Value != 1)
            {
                return false;
            }

            if (Output1[1].Value != 0 ^ Output2[1].Value != 0)
            {
                return false;
            }
            return true;
        }
    }
}
