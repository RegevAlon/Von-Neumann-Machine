using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        private MuxGate m_gMux;

        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();

            

            for (int i = 0; i < iSize; i++)
            {
                m_gMux = new MuxGate();
                m_gMux.ConnectControl(ControlInput);
                m_gMux.ConnectInput1(Input1[i]);
                m_gMux.ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_gMux.Output);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            Input1[0].Value = 1;
            Input1[1].Value = 0;

            Input2[0].Value = 1;
            Input2[1].Value = 1;

            ControlInput.Value = 1;

            if (Output[0].Value != 1 ^ Output[1].Value != 1)
            {
                return false;
            }

            return true;
        }
    }
}
