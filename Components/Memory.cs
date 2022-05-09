using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }

        public BitwiseMultiwayMux m_gMux;
        public BitwiseMultiwayDemux m_gDmux;
        public MultiBitRegister[] m_Registers;
        public WireSet wc;

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();
            wc = new WireSet(1);
            wc[0].ConnectInput(Load);

            m_gDmux = new BitwiseMultiwayDemux(1,AddressSize);
            m_gDmux.ConnectInput(wc);
            m_gDmux.ConnectControl(Address);

            m_gMux = new BitwiseMultiwayMux(WordSize,AddressSize);
            m_gMux.ConnectControl(Address);

            m_Registers = new MultiBitRegister[(int)Math.Pow(2,AddressSize)];
            for (int i = 0; i < m_Registers.Length; i++)
            {
                m_Registers[i] = new MultiBitRegister(WordSize);
                m_Registers[i].ConnectInput(Input);
                m_Registers[i].Load.ConnectInput(m_gDmux.Outputs[i][0]);
                m_gMux.ConnectInput(i, m_Registers[i].Output);
            }
            Output.ConnectInput(m_gMux.Output);


        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            string con = "Load: " + Load.Value +  "\nInput: " + Input.ToString() + "\nAdress: " + Address.GetValue() +  "\nRegisters: ";
            for (int i = 0; i < m_Registers.Length; i++)
            {
                con += m_Registers[i].ToString() + " ";
            }
            con += "\nOutput = " + Output.ToString()+"\n";
            return con;

        }

        public override bool TestGate()
        {
            WireSet check = new WireSet(3);
            Load.Value = 1;
            Address.Set2sComplement(0);
            Input.Set2sComplement(2);
            check.Set2sComplement(2);
            Clock.ClockDown();
            Clock.ClockUp();
            //Console.WriteLine(ToString());
            if (m_Registers[0].Output.GetValue() != 2 || m_Registers[1].Output.GetValue() != 0 || m_Registers[2].Output.GetValue() != 0 || m_Registers[3].Output.GetValue() != 0)
            {
                return false;
            }

            Load.Value = 0;
            Address.Set2sComplement(3);
            Input.Set2sComplement(4);
            check.Set2sComplement(4);
            Clock.ClockDown();
            Clock.ClockUp();
            //Console.WriteLine(ToString());
            if (m_Registers[0].Output.GetValue() != 2 || m_Registers[1].Output.GetValue() != 0 || m_Registers[2].Output.GetValue() != 0 || m_Registers[3].Output.GetValue() != 0)
            {
                return false;
            }

            Load.Value = 1;
            Address.Set2sComplement(0);
            Input.Set2sComplement(0);
            check.Set2sComplement(0);
            Clock.ClockDown();
            Clock.ClockUp();
            //Console.WriteLine(ToString());
            if (m_Registers[0].Output.GetValue() != 0 || m_Registers[1].Output.GetValue() != 0 || m_Registers[2].Output.GetValue() != 0 || m_Registers[3].Output.GetValue() != 0)
            {
                return false;
            }
            return true;
        }
    }
}
