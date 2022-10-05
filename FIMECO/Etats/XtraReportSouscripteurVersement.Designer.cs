namespace FIMECO.Etats
{
    partial class XtraReportSouscripteurVersement
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReportSouscripteurVersement));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSouscripteur = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblProfession = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblClasseMetho = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelAnnee = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.XrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.XrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.XrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.XrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblAppli = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.XrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel6,
            this.xrLabel7});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mMontantRestant", "{0:#,#}")});
            this.xrLabel3.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(667.7556F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(120.2444F, 23F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mMontantTotalVerse", "{0:#,#}")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(544.3863F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(123.3693F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "0";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mMontantCotisation", "{0:#,#}")});
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(419.796F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(124.5904F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mNomPersonne")});
            this.xrLabel6.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(173.6893F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "mPrenomsPersonne")});
            this.xrLabel7.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(186.1893F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(233.6067F, 23F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblAppli,
            this.xrPictureBox1,
            this.xrPictureBox2,
            this.xrPictureBox3,
            this.xrPictureBox4,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel13,
            this.xrLblSouscripteur,
            this.xrLblProfession,
            this.xrLblClasseMetho,
            this.xrLabel5,
            this.xrLabelAnnee,
            this.xrLabel4});
            this.TopMargin.HeightF = 333.3333F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleLeft;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(6.477579F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(107.2917F, 115.0833F);
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleLeft;
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(130.1459F, 10.00001F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(512.5F, 115.0833F);
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
            this.xrPictureBox3.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleLeft;
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(663.3973F, 10.00001F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(128.125F, 115.0833F);
            // 
            // xrPictureBox4
            // 
            this.xrPictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox4.Image")));
            this.xrPictureBox4.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleLeft;
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(6.477579F, 125F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(785.0449F, 28.62502F);
            // 
            // xrLabel11
            // 
            this.xrLabel11.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel11.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(38.72757F, 168.1667F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(124.8557F, 23F);
            this.xrLabel11.StylePriority.UseBackColor = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "Souscripteurs :";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(38.72757F, 191.1667F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(124.8557F, 23F);
            this.xrLabel12.StylePriority.UseBackColor = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Profession :";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel13.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(38.72757F, 214.1667F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(124.8557F, 23F);
            this.xrLabel13.StylePriority.UseBackColor = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Classe Métho :";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblSouscripteur
            // 
            this.xrLblSouscripteur.BackColor = System.Drawing.Color.Transparent;
            this.xrLblSouscripteur.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLblSouscripteur.LocationFloat = new DevExpress.Utils.PointFloat(163.5833F, 168.1667F);
            this.xrLblSouscripteur.Multiline = true;
            this.xrLblSouscripteur.Name = "xrLblSouscripteur";
            this.xrLblSouscripteur.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblSouscripteur.SizeF = new System.Drawing.SizeF(599.8557F, 23F);
            this.xrLblSouscripteur.StylePriority.UseBackColor = false;
            this.xrLblSouscripteur.StylePriority.UseFont = false;
            this.xrLblSouscripteur.StylePriority.UseTextAlignment = false;
            this.xrLblSouscripteur.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblProfession
            // 
            this.xrLblProfession.BackColor = System.Drawing.Color.Transparent;
            this.xrLblProfession.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLblProfession.LocationFloat = new DevExpress.Utils.PointFloat(163.5833F, 191.1667F);
            this.xrLblProfession.Multiline = true;
            this.xrLblProfession.Name = "xrLblProfession";
            this.xrLblProfession.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblProfession.SizeF = new System.Drawing.SizeF(599.8557F, 23F);
            this.xrLblProfession.StylePriority.UseBackColor = false;
            this.xrLblProfession.StylePriority.UseFont = false;
            this.xrLblProfession.StylePriority.UseTextAlignment = false;
            this.xrLblProfession.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblClasseMetho
            // 
            this.xrLblClasseMetho.BackColor = System.Drawing.Color.Transparent;
            this.xrLblClasseMetho.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.xrLblClasseMetho.LocationFloat = new DevExpress.Utils.PointFloat(163.5833F, 214.1667F);
            this.xrLblClasseMetho.Multiline = true;
            this.xrLblClasseMetho.Name = "xrLblClasseMetho";
            this.xrLblClasseMetho.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblClasseMetho.SizeF = new System.Drawing.SizeF(599.8557F, 23F);
            this.xrLblClasseMetho.StylePriority.UseBackColor = false;
            this.xrLblClasseMetho.StylePriority.UseFont = false;
            this.xrLblClasseMetho.StylePriority.UseTextAlignment = false;
            this.xrLblClasseMetho.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(276.5755F, 300.3333F);
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(117.2162F, 29.25F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Année ";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabelAnnee
            // 
            this.xrLabelAnnee.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabelAnnee.LocationFloat = new DevExpress.Utils.PointFloat(393.7917F, 300.3333F);
            this.xrLabelAnnee.Multiline = true;
            this.xrLabelAnnee.Name = "xrLabelAnnee";
            this.xrLabelAnnee.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelAnnee.SizeF = new System.Drawing.SizeF(117.2162F, 29.25F);
            this.xrLabelAnnee.StylePriority.UseFont = false;
            this.xrLabelAnnee.StylePriority.UseTextAlignment = false;
            this.xrLabelAnnee.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(163.5833F, 271.0833F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(504.1723F, 29.25F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Liste des Souscripteurs et leurs Versements";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 4.166667F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.XrTable2});
            this.GroupHeader1.HeightF = 29.16667F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // XrTable2
            // 
            this.XrTable2.BackColor = System.Drawing.Color.Gainsboro;
            this.XrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.XrTable2.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.XrTable2.Name = "XrTable2";
            this.XrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.XrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.XrTableRow2});
            this.XrTable2.SizeF = new System.Drawing.SizeF(775.5F, 24.37498F);
            this.XrTable2.StylePriority.UseBackColor = false;
            this.XrTable2.StylePriority.UseFont = false;
            this.XrTable2.StylePriority.UsePadding = false;
            // 
            // XrTableRow2
            // 
            this.XrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.XrTableCell8,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.XrTableRow2.Name = "XrTableRow2";
            this.XrTableRow2.Weight = 1D;
            // 
            // XrTableCell8
            // 
            this.XrTableCell8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XrTableCell8.Name = "XrTableCell8";
            this.XrTableCell8.StylePriority.UseFont = false;
            this.XrTableCell8.StylePriority.UseTextAlignment = false;
            this.XrTableCell8.Text = "Nom";
            this.XrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.XrTableCell8.Weight = 0.78325592963697921D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Prenoms";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell1.Weight = 1.0534545832078419D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Mtant Annuel";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell2.Weight = 0.56184270919657886D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Mtant Versé";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell3.Weight = 0.55633670557145565D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Mtant Restant";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell4.Weight = 0.54224458612419268D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.XrPageInfo1});
            this.PageFooter.HeightF = 20.83333F;
            this.PageFooter.Name = "PageFooter";
            // 
            // XrPageInfo1
            // 
            this.XrPageInfo1.Format = "Page {0} sur {1}";
            this.XrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(687.9166F, 0F);
            this.XrPageInfo1.Name = "XrPageInfo1";
            this.XrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.XrPageInfo1.SizeF = new System.Drawing.SizeF(103.6058F, 15.70833F);
            this.XrPageInfo1.StylePriority.UseTextAlignment = false;
            this.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLblAppli
            // 
            this.xrLblAppli.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.xrLblAppli.ForeColor = System.Drawing.Color.Green;
            this.xrLblAppli.LocationFloat = new DevExpress.Utils.PointFloat(283.375F, 237.1667F);
            this.xrLblAppli.Multiline = true;
            this.xrLblAppli.Name = "xrLblAppli";
            this.xrLblAppli.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblAppli.SizeF = new System.Drawing.SizeF(216.1746F, 29.25F);
            this.xrLblAppli.StylePriority.UseFont = false;
            this.xrLblAppli.StylePriority.UseForeColor = false;
            this.xrLblAppli.StylePriority.UseTextAlignment = false;
            this.xrLblAppli.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(FIMECO.Models.CEtatSouscriptVersement);
            // 
            // XtraReportSouscripteurVersement
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.PageFooter});
            this.DataSource = this.bindingSource1;
            this.Margins = new System.Drawing.Printing.Margins(25, 27, 333, 4);
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.XrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        internal DevExpress.XtraReports.UI.XRTable XrTable2;
        internal DevExpress.XtraReports.UI.XRTableRow XrTableRow2;
        internal DevExpress.XtraReports.UI.XRTableCell XrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabelAnnee;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLblSouscripteur;
        private DevExpress.XtraReports.UI.XRLabel xrLblProfession;
        private DevExpress.XtraReports.UI.XRLabel xrLblClasseMetho;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox2;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox3;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox4;
        internal DevExpress.XtraReports.UI.XRPageInfo XrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLblAppli;
    }
}
