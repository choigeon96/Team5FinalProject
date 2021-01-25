﻿using CompanyManager.Properties;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Util;
using VO;
using static CompanyManager.PopupShift;

namespace CompanyManager
{
    public partial class FrmShift : CompanyManager.MDIBaseForm
    {
        CheckBox headerCheckBox = new CheckBox();
        List<ShiftVO> shift;
        List<CodeVO> code;

        List<MachineVO> machine;
        public FrmShift()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 등록버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            PopupShift popup = new PopupShift(OpenMode.Insert);
            
            if (popup.ShowDialog() == DialogResult.OK)
            {
                GetdgvColumn();
                DataLoad();

                ComboBoxBinding();
            }
        }

        private void FrmShift_Load(object sender, EventArgs e)
        {

            GetdgvColumn();
            DataLoad();

            ComboBoxBinding();
        }
        /// <summary>
        /// 콤보박스 바인딩
        /// </summary>
        private void ComboBoxBinding()
        {
            MachineService service = new MachineService();
            machine = service.GetMachine();
            machine.Insert(0, new MachineVO { machine_name ="전체"})   ;           
            CommonUtil.BindingComboBox(cboMachine, machine, "machine_id", "machine_name");

            //var shift1 = shift.ConvertAll(o => o);
            //shift1.Insert(0, new ShiftVO { shift_type = "전체" });
            //CommonUtil.BindingComboBoxPart(cboShift, shift1, "shift_type");


            CodeService service1 = new CodeService();
            code= service1.GetAllCommonCode();
            var code1 = (from All in code where All.category == "shift_type" select All).ToList();
            code1.Insert(0, new CodeVO { name = "전체" });
            CommonUtil.BindingComboBox(cboShift, code1, "code", "name");


        }
      
        /// <summary>
        /// 그리드뷰 디자인
        /// </summary>
        private void GetdgvColumn()
        {
            CommonUtil.SetDGVDesign_Num(dgvShift);

            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.Name = "chk";
            col.HeaderText = "";
            col.Width = 30;
            dgvShift.Columns.Add(col);
            CommonUtil.AddGridImageColumn(dgvShift, Resources.Edit_16x16, "Edit", 30);
            CommonUtil.AddGridTextColumn(dgvShift, "shift_id", "shift_id", 57, false, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "설비코드", "machine_id", 57,true,DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "설비명", "machine_name",97);
            CommonUtil.AddGridTextColumn(dgvShift, "Shift", "shift_type", 40, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "시작시간", "shift_stime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "완료시간", "shift_etime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "적용시작일자", "shift_sdate",97, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "적용완료일자", "shift_edate", 97, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "직접투입인원", "Directly_Input_Person", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "간접투입인원", "Indirect_Input_Person", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "정상직접작업시간", "Nomal_Direct_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "정상간접작업시간", "Nomal_indirect_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업직접작업시간", "Overtime_Directly_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업간접작업시간", "Overtime_Indirect_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업직접투입인원", "Overtime_Directly_Input_Person", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업간접투입인원", "Overtime_Indirect_Input_Person", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "직접사고작업시간", "Directly_Accident_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "간접사고작업시간", "Indirect_Accident_WorkTime", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업직접사고시간", "Overtime_Directly_Accident_Time", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "잔업간접사고시간", "Overtime_Indirect_Accident_Time", 57, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "비고", "shift_comment", 101, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil.AddGridTextColumn(dgvShift, "사용유무", "shift_use", 95, true, DataGridViewContentAlignment.MiddleCenter);          
            CommonUtil.AddGridTextColumn(dgvShift, "최초등록일", "ins_date", 150, false);
            CommonUtil.AddGridTextColumn(dgvShift, "최초등록자", "ins_emp",100, false);
            CommonUtil.AddGridTextColumn(dgvShift, "최종수정일", "up_date", 120, false);
            CommonUtil.AddGridTextColumn(dgvShift, "최종수정자", "up_emp", 170,false);


            Point heagerCellLocation = dgvShift.GetCellDisplayRectangle(1, -1, true).Location;          
            headerCheckBox.Location = new Point(heagerCellLocation.X + 27, heagerCellLocation.Y + 2);
            headerCheckBox.Size = new Size(18, 18);
            headerCheckBox.Click += HeaderCheckBox_Click;
            dgvShift.Controls.Add(headerCheckBox);


        }
        /// <summary>
        /// 데이터로드
        /// </summary>
        private void DataLoad()
        {
            ShiftService service = new ShiftService();
            shift =  service.GetShift();
            dgvShift.DataSource = shift;
        }

        /// <summary>
        /// 체크박스
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderCheckBox_Click(object sender, EventArgs e)
        {
            //현재 cell의 편집모드를 종료 =>commit같은 거
            dgvShift.EndEdit();

            foreach (DataGridViewRow row in dgvShift.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["chk"];
                chk.Value = headerCheckBox.Checked;

            }
        }
        /// <summary>
        /// 그리드뷰 체크박스 위치 조정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShift_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                headerCheckBox.Location = new Point(headerCheckBox.Location.X - (e.NewValue - e.OldValue), headerCheckBox.Location.Y);
                headerCheckBox.Visible = headerCheckBox.Location.X > dgvShift.Location.X + 50;
            }
        }
        /// <summary>
        /// 조회 버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btbSearch_Click(object sender, EventArgs e)
        {
           
           if (cboShift.Text == "전체" && cboMachine.Text == "전체")
           {
               dgvShift.DataSource = null;
               dgvShift.DataSource = shift;
           }
           else if (cboShift.Text != "전체" && cboMachine.Text == "전체")
           {
               dgvShift.DataSource = null;
               dgvShift.DataSource = (from All in shift where All.shift_type == cboShift.Text select All).ToList();
           }
           else if (cboMachine.Text != "전체" && cboShift.Text == "전체")
           {
               dgvShift.DataSource = null;
               dgvShift.DataSource = (from All in shift where All.machine_name == cboMachine.Text select All).ToList();
           }
           else
           {
               dgvShift.DataSource = null;
               dgvShift.DataSource = (from All in shift where All.machine_name == cboMachine.Text && All.shift_type == cboShift.Text select All).ToList();
           }         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            int rowIndex = dgvShift.CurrentRow.Index;

            ShiftVO info;
            
          
            
            // info.PartID = Convert.ToInt32(dgvShift.CurrentRow.Cells[1].Value);


            PopupShift popup = new PopupShift(OpenMode.Update);

            if (popup.ShowDialog() == DialogResult.OK)
            {
                GetdgvColumn();
                DataLoad();
                ComboBoxBinding();
            }
        }
    }
}
