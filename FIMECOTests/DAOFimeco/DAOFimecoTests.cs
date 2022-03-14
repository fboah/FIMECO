using Microsoft.VisualStudio.TestTools.UnitTesting;
using FIMECO.DAOFimeco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMECO.Models;

namespace FIMECO.DAOFimeco.Tests
{
    //[TestClass()]
    //public class DAOFimecoTests
    //{
    //    [TestMethod()]
    //    public void AddSouscripteurTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CSouscripteur();

    //        cc.mCode = "QQQQ";
    //        cc.mNom = "TOTO";
    //        cc.mPrenoms = "RINA";
    //        cc.mIdClasseMetho = 2;
    //        cc.mDateNaissance = DateTime.Now.Date;
    //        cc.mDateSouscription = DateTime.Now.Date;
    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "\\" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "\\" + Environment.UserDomainName;

    //        ret = dao.AddSouscripteur(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void UpdateClientOperationTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CSouscripteur();

    //        cc.mId = 1;
    //        //cc.mC_Num = "2ATECHNOLOGIES";
    //        //cc.mC_Intitule = "2ATECHNOLOGIES";
    //        //cc.mC_NumDocument = "TESTFR";
    //        cc.mDateSouscription = DateTime.Parse("13/11/2018");
    //        cc.mDateCreation = DateTime.Parse("13/11/2018");
    //        cc.mDateNaissance = DateTime.Parse("13/11/2008");
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "//" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "//" + Environment.UserDomainName;

    //        ret = dao.UpdateSouscripteur(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void DeleteSouscripteurTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CSouscripteur();

    //        cc.mId = 1;

    //        ret = dao.DeleteSouscripteur(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetSouscripteurByIdTest()
    //    {
    //        CSouscripteur ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CSouscripteur();

    //        cc.mId = 3;

    //        ret = dao.GetSouscripteurById(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetAllSouscripteurTest()
    //    {
    //        List<CSouscripteur> ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        ret = dao.GetAllSouscripteur(chaine);
    //    }

    //    [TestMethod()]
    //    public void AddClasseMethoTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CClasseMetho();

    //        cc.mNomClasse = "SHILO";
    //        cc.mNomConducteur1 = "SERGE";
    //        cc.mPrenomConducteur1 = "BABOKOISSI";
    //        cc.mQuartier = "SIDECI LEM";


    //        ret = dao.AddClasseMetho(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void UpdateClasseMethoTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CClasseMetho();

    //        cc.mId = 1;
    //        //cc.mC_Num = "2ATECHNOLOGIES";
    //        //cc.mC_Intitule = "2ATECHNOLOGIES";
    //        //cc.mC_NumDocument = "TESTFR";
    //        cc.mQuartier = "RIVERA PALMERAIE";

    //        ret = dao.UpdateClasseMetho(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void DeleteClasseMethoTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CClasseMetho();

    //        cc.mId = 1;

    //        ret = dao.DeleteClasseMetho(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetClasseMethoByIdTest()
    //    {
    //        CClasseMetho ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CClasseMetho();

    //        cc.mId = 3;

    //        ret = dao.GetClasseMethoById(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetAllClasseMethoTest()
    //    {
    //        List<CClasseMetho> ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        ret = dao.GetAllClasseMetho(chaine);
    //    }

    //    [TestMethod()]
    //    public void AddMembreSouscripteurTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CMembreSouscripteur();

    //        cc.mIdSouscripteur = 2;
    //        cc.mNom = "FAFA";
    //        cc.mPrenoms = "MOURAD";

    //        cc.mDateNaissance = DateTime.Now.Date;

    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "\\" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "\\" + Environment.UserDomainName;

    //        ret = dao.AddMembreSouscripteur(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void UpdateMembreSouscripteurTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CMembreSouscripteur();

    //        cc.mId = 1;
    //        cc.mIdSouscripteur = 2;
    //        //cc.mC_Num = "2ATECHNOLOGIES";
    //        //cc.mC_Intitule = "2ATECHNOLOGIES";
    //        //cc.mC_NumDocument = "TESTFR";
    //        cc.mNom = "ZATO";
    //        cc.mPrenoms = "RIKIKI";
    //        cc.mSexe = "M";

    //        cc.mDateCreation = DateTime.Parse("13/11/2018");
    //        cc.mDateNaissance = DateTime.Parse("13/11/2008");
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "//" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "//" + Environment.UserDomainName;

    //        ret = dao.UpdateMembreSouscripteur(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void DeleteMembreSouscripteurTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CMembreSouscripteur();

    //        cc.mId = 3;

    //        ret = dao.DeleteMembreSouscripteur(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetMembreSouscripteurByIdTest()
    //    {
    //        CMembreSouscripteur ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CSouscripteur();

    //        cc.mId = 2;

    //        ret = dao.GetMembreSouscripteurById(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetAllMembreSouscripteurTest()
    //    {
    //        List<CMembreSouscripteur> ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        ret = dao.GetAllMembreSouscripteur(chaine);
    //    }

    //    [TestMethod()]
    //    public void AddCotisationAnnuelleTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CCotisationAnnuelle();

    //        cc.mIdSouscripteur = 4;
    //        cc.mAnnee = 2019;
    //        cc.mMontantCotisation = 300000;

    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "\\" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "\\" + Environment.UserDomainName;

    //        ret = dao.AddCotisationAnnuelle(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void UpdateCotisationAnnuelleTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CCotisationAnnuelle();

    //        cc.mId = 1;
    //        cc.mIdSouscripteur = 2;
    //        //cc.mC_Num = "2ATECHNOLOGIES";
    //        //cc.mC_Intitule = "2ATECHNOLOGIES";
    //        //cc.mC_NumDocument = "TESTFR";
    //        cc.mAnnee = 2017;

    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "//" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "//" + Environment.UserDomainName;

    //        ret = dao.UpdateCotisationAnnuelle(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void DeleteCotisationAnnuelleTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CCotisationAnnuelle();

    //        cc.mId = 1;

    //        ret = dao.DeleteCotisationAnnuelle(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetCotisationAnnuelleByIdTest()
    //    {
    //        CCotisationAnnuelle ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CCotisationAnnuelle();

    //        cc.mId = 2;

    //        ret = dao.GetCotisationAnnuelleById(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetAllCotisationAnnuelleTest()
    //    {
    //        List<CCotisationAnnuelle> ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        ret = dao.GetAllCotisationAnnuelle(chaine);
    //    }

    //    [TestMethod()]
    //    public void AddVersementTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CVersement();

    //        cc.mIdSouscripteur = 1;
    //        cc.mNomReceveur = "ZEZETO";
    //        cc.mNumeroRecu = "123456";
    //        cc.mMontantVersement = 32000;
    //        cc.mDateVersement = DateTime.Now;
    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "\\" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "\\" + Environment.UserDomainName;

    //        ret = dao.AddVersement(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void UpdateVersementTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CVersement();

    //        cc.mId = 1;
    //        cc.mIdSouscripteur = 2;
    //        //cc.mC_Num = "2ATECHNOLOGIES";
    //        //cc.mC_Intitule = "2ATECHNOLOGIES";
    //        //cc.mC_NumDocument = "TESTFR";
    //        cc.mNomReceveur = "EDOUN RAOUL";

    //        cc.mDateCreation = DateTime.Now;
    //        cc.mDateLastModif = DateTime.Now;
    //        cc.mDateVersement = DateTime.Now;
    //        cc.mUserCreation = Environment.MachineName + "\\" + Environment.UserDomainName;
    //        cc.mUserLastModif = Environment.MachineName + "\\" + Environment.UserDomainName;

    //        ret = dao.UpdateVersement(cc, chaine);
    //    }

    //    [TestMethod()]
    //    public void DeleteVersementTest()
    //    {
    //        bool ret = false;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CVersement();

    //        cc.mId = 2;

    //        ret = dao.DeleteVersement(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetVersementByIdTest()
    //    {
    //        CVersement ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        var cc = new CVersement();

    //        cc.mId = 3;

    //        ret = dao.GetVersementById(cc.mId, chaine);
    //    }

    //    [TestMethod()]
    //    public void GetAllVersementTest()
    //    {
    //        List<CVersement> ret = null;
    //        DAOFimeco dao = new DAOFimeco();

    //        //  var obj1 = new CAlias();

    //        string chaine = @"Initial Catalog=FIMECOBD;Data Source=BOAH\SAGE200;Integrated Security=SSPI";

    //        ret = dao.GetAllVersement(chaine);
    //    }
    //}
}