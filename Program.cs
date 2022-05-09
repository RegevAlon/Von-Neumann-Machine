using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {

            //This is an example of a testing code that you should run for all the gates that you create

            //Create a gate
            AndGate and = new AndGate();
            Console.WriteLine(and + "");
            //Test that the unit testing works properly
            if (!and.TestGate())
                Console.WriteLine("bugbug");
            
            OrGate or = new OrGate();
            Console.WriteLine(or + "");
            //Test that the unit testing works properly
            if (!or.TestGate())
                Console.WriteLine("bugbug");

            XorGate xor = new XorGate();
            Console.WriteLine(xor + "");
            //Test that the unit testing works properly
            if (!xor.TestGate())
                Console.WriteLine("bugbug");

            MuxGate mux = new MuxGate();
            Console.WriteLine(mux + "");
            //Test that the unit testing works properly
            if (!mux.TestGate())
                Console.WriteLine("bugbug");

            Demux demux = new Demux();
            Console.WriteLine(demux + "");
            //Test that the unit testing works properly
            if (!demux.TestGate())
                Console.WriteLine("bugbug");

            MultiBitAndGate muland = new MultiBitAndGate(4);
            Console.WriteLine(muland + "");
            //Test that the unit testing works properly
            if (!muland.TestGate())
                Console.WriteLine("bugbug");

            MultiBitOrGate mulor = new MultiBitOrGate(3);
            Console.WriteLine(mulor + "");
            //Test that the unit testing works properly
            if (!mulor.TestGate())
                Console.WriteLine("bugbug");

            BitwiseAndGate wiseand = new BitwiseAndGate(2);
            Console.WriteLine(wiseand + "");
            //Test that the unit testing works properly
            if (!wiseand.TestGate())
                Console.WriteLine("bugbug");

            BitwiseOrGate wiseor = new BitwiseOrGate(2);
            Console.WriteLine(wiseor + "");
            //Test that the unit testing works properly
            if (!wiseor.TestGate())
                Console.WriteLine("bugbug");

            BitwiseNotGate wiesnot = new BitwiseNotGate(3);
            Console.WriteLine(wiesnot + "");
            //Test that the unit testing works properly
            if (!wiesnot.TestGate())
                Console.WriteLine("bugbug");

            BitwiseMux wisemux = new BitwiseMux(2);
            Console.WriteLine(wisemux + "");
            //Test that the unit testing works properly
            if (!wisemux.TestGate())
                Console.WriteLine("bugbug");

            BitwiseDemux wisedemux = new BitwiseDemux(2);
            Console.WriteLine(wisedemux + "");
            //Test that the unit testing works properly
            if (!wisedemux.TestGate())
                Console.WriteLine("bugbug");

            MultiBitMux multimux = new MultiBitMux(8,3);
            Console.WriteLine(multimux + "");
            //Test that the unit testing works properly
            if (!multimux.TestGate())
                Console.WriteLine("bugbug");

            BitwiseMultiwayMux wisemulmux = new BitwiseMultiwayMux(3, 2);
            Console.WriteLine(wisemulmux + "");
            //Test that the unit testing works properly
            if (!wisemulmux.TestGate())
                Console.WriteLine("bugbug");

            WireSet m_ws = new WireSet(4);
            m_ws[0].Value = 1;
            m_ws[1].Value = 1;
            m_ws[2].Value = 0;
            m_ws[3].Value = 1;
            Console.WriteLine(m_ws.GetValue());

            WireSet m_ws2 = new WireSet(5);
            m_ws2.SetValue(15);
            m_ws2.GetValue();
            Console.WriteLine(m_ws2.GetValue());
            Console.WriteLine(m_ws2.ToString());


            HalfAdder ha = new HalfAdder();
            Console.WriteLine(ha + "");
            //Test that the unit testing works properly
            if (!ha.TestGate())
                Console.WriteLine("bugbug");

            FullAdder fa = new FullAdder();
            Console.WriteLine(fa + "");
            //Test that the unit testing works properly
            if (!fa.TestGate())
                Console.WriteLine("bugbug");

            MultiBitAdder mulba = new MultiBitAdder(4);
            Console.WriteLine(mulba + "");
            //Test that the unit testing works properly
            if (!mulba.TestGate())
                Console.WriteLine("bugbug");

            WireSet m_ws3 = new WireSet(8);
            m_ws3.Set2sComplement(-45);
            Console.WriteLine(m_ws3.ToString());
            Console.WriteLine(m_ws3.Get2sComplement());
            
            ALU alu = new ALU(6);
            Console.WriteLine(alu + "");
            //Test that the unit testing works properly
            if (!alu.TestGate())
                Console.WriteLine("bugbug");

            SingleBitRegister sbr = new SingleBitRegister();
            Console.WriteLine(sbr + "");
            //Test that the unit testing works properly
            if (!sbr.TestGate())
                Console.WriteLine("bugbug");

            MultiBitRegister mbr = new MultiBitRegister(2);
            Console.WriteLine(mbr + "");
            //Test that the unit testing works properly
            if (!mbr.TestGate())
                Console.WriteLine("bugbug");
            
            Memory mem = new Memory(2, 3);
            Console.WriteLine(mem + "");
            //Test that the unit testing works properly
            if (!mem.TestGate())
                Console.WriteLine("bugbug");

            //Now we ruin the nand gates that are used in all other gates. The gate should not work properly after this.
            NAndGate.Corrupt = true;
            if (and.TestGate())
                Console.WriteLine("bugbug");

            Console.WriteLine("done");
        }
    }
}
