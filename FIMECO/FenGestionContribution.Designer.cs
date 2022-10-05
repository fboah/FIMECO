namespace FIMECO
{
    partial class FenGestionContribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FenGestionContribution));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sBtnFermer = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnEnregistrer = new DevExpress.XtraEditors.SimpleButton();
            this.sNumMontant = new DevExpress.XtraEditors.SpinEdit();
            this.sNumAnnee = new DevExpress.XtraEditors.SpinEdit();
            this.CmbSouscripteurCot = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sNumMontant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnnee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSouscripteurCot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sBtnFermer);
            this.layoutControl1.Controls.Add(this.sBtnEnregistrer);
            this.layoutControl1.Controls.Add(this.sNumMontant);
            this.layoutControl1.Controls.Add(this.sNumAnnee);
            this.layoutControl1.Controls.Add(this.CmbSouscripteurCot);
            this.layoutControl1.Location = new System.Drawing.Point(-9, -9);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(573, 94);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // sBtnFermer
            // 
            this.sBtnFermer.Location = new System.Drawing.Point(288, 60);
            this.sBtnFermer.Name = "sBtnFermer";
            this.sBtnFermer.Size = new System.Drawing.Size(273, 22);
            this.sBtnFermer.StyleController = this.layoutControl1;
            this.sBtnFermer.TabIndex = 8;
            this.sBtnFermer.Text = "Fermer";
            this.sBtnFermer.Click += new System.EventHandler(this.sBtnFermer_Click);
            // 
            // sBtnEnregistrer
            // 
            this.sBtnEnregistrer.Location = new System.Drawing.Point(12, 60);
            this.sBtnEnregistrer.Name = "sBtnEnregistrer";
            this.sBtnEnregistrer.Size = new System.Drawing.Size(272, 22);
            this.sBtnEnregistrer.StyleController = this.layoutControl1;
            this.sBtnEnregistrer.TabIndex = 7;
            this.sBtnEnregistrer.Text = "Enregistrer";
            this.sBtnEnregistrer.Click += new System.EventHandler(this.sBtnEnregistrer_Click);
            // 
            // sNumMontant
            // 
            this.sNumMontant.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sNumMontant.Location = new System.Drawing.Point(351, 36);
            this.sNumMontant.Name = "sNumMontant";
            this.sNumMontant.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sNumMontant.Properties.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sNumMontant.Properties.IsFloatValue = false;
            this.sNumMontant.Properties.Mask.EditMask = "N00";
            this.sNumMontant.Size = new System.Drawing.Size(210, 20);
            this.sNumMontant.StyleController = this.layoutControl1;
            this.sNumMontant.TabIndex = 6;
            // 
            // sNumAnnee
            // 
            this.sNumAnnee.EditValue = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            this.sNumAnnee.Location = new System.Drawing.Point(75, 36);
            this.sNumAnnee.Name = "sNumAnnee";
            this.sNumAnnee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sNumAnnee.Properties.IsFloatValue = false;
            this.sNumAnnee.Properties.Mask.EditMask = "N00";
            this.sNumAnnee.Properties.MaxValue = new decimal(new int[] {
            2999,
            0,
            0,
            0});
            this.sNumAnnee.Properties.MinValue = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            this.sNumAnnee.Size = new System.Drawing.Size(209, 20);
            this.sNumAnnee.StyleController = this.layoutControl1;
            this.sNumAnnee.TabIndex = 5;
            // 
            // CmbSouscripteurCot
            // 
            this.CmbSouscripteurCot.Location = new System.Drawing.Point(75, 12);
            this.CmbSouscripteurCot.Name = "CmbSouscripteurCot";
            this.CmbSouscripteurCot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbSouscripteurCot.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mId", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mNom", 30, "Nom"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mPrenoms", 30, "Prenoms")});
            this.CmbSouscripteurCot.Size = new System.Drawing.Size(486, 20);
            this.CmbSouscripteurCot.StyleController = this.layoutControl1;
            this.CmbSouscripteurCot.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(573, 94);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.CmbSouscripteurCot;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(553, 24);
            this.layoutControlItem1.Text = "Souscripteur";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sNumAnnee;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(276, 24);
            this.layoutControlItem2.Text = "Année";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sNumMontant;
            this.layoutControlItem3.Location = new System.Drawing.Point(276, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(277, 24);
            this.layoutControlItem3.Text = "Montant";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sBtnEnregistrer;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(276, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.sBtnFermer;
            this.layoutControlItem5.Location = new System.Drawing.Point(276, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(277, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // FenGestionContribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 76);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FenGestionContribution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FenGestContribution";
            this.Load += new System.EventHandler(this.FenGestionContribution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sNumMontant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnnee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSouscripteurCot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton sBtnFermer;
        private DevExpress.XtraEditors.SimpleButton sBtnEnregistrer;
        private DevExpress.XtraEditors.SpinEdit sNumMontant;
        private DevExpress.XtraEditors.SpinEdit sNumAnnee;
        private DevExpress.XtraEditors.LookUpEdit CmbSouscripteurCot;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}