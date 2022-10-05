namespace FIMECO
{
    partial class FenCotisationAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FenCotisationAn));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sBtnSupprimerContribution = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnModifierContribution = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnAjoutContribution = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnApercu = new DevExpress.XtraEditors.SimpleButton();
            this.Années = new DevExpress.XtraEditors.GroupControl();
            this.sNumAnFin = new DevExpress.XtraEditors.SpinEdit();
            this.sNumAnDeb = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlCotisationAn = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1colmIdC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2colmAnneeC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3mUserCreationC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3mUserLastModifC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3mDateCreationC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3mDateLastModif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Années)).BeginInit();
            this.Années.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnDeb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCotisationAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Années, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControlCotisationAn, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, -1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.28972F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.71028F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 321F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(845, 425);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sBtnSupprimerContribution);
            this.panelControl1.Controls.Add(this.sBtnModifierContribution);
            this.panelControl1.Controls.Add(this.sBtnAjoutContribution);
            this.panelControl1.Controls.Add(this.sBtnApercu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 72);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(839, 28);
            this.panelControl1.TabIndex = 0;
            // 
            // sBtnSupprimerContribution
            // 
            this.sBtnSupprimerContribution.Location = new System.Drawing.Point(389, 1);
            this.sBtnSupprimerContribution.Name = "sBtnSupprimerContribution";
            this.sBtnSupprimerContribution.Size = new System.Drawing.Size(127, 25);
            this.sBtnSupprimerContribution.TabIndex = 3;
            this.sBtnSupprimerContribution.Text = "Supprimer Cotisation";
            this.sBtnSupprimerContribution.Click += new System.EventHandler(this.sBtnSupprimerContribution_Click);
            // 
            // sBtnModifierContribution
            // 
            this.sBtnModifierContribution.Location = new System.Drawing.Point(260, 1);
            this.sBtnModifierContribution.Name = "sBtnModifierContribution";
            this.sBtnModifierContribution.Size = new System.Drawing.Size(127, 25);
            this.sBtnModifierContribution.TabIndex = 2;
            this.sBtnModifierContribution.Text = "Modifier Cotisation";
            this.sBtnModifierContribution.Click += new System.EventHandler(this.sBtnModifierContribution_Click);
            // 
            // sBtnAjoutContribution
            // 
            this.sBtnAjoutContribution.Location = new System.Drawing.Point(131, 1);
            this.sBtnAjoutContribution.Name = "sBtnAjoutContribution";
            this.sBtnAjoutContribution.Size = new System.Drawing.Size(127, 25);
            this.sBtnAjoutContribution.TabIndex = 1;
            this.sBtnAjoutContribution.Text = "Ajouter Cotisation";
            this.sBtnAjoutContribution.Click += new System.EventHandler(this.sBtnAjoutContribution_Click);
            // 
            // sBtnApercu
            // 
            this.sBtnApercu.Location = new System.Drawing.Point(2, 1);
            this.sBtnApercu.Name = "sBtnApercu";
            this.sBtnApercu.Size = new System.Drawing.Size(127, 25);
            this.sBtnApercu.TabIndex = 0;
            this.sBtnApercu.Text = "Aperçu";
            this.sBtnApercu.Click += new System.EventHandler(this.sBtnApercu_Click);
            // 
            // Années
            // 
            this.Années.Controls.Add(this.sNumAnFin);
            this.Années.Controls.Add(this.sNumAnDeb);
            this.Années.Controls.Add(this.label2);
            this.Années.Controls.Add(this.label1);
            this.Années.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Années.Location = new System.Drawing.Point(3, 3);
            this.Années.Name = "Années";
            this.Années.Size = new System.Drawing.Size(839, 63);
            this.Années.TabIndex = 1;
            this.Années.Text = "Années";
            // 
            // sNumAnFin
            // 
            this.sNumAnFin.EditValue = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.sNumAnFin.Location = new System.Drawing.Point(297, 27);
            this.sNumAnFin.Name = "sNumAnFin";
            this.sNumAnFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sNumAnFin.Properties.MaxValue = new decimal(new int[] {
            2999,
            0,
            0,
            0});
            this.sNumAnFin.Properties.MinValue = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.sNumAnFin.Size = new System.Drawing.Size(159, 20);
            this.sNumAnFin.TabIndex = 17;
            // 
            // sNumAnDeb
            // 
            this.sNumAnDeb.EditValue = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.sNumAnDeb.Location = new System.Drawing.Point(112, 27);
            this.sNumAnDeb.Name = "sNumAnDeb";
            this.sNumAnDeb.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sNumAnDeb.Properties.MaxValue = new decimal(new int[] {
            2900,
            0,
            0,
            0});
            this.sNumAnDeb.Properties.MinValue = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.sNumAnDeb.Size = new System.Drawing.Size(159, 20);
            this.sNumAnDeb.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "A";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "De";
            // 
            // gridControlCotisationAn
            // 
            this.gridControlCotisationAn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlCotisationAn.Location = new System.Drawing.Point(3, 106);
            this.gridControlCotisationAn.MainView = this.gridView1;
            this.gridControlCotisationAn.Name = "gridControlCotisationAn";
            this.gridControlCotisationAn.Size = new System.Drawing.Size(839, 316);
            this.gridControlCotisationAn.TabIndex = 2;
            this.gridControlCotisationAn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1colmIdC,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn2colmAnneeC,
            this.gridColumn2,
            this.gridColumn3mUserCreationC,
            this.gridColumn3mUserLastModifC,
            this.gridColumn3mDateCreationC,
            this.gridColumn3mDateLastModif});
            this.gridView1.GridControl = this.gridControlCotisationAn;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1colmIdC
            // 
            this.gridColumn1colmIdC.Caption = "Id";
            this.gridColumn1colmIdC.FieldName = "mId";
            this.gridColumn1colmIdC.Name = "gridColumn1colmIdC";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "IdSouscripteur";
            this.gridColumn1.FieldName = "mIdSouscripteur";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Nom";
            this.gridColumn3.FieldName = "mNom";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 61;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Prenoms";
            this.gridColumn4.FieldName = "mPrenoms";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn2colmAnneeC
            // 
            this.gridColumn2colmAnneeC.Caption = "Annee";
            this.gridColumn2colmAnneeC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2colmAnneeC.FieldName = "mAnnee";
            this.gridColumn2colmAnneeC.Name = "gridColumn2colmAnneeC";
            this.gridColumn2colmAnneeC.Visible = true;
            this.gridColumn2colmAnneeC.VisibleIndex = 2;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Montant Cotisation";
            this.gridColumn2.DisplayFormat.FormatString = "n0";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "mMontantCotisation";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn3mUserCreationC
            // 
            this.gridColumn3mUserCreationC.Caption = "UserCreation";
            this.gridColumn3mUserCreationC.FieldName = "mUserCreation";
            this.gridColumn3mUserCreationC.Name = "gridColumn3mUserCreationC";
            // 
            // gridColumn3mUserLastModifC
            // 
            this.gridColumn3mUserLastModifC.Caption = "UserLastModif";
            this.gridColumn3mUserLastModifC.FieldName = "mUserLastModif";
            this.gridColumn3mUserLastModifC.Name = "gridColumn3mUserLastModifC";
            // 
            // gridColumn3mDateCreationC
            // 
            this.gridColumn3mDateCreationC.Caption = "DateCreation";
            this.gridColumn3mDateCreationC.FieldName = "mDateCreation";
            this.gridColumn3mDateCreationC.Name = "gridColumn3mDateCreationC";
            // 
            // gridColumn3mDateLastModif
            // 
            this.gridColumn3mDateLastModif.Caption = "DateLastModif";
            this.gridColumn3mDateLastModif.FieldName = "mDateLastModif";
            this.gridColumn3mDateLastModif.Name = "gridColumn3mDateLastModif";
            // 
            // FenCotisationAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 424);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FenCotisationAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Des Objectifs Annuels";
            this.Load += new System.EventHandler(this.FenCotisationAn_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Années)).EndInit();
            this.Années.ResumeLayout(false);
            this.Années.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sNumAnDeb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlCotisationAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sBtnSupprimerContribution;
        private DevExpress.XtraEditors.SimpleButton sBtnModifierContribution;
        private DevExpress.XtraEditors.SimpleButton sBtnAjoutContribution;
        private DevExpress.XtraEditors.SimpleButton sBtnApercu;
        private DevExpress.XtraEditors.GroupControl Années;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControlCotisationAn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SpinEdit sNumAnFin;
        private DevExpress.XtraEditors.SpinEdit sNumAnDeb;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1colmIdC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2colmAnneeC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3mUserCreationC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3mUserLastModifC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3mDateCreationC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3mDateLastModif;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}