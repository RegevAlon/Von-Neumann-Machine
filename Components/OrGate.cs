using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the or operation. To implement it, follow the example in the And gate.
    class OrGate : TwoInputGate
    {
        private NotGate m_gNot1;
        private NotGate m_gNot2;
        private NotGate m_gNot3;
        private AndGate m_gAnd;

        public OrGate()
        {
            m_gAnd = new AndGate();
            m_gNot1 = new NotGate();
            m_gNot2 = new NotGate();
            m_gNot3 = new NotGate();

            m_gNot1.ConnectInput(Input1);
            m_gNot2.ConnectInput(Input2);

            m_gAnd.ConnectInput1(m_gNot1.Output);
            m_gAnd.ConnectInput2(m_gNot2.Output);

            m_gNot3.ConnectInput(m_gAnd.Output);

            Output = m_gNot3.Output;
            
        }


        public override string ToString()
        {
            return "Or " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }

}
