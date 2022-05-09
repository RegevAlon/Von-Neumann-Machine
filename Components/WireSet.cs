﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            int bValue = iValue;
            if (bValue  == 0)
            {
                for (int i = 0; i <Size; i++)
                {
                    m_aWires[i].Value = 0;
                }
            }
            else
            {
                int bits = (int)Math.Log2(bValue) + 1;
                for (int i = 0; i < bits; i++)
                {
                    if (bValue % 2 == 0)
                    {
                        m_aWires[i].Value = 0;
                        bValue /= 2;
                    }
                    else
                    {
                        m_aWires[i].Value = 1;
                        bValue /= 2;
                    }
                    for (int j = bits; j < Size; j++)
                    {
                        m_aWires[j].Value = 0;
                    }
                }
            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int iValue = 0;
            for (int i = 0; i< Size; i++)
            {
                if (m_aWires[i].Value == 1)
                {
                    iValue += (int)Math.Pow(2, i);
                }
            }

            return iValue;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            int bValue = iValue;
            if (bValue >= 0)
            {
                SetValue(bValue);
            }
            else
            {
                SetValue(Math.Abs(bValue));

                for (int i = 0; i < Size; i++)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        m_aWires[i].Value = 1;
                    }
                    else
                    {
                        m_aWires[i].Value = 0;
                    }
                    
                }
                for (int j = 0; j < Size; j++)
                {
                    if (m_aWires[j].Value == 1)
                    {
                        m_aWires[j].Value = 0;
                    }
                    else
                    {
                        m_aWires[j].Value = 1;
                        break;
                    }
                }

            }
        }

        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (this[Size - 1].Value == 0 || GetValue() == Math.Pow(2, Size - 1))
            {
                return GetValue();
            }
            else
            {
                WireSet res = new WireSet(Size);
                for (int i = 0; i < Size; i++)
                {
                    if (m_aWires[i].Value == 0)
                    {
                        res[i].Value = 1;
                    }
                    else
                    {
                        res[i].Value = 0;
                    }

                }
                for (int j = 0; j < Size; j++)
                {
                    if (res[j].Value == 1)
                    {
                        res[j].Value = 0;
                    }
                    else
                    {
                        res[j].Value = 1;
                        break;
                    }
                }
                return res.GetValue() * -1;
            }
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
