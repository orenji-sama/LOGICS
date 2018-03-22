using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Data.OracleClient;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.OleDb;
//using System.Data;
using System.Configuration;

namespace ASGuf2
{
    public partial class NewLogin : System.Web.UI.Page
    {
        
        /*
        DAL.Permissions Permissions = new DAL.Permissions();
        DAL.SqlMeth SqlMeth = new DAL.SqlMeth();
        */

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public static string Authentification(DAL.Permissions.Auth logindata)
        {
            DAL.Permissions Permissions = new DAL.Permissions();

            string Result = "-";
            bool Access = false;
            string FullUserName = "";
          //  string[] conparams = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "settings");
          //  string connString = "Data Source=" + conparams[0] + ";Persist Security Info=True;Password=" + conparams[3] + ";User ID=" + conparams[2] + ";Connection Lifetime=60";
            //OleDbConnection conn;
            //conn = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            //{
                try
                {
                    //conn.Open();
                    using (var context = new PrincipalContext(ContextType.Domain, "mlc.gov", logindata.Username, logindata.Password))
                    {
                        if (context.ValidateCredentials(logindata.Username, logindata.Password))
                        {
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, logindata.Username);
                            FullUserName = user.Name;

                        //OracleTransaction tran = conn.BeginTransaction();
                        //Access = Convert.ToBoolean(Permissions.CheckGroup(conn, tran, logindata.Username));
                        Access = true;
                        }
                        else
                        {
                            Result = "Логин или пароль некорретны, либо не назначены права доступа";
                        }
                    }
                }
                catch (Exception Err)
                {
                    Result = Err.Message;
                }
                finally
                {
                    //conn.Close();
                }
            //}
            if (Access)
            {
                if (logindata.Username.ToString().Equals("") || FullUserName.Equals(""))
                    return "Не найдена пользовательская информация в Active Directory";
                HttpContext.Current.Session["ActLogin"] = logindata.Username;
                HttpContext.Current.Session["AllowAccess"] = "true";
                HttpContext.Current.Session["FullUserName"] = FullUserName;
                Result = "";
                

            }
 
            return Result;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            DAL.Permissions Permissions = new DAL.Permissions();

            string[] conparams = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "settings");

            Session["Source"] = conparams[0];
            Session["Port"] = conparams[1];
            Session["SPDUrl"] = conparams[4];
            Session["domain"] = conparams[5];
            Session["SPDFileURL"] = conparams[8];
            Session["SPDUser"] = conparams[9];
            Session["SPDPass"] = conparams[10];
            */
        }

    }
}