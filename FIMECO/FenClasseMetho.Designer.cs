namespace FIMECO
{
    partial class FenClasseMetho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FenClasseMetho));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sBtnSupprClasse = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnModifClasse = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnAjoutClasse = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlClasseMetho = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1mIdM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mNomClasseC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mNomConducteur1C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mPrenomConducteur1C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mTelephoneConducteur1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mEmailConducteur1C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mNomConducteur2C = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mPrenomConducteur2c = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mTelephoneConducteur2c = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mEmailConducteur2v = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1mQuartierC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasseMetho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gridControlClasseMetho, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.640264F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 97.35973F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(697, 340);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sBtnSupprClasse);
            this.panelControl1.Controls.Add(this.sBtnModifClasse);
            this.panelControl1.Controls.Add(this.sBtnAjoutClasse);
            this.panelControl1.Location = new System.Drawing.Point(3, 306);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(685, 31);
            this.panelControl1.TabIndex = 0;
            // 
            // sBtnSupprClasse
            // 
            this.sBtnSupprClasse.Image = ((System.Drawing.Image)(resources.GetObject("sBtnSupprClasse.Image")));
            this.sBtnSupprClasse.Location = new System.Drawing.Point(552, 3);
            this.sBtnSupprClasse.Name = "sBtnSupprClasse";
            this.sBtnSupprClasse.Size = new System.Drawing.Size(132, 26);
            this.sBtnSupprClasse.TabIndex = 5;
            this.sBtnSupprClasse.Text = "Supprimer Classe";
            this.sBtnSupprClasse.Click += new System.EventHandler(this.sBtnSupprClasse_Click);
            // 
            // sBtnModifClasse
            // 
            this.sBtnModifClasse.Image = ((System.Drawing.Image)(resources.GetObject("sBtnModifClasse.Image")));
            this.sBtnModifClasse.Location = new System.Drawing.Point(418, 3);
            this.sBtnModifClasse.Name = "sBtnModifClasse";
            this.sBtnModifClasse.Size = new System.Drawing.Size(132, 26);
            this.sBtnModifClasse.TabIndex = 4;
            this.sBtnModifClasse.Text = "Modifier Classe";
            this.sBtnModifClasse.Click += new System.EventHandler(this.sBtnModifClasse_Click);
            // 
            // sBtnAjoutClasse
            // 
            this.sBtnAjoutClasse.Image = ((System.Drawing.Image)(resources.GetObject("sBtnAjoutClasse.Image")));
            this.sBtnAjoutClasse.Location = new System.Drawing.Point(283, 3);
            this.sBtnAjoutClasse.Name = "sBtnAjoutClasse";
            this.sBtnAjoutClasse.Size = new System.Drawing.Size(132, 26);
            this.sBtnAjoutClasse.TabIndex = 3;
            this.sBtnAjoutClasse.Text = "Ajouter Classe";
            this.sBtnAjoutClasse.Click += new System.EventHandler(this.sBtnAjoutClasse_Click);
            // 
            // gridControlClasseMetho
            // 
            this.gridControlClasseMetho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlClasseMetho.Location = new System.Drawing.Point(3, 11);
            this.gridControlClasseMetho.MainView = this.gridView1;
            this.gridControlClasseMetho.Name = "gridControlClasseMetho";
            this.gridControlClasseMetho.Size = new System.Drawing.Size(691, 289);
            this.gridControlClasseMetho.TabIndex = 1;
            this.gridControlClasseMetho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1mIdM,
            this.gridColumn1mNomClasseC,
            this.gridColumn1mNomConducteur1C,
            this.gridColumn1mPrenomConducteur1C,
            this.gridColumn1mTelephoneConducteur1,
            this.gridColumn1mEmailConducteur1C,
            this.gridColumn1mNomConducteur2C,
            this.gridColumn1mPrenomConducteur2c,
            this.gridColumn1mTelephoneConducteur2c,
            this.gridColumn1mEmailConducteur2v,
            this.gridColumn1mQuartierC});
            this.gridView1.GridControl = this.gridControlClasseMetho;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1mIdM
            // 
            this.gridColumn1mIdM.Caption = "Id";
            this.gridColumn1mIdM.FieldName = "mId";
            this.gridColumn1mIdM.Name = "gridColumn1mIdM";
            // 
            // gridColumn1mNomClasseC
            // 
            this.gridColumn1mNomClasseC.Caption = "Nom Classe";
            this.gridColumn1mNomClasseC.FieldName = "mNomClasse";
            this.gridColumn1mNomClasseC.Name = "gridColumn1mNomClasseC";
            this.gridColumn1mNomClasseC.Visible = true;
            this.gridColumn1mNomClasseC.VisibleIndex = 0;
            // 
            // gridColumn1mNomConducteur1C
            // 
            this.gridColumn1mNomConducteur1C.Caption = "Nom Conducteur1";
            this.gridColumn1mNomConducteur1C.FieldName = "mNomConducteur1";
            this.gridColumn1mNomConducteur1C.Name = "gridColumn1mNomConducteur1C";
            this.gridColumn1mNomConducteur1C.Visible = true;
            this.gridColumn1mNomConducteur1C.VisibleIndex = 1;
            // 
            // gridColumn1mPrenomConducteur1C
            // 
            this.gridColumn1mPrenomConducteur1C.Caption = "Prenom Conducteur1";
            this.gridColumn1mPrenomConducteur1C.FieldName = "mPrenomConducteur1";
            this.gridColumn1mPrenomConducteur1C.Name = "gridColumn1mPrenomConducteur1C";
            this.gridColumn1mPrenomConducteur1C.Visible = true;
            this.gridColumn1mPrenomConducteur1C.VisibleIndex = 2;
            // 
            // gridColumn1mTelephoneConducteur1
            // 
            this.gridColumn1mTelephoneConducteur1.Caption = "Telephone Conducteur1";
            this.gridColumn1mTelephoneConducteur1.FieldName = "mTelephoneConducteur1";
            this.gridColumn1mTelephoneConducteur1.Name = "gridColumn1mTelephoneConducteur1";
            this.gridColumn1mTelephoneConducteur1.Visible = true;
            this.gridColumn1mTelephoneConducteur1.VisibleIndex = 3;
            // 
            // gridColumn1mEmailConducteur1C
            // 
            this.gridColumn1mEmailConducteur1C.Caption = "Email Conducteur1";
            this.gridColumn1mEmailConducteur1C.FieldName = "mEmailConducteur1";
            this.gridColumn1mEmailConducteur1C.Name = "gridColumn1mEmailConducteur1C";
            this.gridColumn1mEmailConducteur1C.Visible = true;
            this.gridColumn1mEmailConducteur1C.VisibleIndex = 4;
            // 
            // gridColumn1mNomConducteur2C
            // 
            this.gridColumn1mNomConducteur2C.Caption = "Nom Conducteur2";
            this.gridColumn1mNomConducteur2C.FieldName = "mNomConducteur2";
            this.gridColumn1mNomConducteur2C.Name = "gridColumn1mNomConducteur2C";
            this.gridColumn1mNomConducteur2C.Visible = true;
            this.gridColumn1mNomConducteur2C.VisibleIndex = 5;
            // 
            // gridColumn1mPrenomConducteur2c
            // 
            this.gridColumn1mPrenomConducteur2c.Caption = "Prenom Conducteur2";
            this.gridColumn1mPrenomConducteur2c.FieldName = "mPrenomConducteur2";
            this.gridColumn1mPrenomConducteur2c.Name = "gridColumn1mPrenomConducteur2c";
            this.gridColumn1mPrenomConducteur2c.Visible = true;
            this.gridColumn1mPrenomConducteur2c.VisibleIndex = 6;
            // 
            // gridColumn1mTelephoneConducteur2c
            // 
            this.gridColumn1mTelephoneConducteur2c.Caption = "Telephone Conducteur2";
            this.gridColumn1mTelephoneConducteur2c.FieldName = "mTelephoneConducteur2";
            this.gridColumn1mTelephoneConducteur2c.Name = "gridColumn1mTelephoneConducteur2c";
            this.gridColumn1mTelephoneConducteur2c.Visible = true;
            this.gridColumn1mTelephoneConducteur2c.VisibleIndex = 7;
            // 
            // gridColumn1mEmailConducteur2v
            // 
            this.gridColumn1mEmailConducteur2v.Caption = "Email Conducteur2";
            this.gridColumn1mEmailConducteur2v.FieldName = "mEmailConducteur2";
            this.gridColumn1mEmailConducteur2v.Name = "gridColumn1mEmailConducteur2v";
            this.gridColumn1mEmailConducteur2v.Visible = true;
            this.gridColumn1mEmailConducteur2v.VisibleIndex = 8;
            // 
            // gridColumn1mQuartierC
            // 
            this.gridColumn1mQuartierC.Caption = "Quartier";
            this.gridColumn1mQuartierC.FieldName = "mQuartier";
            this.gridColumn1mQuartierC.Name = "gridColumn1mQuartierC";
            this.gridColumn1mQuartierC.Visible = true;
            this.gridColumn1mQuartierC.VisibleIndex = 9;
            // 
            // FenClasseMetho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 341);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FenClasseMetho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FenClasseMetho";
            this.Load += new System.EventHandler(this.FenClasseMetho_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlClasseMetho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sBtnSupprClasse;
        private DevExpress.XtraEditors.SimpleButton sBtnModifClasse;
        private DevExpress.XtraEditors.SimpleButton sBtnAjoutClasse;
        private DevExpress.XtraGrid.GridControl gridControlClasseMetho;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mIdM;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mNomClasseC;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mNomConducteur1C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mPrenomConducteur1C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mTelephoneConducteur1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mEmailConducteur1C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mNomConducteur2C;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mPrenomConducteur2c;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mTelephoneConducteur2c;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mEmailConducteur2v;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mQuartierC;
    }
}