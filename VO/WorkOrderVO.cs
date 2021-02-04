﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VO
{
    public class WorkOrderVO
    {

            public int                wo_id           {get;set;}
            public string               item_id          {get;set;}
            public int               prod_id          {get;set;}
            public int               wo_qty           {get;set;}
            public DateTime          wo_sdate         {get;set;}
            public int               wo_people        {get;set;}
            public string          wo_start         {get;set;}
            public string           wo_end           {get;set;}
            public DateTime         ins_date         {get;set;}
            public string           ins_emp          {get;set;}
            public DateTime           up_date          {get;set;}
            public string            up_emp          { get; set; }

        public string wo_state { get; set; }
        public string machine_id { get; set; }
        public string machine_name { get; set; }

    }
}
