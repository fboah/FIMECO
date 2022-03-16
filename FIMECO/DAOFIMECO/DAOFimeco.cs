using FIMECO.Models;
using FIMECO.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMECO.DAOFIMECO
{
   public class DAOFimeco
    {
        //SOUSCRIPTEUR================================================================

        //Ajouter Souscripteur

        public bool AddSouscripteur(CSouscripteur conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_Souscripteur
                        (Code, Nom, Prenoms, Statut,Sexe,DateNaissance,DateSouscription,LieuNaissance,Profession,IdClasseMetho,Telephone,Cellulaire,Email,District,CodeDistrict,Circuit,CodeCircuit,EgliseLocale,CodeEgliseLocale,DateCreation,DateLastModif,UserCreation,UserLastModif,IdProfession,IsAdulte,IsDelete)
                        VALUES (@Code, @Nom, @Prenoms, @Statut,@Sexe,@DateNaissance,@DateSouscription,@LieuNaissance,@Profession,@IdClasseMetho,@Telephone,@Cellulaire,@Email,@District,@CodeDistrict,@Circuit,@CodeCircuit,@EgliseLocale,@CodeEgliseLocale,@DateCreation,@DateLastModif,@UserCreation,@UserLastModif,@IdProfession,@IsAdulte,@IsDelete)";

                            command.Parameters.Add(new SqlParameter("Code", conc.mCode ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Nom", conc.mNom ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Prenoms", conc.mPrenoms ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Statut", conc.mStatutFamilial ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Sexe", conc.mSexe ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateNaissance", conc.mDateNaissance));
                            command.Parameters.Add(new SqlParameter("DateSouscription", conc.mDateSouscription));
                            command.Parameters.Add(new SqlParameter("LieuNaissance", conc.mLieuNaissance ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Profession", conc.mProfession ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("IdClasseMetho", conc.mIdClasseMetho));
                            command.Parameters.Add(new SqlParameter("Telephone", conc.mTelephone ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Cellulaire", conc.mCellulaire ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Email", conc.mEmail ?? string.Empty));
                      
                            command.Parameters.Add(new SqlParameter("District", conc.mDistrict ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("CodeDistrict", conc.mCodeDistrict ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Circuit", conc.mCircuit ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("CodeCircuit", conc.mCodeCircuit ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("EgliseLocale", conc.mEgliseLocale ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("CodeEgliseLocale", conc.mCodeEgliseLocale ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                            command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                            command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("IdProfession", conc.mIdProfession));
                            command.Parameters.Add(new SqlParameter("IsAdulte", conc.mIsAdulte));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));

                            
                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddSouscripteur -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Modifier Souscripteur
        public bool UpdateSouscripteur(CSouscripteur conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_Souscripteur SET 
                        Code = @Code, Nom = @Nom, Prenoms = @Prenoms,Statut = @Statut, Sexe = @Sexe, DateNaissance = @DateNaissance,DateSouscription=@DateSouscription, LieuNaissance = @LieuNaissance, Profession = @Profession , IdClasseMetho= @IdClasseMetho, Telephone= @Telephone, Cellulaire= @Cellulaire, Email= @Email, District= @District, CodeDistrict= @CodeDistrict, Circuit= @Circuit, CodeCircuit= @CodeCircuit, EgliseLocale= @EgliseLocale, CodeEgliseLocale= @CodeEgliseLocale, DateCreation= @DateCreation, DateLastModif= @DateLastModif, UserCreation= @UserCreation, UserLastModif= @UserLastModif,IdProfession=@IdProfession ,IsAdulte=@IsAdulte  WHERE Id = @Id";

                        
                        command.Parameters.Add(new SqlParameter("Code", conc.mCode ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Id", conc.mId ));
                        command.Parameters.Add(new SqlParameter("Nom", conc.mNom ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Prenoms", conc.mPrenoms ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Statut", conc.mStatutFamilial ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Sexe", conc.mSexe ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("DateNaissance", conc.mDateNaissance));
                        command.Parameters.Add(new SqlParameter("DateSouscription", conc.mDateSouscription));
                        command.Parameters.Add(new SqlParameter("LieuNaissance", conc.mLieuNaissance ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Profession", conc.mProfession ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("IdClasseMetho", conc.mIdClasseMetho));
                        command.Parameters.Add(new SqlParameter("Telephone", conc.mTelephone ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Cellulaire", conc.mCellulaire ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Email", conc.mEmail ?? string.Empty));
                       
                        command.Parameters.Add(new SqlParameter("District", conc.mDistrict ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("CodeDistrict", conc.mCodeDistrict ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Circuit", conc.mCircuit ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("CodeCircuit", conc.mCodeCircuit ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("EgliseLocale", conc.mEgliseLocale ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("CodeEgliseLocale", conc.mCodeEgliseLocale ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                        command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                        command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("IdProfession", conc.mIdProfession));
                        command.Parameters.Add(new SqlParameter("IsAdulte", conc.mIsAdulte));

                        
                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateSouscripteur -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                return retour;
            }
        }


        //Supprimer Souscripteur
        public bool DeleteSouscripteur(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"UPDATE FEC_Souscripteur SET IsDelete=1 WHERE Id = @Id";
                           // command.CommandText = @"DELETE FROM FEC_Souscripteur WHERE Id = @Id";

                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Obtenir un Souscripteur par son Id
        public CSouscripteur GetSouscripteurById(int Id, string chaineconnexion)
        {
            CSouscripteur EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_Souscripteur  where Id = @Id and IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CSouscripteur()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mCode = reader["Code"] == DBNull.Value ? string.Empty : reader["Code"] as string,
                                        mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenoms = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mStatutFamilial = reader["Statut"] == DBNull.Value ? string.Empty : reader["Statut"] as string,
                                        mSexe = reader["Sexe"] == DBNull.Value ? string.Empty : reader["Sexe"] as string,
                                        mLieuNaissance = reader["LieuNaissance"] == DBNull.Value ? string.Empty : reader["LieuNaissance"] as string,
                                        mProfession = reader["Profession"] == DBNull.Value ? string.Empty : reader["Profession"] as string,
                                        mTelephone = reader["Telephone"] == DBNull.Value ? string.Empty : reader["Telephone"] as string,
                                        mCellulaire = reader["Cellulaire"] == DBNull.Value ? string.Empty : reader["Cellulaire"] as string,
                                        mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                        mDistrict = reader["District"] == DBNull.Value ? string.Empty : reader["District"] as string,
                                        mCodeDistrict = reader["CodeDistrict"] == DBNull.Value ? string.Empty : reader["CodeDistrict"] as string,
                                        mCircuit = reader["Circuit"] == DBNull.Value ? string.Empty : reader["Circuit"] as string,
                                        mCodeCircuit = reader["CodeCircuit"] == DBNull.Value ? string.Empty : reader["CodeCircuit"] as string,
                                        mEgliseLocale = reader["EgliseLocale"] == DBNull.Value ? string.Empty : reader["EgliseLocale"] as string,
                                        mCodeEgliseLocale = reader["CodeEgliseLocale"] == DBNull.Value ? string.Empty : reader["CodeEgliseLocale"] as string,
                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                        mIsAdulte = reader["IsAdulte"] == DBNull.Value ? string.Empty : reader["IsAdulte"] as string,
                                        mDateNaissance = reader["DateNaissance"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateNaissance"].ToString()),
                                        mDateSouscription = reader["DateSouscription"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateSouscription"].ToString()),
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mIsDelete = reader["IsDelete"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDelete"]),
                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetSouscripteurById -> TypeErreur: " + ex.Message;
                return null;
            }
        }

        //Obtenir liste des elements

        public List<CSouscripteur> GetAllSouscripteur(string chaineconnexion,List<CClasseMetho> ListeCMetho)
        {
            List<CSouscripteur> ListClientOp = new List<CSouscripteur>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Souscripteur WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CSouscripteur()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mCode = reader["Code"] == DBNull.Value ? string.Empty : reader["Code"] as string,
                                        mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenoms = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mStatutFamilial = reader["Statut"] == DBNull.Value ? string.Empty : reader["Statut"] as string,
                                        mSexe = reader["Sexe"] == DBNull.Value ? string.Empty : reader["Sexe"] as string,
                                        mLieuNaissance = reader["LieuNaissance"] == DBNull.Value ? string.Empty : reader["LieuNaissance"] as string,
                                        mProfession = reader["Profession"] == DBNull.Value ? string.Empty : reader["Profession"] as string,
                                        mTelephone = reader["Telephone"] == DBNull.Value ? string.Empty : reader["Telephone"] as string,
                                        mCellulaire = reader["Cellulaire"] == DBNull.Value ? string.Empty : reader["Cellulaire"] as string,
                                        mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                        mDistrict = reader["District"] == DBNull.Value ? string.Empty : reader["District"] as string,
                                        mCodeDistrict = reader["CodeDistrict"] == DBNull.Value ? string.Empty : reader["CodeDistrict"] as string,
                                        mCircuit = reader["Circuit"] == DBNull.Value ? string.Empty : reader["Circuit"] as string,
                                        mCodeCircuit = reader["CodeCircuit"] == DBNull.Value ? string.Empty : reader["CodeCircuit"] as string,
                                        mEgliseLocale = reader["EgliseLocale"] == DBNull.Value ? string.Empty : reader["EgliseLocale"] as string,
                                        mCodeEgliseLocale = reader["CodeEgliseLocale"] == DBNull.Value ? string.Empty : reader["CodeEgliseLocale"] as string,
                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                        mDateNaissance = reader["DateNaissance"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateNaissance"].ToString()),
                                        mDateSouscription = reader["DateSouscription"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateSouscription"].ToString()),
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mClasseMetho = GetClasseMethoLibelle(Convert.ToInt32(reader["IdClasseMetho"]), ListeCMetho),
                                        mIsAdulte = reader["IsAdulte"] == DBNull.Value ? string.Empty : reader["IsAdulte"] as string,
                                        mIsDelete = reader["IsDelete"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDelete"]),
                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }
                            
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllSouscripteur -> TypeErreur: " + ex.Message;
                return null;
            }
        }

        //Obtenir la liste a afficher pour l'aperçu(Tenir compte des arriérés)

        public List<CArriereSouscripteur> GetAllArriereApercu(string chaineconnexion,string AnObjectif,bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous,string PrenomMultiSous,bool IsTousClasseMetho,string ListIdClasse, bool IsTousProfession, string ListIdProfession)
        {
            List<CArriereSouscripteur> ListClientOp = new List<CArriereSouscripteur>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;
                string FiltreClasseMetho = string.Empty;

                string FiltreProfession = string.Empty;

                #region Filtres

                //////////////////////FILTRES SOUSCRIPTEURS//////////////////////////////////
                if (IsSMulSous)//Multiple sousc
                {
                    
                    if (NomMultiSous != string.Empty && PrenomMultiSous != string.Empty)
                    {
                        NomMultiSous = NomMultiSous.Replace(",", "','");

                        PrenomMultiSous = PrenomMultiSous.Replace(",", "','");

                        FiltreSous = " and Nom in ('" + NomMultiSous + "') and Prenoms in ('" + PrenomMultiSous + "') ";

                    }

                    if (NomMultiSous == string.Empty && PrenomMultiSous != string.Empty)
                    {
                        //filtre que sur les prenoms
                        PrenomMultiSous = PrenomMultiSous.Replace(",", "','");

                        FiltreSous = " and Prenoms in ('" + PrenomMultiSous + "') ";

                    }

                    if (NomMultiSous != string.Empty && PrenomMultiSous == string.Empty)
                    {
                        //filtre que sur les NOMS
                        NomMultiSous = NomMultiSous.Replace(",", "','");

                        FiltreSous = " and Nom in ('" + NomMultiSous + "') ";
                        
                    }
                    
                }
                else
                {//DE_A
                    if (!IschkTousSous)
                    {
                        if (NomSousDE != string.Empty && NomSousA != string.Empty)
                        {
                            FiltreSous = " and Nom>='" + NomSousDE + "' and Nom <='" + NomSousA + "'";
                        }
                        
                    }
                    
                }

                //Filtre Classe Metho

                if(!IsTousClasseMetho)
                {
                    if(ListIdClasse!=string.Empty && ListIdClasse!=null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if(ListIdProfession!=string.Empty && ListIdProfession!=null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }
                    
                }

                #endregion

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select S.Id,Code,Nom,Prenoms,NumeroRecu,MontantVersement,DateVersement,Annee,MontantCotisation,S.IdClasseMetho,S.IdProfession ,V.IsDelete as IsDeleteVersement,S.IsDelete as IsDeleteSouscripteur,C.IsDelete as IsDeleteCotisation
                            from FEC_Souscripteur S
                            left join FEC_Versement V on S.Id=V.IdSouscripteur
                            left join FEC_CotisationAnnuelle C on C.IdSouscripteur=S.Id
                            where S.IsDelete=0 AND V.IsDelete=0 and C.IsDelete=0 AND YEAR(DateVersement)<=" + AnObjectif + " and Annee<="+ AnObjectif + " and YEAR(DateVersement)=Annee or C.Annee=" + AnObjectif + " " + FiltreSous+ FiltreProfession+FiltreClasseMetho+
                            " group by S.Id,Code,Nom,Prenoms,Annee,NumeroRecu,MontantVersement,DateVersement,MontantCotisation,S.IdClasseMetho,S.IdProfession ,V.IsDelete ,S.IsDelete ,C.IsDelete";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;
                            
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CArriereSouscripteur()
                                    {
                                        mIdSouscripteur = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mCode = reader["Code"] == DBNull.Value ? string.Empty : reader["Code"] as string,
                                        mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenoms = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mMontantCotisationObjectif = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mIsDeleteCotisation = reader["IsDeleteCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDeleteCotisation"]),
                                        mIsDeleteSouscripteur = reader["IsDeleteSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDeleteSouscripteur"]),
                                        mIsDeleteVersement = reader["IsDeleteVersement"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDeleteVersement"]),
                                        
                                    };

                                    if(ClientOp.mIsDeleteVersement!=1) ListClientOp.Add(ClientOp);


                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllSouscripteurApercu -> TypeErreur: " + ex.Message;
                return null;
            }
        }


        //Obtenir montant arriéré

            public int GetArriere(int Idsous,int AnObjectif,List<CSouscripteur> ListeSouscrip)
            {
                 int res = 0;

                try
                {


                  return res;
                }
                catch(Exception ex)
                {
                return res;

                }
            
            }
        



        public List<CSouscripteur> GetAllSouscripteur(string chaineconnexion)
        {
            List<CSouscripteur> ListClientOp = new List<CSouscripteur>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Souscripteur WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CSouscripteur()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mCode = reader["Code"] == DBNull.Value ? string.Empty : reader["Code"] as string,
                                        mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenoms = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mStatutFamilial = reader["Statut"] == DBNull.Value ? string.Empty : reader["Statut"] as string,
                                        mSexe = reader["Sexe"] == DBNull.Value ? string.Empty : reader["Sexe"] as string,
                                        mLieuNaissance = reader["LieuNaissance"] == DBNull.Value ? string.Empty : reader["LieuNaissance"] as string,
                                        mProfession = reader["Profession"] == DBNull.Value ? string.Empty : reader["Profession"] as string,
                                        mTelephone = reader["Telephone"] == DBNull.Value ? string.Empty : reader["Telephone"] as string,
                                        mCellulaire = reader["Cellulaire"] == DBNull.Value ? string.Empty : reader["Cellulaire"] as string,
                                        mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                        mDistrict = reader["District"] == DBNull.Value ? string.Empty : reader["District"] as string,
                                        mCodeDistrict = reader["CodeDistrict"] == DBNull.Value ? string.Empty : reader["CodeDistrict"] as string,
                                        mCircuit = reader["Circuit"] == DBNull.Value ? string.Empty : reader["Circuit"] as string,
                                        mCodeCircuit = reader["CodeCircuit"] == DBNull.Value ? string.Empty : reader["CodeCircuit"] as string,
                                        mEgliseLocale = reader["EgliseLocale"] == DBNull.Value ? string.Empty : reader["EgliseLocale"] as string,
                                        mCodeEgliseLocale = reader["CodeEgliseLocale"] == DBNull.Value ? string.Empty : reader["CodeEgliseLocale"] as string,
                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                        mDateNaissance = reader["DateNaissance"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateNaissance"].ToString()),
                                        mDateSouscription = reader["DateSouscription"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateSouscription"].ToString()),
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mIsAdulte = reader["IsAdulte"] == DBNull.Value ? string.Empty : reader["IsAdulte"] as string,
                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllSouscripteur -> TypeErreur: " + ex.Message;
                return null;
            }
        }


        public string GetNomSouscripteur(int IdSous,List<CSouscripteur> LCM)
        {
            string ret = string.Empty;
            try
            {
                var lret = LCM.FirstOrDefault(c => c.mId == IdSous);

                ret = lret.mNom;

                return ret;
            }
            catch(Exception ex)
            {
                return ret;
            }
        }

        public string GetPrenomSouscripteur(int IdSous, List<CSouscripteur> LCM)
        {
            string ret = string.Empty;
            try
            {
                var lret = LCM.FirstOrDefault(c => c.mId == IdSous);

                ret = lret.mPrenoms;

                return ret;
            }
            catch (Exception ex)
            {
                return ret;
            }
        }
        
        public string GetClasseMethoLibelle(int IdMetho,List<CClasseMetho> LCM)
        {
            string ret = string.Empty;
            try
            {
                var lret = LCM.FirstOrDefault(c => c.mId == IdMetho);

                ret = lret.mNomClasse;

                return ret;
            }
            catch(Exception ex)
            {
                return ret;
            }
        }

        
        //Classe Metho=================================================================

        //Ajouter Classe Metho

        public bool AddClasseMetho(CClasseMetho conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_ClasseMetho
                        (NomClasse,NomConducteur1,PrenomConducteur1,TelephoneConducteur1,EmailConducteur1,NomConducteur2,PrenomConducteur2,TelephoneConducteur2,EmailConducteur2,Quartier,IsDelete)
                        VALUES (@NomClasse,@NomConducteur1,@PrenomConducteur1,@TelephoneConducteur1,@EmailConducteur1,@NomConducteur2,@PrenomConducteur2,@TelephoneConducteur2,@EmailConducteur2,@Quartier,@IsDelete)";

                            command.Parameters.Add(new SqlParameter("NomClasse", conc.mNomClasse ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("NomConducteur1", conc.mNomConducteur1 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("PrenomConducteur1", conc.mPrenomConducteur1 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("TelephoneConducteur1", conc.mTelephoneConducteur1 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("EmailConducteur1", conc.mEmailConducteur1 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("NomConducteur2", conc.mNomConducteur2 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("PrenomConducteur2", conc.mPrenomConducteur2 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("TelephoneConducteur2", conc.mTelephoneConducteur2 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("EmailConducteur2", conc.mEmailConducteur2 ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Quartier", conc.mQuartier ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));
                            

                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddClasseMetho -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Modifier Classe Metho
        public bool UpdateClasseMetho(CClasseMetho conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_ClasseMetho SET 
                        NomClasse = @NomClasse, NomConducteur1 = @NomConducteur1, PrenomConducteur1 = @PrenomConducteur1,TelephoneConducteur1 = @TelephoneConducteur1, NomConducteur2 = @NomConducteur2, PrenomConducteur2 = @PrenomConducteur2, TelephoneConducteur2 = @TelephoneConducteur2, EmailConducteur2 = @EmailConducteur2 , Quartier= @Quartier  WHERE Id = @Id";

                        command.Parameters.Add(new SqlParameter("Id", conc.mId));
                        command.Parameters.Add(new SqlParameter("NomClasse", conc.mNomClasse ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("NomConducteur1", conc.mNomConducteur1 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("PrenomConducteur1", conc.mPrenomConducteur1 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("TelephoneConducteur1", conc.mTelephoneConducteur1 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("EmailConducteur1", conc.mEmailConducteur1 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("NomConducteur2", conc.mNomConducteur2));
                        command.Parameters.Add(new SqlParameter("PrenomConducteur2", conc.mPrenomConducteur2 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("TelephoneConducteur2", conc.mTelephoneConducteur2 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("EmailConducteur2", conc.mEmailConducteur2 ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Quartier", conc.mQuartier ?? string.Empty));
      
                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateClasseMetho -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                return retour;
            }
        }

        //Supprimer  Classe Metho
        public bool DeleteClasseMetho(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                           // command.CommandText = @"DELETE FROM FEC_ClasseMetho WHERE Id = @Id";
                            command.CommandText = @"UPDATE FEC_ClasseMetho SET IsDelete=1 WHERE Id = @Id";


                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteClasseMetho -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }
        
        //Obtenir un Classe Metho par son Id
        public CClasseMetho GetClasseMethoById(int Id, string chaineconnexion)
        {
            CClasseMetho EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_ClasseMetho  where Id = @Id AND IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CClasseMetho()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mNomClasse = reader["NomClasse"] == DBNull.Value ? string.Empty : reader["NomClasse"] as string,
                                        mNomConducteur1 = reader["NomConducteur1"] == DBNull.Value ? string.Empty : reader["NomConducteur1"] as string,
                                        mPrenomConducteur1 = reader["PrenomConducteur1"] == DBNull.Value ? string.Empty : reader["PrenomConducteur1"] as string,
                                        mTelephoneConducteur1 = reader["TelephoneConducteur1"] == DBNull.Value ? string.Empty : reader["TelephoneConducteur1"] as string,
                                        mEmailConducteur1 = reader["EmailConducteur1"] == DBNull.Value ? string.Empty : reader["EmailConducteur1"] as string,
                                        mNomConducteur2 = reader["NomConducteur2"] == DBNull.Value ? string.Empty : reader["NomConducteur2"] as string,
                                        mPrenomConducteur2 = reader["PrenomConducteur2"] == DBNull.Value ? string.Empty : reader["PrenomConducteur2"] as string,
                                        mTelephoneConducteur2 = reader["TelephoneConducteur2"] == DBNull.Value ? string.Empty : reader["TelephoneConducteur2"] as string,
                                        mEmailConducteur2 = reader["EmailConducteur2"] == DBNull.Value ? string.Empty : reader["EmailConducteur2"] as string,
                                        mQuartier = reader["Quartier"] == DBNull.Value ? string.Empty : reader["Quartier"] as string,
                                       
                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetClasseMethoById -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Obtenir liste des elements

        public List<CClasseMetho> GetAllClasseMetho(string chaineconnexion)
        {
            List<CClasseMetho> ListClientOp = new List<CClasseMetho>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_ClasseMetho Where  IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CClasseMetho()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mNomClasse = reader["NomClasse"] == DBNull.Value ? string.Empty : reader["NomClasse"] as string,
                                        mNomConducteur1 = reader["NomConducteur1"] == DBNull.Value ? string.Empty : reader["NomConducteur1"] as string,
                                        mPrenomConducteur1 = reader["PrenomConducteur1"] == DBNull.Value ? string.Empty : reader["PrenomConducteur1"] as string,
                                        mTelephoneConducteur1 = reader["TelephoneConducteur1"] == DBNull.Value ? string.Empty : reader["TelephoneConducteur1"] as string,
                                        mEmailConducteur1 = reader["EmailConducteur1"] == DBNull.Value ? string.Empty : reader["EmailConducteur1"] as string,
                                        mNomConducteur2 = reader["NomConducteur2"] == DBNull.Value ? string.Empty : reader["NomConducteur2"] as string,
                                        mPrenomConducteur2 = reader["PrenomConducteur2"] == DBNull.Value ? string.Empty : reader["PrenomConducteur2"] as string,
                                        mTelephoneConducteur2 = reader["TelephoneConducteur2"] == DBNull.Value ? string.Empty : reader["TelephoneConducteur2"] as string,
                                        mEmailConducteur2 = reader["EmailConducteur2"] == DBNull.Value ? string.Empty : reader["EmailConducteur2"] as string,
                                        mQuartier = reader["Quartier"] == DBNull.Value ? string.Empty : reader["Quartier"] as string,
                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllClasseMetho -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Profession==========================================================

        //Ajouter Profession

        public bool AddProfession(CProfession conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_Profession
                        (Libelle,IsDelete)
                        VALUES (@Libelle,@IsDelete)";

                            command.Parameters.Add(new SqlParameter("Libelle", conc.mLibelle ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));
                        
                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddProfession -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Modifier Profession
        public bool UpdateProfession(CProfession conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_Profession SET 
                        Libelle = @Libelle WHERE Id = @Id";

                        command.Parameters.Add(new SqlParameter("Id", conc.mId));
                        command.Parameters.Add(new SqlParameter("Libelle", conc.mLibelle ?? string.Empty));
                     

                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateProfession -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return retour;
            }
        }

        //Supprimer Profession
        public bool DeleteProfession(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"UPDATE FEC_Profession SET IsDelete=1 WHERE Id = @Id";
                           // command.CommandText = @"DELETE FROM FEC_Profession WHERE Id = @Id";

                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteProfession -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Obtenir Profession par son Id
        public CProfession GetProfessionById(int Id, string chaineconnexion)
        {
            CProfession EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_Profession  where Id = @Id and IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CProfession()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mLibelle = reader["Libelle"] == DBNull.Value ? string.Empty : reader["Libelle"] as string,
                                    
                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetProfessionById -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Obtenir liste des elements Profession

        public List<CProfession> GetAllProfession(string chaineconnexion)
        {
            List<CProfession> ListClientOp = new List<CProfession>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Profession WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CProfession()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mLibelle = reader["Libelle"] == DBNull.Value ? string.Empty : reader["Libelle"] as string,
                                       
                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllClasseMetho -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        
        //Membre Souscripteur=========================================================

        #region Membre Souscripteur

        //Ajouter Membre Souscripteur

        public bool AddMembreSouscripteur(CMembreSouscripteur conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_MembreSouscripteur
                        (IdSouscripteur, NomMembre, PrenomsMembre, Statut,Sexe,DateNaissance,LieuNaissance,Profession,Telephone,Cellulaire,Email,DateCreation,DateLastModif,UserCreation,UserLastModif,IdProfession,IsAdulteMembre,IsDelete)
                        VALUES (@IdSouscripteur, @NomMembre, @PrenomsMembre, @Statut,@Sexe,@DateNaissance,@LieuNaissance,@Profession,@Telephone,@Cellulaire,@Email,@DateCreation,@DateLastModif,@UserCreation,@UserLastModif,@IdProfession,@IsAdulteMembre,@IsDelete)";

                            command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                            command.Parameters.Add(new SqlParameter("NomMembre", conc.mNomMembre ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("PrenomsMembre", conc.mPrenomsMembre ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Statut", conc.mStatutFamilial ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Sexe", conc.mSexe ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateNaissance", conc.mDateNaissance));
                            command.Parameters.Add(new SqlParameter("LieuNaissance", conc.mLieuNaissance ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Profession", conc.mProfession ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Telephone", conc.mTelephone ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Cellulaire", conc.mCellulaire ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Email", conc.mEmail ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                            command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                            command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("IdProfession", conc.mIdProfession));
                            command.Parameters.Add(new SqlParameter("IsAdulteMembre", conc.mIsAdulteMembre));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));

                            
                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddMembreSouscripteur -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Modifier Membre Souscripteur
        public bool UpdateMembreSouscripteur(CMembreSouscripteur conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_MembreSouscripteur SET 
                        IdSouscripteur = @IdSouscripteur, NomMembre = @NomMembre, PrenomsMembre = @PrenomsMembre,Statut = @Statut, Sexe = @Sexe, DateNaissance = @DateNaissance, LieuNaissance = @LieuNaissance, Profession = @Profession , Telephone= @Telephone, Cellulaire= @Cellulaire, Email= @Email, DateCreation= @DateCreation, DateLastModif= @DateLastModif, UserCreation= @UserCreation, UserLastModif= @UserLastModif,IdProfession=@IdProfession,IsAdulteMembre=@IsAdulteMembre WHERE Id = @Id";
                        
                        command.Parameters.Add(new SqlParameter("Id", conc.mId));
                        command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                        command.Parameters.Add(new SqlParameter("NomMembre", conc.mNomMembre ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("PrenomsMembre", conc.mPrenomsMembre ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Statut", conc.mStatutFamilial ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Sexe", conc.mSexe ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("DateNaissance", conc.mDateNaissance));
                        command.Parameters.Add(new SqlParameter("LieuNaissance", conc.mLieuNaissance ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Profession", conc.mProfession ?? string.Empty));
                    
                        command.Parameters.Add(new SqlParameter("Telephone", conc.mTelephone ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Cellulaire", conc.mCellulaire ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("Email", conc.mEmail ?? string.Empty));
                     
                        command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                        command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                        command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("IdProfession", conc.mIdProfession));
                        command.Parameters.Add(new SqlParameter("IsAdulteMembre", conc.mIsAdulteMembre));

                        
                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateMembreSouscripteur -> TypeErreur: " + ex.Message;
                // CAlias.Log(msg);
                return retour;
            }
        }

        //Supprimer Membre Souscripteur
        public bool DeleteMembreSouscripteur(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"UPDATE FEC_MembreSouscripteur SET IsDelete=1 WHERE Id = @Id";

                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteMembreSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }


        //Supprimer Membre Souscripteur
        public bool DeleteMembreSouscripteur(string req, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //var clientId = command.CreateParameter();
                            //clientId.ParameterName = "@Id";
                            //clientId.Value = Id;
                            //command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteMembreSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }


        //Obtenir un Membre Souscripteur par son Id
        public CMembreSouscripteur GetMembreSouscripteurById(int Id, string chaineconnexion)
        {
            CMembreSouscripteur EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_MembreSouscripteur  where Id = @Id and IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CMembreSouscripteur()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),

                                        mNomMembre = reader["NomMembre"] == DBNull.Value ? string.Empty : reader["NomMembre"] as string,
                                        mPrenomsMembre = reader["PrenomsMembre"] == DBNull.Value ? string.Empty : reader["PrenomsMembre"] as string,
                                        mStatutFamilial = reader["Statut"] == DBNull.Value ? string.Empty : reader["Statut"] as string,
                                        mSexe = reader["Sexe"] == DBNull.Value ? string.Empty : reader["Sexe"] as string,
                                        mLieuNaissance = reader["LieuNaissance"] == DBNull.Value ? string.Empty : reader["LieuNaissance"] as string,
                                        mProfession = reader["Profession"] == DBNull.Value ? string.Empty : reader["Profession"] as string,
                                        mTelephone = reader["Telephone"] == DBNull.Value ? string.Empty : reader["Telephone"] as string,
                                        mCellulaire = reader["Cellulaire"] == DBNull.Value ? string.Empty : reader["Cellulaire"] as string,
                                        mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mIsAdulteMembre = reader["IsAdulteMembre"] == DBNull.Value ? string.Empty : reader["IsAdulteMembre"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                        mDateNaissance = reader["DateNaissance"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateNaissance"].ToString()),
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),
                                        
                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetMembreSouscripteurById -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Obtenir liste des elements

        public List<CMembreSouscripteur> GetAllMembreSouscripteur(string chaineconnexion)
        {
            List<CMembreSouscripteur> ListClientOp = new List<CMembreSouscripteur>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_MembreSouscripteur WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CMembreSouscripteur()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),

                                        mNomMembre = reader["NomMembre"] == DBNull.Value ? string.Empty : reader["NomMembre"] as string,
                                        mPrenomsMembre = reader["PrenomsMembre"] == DBNull.Value ? string.Empty : reader["PrenomsMembre"] as string,
                                        mStatutFamilial = reader["Statut"] == DBNull.Value ? string.Empty : reader["Statut"] as string,
                                        mSexe = reader["Sexe"] == DBNull.Value ? string.Empty : reader["Sexe"] as string,
                                        mLieuNaissance = reader["LieuNaissance"] == DBNull.Value ? string.Empty : reader["LieuNaissance"] as string,
                                        mProfession = reader["Profession"] == DBNull.Value ? string.Empty : reader["Profession"] as string,
                                        mTelephone = reader["Telephone"] == DBNull.Value ? string.Empty : reader["Telephone"] as string,
                                        mCellulaire = reader["Cellulaire"] == DBNull.Value ? string.Empty : reader["Cellulaire"] as string,
                                        mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                    
                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                        mDateNaissance = reader["DateNaissance"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateNaissance"].ToString()),
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),
                                        mIsAdulteMembre = reader["IsAdulteMembre"] == DBNull.Value ? string.Empty : reader["IsAdulteMembre"] as string,

                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllMembreSouscripteur -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        #endregion

        // Cotisation Annuelle===========================================================================

        public bool AddCotisationAnnuelle(CCotisationAnnuelle conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_CotisationAnnuelle
                        (IdSouscripteur,Annee,MontantCotisation,UserCreation,UserLastModif,DateCreation,DateLastModif,IsDelete)
                        VALUES (@IdSouscripteur, @Annee, @MontantCotisation, @UserCreation,@UserLastModif,@DateCreation,@DateLastModif,@IsDelete)";
                            
                            command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                            command.Parameters.Add(new SqlParameter("Annee", conc.mAnnee));
                            command.Parameters.Add(new SqlParameter("MontantCotisation", conc.mMontantCotisation));
                            command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                            command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));
                       

                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddCotisationAnnuelle -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }
        
        //Modifier Cotisation
        public bool UpdateCotisationAnnuelle(CCotisationAnnuelle conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_CotisationAnnuelle SET 
                        IdSouscripteur = @IdSouscripteur, Annee = @Annee, MontantCotisation = @MontantCotisation, DateCreation= @DateCreation, DateLastModif= @DateLastModif, UserCreation= @UserCreation, UserLastModif= @UserLastModif WHERE Id = @Id";

                        command.Parameters.Add(new SqlParameter("Id", conc.mId));
                        command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                        command.Parameters.Add(new SqlParameter("Annee", conc.mAnnee));
                        command.Parameters.Add(new SqlParameter("MontantCotisation", conc.mMontantCotisation));
  
                        command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                        command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                        command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));


                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateCotisationAnnuelle -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                return retour;
            }
        }

        //Supprimer Cotisation
        public bool DeleteCotisationAnnuelle(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"UPDATE FEC_CotisationAnnuelle SET IsDelete=1 WHERE Id = @Id";
                         //   command.CommandText = @"DELETE FROM FEC_CotisationAnnuelle WHERE Id = @Id";

                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteCotisationAnnuelle -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }


        //Supprimer Cotisation
        public bool DeleteCotisationAnnuelle(string  requete, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = requete;
                            
                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteCotisationAnnuelle -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        
        //Obtenir Cotisation par son Id
        public CCotisationAnnuelle GetCotisationAnnuelleById(int Id, string chaineconnexion)
        {
            CCotisationAnnuelle EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_CotisationAnnuelle  where Id = @Id AND IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantCotisation"]),


                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                      
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),

                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetCotisationAnnuelleById -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        //Obtenir liste des elements

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleApercu(string chaineconnexion,List<CSouscripteur> ListSous)
        {
            List<CCotisationAnnuelle> ListClientOp = new List<CCotisationAnnuelle>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;
                

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_CotisationAnnuelle WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),


                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,
                                    
                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllCotisationAnnuelle -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelle(string chaineconnexion, List<CSouscripteur> ListSous)
        {
            List<CCotisationAnnuelle> ListClientOp = new List<CCotisationAnnuelle>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;
                
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_CotisationAnnuelle WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),


                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllCotisationAnnuelle -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        //Filtrer Aperçu Cotisation Annuelle

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleByAn(string chaineconnexion,string anneeDeb,string anneeFin,List<CSouscripteur> ListSous)
        {
            List<CCotisationAnnuelle> ListClientOp = new List<CCotisationAnnuelle>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_CotisationAnnuelle where Annee>=@anneeDeb and  Annee<=@anneeFin  AND IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            command.Parameters.Add(new SqlParameter("anneeDeb", anneeDeb));
                            command.Parameters.Add(new SqlParameter("anneeFin", anneeFin));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllCotisationAnnuellebyAN -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleByAnMontant(string chaineconnexion, string annee)
        {
            List<CCotisationAnnuelle> ListClientOp = new List<CCotisationAnnuelle>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_CotisationAnnuelle where Annee=@annee AND IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            command.Parameters.Add(new SqlParameter("annee", annee));
                         
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mAnnee = reader["Annee"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Annee"]),
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                       
                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllCotisationAnnuellebyAN -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        //CVersement===================================================================================

        public bool AddVersement(CVersement conc, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"INSERT INTO FEC_Versement
                        (IdSouscripteur,NumeroRecu,MontantVersement,DateVersement,NomReceveur,UserCreation,UserLastModif,DateCreation,DateLastModif,IsDelete)
                        VALUES (@IdSouscripteur, @NumeroRecu, @MontantVersement,@DateVersement,@NomReceveur, @UserCreation,@UserLastModif,@DateCreation,@DateLastModif,@IsDelete)";
                            

                            command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                            command.Parameters.Add(new SqlParameter("NumeroRecu", conc.mNumeroRecu ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("NomReceveur", conc.mNomReceveur ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("MontantVersement", conc.mMontantVersement));
                            command.Parameters.Add(new SqlParameter("DateVersement", conc.mDateVersement));
                            command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                            command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));


                            command.ExecuteNonQuery();

                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;

            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> AddVersement -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Modifier Versement
        public bool UpdateVersement(CVersement conc, string chaineconnexion)
        {
            bool retour = false;
            if (conc == null) throw new ArgumentNullException(nameof(conc));

            var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

            try
            {
                using (var mConnection = mProvider.CreateConnection())
                {
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        command.CommandText =
                            @"UPDATE FEC_Versement SET 
                        IdSouscripteur = @IdSouscripteur,NumeroRecu = @NumeroRecu,MontantVersement = @MontantVersement, DateVersement = @DateVersement, NomReceveur = @NomReceveur, DateCreation= @DateCreation, DateLastModif= @DateLastModif, UserCreation= @UserCreation, UserLastModif= @UserLastModif WHERE Id = @Id";
                        

                        command.Parameters.Add(new SqlParameter("Id", conc.mId));
                        command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                        command.Parameters.Add(new SqlParameter("NumeroRecu", conc.mNumeroRecu ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("MontantVersement", conc.mMontantVersement));
                        command.Parameters.Add(new SqlParameter("DateVersement", conc.mDateVersement));
                        command.Parameters.Add(new SqlParameter("NomReceveur", conc.mNomReceveur ?? string.Empty));

                        command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                        command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                        command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                        command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));


                        command.ExecuteNonQuery();
                        retour = true;
                        command.Connection.Close();

                        return retour;

                    }
                }
            }
            catch (Exception ex)
            {
                retour = false;
                var msg = "DAOFimeco -> UpdateVersement -> TypeErreur: " + ex.Message;
                 CAlias.Log(msg);
                return retour;
            }
        }

        //Supprimer Versement
        public bool DeleteVersement(int Id, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"UPDATE FEC_Versement SET IsDelete=1 WHERE Id = @Id";
                          //  command.CommandText = @"DELETE FROM FEC_Versement WHERE Id = @Id";

                            var clientId = command.CreateParameter();
                            clientId.ParameterName = "@Id";
                            clientId.Value = Id;
                            command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Supprimer Versement
        public bool DeleteVersementChaine(string req, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText =req;

                            //var clientId = command.CreateParameter();
                            //clientId.ParameterName = "@Id";
                            //clientId.Value = Id;
                            //command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> DeleteVersementChaine -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }

        //Execute Requete========================

        public bool ExecuteRequete(string req, string chaineconnexion)
        {
            bool retour = false;
            try
            {
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

                using (var mConnection = mProvider.CreateConnection())
                {
                    //  if (mConnection == null) return;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();
                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //var clientId = command.CreateParameter();
                            //clientId.ParameterName = "@Id";
                            //clientId.Value = Id;
                            //command.Parameters.Add(clientId);

                            command.ExecuteNonQuery();
                            retour = true;
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

                return retour;
            }
            catch (Exception ex)
            {
                retour = false;

                var msg = "DAOFimeco -> ExecuteRequete -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
            }

        }
        
        //Obtenir Versement par son Id
        public CVersement GetVersementById(int Id, string chaineconnexion,List<CSouscripteur> ListSous)
        {
            CVersement EltConcurrent = null;
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())

                {

                    req = @" select * " +
                    "  from FEC_Versement  where Id = @Id and IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;

                            var EltConcurrentId = command.CreateParameter();
                            EltConcurrentId.ParameterName = "@Id";
                            EltConcurrentId.Value = Id;
                            command.Parameters.Add(EltConcurrentId);

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    EltConcurrent = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,

                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),

                                    };

                                }

                                return EltConcurrent;
                            }
                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetVersementById -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Obtenir liste des elements

        public List<CVersement> GetAllVersement(string chaineconnexion,List<CSouscripteur>ListSous)
        {
            List<CVersement> ListClientOp = new List<CVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Versement WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),

                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Pour la suppression
        public List<CVersement> GetAllVersement(string chaineconnexion)
        {
            List<CVersement> ListClientOp = new List<CVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Versement WHERE IsDelete=0  ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                       
                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //Aperçu

        public List<CVersement> GetAllVersementApercu(string chaineconnexion, string anneeDeb, string anneeFin, List<CSouscripteur> ListSous,bool IsTousSous,string NomDeb,string NomFin ,bool IsMultipleSous,string ListMultipleId)
        {
            List<CVersement> ListClientOp = new List<CVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;
                
                string FiltreSous = string.Empty;

                //////////////////////FILTRES SOUSCRIPTEURS//////////////////////////////////
                if (IsMultipleSous)//Multiple sousc
                {
                    if (ListMultipleId != string.Empty && ListMultipleId!=null)
                    {
                        //filtre que sur les NOMS
                        ListMultipleId = ListMultipleId.Replace(",", "','");

                        FiltreSous = " and IdSouscripteur in ('" + ListMultipleId + "') ";

                    }

                }
                else
                {//DE_A
                    if (!IsTousSous)
                    {
                        if (NomDeb != string.Empty && NomFin != string.Empty)
                        {
                            FiltreSous = " and Nom>='" + NomDeb + "' and Nom <='" + NomFin + "'";
                        }

                    }

                }
                
                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Versement V left join FEC_Souscripteur S on S.Id=V.IdSouscripteur where DateVersement>=@anneeDeb and  DateVersement<=@anneeFin AND S.IsDelete=0 "+ FiltreSous;

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;
                            command.Parameters.Add(new SqlParameter("anneeDeb", anneeDeb));
                            command.Parameters.Add(new SqlParameter("anneeFin", anneeFin));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),

                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        public List<CVersement> GetAllVersementApercuMontant(string chaineconnexion, string annee)
        {
            List<CVersement> ListClientOp = new List<CVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;
                

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @" select * " +
                    "  from FEC_Versement V left join FEC_Souscripteur S on S.Id=V.IdSouscripteur where YEAR(DateVersement)=@annee  AND S.IsDelete=0 and V.IsDelete=0 ";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;
                            command.Parameters.Add(new SqlParameter("annee", annee));
                        

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                       
                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),

                                        mUserCreation = reader["UserCreation"] == DBNull.Value ? string.Empty : reader["UserCreation"] as string,
                                        mUserLastModif = reader["UserLastModif"] == DBNull.Value ? string.Empty : reader["UserLastModif"] as string,

                                        mDateCreation = reader["DateCreation"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateCreation"].ToString()),
                                        mDateLastModif = reader["DateLastModif"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateLastModif"].ToString()),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetAllVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }



        //Liste des Souscripteurs et membres

        //Obtenir liste des elements Profession

        public List<CEtatSouscriptMembre> GetEtatSouscripteurMembre(string chaineconnexion)
        {
            List<CEtatSouscriptMembre> ListClientOp = new List<CEtatSouscriptMembre>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  select S.Nom ,S.Prenoms ,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre from FEC_Souscripteur S
                            left join FEC_MembreSouscripteur M 
                            on S.Id=M.IdSouscripteur WHERE S.isdelete=0 
                            group by S.Nom ,S.Prenoms,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre";

                    if (mConnection == null) return null;
                    mConnection.ConnectionString = chaineconnexion;
                    mConnection.Open();

                    using (var command = mConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = req;

                            //limite du temps de reponse 5 minute
                            command.CommandTimeout = 300;


                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CEtatSouscriptMembre()
                                    {
                                       // mIdSouscripteur = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mNomPersonne = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenomsPersonne = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mStatutAgeSous = reader["IsAdulte"] == DBNull.Value ? string.Empty : reader["IsAdulte"] as string,

                                        mNomMembre = reader["NomMembre"] == DBNull.Value ? string.Empty : reader["NomMembre"] as string,
                                        mPrenomsMembre = reader["PrenomsMembre"] == DBNull.Value ? string.Empty : reader["PrenomsMembre"] as string,
                                        mStatutAgeMembre = reader["IsAdulteMembre"] == DBNull.Value ? string.Empty : reader["IsAdulteMembre"] as string,

                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                return ListClientOp;
                            }

                        }
                        finally
                        {
                            mConnection.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = "DAOFimeco -> GetEtatSouscripteurMembre -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }




    }
}
