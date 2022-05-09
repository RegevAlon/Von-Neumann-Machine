using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }
        public MuxGate m_gMux;
        public DFlipFlopGate m_gDFF;

        public SingleBitRegister()
        {
            
            Input = new Wire();
            Load = new Wire();
            m_gMux = new MuxGate();
            m_gDFF = new DFlipFlopGate();
            m_gDFF.ConnectInput(m_gMux.Output);
            m_gMux.ConnectControl(Load);
            m_gMux.ConnectInput1(m_gDFF.Output);
            m_gMux.ConnectInput2(Input);
            Output = (m_gDFF.Output);



        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Load.Value = 0;
            Input.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 1;
            if (Output.Value != 0)
                return false;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 0;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }
}
