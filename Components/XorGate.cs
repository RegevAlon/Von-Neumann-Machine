using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the xor operation. To implement it, follow the example in the And gate.
    class XorGate : TwoInputGate
    {
        private NotGate m_gNot1;
        private NotGate m_gNot2;
        private AndGate m_gAnd1;
        private AndGate m_gAnd2;
        private OrGate m_gOr;

        public XorGate()
        {
            m_gAnd1 = new AndGate();
            m_gAnd2 = new AndGate();
            m_gNot1 = new NotGate();
            m_gNot2 = new NotGate();
            m_gOr = new OrGate();

            m_gNot1.ConnectInput(Input1);
            m_gAnd1.ConnectInput1(m_gNot1.Output);
            m_gAnd1.ConnectInput2(Input2);
            
            m_gNot2.ConnectInput(Input2);
            m_gAnd2.ConnectInput1(Input1);
            m_gAnd2.ConnectInput2(m_gNot2.Output);

            m_gOr.ConnectInput1(m_gAnd1.Output);
            m_gOr.ConnectInput2(m_gAnd2.Output);

            Output = m_gOr.Output;
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(xor)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        //this method is used to test the gate. 
        //we simply check whether the truth table is properly implemented.
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
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
