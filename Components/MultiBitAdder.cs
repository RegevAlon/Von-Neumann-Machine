using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }

        public FullAdder[] m_gFas;



        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(Size);
            Input2 = new WireSet(Size);
            Output = new WireSet(Size);
            Overflow = new Wire();
            m_gFas = new FullAdder[Size];
            for (int i = 0; i <Size; i++)
            {
                m_gFas[i] = new FullAdder();
            }
            m_gFas[0].ConnectInput1(Input1[0]);
            m_gFas[0].ConnectInput2(Input2[0]);
            Output[0].ConnectInput(m_gFas[0].Output);


            for (int j = 1; j <Size; j++)
            {
                m_gFas[j].ConnectInput1(Input1[j]);
                m_gFas[j].ConnectInput2(Input2[j]);
                m_gFas[j].CarryInput.ConnectInput(m_gFas[j-1].CarryOutput);
                Output[j].ConnectInput(m_gFas[j].Output);
            }
            Overflow.ConnectInput(m_gFas.Last().CarryOutput);
        }

        public override string ToString()
        {
            return Input1 + "(" + Input1 + ")" + " + " + Input2 + "(" + Input2 + ")" + " = " + Output + "(" + Output + ")" + " (C" + Overflow.Value + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input1[0].Value = 1;
            Input1[1].Value = 0;
            Input1[2].Value = 1;
            Input1[3].Value = 1;

            Input2[0].Value = 0;
            Input2[1].Value = 1;
            Input2[2].Value = 0;
            Input2[3].Value = 1;


            if (Output[1].Value != 1)
            {
                return false;
            }
            return true;
        }
    }
}
