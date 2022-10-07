namespace FIMECO.Authentification
{
    partial class MAJMDP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAJMDP));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sBtnFermer = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnValider = new DevExpress.XtraEditors.SimpleButton();
            this.txtMotPasseConfirm = new DevExpress.XtraEditors.TextEdit();
            this.txtMotPasse = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotPasseConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotPasse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.layoutControl1.Controls.Add(this.sBtnFermer);
            this.layoutControl1.Controls.Add(this.sBtnValider);
            this.layoutControl1.Controls.Add(this.txtMotPasseConfirm);
            this.layoutControl1.Controls.Add(this.txtMotPasse);
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(496, 98);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sBtnFermer
            // 
            this.sBtnFermer.Image = ((System.Drawing.Image)(resources.GetObject("sBtnFermer.Image")));
            this.sBtnFermer.Location = new System.Drawing.Point(250, 60);
            this.sBtnFermer.Name = "sBtnFermer";
            this.sBtnFermer.Size = new System.Drawing.Size(234, 22);
            this.sBtnFermer.StyleController = this.layoutControl1;
            this.sBtnFermer.TabIndex = 7;
            this.sBtnFermer.Text = "Fermer";
            // 
            // sBtnValider
            // 
            this.sBtnValider.Image = ((System.Drawing.Image)(resources.GetObject("sBtnValider.Image")));
            this.sBtnValider.Location = new System.Drawing.Point(12, 60);
            this.sBtnValider.Name = "sBtnValider";
            this.sBtnValider.Size = new System.Drawing.Size(234, 22);
            this.sBtnValider.StyleController = this.layoutControl1;
            this.sBtnValider.TabIndex = 6;
            this.sBtnValider.Text = "Valider";
            this.sBtnValider.Click += new System.EventHandler(this.sBtnValider_Click);
            // 
            // txtMotPasseConfirm
            // 
            this.txtMotPasseConfirm.Location = new System.Drawing.Point(175, 36);
            this.txtMotPasseConfirm.Name = "txtMotPasseConfirm";
            this.txtMotPasseConfirm.Properties.UseSystemPasswordChar = true;
            this.txtMotPasseConfirm.Size = new System.Drawing.Size(309, 20);
            this.txtMotPasseConfirm.StyleController = this.layoutControl1;
            this.txtMotPasseConfirm.TabIndex = 5;
            this.txtMotPasseConfirm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMotPasseConfirm_KeyPress);
            // 
            // txtMotPasse
            // 
            this.txtMotPasse.Location = new System.Drawing.Point(175, 12);
            this.txtMotPasse.Name = "txtMotPasse";
            this.txtMotPasse.Properties.UseSystemPasswordChar = true;
            this.txtMotPasse.Size = new System.Drawing.Size(309, 20);
            this.txtMotPasse.StyleController = this.layoutControl1;
            this.txtMotPasse.TabIndex = 4;
            this.txtMotPasse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMotPasse_KeyPress);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(496, 98);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtMotPasse;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem1.Text = "Nouveau Mot de Passe";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(160, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtMotPasseConfirm;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(476, 24);
            this.layoutControlItem2.Text = "Confirmer Nouveau Mot de Passe";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(160, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sBtnValider;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(238, 30);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sBtnFermer;
            this.layoutControlItem4.Location = new System.Drawing.Point(238, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(238, 30);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // MAJMDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 98);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MAJMDP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAJMDP";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMotPasseConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMotPasse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton sBtnFermer;
        private DevExpress.XtraEditors.SimpleButton sBtnValider;
        private DevExpress.XtraEditors.TextEdit txtMotPasseConfirm;
        private DevExpress.XtraEditors.TextEdit txtMotPasse;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}