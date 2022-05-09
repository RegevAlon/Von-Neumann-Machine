using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        private OrGate m_gOr;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            for (int i = 0; i < iSize; i++)
            {
                m_gOr = new OrGate();
                m_gOr.ConnectInput1(Input1[i]);
                m_gOr.ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_gOr.Output);
            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(or)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            Input1[0].Value = 0;
            Input1[1].Value = 1;

            Input2[0].Value = 0;
            Input2[1].Value = 1;

            if (Output[0].Value != 0)
            {
                return false;
            }


            Input1[0].Value = 1;
            Input1[1].Value = 1;

            Input2[0].Value = 0;
            Input2[1].Value = 1;

            if (Output[0].Value != 1)
            {
                return false;
            }

            Input1[0].Value = 0;
            Input1[1].Value = 1;

            Input2[0].Value = 0;
            Input2[1].Value = 1;

            if (Output[0].Value != 0)
            {
                return false;
            }
            return true;
        }
    }
}
