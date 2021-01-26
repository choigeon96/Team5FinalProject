﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAC;
using VO;
namespace Service
{
    public class CodeService
    {
        public Dictionary<string, List<CodeVO>> GetCommonCode(string[] codes)
        {
            CodeDAC dac = new CodeDAC();
            return dac.GetCommonCode(codes);
        }
    }
}