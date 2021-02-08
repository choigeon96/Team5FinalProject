﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Util;

namespace CompanyManager
{
    public partial class FrmDemandPlan : CompanyManager.MDIBaseForm
    {
        public FrmDemandPlan()
        {
            InitializeComponent();
            CommonUtil.SetInitGridView(dataGridView2);
            CommonUtil.SetDGVDesign_Num(dataGridView2);
            CommonUtil.AddGridCheckColumn(dataGridView2, "");
            CommonUtil.AddGridTextColumn(dataGridView2, "plan_id", "plan_id", 80,false, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dataGridView2, "고객사코드", "company_id", 80, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dataGridView2, "고객사명", "company_name", 100, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dataGridView2, "고객주문번호", "order_id", 100, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dataGridView2, "품목", "item_id", 100, true, DataGridViewContentAlignment.MiddleCenter);
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.AutoGenerateColumns = true;
            dtpend.Value = dtpstart.Value.AddDays(30);
            
        }

        private void FrmDemandPlan_Load(object sender, EventArgs e)
        {
            //먼저 세팅된 정보로 조회
            btnSearch.PerformClick();



        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpstart.Value, dtpend.Value) > 0)
            {
                MessageBox.Show("종료일자는 시작일자보다 커야합니다.");
                return;
            }


            Service.DemandService service = new Service.DemandService();
            DataTable dt = service.GetDemandPlan(dtpstart.Value,dtpend.Value);

            var temp = from plan in dt.AsEnumerable()
                              where (plan.Field<int>("company_id").ToString().Contains(txtcompany.Text) || plan.Field<string>("company_name").ToString().Contains(txtcompany.Text))&&
                                    plan.Field<int>("plan_id").ToString().Contains(txtpaneid.Text) &&
                                    plan.Field<string>("item_id").ToString().Contains(txtSubject.Text)
                              select plan;
            //temp.AsDataView();

            //DataView dv = dt.DefaultView;

            //dv.RowFilter = $"company_id.ToString() = '{txtcompany.Text}'";

            dataGridView2.DataSource = temp.AsDataView();
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {

        }
    }
}
