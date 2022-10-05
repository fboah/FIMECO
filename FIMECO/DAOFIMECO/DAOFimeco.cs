using FIMECO.Models;
using FIMECO.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIMECO.DAOFIMECO
{
   public class DAOFimeco
    {
        private IDbConnection mConnection;
        private readonly DbProviderFactory mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");

        private string Appli = "FIMECO";

        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";




        #region Cryptage_Decryptage

        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public string EncryptOLD(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            // string key = "DUBOAH87";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string DecryptOLD(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue("SecurityKey",typeof(String));
            string key = "DUBOAH87";
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion


        #region Profil


        public List<CProfil> getProfils(string Chaineconnex)
        {
            var listPays = new List<CProfil>();
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return listPays;
                mConnection.ConnectionString = Chaineconnex;
                mConnection.Open();

                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"SELECT * from FEC_Profil";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pays = new CProfil
                                {
                                    mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    mDescription = reader["Libelle"] == DBNull.Value ? string.Empty : reader["Libelle"] as string,

                                };

                                listPays.Add(pays);
                            }
                            return listPays;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> getProfils-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return listPays;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        #endregion

        #region UserProfil


        public List<CUserProfil> getUserProfil(string Chaineconnex)
        {
            var listPays = new List<CUserProfil>();
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return listPays;
                mConnection.ConnectionString = Chaineconnex;
                mConnection.Open();

                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"SELECT * from FEC_UserProfil";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pays = new CUserProfil
                                {
                                    mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    mIdUser = reader["IdUser"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdUser"]),
                                    mIdProfil = reader["IdProfil"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfil"]),

                                };

                                listPays.Add(pays);
                            }
                            return listPays;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> getUserProfil-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return listPays;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        public bool deleteUserProfil(int IdUser, string chaineconx)
        {
            bool res = false;
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return res;
                mConnection.ConnectionString = chaineconx;
                mConnection.Open();
                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"DELETE FROM FEC_UserProfil WHERE IdUser = @IdUser";

                        var licenceId = command.CreateParameter();
                        licenceId.ParameterName = "@IdUser";
                        licenceId.Value = IdUser;
                        command.Parameters.Add(licenceId);

                        command.ExecuteNonQuery();
                        res = true;

                        return res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> deleteUserProfil-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return res;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        public bool addUserProfil(int IdUser, List<int> ListeProfils, string ChaineConx)
        {
            bool res = false;

            using (mConnection = mProvider.CreateConnection())
            {
                mConnection.ConnectionString = ChaineConx;
                mConnection.Open();
                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        foreach (var item in ListeProfils)
                        {
                            command.CommandText += @"INSERT INTO FEC_UserProfil
                        (IdUser, IdProfil)
                        VALUES (" + IdUser + ", " + item.ToString() + ")";

                        }

                        command.ExecuteNonQuery();
                        res = true;

                        return res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> addUserProfil-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return res;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        #endregion

        #region UserProfilData

        public List<CUserProfilData> getUserProfilData(string Chaineconnex)
        {
            var listPays = new List<CUserProfilData>();
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return listPays;
                mConnection.ConnectionString = Chaineconnex;
                mConnection.Open();

                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"select U.Id,U.Nom,U.Login,U.Password,U.Email,P.Id as IdProfil,P.Libelle,UP.Id as IdUserProfil from FEC_User U 
                                                left join FEC_UserProfil UP on U.Id=UP.IdUser
                                                left join FEC_Profil P on UP.IdProfil=P.Id  where U.IsDelete=0";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pays = new CUserProfilData
                                {
                                    mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                    mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    mLogin = reader["Login"] == DBNull.Value ? string.Empty : reader["Login"] as string,
                                    mPassword = reader["Password"] == DBNull.Value ? string.Empty : reader["Password"] as string,
                                    mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                    mIdProfil = reader["IdProfil"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfil"]),
                                    mIdUserProfil = reader["IdUserProfil"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdUserProfil"]),
                                    //  mIdFonction = reader["IdFonction"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdFonction"]),
                                    mDescription = reader["Libelle"] == DBNull.Value ? string.Empty : reader["Libelle"] as string,

                                };

                                listPays.Add(pays);
                            }
                            return listPays;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> getUserProfilData-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return listPays;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        #endregion

        #region User


        public List<CUser> getAllUsers(string Chaineconnex)
        {
            var listPays = new List<CUser>();
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return listPays;
                mConnection.ConnectionString = Chaineconnex;
                mConnection.Open();

                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"SELECT * from FEC_User Where IsDelete=0 ";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pays = new CUser
                                {
                                    mNom = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                    mLogin = reader["Login"] == DBNull.Value ? string.Empty : reader["Login"] as string,
                                    mPassword = reader["Password"] == DBNull.Value ? string.Empty : reader["Password"] as string,
                                    mEmail = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"] as string,
                                    mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                    mIsDelete = reader["IsDelete"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IsDelete"]),
                                    //   mIdFonction = reader["IdFonction"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdFonction"]),

                                };

                                listPays.Add(pays);
                            }
                            return listPays;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> getAllUsers-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);

                        return listPays;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        public bool addUser(CUser client, string ChaineConx)
        {
            bool res = false;

            using (mConnection = mProvider.CreateConnection())
            {
                mConnection.ConnectionString = ChaineConx;
                mConnection.Open();
                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"INSERT INTO FEC_User
                        (Nom, Login,Password,Email,IsDelete)
                        VALUES (@Nom, @Login,@Password,@Email,@IsDelete)";

                        command.Parameters.Add(new SqlParameter("Nom", client.mNom ?? ""));
                        command.Parameters.Add(new SqlParameter("Login", client.mLogin ?? ""));
                        command.Parameters.Add(new SqlParameter("Password", client.mPassword ?? ""));
                        command.Parameters.Add(new SqlParameter("Email", client.mEmail ?? ""));
                        command.Parameters.Add(new SqlParameter("IsDelete", client.mIsDelete));

                        command.ExecuteNonQuery();
                        res = true;

                        return res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> addUser-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return res;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        public bool updateUser(CUser client, string chaineconx)
        {
            bool res = false;

            using (mConnection = mProvider.CreateConnection())
            {
                mConnection.ConnectionString = chaineconx;
                mConnection.Open();
                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"UPDATE FEC_User SET 
                        Nom = @Nom,Login=@Login, Password = @Password,Email=@Email WHERE Id = @Id";

                        command.Parameters.Add(new SqlParameter("Id", client.mId));
                        command.Parameters.Add(new SqlParameter("Nom", client.mNom ?? ""));
                        command.Parameters.Add(new SqlParameter("Login", client.mLogin ?? ""));
                        command.Parameters.Add(new SqlParameter("Password", client.mPassword ?? ""));
                        command.Parameters.Add(new SqlParameter("Email", client.mEmail ?? ""));
                        // command.Parameters.Add(new SqlParameter("IdFonction", client.mIdFonction));

                        command.ExecuteNonQuery();
                        res = true;

                        return res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> updateUser-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return res;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        public bool deleteUser(int Id, string chaineconx)
        {
            bool res = false;
            using (mConnection = mProvider.CreateConnection())
            {
                if (mConnection == null) return res;
                mConnection.ConnectionString = chaineconx;
                mConnection.Open();
                using (var command = mConnection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"UPDATE FEC_User  SET 
                        IsDelete = 1 WHERE Id = @Id";

                        var licenceId = command.CreateParameter();
                        licenceId.ParameterName = "@Id";
                        licenceId.Value = Id;
                        command.Parameters.Add(licenceId);

                        command.ExecuteNonQuery();
                        res = true;

                        return res;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", Appli, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var msg = "DAOFimeco -> deleteUser-> TypeErreur: " + ex.Message;
                        CAlias.Log(msg);
                        return res;
                    }
                    finally
                    {
                        mConnection.Close();
                    }
                }
            }
        }

        #endregion


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
                    "  from FEC_Souscripteur WHERE IsDelete=0  order by Nom ";

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


        public List<CSouscripteur> GetAllSouscripteurFILTRE(string chaineconnexion,List<CClasseMetho> ListeCMetho, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession)
        {
            List<CSouscripteur> ListClientOp = new List<CSouscripteur>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;
                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and IdProfession in ('" + ListIdProfession + "') ";
                    }

                }

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    if(FiltreSous!=string.Empty || FiltreClasseMetho!=string.Empty || FiltreProfession!=string.Empty)
                    {
                        req = @" select * " +
                   "  from FEC_Souscripteur WHERE IsDelete=0  "+FiltreSous +FiltreProfession+ FiltreClasseMetho;
                    }
                    else
                    {
                        if (FiltreSous == string.Empty && FiltreClasseMetho == string.Empty && FiltreProfession == string.Empty)
                        {
                            req = @" select * " +
                                "  from FEC_Souscripteur WHERE IsDelete=0  ";
                        }
                    
                    }

                   

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

        public List<CArriereSouscripteur> GetAllArriereApercu(string chaineconnexion,string AnObjectif,bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous,string PrenomMultiSous,bool IsTousClasseMetho,string ListIdClasse, bool IsTousProfession, string ListIdProfession, string idAppli)
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
                            where S.IsDelete=0 AND V.IsDelete=0 and C.IsDelete=0 AND YEAR(DateVersement)<=" + AnObjectif + " and Annee<="+ AnObjectif + " and C.Type_Gestion=" + idAppli + " and V.Type_Gestion=" + idAppli + " and YEAR(DateVersement)=Annee or (C.Annee=" + AnObjectif + " and C.Type_Gestion = " + idAppli + " and V.Type_Gestion=" + idAppli + ") " + FiltreSous+ FiltreProfession+FiltreClasseMetho+
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

        //TRACABILITE==================================================================

        //Ajouter Trace

        public bool AddTrace(CTracabilite conc, string chaineconnexion)
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
                            command.CommandText = @"INSERT INTO FEC_Tracabilite
                        (TypeOperation, MachineAction, Contenu,DateAction)
                        VALUES (@TypeOperation, @MachineAction, @Contenu,@DateAction)";

                            command.Parameters.Add(new SqlParameter("TypeOperation", conc.mTypeOperation ?? string.Empty));
                            //command.Parameters.Add(new SqlParameter("Login", conc.mLogin ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("MachineAction", conc.mMachineAction ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("Contenu", conc.mContenu ?? string.Empty));
                           
                            command.Parameters.Add(new SqlParameter("DateAction", conc.mDateAction));
                     
                            
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

                var msg = "DAOFimeco -> AddTrace -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                //  MessageBox.Show("Une erreur est survenue! Veuillez contacter votre Administrateur!", "FORECASTCOM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return retour;
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
                    "  from FEC_ClasseMetho Where  IsDelete=0  order by NomClasse ";

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
                    "  from FEC_Profession WHERE IsDelete=0  order by Libelle ";

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
                var msg = "DAOFimeco -> GetAllProfession -> TypeErreur: " + ex.Message;
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
                        (IdSouscripteur,Annee,MontantCotisation,UserCreation,UserLastModif,DateCreation,DateLastModif,IsDelete,Type_Gestion)
                        VALUES (@IdSouscripteur, @Annee, @MontantCotisation, @UserCreation,@UserLastModif,@DateCreation,@DateLastModif,@IsDelete,@Type_Gestion)";
                            
                            command.Parameters.Add(new SqlParameter("IdSouscripteur", conc.mIdSouscripteur));
                            command.Parameters.Add(new SqlParameter("Annee", conc.mAnnee));
                            command.Parameters.Add(new SqlParameter("MontantCotisation", conc.mMontantCotisation));
                            command.Parameters.Add(new SqlParameter("UserCreation", conc.mUserCreation ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("UserLastModif", conc.mUserLastModif ?? string.Empty));
                            command.Parameters.Add(new SqlParameter("DateCreation", conc.mDateCreation));
                            command.Parameters.Add(new SqlParameter("DateLastModif", conc.mDateLastModif));
                            command.Parameters.Add(new SqlParameter("IsDelete", conc.mIsDelete));
                            command.Parameters.Add(new SqlParameter("Type_Gestion", conc.mIdTypeAppli));
                       

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

                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),
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

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleApercu(string chaineconnexion,List<CSouscripteur> ListSous,string idTypeAppli)
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
                    "  from FEC_CotisationAnnuelle WHERE IsDelete=0 and  Type_Gestion=@idTypeAppli  ";

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


                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CCotisationAnnuelle()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),
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
        

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelle(string chaineconnexion, List<CSouscripteur> ListSous,string idTypeAppli)
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
                    "  from FEC_CotisationAnnuelle WHERE IsDelete=0 and  Type_Gestion=@idTypeAppli ";

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

                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));
                            
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
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),

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

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleByAn(string chaineconnexion,string anneeDeb,string anneeFin,List<CSouscripteur> ListSous,string idTypeAppli)
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
                    "  from FEC_CotisationAnnuelle where Annee>=@anneeDeb and  Annee<=@anneeFin  AND IsDelete=0 and  Type_Gestion=@idTypeAppli ";

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
                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));

                            

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
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),
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
                var msg = "DAOFimeco -> GetAllCotisationAnnuelleByAn -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        public List<CCotisationAnnuelle> GetAllCotisationAnnuelleByAnMontant(string chaineconnexion, string annee, string idTypeAppli)
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
                    "  from FEC_CotisationAnnuelle where Annee=@annee AND IsDelete=0 and  Type_Gestion=@idTypeAppli ";

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

                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));

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
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),

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
                var msg = "DAOFimeco -> GetAllCotisationAnnuelleByAnMontant -> TypeErreur: " + ex.Message;
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
                        (IdSouscripteur,NumeroRecu,MontantVersement,DateVersement,NomReceveur,UserCreation,UserLastModif,DateCreation,DateLastModif,IsDelete,IdReceveur,Type_Gestion)
                        VALUES (@IdSouscripteur, @NumeroRecu, @MontantVersement,@DateVersement,@NomReceveur, @UserCreation,@UserLastModif,@DateCreation,@DateLastModif,@IsDelete,@IdReceveur,@Type_Gestion)";
                            

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
                            command.Parameters.Add(new SqlParameter("IdReceveur", conc.mIdReceveur));
                            command.Parameters.Add(new SqlParameter("Type_Gestion", conc.mIdTypeAppli));

                            

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
                        IdSouscripteur = @IdSouscripteur,NumeroRecu = @NumeroRecu,MontantVersement = @MontantVersement, DateVersement = @DateVersement, NomReceveur = @NomReceveur, DateCreation= @DateCreation, DateLastModif= @DateLastModif, UserCreation= @UserCreation, UserLastModif= @UserLastModif,IdReceveur=@IdReceveur WHERE Id = @Id";
                        

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
                        command.Parameters.Add(new SqlParameter("IdReceveur", conc.mIdReceveur));
                        

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
                                        mIdReceveur = reader["IdReceveur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdReceveur"]),
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

        public List<CVersement> GetAllVersement(string chaineconnexion,List<CSouscripteur>ListSous,string idTypeAppli)
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
                    "  from FEC_Versement WHERE IsDelete=0 and  Type_Gestion=@idTypeAppli ";

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


                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdReceveur = reader["IdReceveur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdReceveur"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                        mNomReceveur = reader["NomReceveur"] == DBNull.Value ? string.Empty : reader["NomReceveur"] as string,
                                        mNumeroRecu = reader["NumeroRecu"] == DBNull.Value ? string.Empty : reader["NumeroRecu"] as string,
                                        mNom = GetNomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mPrenoms = GetPrenomSouscripteur(Convert.ToInt32(reader["IdSouscripteur"]), ListSous),
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),
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
                                        mIdReceveur = reader["IdReceveur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdReceveur"]),
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
                    "  from FEC_Versement V left join FEC_Souscripteur S on S.Id=V.IdSouscripteur where DateVersement>=@anneeDeb and  DateVersement<=@anneeFin AND V.IsDelete=0 "+ FiltreSous;

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
                var msg = "DAOFimeco -> GetAllVersementApercu -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        public List<CVersement> GetAllVersementApercuMontant(string chaineconnexion, string annee,string idTypeAppli)
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
                    "  from FEC_Versement V left join FEC_Souscripteur S on S.Id=V.IdSouscripteur where YEAR(DateVersement)=@annee  AND S.IsDelete=0 and V.IsDelete=0 and V.Type_Gestion=@idTypeAppli ";

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
                            command.Parameters.Add(new SqlParameter("idTypeAppli", idTypeAppli));
                        

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mIdTypeAppli = reader["Type_Gestion"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Type_Gestion"]),
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
                var msg = "DAOFimeco -> GetAllVersementApercuMontant -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }

        //============================ETAT==========================================

        //Liste des Souscripteurs et membres

        //Obtenir liste des elements Profession
       
        public List<CEtatSouscriptMembre> GetEtatSouscripteurMembre(string chaineconnexion, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession)
        {
            List<CEtatSouscriptMembre> ListClientOp = new List<CEtatSouscriptMembre>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }

                }


                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  select CM.NomClasse,S.Nom ,S.Prenoms ,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession from FEC_Souscripteur S
                                left join FEC_MembreSouscripteur M on S.Id=M.IdSouscripteur
                                left join FEC_ClasseMetho CM on Cm.Id=S.IdClasseMetho
                                 WHERE S.isdelete=0 "+ FiltreSous +FiltreProfession+FiltreClasseMetho+
                                    " group by NomClasse,S.Nom ,S.Prenoms ,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession "+
                                   " order by NomClasse,S.Nom ,S.Prenoms ,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession";
                    //group by CM.NomClasse,S.Nom ,S.Prenoms,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre
                    //order by CM.NomClasse,S.Nom ,S.Prenoms,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre";


                    //req = @"  select S.Nom ,S.Prenoms ,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre from FEC_Souscripteur S
                    //        left join FEC_MembreSouscripteur M 
                    //        on S.Id=M.IdSouscripteur WHERE S.isdelete=0 
                    //        group by S.Nom ,S.Prenoms,S.IsAdulte,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre";


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
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mClasse = reader["NomClasse"] == DBNull.Value ? string.Empty : reader["NomClasse"] as string,

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


        //Liste Souscripteurs Montant Total par classe

        public List<CEtatSouscriptClasseMontant> GetEtatSouscripteurMembreClasseVersement1(string chaineconnexion, string AnObjectif, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession)
        {
            List<CEtatSouscriptClasseMontant> ListClientOp = new List<CEtatSouscriptClasseMontant>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }

                }


                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  select  A.IdClasseMetho,A.Id,A.NomClasse,A.Nom,A.Prenoms,A.MontantCotisation,A.IdMembre,A.NomMembre,A.PrenomsMembre,B.DateVersement,B.MontantVersement,A.IdProfession from 
                                  (  (
                                    select CM.NomClasse,S.Id,S.Nom ,S.Prenoms ,S.IsAdulte,CA.MontantCotisation,M.Id as IdMembre,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession from FEC_Souscripteur S
                                    left join FEC_MembreSouscripteur M on S.Id=M.IdSouscripteur
                                    left join FEC_ClasseMetho CM on Cm.Id=S.IdClasseMetho
                                    left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                                    WHERE S.isdelete=0 and CA.Annee=" + AnObjectif + " " + FiltreSous + FiltreClasseMetho + FiltreProfession +
                               " )A " +
                                " left join " +
                                "  ( " +
                               "  select V.IdSouscripteur,V.DateVersement,V.MontantVersement from FEC_Versement V " +
                               "  where V.IsDelete=0 AND YEAR(DateVersement)=" + AnObjectif + " " +
                               "  )B on A.Id=B.IdSouscripteur ) " +

                               "  group by A.IdClasseMetho,A.NomClasse,A.Id,A.Nom,A.Prenoms,A.MontantCotisation,A.IdMembre,A.NomMembre,A.PrenomsMembre,B.DateVersement,B.MontantVersement,A.IdProfession " +
                                " order by A.IdClasseMetho,A.NomClasse,A.Id,A.Nom,A.Prenoms,A.MontantCotisation,A.IdMembre,A.NomMembre,A.PrenomsMembre,B.DateVersement,B.MontantVersement,A.IdProfession ";

                 

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
                                    var ClientOp = new CEtatSouscriptClasseMontant()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdMembre = reader["IdMembre"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdMembre"]),
                                        mNomClasse = reader["NomClasse"] == DBNull.Value ? string.Empty : reader["NomClasse"] as string,
                                        mNomPersonne = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenomsPersonne = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),

                                        mNomMembre = reader["NomMembre"] == DBNull.Value ? string.Empty : reader["NomMembre"] as string,
                                        mPrenomsMembre = reader["PrenomsMembre"] == DBNull.Value ? string.Empty : reader["PrenomsMembre"] as string,

                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantVersement"]),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                var LIdClasse = ListClientOp.Select(z => z.mIdClasseMetho).Distinct();

                                List<CEtatSouscriptClasseMontant> ListFinale = new List<CEtatSouscriptClasseMontant>();

                                foreach (var it in LIdClasse)
                                {
                                    var Lret = ListClientOp.Where(c => c.mIdClasseMetho == it).ToList();

                                    if(Lret.Count>0)
                                    {
                                        long MontantSouscriptionClasse = 0;

                                        long MontantVersementClasse = 0;

                                        var LIdSouscriptClasse = Lret.Select(z => z.mId).Distinct();

                                        //Ramener le montant total de souscription annuelle

                                        foreach(var elt in LIdSouscriptClasse)
                                        {
                                            var Cret = Lret.FirstOrDefault(c => c.mId == elt);

                                            MontantSouscriptionClasse += Cret.mMontantCotisation;
                                        }

                                        foreach(var obj in Lret)
                                        {
                                            MontantVersementClasse += obj.mMontantVersement;
                                        }


                                        foreach(var kkb in Lret)
                                        {
                                            kkb.mMontantTotalSouscrit = MontantSouscriptionClasse;
                                            kkb.mMontantTotalVerse = MontantVersementClasse;

                                        }


                                        ListFinale.AddRange(Lret);
                                    }

                                }


                                List<CEtatSouscriptClasseMontant> LToReturn = new List<CEtatSouscriptClasseMontant>();
                                
                                foreach (var it in LIdClasse)
                                {
                                    var ListeOPClasse = ListFinale.Where(c => c.mIdClasseMetho == it).ToList();
                                    
                                    var LSouscripteurClasse = ListeOPClasse.Select(z => z.mId).Distinct();

                                  //Liste des membres par souscripteur et par Classe

                                    foreach(var d in LSouscripteurClasse)
                                    {
                                        var LMembre = ListeOPClasse.Where(x => x.mId == d).ToList();

                                        var LMembreClasse= LMembre.Select(z => z.mIdMembre).Distinct();

                                        foreach (var i in LMembreClasse)
                                        {
                                            var CExist = ListFinale.FirstOrDefault(c => c.mIdClasseMetho == it && c.mId == d && c.mIdMembre == i);

                                            bool isExist = LToReturn.Exists(c => c.mIdClasseMetho == CExist.mIdClasseMetho && c.mId == CExist.mId && c.mIdMembre == CExist.mIdMembre);

                                            if (!isExist) LToReturn.Add(CExist);

                                        }
                                    }


                                }


                                    return LToReturn;
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
                var msg = "DAOFimeco -> GetEtatSouscripteurMembreClasseVersement1 -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }
        
        public List<CEtatSouscriptClasseMontant> GetEtatSouscripteurMembreClasseVersement(string chaineconnexion, string AnObjectif, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession,string idAppli)
        {
            List<CEtatSouscriptClasseMontant> ListClientOp = new List<CEtatSouscriptClasseMontant>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

                #region Filtre

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }

                }


                #endregion

                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"   select CM.NomClasse,S.Id,S.Nom ,S.Prenoms ,S.IsAdulte,CA.MontantCotisation,M.Id as IdMembre,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession from FEC_Souscripteur S
                                    left join FEC_MembreSouscripteur M on S.Id=M.IdSouscripteur
                                    left join FEC_ClasseMetho CM on Cm.Id=S.IdClasseMetho
                                    left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                                    WHERE S.isdelete=0 and CA.Type_Gestion=@idAppli and CA.Annee=" + AnObjectif + " " + FiltreSous + FiltreClasseMetho + FiltreProfession +
                               "  group  by CM.NomClasse,S.Id,S.Nom ,S.Prenoms ,S.IsAdulte,CA.MontantCotisation, M.Id,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession " +
                                " order  by CM.NomClasse,S.Id,S.Nom ,S.Prenoms ,S.IsAdulte,CA.MontantCotisation, M.Id,M.NomMembre ,M.PrenomsMembre ,M.IsAdulteMembre,S.IdClasseMetho,S.IdProfession ";
                    

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

                            command.Parameters.Add(new SqlParameter("idAppli", idAppli));



                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CEtatSouscriptClasseMontant()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mIdMembre = reader["IdMembre"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdMembre"]),
                                        mNomClasse = reader["NomClasse"] == DBNull.Value ? string.Empty : reader["NomClasse"] as string,
                                        mNomPersonne = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenomsPersonne = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),

                                        mNomMembre = reader["NomMembre"] == DBNull.Value ? string.Empty : reader["NomMembre"] as string,
                                        mPrenomsMembre = reader["PrenomsMembre"] == DBNull.Value ? string.Empty : reader["PrenomsMembre"] as string,

                                        mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                      //  mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),
                                       // mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantVersement"]),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }

                                var LIdClasse = ListClientOp.Select(z => z.mIdClasseMetho).Distinct();

                                List<CEtatSouscriptClasseMontant> ListFinale = new List<CEtatSouscriptClasseMontant>();

                                foreach (var it in LIdClasse)
                                {
                                    var Lret = ListClientOp.Where(c => c.mIdClasseMetho == it).ToList();

                                    if (Lret.Count > 0)
                                    {
                                        long MontantSouscriptionClasse = 0;

                                      //  long MontantVersementClasse = 0;

                                        var LIdSouscriptClasse = Lret.Select(z => z.mId).Distinct();

                                        //Ramener le montant total de souscription annuelle

                                        foreach (var elt in LIdSouscriptClasse)
                                        {
                                            var Cret = Lret.FirstOrDefault(c => c.mId == elt);

                                            MontantSouscriptionClasse += Cret.mMontantCotisation;
                                        }

                                        //foreach (var obj in Lret)
                                        //{
                                        //    MontantVersementClasse += obj.mMontantVersement;
                                        //}


                                        foreach (var kkb in Lret)
                                        {
                                            kkb.mMontantTotalSouscrit = MontantSouscriptionClasse;
                                         //   kkb.mMontantTotalVerse = MontantVersementClasse;

                                        }


                                        ListFinale.AddRange(Lret);
                                    }

                                }


                                //List<CEtatSouscriptClasseMontant> LToReturn = new List<CEtatSouscriptClasseMontant>();

                                //foreach (var it in LIdClasse)
                                //{
                                //    var ListeOPClasse = ListFinale.Where(c => c.mIdClasseMetho == it).ToList();

                                //    var LSouscripteurClasse = ListeOPClasse.Select(z => z.mId).Distinct();

                                //    //Liste des membres par souscripteur et par Classe

                                //    foreach (var d in LSouscripteurClasse)
                                //    {
                                //        var LMembre = ListeOPClasse.Where(x => x.mId == d).ToList();

                                //        var LMembreClasse = LMembre.Select(z => z.mIdMembre).Distinct();

                                //        foreach (var i in LMembreClasse)
                                //        {
                                //            var CExist = ListFinale.FirstOrDefault(c => c.mIdClasseMetho == it && c.mId == d && c.mIdMembre == i);

                                //            bool isExist = LToReturn.Exists(c => c.mIdClasseMetho == CExist.mIdClasseMetho && c.mId == CExist.mId && c.mIdMembre == CExist.mIdMembre);

                                //            if (!isExist) LToReturn.Add(CExist);

                                //        }
                                //    }


                                //}


                                return ListFinale;
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
                var msg = "DAOFimeco -> GetEtatSouscripteurMembreClasseVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }
        
        //Souscritpteurs et Versement

        public List<CEtatSouscriptVersement> GetEtatSouscripteurVersement(string chaineconnexion,string AnObjectif, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession,string idAppli)
        {
            List<CEtatSouscriptVersement> ListClientOp = new List<CEtatSouscriptVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }

                }


                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  Select A.Id,A.Nom,A.Prenoms,A.MontantCotisation,B.DateVersement,B.MontantVersement,A.IdClasseMetho,A.IdProfession from 
                                (
                                select S.Id,S.Nom ,S.Prenoms ,CA.MontantCotisation,S.IdClasseMetho,S.IdProfession from FEC_Souscripteur S
                                left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                                WHERE S.isdelete=0 AND CA.Type_Gestion=@idAppli  and CA.IsDelete=0 and CA.Annee=" + AnObjectif + " " + FiltreSous+FiltreClasseMetho+FiltreProfession+
                               " )A " +
                                " left join "+
                                "  ( "+
                               "  select V.IdSouscripteur,V.DateVersement,V.MontantVersement from FEC_Versement V "+
                               "  where V.IsDelete=0 AND V.Type_Gestion=@idAppli AND YEAR(DateVersement)=" + AnObjectif + " " +
                               "  )B on A.Id=B.IdSouscripteur " +

                               "  group by A.Id,A.Nom ,A.Prenoms,B.MontantVersement,B.DateVersement,A.MontantCotisation,A.IdClasseMetho,A.IdProfession " +
                                " order by A.Id,A.Nom ,A.Prenoms,B.MontantVersement,B.DateVersement,A.MontantCotisation,A.IdClasseMetho,A.IdProfession ";
                    
                    //req = @"  select S.Id,S.Nom ,S.Prenoms ,V.MontantVersement,V.DateVersement,CA.MontantCotisation from FEC_Souscripteur S
                    //        left join FEC_Versement V on V.IdSouscripteur=S.Id
                    //        left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                    //        WHERE S.isdelete=0 and V.IsDelete=0 and CA.IsDelete=0 AND YEAR(DateVersement)=" + AnObjectif + " " +
                    //       " group by S.Id,S.Nom ,S.Prenoms,V.MontantVersement,V.DateVersement,CA.MontantCotisation " +
                    //       " order by S.Id,S.Nom ,S.Prenoms,V.MontantVersement,V.DateVersement,CA.MontantCotisation";


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
                            command.Parameters.Add(new SqlParameter("idAppli", idAppli));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CEtatSouscriptVersement()
                                    {
                                         mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                         mIdProfession = reader["IdProfession"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProfession"]),
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                      
                                        mNomPersonne = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenomsPersonne = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantVersement"]),


                                    };

                                    ListClientOp.Add(ClientOp);
                                }


                                var LartId = ListClientOp.Select(z => z.mId).Distinct();


                                foreach (var it in LartId)
                                {
                                    var ListeID = new List<CEtatSouscriptVersement>();

                                    ListeID = ListClientOp.Where(c => c.mId == it).ToList();
                                    
                                    if(ListeID.Count>0)
                                    {
                                        long mtantVerseTOTAL = 0;
                                        foreach (var elt in ListeID)
                                        {
                                            mtantVerseTOTAL += elt.mMontantVersement;
                                            
                                        }

                                        //Mettre à jour la liste

                                        foreach(var obj in ListClientOp)
                                        {
                                            if (obj.mId == it)
                                            {
                                                obj.mMontantTotalVerse = mtantVerseTOTAL;

                                                obj.mMontantRestant = obj.mMontantCotisation - mtantVerseTOTAL;

                                            }
                                        }
                                    }


                                }


                                var ListeRetour = new List<CEtatSouscriptVersement>();

                                foreach(var kk in LartId)
                                {
                                    var Cret = ListClientOp.FirstOrDefault(c => c.mId == kk);

                                    ListeRetour.Add(Cret);
                                }

                                return ListeRetour.OrderBy(c=>c.mNomPersonne).ToList();
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
                var msg = "DAOFimeco -> GetEtatSouscripteurVersement -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }


        public List<CEtatSouscriptVersement> GetEtatSouscripteurVersementDetails(string chaineconnexion, string AnObjectif, bool IsSMulSous, bool IschkTousSous, string NomSousDE, string NomSousA, string NomMultiSous, string PrenomMultiSous, bool IsTousClasseMetho, string ListIdClasse, bool IsTousProfession, string ListIdProfession,string idAppli)
        {
            List<CEtatSouscriptVersement> ListClientOp = new List<CEtatSouscriptVersement>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;

                string FiltreSous = string.Empty;

                string FiltreProfession = string.Empty;

                string FiltreClasseMetho = string.Empty;

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

                if (!IsTousClasseMetho)
                {
                    if (ListIdClasse != string.Empty && ListIdClasse != null)
                    {

                        ListIdClasse = ListIdClasse.Replace(",", "','");

                        FiltreClasseMetho = " and S.IdClasseMetho in ('" + ListIdClasse + "') ";
                    }

                }

                //Filtre Profession
                if (!IsTousProfession)
                {
                    if (ListIdProfession != string.Empty && ListIdProfession != null)
                    {
                        ListIdProfession = ListIdProfession.Replace(",", "','");

                        FiltreProfession = " and S.IdProfession in ('" + ListIdProfession + "') ";
                    }

                }


                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  Select A.Id,A.Nom,A.Prenoms,A.MontantCotisation,B.DateVersement,B.MontantVersement,A.IdClasseMetho,A.IdProfession from 
                                (
                                select S.Id,S.Nom ,S.Prenoms ,CA.MontantCotisation,S.IdClasseMetho,S.IdProfession from FEC_Souscripteur S
                                left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                                WHERE S.isdelete=0 AND CA.Type_Gestion=@idAppli and CA.IsDelete=0 and CA.Annee=" + AnObjectif + " " + FiltreSous + FiltreClasseMetho + FiltreProfession +
                               " )A " +
                                " left join " +
                                "  ( " +
                               "  select V.IdSouscripteur,V.DateVersement,V.MontantVersement from FEC_Versement V " +
                               "  where V.IsDelete=0 AND V.Type_Gestion=@idAppli AND YEAR(DateVersement)=" + AnObjectif + " " +
                               "  )B on A.Id=B.IdSouscripteur " +

                               "  group by A.Id,A.Nom ,A.Prenoms,B.DateVersement,A.MontantCotisation ,B.MontantVersement,A.IdClasseMetho,A.IdProfession " +
                                " order by A.Id,A.Nom ,A.Prenoms,B.DateVersement,A.MontantCotisation ,B.MontantVersement,A.IdClasseMetho,A.IdProfession ";

                    //req = @"  select S.Id,S.Nom ,S.Prenoms ,V.MontantVersement,V.DateVersement,CA.MontantCotisation from FEC_Souscripteur S
                    //        left join FEC_Versement V on V.IdSouscripteur=S.Id
                    //        left join FEC_CotisationAnnuelle CA on S.Id=CA.IdSouscripteur
                    //        WHERE S.isdelete=0 and V.IsDelete=0 and CA.IsDelete=0 AND YEAR(DateVersement)=" + AnObjectif + " " +
                    //       " group by S.Id,S.Nom ,S.Prenoms,V.MontantVersement,V.DateVersement,CA.MontantCotisation " +
                    //       " order by S.Id,S.Nom ,S.Prenoms,V.MontantVersement,V.DateVersement,CA.MontantCotisation";


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
                            command.Parameters.Add(new SqlParameter("idAppli", idAppli));

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CEtatSouscriptVersement()
                                    {
                                        mId = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),
                                        mNomPersonne = reader["Nom"] == DBNull.Value ? string.Empty : reader["Nom"] as string,
                                        mPrenomsPersonne = reader["Prenoms"] == DBNull.Value ? string.Empty : reader["Prenoms"] as string,
                                        mMontantCotisation = reader["MontantCotisation"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantCotisation"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MontantVersement"]),


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
                var msg = "DAOFimeco -> GetEtatSouscripteurVersementDetails -> TypeErreur: " + ex.Message;
                CAlias.Log(msg);
                return null;
            }
        }



        public List<CVersementSHORT> GetAllVersementSHORT(string chaineconnexion, string annee,string idappli)
        {
            List<CVersementSHORT> ListClientOp = new List<CVersementSHORT>();
            try
            {
                string req = string.Empty;
                string reqFINAL = string.Empty;


                var mProvider = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (var mConnection = mProvider.CreateConnection())
                {
                    req = @"  select S.IdClasseMetho,V.IdSouscripteur,V.DateVersement,V.MontantVersement from FEC_Versement V  left join FEC_Souscripteur S on S.Id=V.IdSouscripteur  where V.IsDelete=0 AND YEAR(DateVersement)=@annee and V.Type_Gestion=@idappli  ";

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
                            command.Parameters.Add(new SqlParameter("idappli", idappli));

                            

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var ClientOp = new CVersementSHORT()
                                    {
                                        mIdClasseMetho = reader["IdClasseMetho"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdClasseMetho"]),
                                        mIdSouscripteur = reader["IdSouscripteur"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdSouscripteur"]),
                                        mMontantVersement = reader["MontantVersement"] == DBNull.Value ? 0 : Convert.ToInt64(reader["MontantVersement"]),
                                      
                                        mDateVersement = reader["DateVersement"] == DBNull.Value ? new DateTime() : DateTime.Parse(reader["DateVersement"].ToString()),
                                        
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
                var msg = "DAOFimeco -> GetAllVersementSHORT -> TypeErreur: " + ex.Message;
              //  CAlias.Log(msg);
                return null;
            }
        }


    }
}
