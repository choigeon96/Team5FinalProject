﻿using DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VO;

namespace Service
{
    public class SalesOrderService
    {
        public bool CommitSO(List<SalesOrderVO> list)
        {
            SalesOrderDAC dac = new SalesOrderDAC();
            return dac.CommitSO(list);
        }

        public List<CodeVO> GetAllCodes()
        {
            SalesOrderDAC dac = new SalesOrderDAC();
            return dac.GetAllCodes();
        }
    }
}
