using System;

namespace Components
{
    //This class is used to implement the ALU
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        //inputs
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Control { get; private set; }

        //outputs
        public WireSet Output { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }

        //your code here
        public WireSet m_wZero { get; private set; }
        public WireSet m_wZero2 { get; private set; }
        public WireSet m_wZero3 { get; private set; }
        public WireSet m_wOne { get; private set; }
        public WireSet m_wnOne { get; private set; }
        BitwiseMultiwayMux m_gBwmux;
        public BitwiseNotGate m_gBwNot1 { get; private set; }
        public BitwiseNotGate m_gBwNot2 { get; private set; }
        public WireSet OposeX { get; private set; }
        public WireSet OposeY { get; private set; }
        public MultiBitAdder m_gMbA1 { get; private set; }
        public MultiBitAdder m_gMbA2 { get; private set; }
        public MultiBitAdder m_gMbA3 { get; private set; }
        public MultiBitAdder m_gMbA4 { get; private set; }
        public MultiBitAdder m_gMbA5 { get; private set; }
        public MultiBitAdder m_gMbA6 { get; private set; }
        public MultiBitAdder m_gMbA7 { get; private set; }
        public BitwiseAndGate m_gBwAnd { get; private set; }
        public BitwiseOrGate m_gBwOr { get; private set; }
        public MultiBitOrGate m_gmOr1 { get; private set; }
        public MultiBitOrGate m_gmOr2 { get; private set; }
        public AndGate m_gAnd { get; private set; }
        public OrGate m_gOr { get; private set; }
        public MultiBitAdder m_gMbA8 { get; private set; }
        public MultiBitAdder m_gMbA9 { get; private set; }


        public ALU(int iSize)
        {

            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            Control = new WireSet(6);
            Output = new WireSet(Size);
            Zero = new Wire();


            //Create and connect all the internal components
            m_gBwmux = new BitwiseMultiwayMux(Size, 6);

            m_wZero = new WireSet(Size);
            m_wZero.Set2sComplement(0);
            m_gBwmux.ConnectInput(0, m_wZero);

            m_wOne = new WireSet(Size);
            m_wOne.Set2sComplement(1);
            m_gBwmux.ConnectInput(1, m_wOne);

            m_gBwmux.ConnectInput(2, InputX);
            m_gBwmux.ConnectInput(3, InputY);

            m_gBwNot1 = new BitwiseNotGate(Size);
            m_gBwNot1.ConnectInput(InputX);
            m_gBwmux.ConnectInput(4, m_gBwNot1.Output);

            m_gBwNot2 = new BitwiseNotGate(Size);
            m_gBwNot2.ConnectInput(InputY);
            m_gBwmux.ConnectInput(5, m_gBwNot2.Output);

            OposeX = new WireSet(Size);
            OposeX.ConnectInput(m_gBwNot1.Output);
            m_gMbA8 = new MultiBitAdder(Size);
            m_gMbA8.ConnectInput1(OposeX);
            m_gMbA8.ConnectInput2(m_wOne);
            m_gBwmux.ConnectInput(6, m_gMbA8.Output);

            OposeY = new WireSet(Size);
            OposeY.ConnectInput(m_gBwNot2.Output);
            m_gMbA9 = new MultiBitAdder(Size);
            m_gMbA9.ConnectInput1(OposeY);
            m_gMbA9.ConnectInput2(m_wOne);
            m_gBwmux.ConnectInput(7, m_gMbA9.Output);

            m_gMbA1 = new MultiBitAdder(Size);
            m_gMbA1.ConnectInput1(InputX);
            m_gMbA1.ConnectInput2(m_wOne);
            m_gBwmux.ConnectInput(8, m_gMbA1.Output);

            m_gMbA2 = new MultiBitAdder(Size);
            m_gMbA2.ConnectInput1(InputY);
            m_gMbA2.ConnectInput2(m_wOne);
            m_gBwmux.ConnectInput(9, m_gMbA2.Output);

            m_wnOne = new WireSet(Size);
            m_gMbA3 = new MultiBitAdder(Size);
            m_wnOne.Set2sComplement(-1);
            m_gMbA3.ConnectInput1(InputX);
            m_gMbA3.ConnectInput2(m_wnOne);
            m_gBwmux.ConnectInput(10, m_gMbA3.Output);

            m_gMbA4 = new MultiBitAdder(Size);
            m_gMbA4.ConnectInput1(InputY);
            m_gMbA4.ConnectInput2(m_wnOne);
            m_gBwmux.ConnectInput(11, m_gMbA4.Output);

            m_gMbA5 = new MultiBitAdder(Size);
            m_gMbA5.ConnectInput1(InputX);
            m_gMbA5.ConnectInput2(InputY);
            m_gBwmux.ConnectInput(12, m_gMbA5.Output);

            m_gMbA6 = new MultiBitAdder(Size);
            m_gMbA6.ConnectInput1(InputX);
            m_gMbA6.ConnectInput2(m_gMbA9.Output);
            m_gBwmux.ConnectInput(13, m_gMbA6.Output);

            m_gMbA7 = new MultiBitAdder(Size);
            m_gMbA7.ConnectInput1(InputY);
            m_gMbA7.ConnectInput2(m_gMbA8.Output);
            m_gBwmux.ConnectInput(14, m_gMbA7.Output);

            m_gBwAnd = new BitwiseAndGate(Size);
            m_gBwAnd.ConnectInput1(InputX);
            m_gBwAnd.ConnectInput2(InputY);
            m_gBwmux.ConnectInput(15, m_gBwAnd.Output);

            m_gAnd = new AndGate();
            m_gmOr1 = new MultiBitOrGate(Size);
            m_gmOr2 = new MultiBitOrGate(Size);
            m_gmOr1.ConnectInput(InputX);
            m_gmOr2.ConnectInput(InputY);
            m_gAnd.ConnectInput1(m_gmOr1.Output);
            m_gAnd.ConnectInput2(m_gmOr2.Output);

            m_wZero2 = new WireSet(Size);
            m_wZero2.Set2sComplement(0);
            m_wZero2[0].ConnectInput(m_gAnd.Output);
            m_gBwmux.ConnectInput(16, m_wZero2);

            m_gBwOr = new BitwiseOrGate(Size);
            m_gBwOr.ConnectInput1(InputX);
            m_gBwOr.ConnectInput2(InputY);
            m_gBwmux.ConnectInput(17, m_gBwOr.Output);

            m_gOr = new OrGate();
            m_gmOr1 = new MultiBitOrGate(Size);
            m_gmOr2 = new MultiBitOrGate(Size);
            m_gmOr1.ConnectInput(InputX);
            m_gmOr2.ConnectInput(InputY);
            m_gOr.ConnectInput1(m_gmOr1.Output);
            m_gOr.ConnectInput2(m_gmOr2.Output);

            m_wZero3 = new WireSet(Size);
            m_wZero3.Set2sComplement(0);
            m_wZero3[0].ConnectInput(m_gOr.Output);
            m_gBwmux.ConnectInput(18, m_wZero3);
           
            m_gBwmux.ConnectControl(Control);


            for (int i = 0; i < Size; i++)
            {

                Output[i].ConnectInput(m_gBwmux.Output[i]);
            }
        }

        public override bool TestGate()
        {
            InputX.Set2sComplement(7);
            InputY.Set2sComplement(10);



            //Console.WriteLine(InputX.ToString() + " , " + InputY.ToString());

            Control.SetValue(0);
            Console.WriteLine("0 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(1);
            Console.WriteLine("1 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(2);
            Console.WriteLine("2 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(3);
            Console.WriteLine("3 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(4);
            Console.WriteLine("4 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(5);
            Console.WriteLine("5 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(6);
            Console.WriteLine("6 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(7);
            Console.WriteLine("7 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(8);
            Console.WriteLine("8 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(9);
            Console.WriteLine("9 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(10);
            Console.WriteLine("10 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(11);
            Console.WriteLine("11 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(12);
            Console.WriteLine("12 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(13);
            Console.WriteLine("13 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(14);
            Console.WriteLine("14 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(15);
            Console.WriteLine("15 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(16);
            Console.WriteLine("16 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(17);
            Console.WriteLine("17 " + Output.ToString() + " Control: " + Control.ToString());
            Control.SetValue(18);
            Console.WriteLine("18 " + Output.ToString() + " Control: " + Control.ToString());


            return true;
        }
    }
}
