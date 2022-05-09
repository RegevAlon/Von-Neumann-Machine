using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }
        public SingleBitRegister[] m_Msbr;


        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();

            m_Msbr = new SingleBitRegister[Size];
            for(int i = 0; i < Size; i++)
            {
                m_Msbr[i] = new SingleBitRegister();
                m_Msbr[i].ConnectLoad(Load);
                m_Msbr[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(m_Msbr[i].Output);
            }


        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }


        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Load.Value = 1;
            Input[0].Value = 1;
            Input[1].Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output[0].Value != 1)
                return false;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input[0].Value = 1;
            Input[1].Value = 1;

            if (Output[0].Value != 1)
                return false;
            return true;
        }
    }
}
