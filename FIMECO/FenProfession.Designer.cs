namespace FIMECO
{
    partial class FenProfession
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FenProfession));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sBtnSupprProfession = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnModifProfession = new DevExpress.XtraEditors.SimpleButton();
            this.sBtnAjoutProfession = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlProfession = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1mIdP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1MLibelleP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProfession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gridControlProfession, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.04478F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.955224F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 402);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sBtnSupprProfession);
            this.panelControl1.Controls.Add(this.sBtnModifProfession);
            this.panelControl1.Controls.Add(this.sBtnAjoutProfession);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(3, 369);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(435, 30);
            this.panelControl1.TabIndex = 0;
            // 
            // sBtnSupprProfession
            // 
            this.sBtnSupprProfession.Image = ((System.Drawing.Image)(resources.GetObject("sBtnSupprProfession.Image")));
            this.sBtnSupprProfession.Location = new System.Drawing.Point(302, 2);
            this.sBtnSupprProfession.Name = "sBtnSupprProfession";
            this.sBtnSupprProfession.Size = new System.Drawing.Size(132, 26);
            this.sBtnSupprProfession.TabIndex = 8;
            this.sBtnSupprProfession.Text = "Supprimer Profession";
            this.sBtnSupprProfession.Click += new System.EventHandler(this.sBtnSupprProfession_Click);
            // 
            // sBtnModifProfession
            // 
            this.sBtnModifProfession.Image = ((System.Drawing.Image)(resources.GetObject("sBtnModifProfession.Image")));
            this.sBtnModifProfession.Location = new System.Drawing.Point(168, 2);
            this.sBtnModifProfession.Name = "sBtnModifProfession";
            this.sBtnModifProfession.Size = new System.Drawing.Size(132, 26);
            this.sBtnModifProfession.TabIndex = 7;
            this.sBtnModifProfession.Text = "Modifier Profession";
            this.sBtnModifProfession.Click += new System.EventHandler(this.sBtnModifProfession_Click);
            // 
            // sBtnAjoutProfession
            // 
            this.sBtnAjoutProfession.Image = ((System.Drawing.Image)(resources.GetObject("sBtnAjoutProfession.Image")));
            this.sBtnAjoutProfession.Location = new System.Drawing.Point(33, 2);
            this.sBtnAjoutProfession.Name = "sBtnAjoutProfession";
            this.sBtnAjoutProfession.Size = new System.Drawing.Size(132, 26);
            this.sBtnAjoutProfession.TabIndex = 6;
            this.sBtnAjoutProfession.Text = "Ajouter Profession";
            this.sBtnAjoutProfession.Click += new System.EventHandler(this.sBtnAjoutProfession_Click);
            // 
            // gridControlProfession
            // 
            this.gridControlProfession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProfession.Location = new System.Drawing.Point(3, 3);
            this.gridControlProfession.MainView = this.gridView1;
            this.gridControlProfession.Name = "gridControlProfession";
            this.gridControlProfession.Size = new System.Drawing.Size(435, 360);
            this.gridControlProfession.TabIndex = 1;
            this.gridControlProfession.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1mIdP,
            this.gridColumn1MLibelleP});
            this.gridView1.GridControl = this.gridControlProfession;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1mIdP
            // 
            this.gridColumn1mIdP.Caption = "mId";
            this.gridColumn1mIdP.Name = "gridColumn1mIdP";
            // 
            // gridColumn1MLibelleP
            // 
            this.gridColumn1MLibelleP.Caption = "Libelle";
            this.gridColumn1MLibelleP.FieldName = "mLibelle";
            this.gridColumn1MLibelleP.Name = "gridColumn1MLibelleP";
            this.gridColumn1MLibelleP.Visible = true;
            this.gridColumn1MLibelleP.VisibleIndex = 0;
            this.gridColumn1MLibelleP.Width = 150;
            // 
            // FenProfession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FenProfession";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FenProfession";
            this.Load += new System.EventHandler(this.FenProfession_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProfession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sBtnSupprProfession;
        private DevExpress.XtraEditors.SimpleButton sBtnModifProfession;
        private DevExpress.XtraEditors.SimpleButton sBtnAjoutProfession;
        private DevExpress.XtraGrid.GridControl gridControlProfession;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1mIdP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1MLibelleP;
    }
}