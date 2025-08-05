namespace QuanLyPhongGym_nhom5.GUI
{
    partial class DangKyDV_GT
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangKyDV_GT));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            userControldkDichvu_GoiTap1 = new UserControlDKDichvu_GoiTap();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2CircleButtonhuy = new Guna.UI2.WinForms.Guna2CircleButton();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(userControldkDichvu_GoiTap1);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Location = new Point(12, 12);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(912, 605);
            guna2Panel1.TabIndex = 0;
            // 
            // userControldkDichvu_GoiTap1
            // 
            userControldkDichvu_GoiTap1.Location = new Point(0, 0);
            userControldkDichvu_GoiTap1.Name = "userControldkDichvu_GoiTap1";
            userControldkDichvu_GoiTap1.Size = new Size(909, 602);
            userControldkDichvu_GoiTap1.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = this;
            // 
            // guna2CircleButtonhuy
            // 
            guna2CircleButtonhuy.BackColor = Color.Transparent;
            guna2CircleButtonhuy.DisabledState.BorderColor = Color.DarkGray;
            guna2CircleButtonhuy.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2CircleButtonhuy.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2CircleButtonhuy.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2CircleButtonhuy.FillColor = Color.Transparent;
            guna2CircleButtonhuy.Font = new Font("Segoe UI", 9F);
            guna2CircleButtonhuy.ForeColor = Color.White;
            guna2CircleButtonhuy.Image = (Image)resources.GetObject("guna2CircleButtonhuy.Image");
            guna2CircleButtonhuy.ImageSize = new Size(40, 40);
            guna2CircleButtonhuy.Location = new Point(930, 12);
            guna2CircleButtonhuy.Name = "guna2CircleButtonhuy";
            guna2CircleButtonhuy.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2CircleButtonhuy.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CircleButtonhuy.Size = new Size(60, 51);
            guna2CircleButtonhuy.TabIndex = 1;
            guna2CircleButtonhuy.Click += guna2CircleButtonhuy_Click;
            // 
            // DangKyDV_GT
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(180, 192, 255);
            ClientSize = new Size(992, 627);
            Controls.Add(guna2CircleButtonhuy);
            Controls.Add(guna2Panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DangKyDV_GT";
            Text = "DangKyDV_GT";
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButtonhuy;
        private UserControlDKDichvu_GoiTap userControldkDichvu_GoiTap1;
    }
}