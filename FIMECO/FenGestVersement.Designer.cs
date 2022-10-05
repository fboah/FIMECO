namespace FIMECO
{
    partial class FenGestVersement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FenGestVersement));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.CmbReceveur = new DevExpress.XtraEditors.LookUpEdit();
            this.sBtnFermer = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnEnregistrer = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumeroRecu = new DevExpress.XtraEditors.TextEdit();
            this.dateVersement = new DevExpress.XtraEditors.DateEdit();
            this.sNumMontant = new DevExpress.XtraEditors.SpinEdit();
            this.CmbSouscripteur = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CmbReceveur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroRecu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVersement.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVersement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumMontant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSouscripteur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.CmbReceveur);
            this.layoutControl1.Controls.Add(this.sBtnFermer);
            this.layoutControl1.Controls.Add(this.sBtnEnregistrer);
            this.layoutControl1.Controls.Add(this.txtNumeroRecu);
            this.layoutControl1.Controls.Add(this.dateVersement);
            this.layoutControl1.Controls.Add(this.sNumMontant);
            this.layoutControl1.Controls.Add(this.CmbSouscripteur);
            this.layoutControl1.Location = new System.Drawing.Point(-10, -10);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(654, 143);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // CmbReceveur
            // 
            this.CmbReceveur.Location = new System.Drawing.Point(92, 84);
            this.CmbReceveur.Name = "CmbReceveur";
            this.CmbReceveur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbReceveur.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mId", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mNom", "Nom Receveur")});
            this.CmbReceveur.Properties.ReadOnly = true;
            this.CmbReceveur.Size = new System.Drawing.Size(550, 20);
            this.CmbReceveur.StyleController = this.layoutControl1;
            this.CmbReceveur.TabIndex = 11;
            // 
            // sBtnFermer
            // 
            this.sBtnFermer.Location = new System.Drawing.Point(328, 108);
            this.sBtnFermer.Name = "sBtnFermer";
            this.sBtnFermer.Size = new System.Drawing.Size(314, 22);
            this.sBtnFermer.StyleController = this.layoutControl1;
            this.sBtnFermer.TabIndex = 10;
            this.sBtnFermer.Text = "Fermer";
            this.sBtnFermer.Click += new System.EventHandler(this.sBtnFermer_Click);
            // 
            // sBtnEnregistrer
            // 
            this.sBtnEnregistrer.Location = new System.Drawing.Point(12, 108);
            this.sBtnEnregistrer.Name = "sBtnEnregistrer";
            this.sBtnEnregistrer.Size = new System.Drawing.Size(312, 22);
            this.sBtnEnregistrer.StyleController = this.layoutControl1;
            this.sBtnEnregistrer.TabIndex = 9;
            this.sBtnEnregistrer.Text = "Enregistrer";
            this.sBtnEnregistrer.Click += new System.EventHandler(this.sBtnEnregistrer_Click);
            // 
            // txtNumeroRecu
            // 
            this.txtNumeroRecu.Location = new System.Drawing.Point(92, 60);
            this.txtNumeroRecu.Name = "txtNumeroRecu";
            this.txtNumeroRecu.Size = new System.Drawing.Size(550, 20);
            this.txtNumeroRecu.StyleController = this.layoutControl1;
            this.txtNumeroRecu.TabIndex = 7;
            // 
            // dateVersement
            // 
            this.dateVersement.EditValue = null;
            this.dateVersement.Location = new System.Drawing.Point(408, 36);
            this.dateVersement.Name = "dateVersement";
            this.dateVersement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateVersement.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateVersement.Size = new System.Drawing.Size(234, 20);
            this.dateVersement.StyleController = this.layoutControl1;
            this.dateVersement.TabIndex = 6;
            // 
            // sNumMontant
            // 
            this.sNumMontant.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sNumMontant.Location = new System.Drawing.Point(92, 36);
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
            this.sNumMontant.Size = new System.Drawing.Size(232, 20);
            this.sNumMontant.StyleController = this.layoutControl1;
            this.sNumMontant.TabIndex = 5;
            // 
            // CmbSouscripteur
            // 
            this.CmbSouscripteur.Location = new System.Drawing.Point(92, 12);
            this.CmbSouscripteur.Name = "CmbSouscripteur";
            this.CmbSouscripteur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CmbSouscripteur.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mId", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mNom", 30, "Nom"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mPrenoms", 30, "Prenoms")});
            this.CmbSouscripteur.Properties.NullText = "";
            this.CmbSouscripteur.Properties.ReadOnly = true;
            this.CmbSouscripteur.Size = new System.Drawing.Size(550, 20);
            this.CmbSouscripteur.StyleController = this.layoutControl1;
            this.CmbSouscripteur.TabIndex = 4;
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
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(654, 143);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.CmbSouscripteur;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(634, 24);
            this.layoutControlItem1.Text = "Souscripteur";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sNumMontant;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(316, 24);
            this.layoutControlItem2.Text = "Montant";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dateVersement;
            this.layoutControlItem3.Location = new System.Drawing.Point(316, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(318, 24);
            this.layoutControlItem3.Text = "Date Versement";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtNumeroRecu;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(634, 24);
            this.layoutControlItem4.Text = "Numéro Reçu";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.sBtnEnregistrer;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(316, 27);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.sBtnFermer;
            this.layoutControlItem7.Location = new System.Drawing.Point(316, 96);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(318, 27);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.CmbReceveur;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(634, 24);
            this.layoutControlItem8.Text = "Receveur";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(77, 13);
            // 
            // FenGestVersement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 123);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FenGestVersement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FenGestVersement";
            this.Load += new System.EventHandler(this.FenGestVersement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CmbReceveur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroRecu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVersement.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateVersement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumMontant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CmbSouscripteur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton sBtnFermer;
        private DevExpress.XtraEditors.SimpleButton sBtnEnregistrer;
        private DevExpress.XtraEditors.TextEdit txtNumeroRecu;
        private DevExpress.XtraEditors.DateEdit dateVersement;
        private DevExpress.XtraEditors.SpinEdit sNumMontant;
        private DevExpress.XtraEditors.LookUpEdit CmbSouscripteur;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LookUpEdit CmbReceveur;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}