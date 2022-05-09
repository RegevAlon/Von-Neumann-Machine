using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitMux : MultiBitGate
    {
        public int Size { get; private set; }
        public int ControlBits { get; private set; }
        public int Iterations { get; private set; }

        public WireSet ControlInput { get; private set; }
        public Queue<Wire> m_Wires;
        public MuxGate m_Mux;

        public MultiBitMux(int iInputCount, int cControlBits)
            : base(iInputCount)
        {
            ControlBits = cControlBits;
            Size = iInputCount;
            ControlInput = new WireSet(ControlBits);
            m_Wires = new Queue<Wire>(Size/2);


            for (int i = 0; i < Size; i+=2)
            {
                m_Mux = new MuxGate();
                m_Mux.ConnectControl(ControlInput[0]);
                m_Mux.ConnectInput1(m_wsInput[i]);
                m_Mux.ConnectInput2(m_wsInput[i+1]);
                m_Wires.Enqueue(m_Mux.Output);
            }

            Iterations = Size / 4;

            for (int i = 1; i < ControlBits; i++)
            {
                for(int j = 0; j < Iterations; j++)
                {
                    m_Mux = new MuxGate();
                    m_Mux.ConnectControl(ControlInput[i]);
                    m_Mux.ConnectInput1(m_Wires.Dequeue());
                    m_Mux.ConnectInput2(m_Wires.Dequeue());
                    m_Wires.Enqueue(m_Mux.Output);

                    Iterations *= 1 / 2;


                }
            }
            Output.ConnectInput(m_Wires.Dequeue());
        }



        public override bool TestGate()
        {
            m_wsInput[0].Value = 0;
            m_wsInput[1].Value = 1;
            m_wsInput[2].Value = 0;
            m_wsInput[3].Value = 1;
            m_wsInput[4].Value = 0;
            m_wsInput[5].Value = 1;
            m_wsInput[6].Value = 0;
            m_wsInput[7].Value = 1;

            ControlInput[0].Value = 1;
            ControlInput[1].Value = 1;
            ControlInput[2].Value = 0;

            Console.WriteLine("Mmux" + Output.Value);
            if (Output.Value != 1)
            {
                
                return false;
            }
            return true;


        }


    }
}