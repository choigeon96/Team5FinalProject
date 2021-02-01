﻿using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VO;

namespace CompanyManager
{
    public partial class FrmLogin : CompanyManager.PopupBaseForm
    {
        
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            if(txtID.Text.Length < 1 || txtPassword.Text.Length < 1)
            {
                MessageBox.Show("아이디 또는 비밀번호를 확인해주세요.");
                return;
            }
            EmployeeService emp = new EmployeeService();
            (bool result, EmployeeVO employee) = emp.GetLogin(txtID.Text, txtPassword.Text);
            
            if (!result)
            {

                MessageBox.Show("아이디 또는 비밀번호가 일치하지 않습니다. 다시 확인해 주세요.");
                return;
            }
            else
            {

                MessageBox.Show("로그인 성공");
                txtID.Text = "";
                txtPassword.Text = "";
                this.Hide();
                FrmMain main = new FrmMain(employee, this);
                main.Show();
            }
            


        }
    }
}
