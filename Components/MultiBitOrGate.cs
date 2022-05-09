using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        private List<OrGate> m_gOr;

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            m_gOr = new List<OrGate>(m_wsInput.Size);
            for (int i = 0; i < m_wsInput.Size - 1; i++)
            {

                m_gOr.Add(new OrGate());
            }

            m_gOr[0].ConnectInput1(m_wsInput[0]);
            m_gOr[0].ConnectInput2(m_wsInput[1]);

            if (m_wsInput.Size > 2)
            {
                for (int i = 2; i < iInputCount; i++)
                {

                    m_gOr[i - 1].ConnectInput1(m_gOr[i - 2].Output);
                    m_gOr[i - 1].ConnectInput2(m_wsInput[i]);

                }
            }

            Output = m_gOr[m_wsInput.Size - 2].Output;

        }

        public override bool TestGate()
        {
            m_wsInput[0].Value = 1;
            m_wsInput[1].Value = 1;
            m_wsInput[2].Value = 0;

            if (Output.Value != 1)
            {
                return false;
            }

            m_wsInput[0].Value = 1;
            m_wsInput[1].Value = 1;
            m_wsInput[2].Value = 1;

            if (Output.Value != 1)
            {
                return false;
            }

            m_wsInput[0].Value = 0;
            m_wsInput[1].Value = 0;
            m_wsInput[2].Value = 0;

            if (Output.Value != 0)
            {
                return false;
            }
            return true;
            }
        }
}
