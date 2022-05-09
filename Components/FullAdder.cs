using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }

        HalfAdder m_gHf1;
        HalfAdder m_gHf2;
        OrGate m_gOr;



        public FullAdder()
        {
            CarryInput = new Wire();
            CarryOutput = new Wire();
            m_gHf1 = new HalfAdder();
            m_gHf2 = new HalfAdder();
            m_gOr = new OrGate();


            m_gHf1.ConnectInput1(Input1);
            m_gHf1.ConnectInput2(Input2);

            m_gOr.ConnectInput1(m_gHf1.CarryOutput);

            m_gHf2.ConnectInput1(CarryInput);
            m_gHf2.ConnectInput2(m_gHf1.Output);

            m_gOr.ConnectInput2(m_gHf2.CarryOutput);
            this.CarryOutput.ConnectInput(m_gOr.Output);

            Output.ConnectInput(m_gHf2.Output);
            







        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 1)
            {
                return false;
            }

            return true;

        }
    }
}
