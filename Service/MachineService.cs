﻿using DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VO;

namespace Service
{
   public class MachineService
    {
       
       public List<MachineVO> GetMachine()
       {
           MachineDAC dac = new MachineDAC();
           return dac.GetMachine();
       }

        public List<MachinesVO> GetMachine(int mgrade)
        {
            MachineDAC dac = new MachineDAC();
            return dac.GetMachine(mgrade);
        }

        public bool UpdateMachine(MachinesVO mvo)
        {
            MachineDAC dac = new MachineDAC();
            return dac.UpdateMachine(mvo);
        }

        public bool InsertMachine(MachinesVO mvo)
        {
            MachineDAC dac = new MachineDAC();
            return dac.InsertMachine(mvo);
        }

        public bool DeleteMachine(string machine_id)
        {
            MachineDAC dac = new MachineDAC();
            return dac.DeleteMachine(machine_id);
        }
    }
}
