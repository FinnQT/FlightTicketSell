namespace WinFormsApp3
{
    partial class QuanlyUse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            DoanThu = new TabPage();
            button1 = new Button();
            label6 = new Label();
            label5 = new Label();
            textBox_TONGDOANTHU = new TextBox();
            label4 = new Label();
            button_DTNAM = new Button();
            button_DTTHANG = new Button();
            textBox_thang = new TextBox();
            label2 = new Label();
            label1 = new Label();
            dataGridView_DTPhieuDat = new DataGridView();
            dataGridView_DTVE = new DataGridView();
            tabPage2 = new TabPage();
            textBox4 = new TextBox();
            label9 = new Label();
            textBox3 = new TextBox();
            label8 = new Label();
            textBox2 = new TextBox();
            label7 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            button2 = new Button();
            tabControl1.SuspendLayout();
            DoanThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DTPhieuDat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DTVE).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(DoanThu);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(15, 15);
            tabControl1.Margin = new Padding(4, 4, 4, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1254, 764);
            tabControl1.TabIndex = 0;
            // 
            // DoanThu
            // 
            DoanThu.BackColor = Color.Linen;
            DoanThu.Controls.Add(button1);
            DoanThu.Controls.Add(label6);
            DoanThu.Controls.Add(label5);
            DoanThu.Controls.Add(textBox_TONGDOANTHU);
            DoanThu.Controls.Add(label4);
            DoanThu.Controls.Add(button_DTNAM);
            DoanThu.Controls.Add(button_DTTHANG);
            DoanThu.Controls.Add(textBox_thang);
            DoanThu.Controls.Add(label2);
            DoanThu.Controls.Add(label1);
            DoanThu.Controls.Add(dataGridView_DTPhieuDat);
            DoanThu.Controls.Add(dataGridView_DTVE);
            DoanThu.Location = new Point(4, 34);
            DoanThu.Margin = new Padding(4, 4, 4, 4);
            DoanThu.Name = "DoanThu";
            DoanThu.Padding = new Padding(4, 4, 4, 4);
            DoanThu.Size = new Size(1246, 726);
            DoanThu.TabIndex = 0;
            DoanThu.Text = "Doanh Thu";
            DoanThu.Click += tabPage1_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Arial Narrow", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(203, 623);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(118, 36);
            button1.TabIndex = 24;
            button1.Text = "Xuất file";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Narrow", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(523, 371);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(121, 33);
            label6.TabIndex = 23;
            label6.Text = "Phiếu Đặt";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial Narrow", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(601, 54);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(43, 33);
            label5.TabIndex = 22;
            label5.Text = "Vé";
            // 
            // textBox_TONGDOANTHU
            // 
            textBox_TONGDOANTHU.Location = new Point(274, 539);
            textBox_TONGDOANTHU.Margin = new Padding(4, 4, 4, 4);
            textBox_TONGDOANTHU.Name = "textBox_TONGDOANTHU";
            textBox_TONGDOANTHU.Size = new Size(155, 31);
            textBox_TONGDOANTHU.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(104, 548);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(163, 29);
            label4.TabIndex = 20;
            label4.Text = "Tổng Doanh Thu";
            // 
            // button_DTNAM
            // 
            button_DTNAM.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_DTNAM.Location = new Point(121, 418);
            button_DTNAM.Margin = new Padding(4, 4, 4, 4);
            button_DTNAM.Name = "button_DTNAM";
            button_DTNAM.Size = new Size(214, 75);
            button_DTNAM.TabIndex = 19;
            button_DTNAM.Text = "Doanh Thu Tổng";
            button_DTNAM.UseVisualStyleBackColor = true;
            button_DTNAM.Click += button_DTNAM_Click_1;
            // 
            // button_DTTHANG
            // 
            button_DTTHANG.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button_DTTHANG.Location = new Point(121, 359);
            button_DTTHANG.Margin = new Padding(4, 4, 4, 4);
            button_DTTHANG.Name = "button_DTTHANG";
            button_DTTHANG.Size = new Size(214, 51);
            button_DTTHANG.TabIndex = 18;
            button_DTTHANG.Text = "Doanh Thu Tháng";
            button_DTTHANG.UseVisualStyleBackColor = true;
            button_DTTHANG.Click += button_DTTHANG_Click_1;
            // 
            // textBox_thang
            // 
            textBox_thang.Location = new Point(179, 192);
            textBox_thang.Margin = new Padding(4, 4, 4, 4);
            textBox_thang.Name = "textBox_thang";
            textBox_thang.Size = new Size(155, 31);
            textBox_thang.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(86, 196);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(69, 29);
            label2.TabIndex = 16;
            label2.Text = "Tháng";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 16F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(222, 34);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(173, 37);
            label1.TabIndex = 15;
            label1.Text = "Doanh thu";
            // 
            // dataGridView_DTPhieuDat
            // 
            dataGridView_DTPhieuDat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_DTPhieuDat.Location = new Point(652, 371);
            dataGridView_DTPhieuDat.Margin = new Padding(4, 4, 4, 4);
            dataGridView_DTPhieuDat.Name = "dataGridView_DTPhieuDat";
            dataGridView_DTPhieuDat.RowHeadersWidth = 51;
            dataGridView_DTPhieuDat.RowTemplate.Height = 29;
            dataGridView_DTPhieuDat.Size = new Size(504, 299);
            dataGridView_DTPhieuDat.TabIndex = 14;
            // 
            // dataGridView_DTVE
            // 
            dataGridView_DTVE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_DTVE.Location = new Point(652, 54);
            dataGridView_DTVE.Margin = new Padding(4, 4, 4, 4);
            dataGridView_DTVE.Name = "dataGridView_DTVE";
            dataGridView_DTVE.RowHeadersWidth = 51;
            dataGridView_DTVE.RowTemplate.Height = 29;
            dataGridView_DTVE.Size = new Size(504, 299);
            dataGridView_DTVE.TabIndex = 13;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Linen;
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(textBox3);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(textBox2);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(textBox1);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Margin = new Padding(4, 4, 4, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 4, 4, 4);
            tabPage2.Size = new Size(1246, 726);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Acount";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(331, 456);
            textBox4.Margin = new Padding(4, 4, 4, 4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(333, 31);
            textBox4.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(69, 465);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(250, 29);
            label9.TabIndex = 6;
            label9.Text = "Mật khẩu mới một lần nữa";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(331, 345);
            textBox3.Margin = new Padding(4, 4, 4, 4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(333, 31);
            textBox3.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(168, 354);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(137, 29);
            label8.TabIndex = 4;
            label8.Text = "Mật khẩu mới";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(331, 222);
            textBox2.Margin = new Padding(4, 4, 4, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(333, 31);
            textBox2.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(196, 231);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(98, 29);
            label7.TabIndex = 2;
            label7.Text = "Mật khẩu";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(331, 126);
            textBox1.Margin = new Padding(4, 4, 4, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(333, 31);
            textBox1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(220, 135);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(77, 29);
            label3.TabIndex = 0;
            label3.Text = "Tên TK";
            // 
            // button2
            // 
            button2.Font = new Font("Arial Narrow", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(542, 576);
            button2.Name = "button2";
            button2.Size = new Size(185, 49);
            button2.TabIndex = 8;
            button2.Text = "Cập nhật";
            button2.UseVisualStyleBackColor = true;
            // 
            // QuanlyUse
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1272, 782);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "QuanlyUse";
            Text = "QuanlyUse";
            tabControl1.ResumeLayout(false);
            DoanThu.ResumeLayout(false);
            DoanThu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DTPhieuDat).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DTVE).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage DoanThu;
        private TabPage tabPage2;
        private Label label6;
        private Label label5;
        private TextBox textBox_TONGDOANTHU;
        private Label label4;
        private Button button_DTNAM;
        private Button button_DTTHANG;
        private TextBox textBox_thang;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView_DTPhieuDat;
        private DataGridView dataGridView_DTVE;
        private Button button1;
        private Label label3;
        private TextBox textBox4;
        private Label label9;
        private TextBox textBox3;
        private Label label8;
        private TextBox textBox2;
        private Label label7;
        private TextBox textBox1;
        private Button button2;
    }
}