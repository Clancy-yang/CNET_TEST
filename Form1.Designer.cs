
namespace CNET_TEST
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.process_id = new System.Windows.Forms.Label();
            this.process_id_CB = new System.Windows.Forms.ComboBox();
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.username = new System.Windows.Forms.Label();
            this.service_status = new System.Windows.Forms.Label();
            this.process_status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 89);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(244, 98);
            this.textBox1.TabIndex = 0;
            // 
            // input
            // 
            this.input.Enabled = false;
            this.input.Location = new System.Drawing.Point(12, 193);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(163, 21);
            this.input.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(182, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "连接";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(201, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(75, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(53, 21);
            this.textBox2.TabIndex = 5;
            // 
            // process_id
            // 
            this.process_id.AutoSize = true;
            this.process_id.Location = new System.Drawing.Point(10, 66);
            this.process_id.Name = "process_id";
            this.process_id.Size = new System.Drawing.Size(47, 12);
            this.process_id.TabIndex = 6;
            this.process_id.Text = "进程ID:";
            // 
            // process_id_CB
            // 
            this.process_id_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.form1BindingSource1, "Text", true));
            this.process_id_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.process_id_CB.FormattingEnabled = true;
            this.process_id_CB.Location = new System.Drawing.Point(56, 63);
            this.process_id_CB.Name = "process_id_CB";
            this.process_id_CB.Size = new System.Drawing.Size(200, 20);
            this.process_id_CB.Sorted = true;
            this.process_id_CB.TabIndex = 7;
            this.process_id_CB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.process_id_CB.Click += new System.EventHandler(this.process_id_CB_Click);
            // 
            // form1BindingSource1
            // 
            this.form1BindingSource1.DataSource = typeof(CNET_TEST.Form1);
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(10, 37);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(59, 12);
            this.username.TabIndex = 8;
            this.username.Text = "用户名称:";
            // 
            // service_status
            // 
            this.service_status.AutoSize = true;
            this.service_status.ForeColor = System.Drawing.Color.Red;
            this.service_status.Location = new System.Drawing.Point(80, 9);
            this.service_status.Name = "service_status";
            this.service_status.Size = new System.Drawing.Size(41, 12);
            this.service_status.TabIndex = 9;
            this.service_status.Text = "未连接";
            // 
            // process_status
            // 
            this.process_status.AutoSize = true;
            this.process_status.ForeColor = System.Drawing.Color.Red;
            this.process_status.Location = new System.Drawing.Point(216, 9);
            this.process_status.Name = "process_status";
            this.process_status.Size = new System.Drawing.Size(41, 12);
            this.process_status.TabIndex = 10;
            this.process_status.Text = "未绑定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "服务器状态:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "进程绑定状态:";
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(CNET_TEST.Form1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 224);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.process_status);
            this.Controls.Add(this.service_status);
            this.Controls.Add(this.username);
            this.Controls.Add(this.process_id_CB);
            this.Controls.Add(this.process_id);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.input);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "一键断网小程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label process_id;
        private System.Windows.Forms.ComboBox process_id_CB;
        private System.Windows.Forms.BindingSource form1BindingSource1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label service_status;
        private System.Windows.Forms.Label process_status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

