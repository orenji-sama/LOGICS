using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        string Com;
        // стандартный селект для полного отображения таблицы
        string standartCom = "SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Реализуется в срок '||PLANDATE  WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Превышен срок реализации'     WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss')) THEN 'Выполнено с нарушением срока'     END as TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 ORDER BY LOGROWID";

        protected void Command(string Com)
        {
            OleDbDataAdapter da;
            OleDbConnection con;
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = Com;
            cmd.Connection = con;
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            con.Open();
            cmd.ExecuteNonQuery();                                       
            Session["tbl"] = ds;
            FillGrid();       
            con.Close();            
        }
        // процедура обработки всех делитов, апдейтов, инсертов
        protected void DoEditTable (string Com)
        {
            OleDbDataAdapter da;
            OleDbConnection con;
            DataSet ds = new DataSet();
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = Com;
            cmd.Connection = con;
            da = new OleDbDataAdapter(cmd);           
            con.Open();
            cmd.ExecuteNonQuery();            
            con.Close();
            Com = string.Format(standartCom);
            Command(Com);

        }
        protected void CleanTexbox()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox18.Text = "";
            TextBox19.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
            TextBox21.Text = "";
        }     

        protected void Page_Load(object sender, EventArgs e)
        {
            //редирект
            /*  if (HttpContext.Current.Session["AllowAccess"] != "true")
              {
                  Response.Redirect("/login.aspx");
              }
              */
            GridView2.Visible = true;
            popupPanel.Visible = false;
            popupdiv.Visible = false;
            if (CheckBox1.Checked)
            {
                Label1.Visible = false;                         
                Label3.Visible = true;
                TextBox1.Width=300;
            }
            else
            {
                Label1.Visible = true;
                TextBox1.Visible = true;                
                Label3.Visible = false;
                TextBox1.Width = 80;
            }

            if (!Page.IsPostBack)
            {              
                Com = string.Format(standartCom);
                Command(Com);                
            }          

        }

        //спизженый в инете кусок кода для расчета рабочих дней
        public DateTime AddWorkDays(DateTime date, int workingDays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);
                if (newDate.DayOfWeek != DayOfWeek.Saturday &&
                    newDate.DayOfWeek != DayOfWeek.Sunday) 
                {
                    workingDays -= direction;
                }
            }
            return newDate;
        }

        // счетчик страниц
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;           
            FillGrid();
        }

        //сбросить все нафиг, отобразить изнчальный грид
        void ClearTable ()
        {
            GridView2.EditIndex = -1;
            GridView2.Visible = true;
            popupPanel.Visible = false;
            TextBox1.Text = "";                    
            Com = string.Format(standartCom);       
            Command(Com);
            CleanTexbox();
            Button4.Visible = true;

        }

        //кнопка поиска
        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text=TextBox1.Text.Trim();
            
            if (!CheckBox1.Checked)
            {
                if (TextBox1.Text != "") 
                {                    
                        Com = string.Format("SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Реализуется в срок '||PLANDATE  WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Превышен срок реализации'     WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss')) THEN 'Выполнено с нарушением срока'     END as TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 WHERE RUBRNAMESPD like '%" + TextBox1.Text + "%' ORDER BY LOGROWID");
                        Command(Com);                    
                }  
                else
                {
                    // надо бы какую-нить ошибульку запилить
                }
            }
            else
            {
                string request = TextBox1.Text;
                string newrequest = "";
                request = request.Replace(" ", string.Empty);
                request =request.Replace(',', ' ');  
                newrequest= request.Replace (" ", "' OR RUBRNAMESPD = '");                   
               if (TextBox1.Text != "")
               {
                   Com = string.Format("SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Реализуется в срок '||PLANDATE  WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is NULL THEN 'Превышен срок реализации'     WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss')) THEN 'Выполнено с нарушением срока'     END as TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 WHERE RUBRNAMESPD = '" + newrequest + "'");
                  Command(Com);  
               }
                else
                {
                    // надо бы какую-нить ошибульку запилить
                }
            }
        }

        void FillGrid()
        {
            GridView2.DataSource = Session["tbl"];            
            GridView2.DataBind();

            foreach (GridViewRow row in GridView2.Rows)
            {

                if (row.Cells[9].Text.Equals("Выполнено в срок"))
                {
                    row.CssClass = "CompletedRowStyle";
                }
                else
                    if (row.Cells[9].Text.Equals("Превышен срок реализации"))
                {
                    row.CssClass = "RegAndError";
                }
                else
                        if (row.Cells[9].Text.Equals("Выполнено с нарушением срока"))
                {
                    row.CssClass = "RegAndCorrected";
                }
                else
                {
                    row.CssClass = "SEDO";
                }
            }

        }        

        //кнопка "сброс", "отмена"
        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearTable();
            GridView2.EditIndex = -1; //по идее должно убирать редактирование, но как-то хуюшки
            popupdiv.Visible = false;
        }        

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (GridView2.Rows.Count > 0 && e.RowIndex >= 0)
            { 
                int Eid = e.RowIndex+1;
                Com = string.Format("DELETE FROM LOGICS2 WHERE LOGROWID=" + Eid + "");
                DoEditTable(Com);

                for (int i= Eid+1; i<= GetMaxID(); i++)
                {
                    Com = string.Format("UPDATE LOGICS2 SET LOGROWID="+(i-1)+ " WHERE LOGROWID=" + i+"");
                    DoEditTable(Com);
                }
            }
        }        

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            popupdiv.Visible = true;
            popupPanel.Visible = true;
            GridView2.Visible = false;
            Button4.Visible = false;
            Button5.Visible = true;
            Button3.Visible = false;
            ViewState["row"] = e.NewEditIndex;            
            string erow = Convert.ToString(Convert.ToInt32(ViewState["row"])+1);
            TextBox2.Text = GetRowData("LOGNAME", erow);
            TextBox3.Text = GetRowData("RUBRNAMESPD", erow);
            TextBox4.Text = GetRowData("RUBRNAMEESRD", erow);
            TextBox5.Text = GetRowData("IDDK", erow);
            TextBox6.Text = GetRowData("DATEAR", erow);
            TextBox7.Text = GetRowData("DOCNUM", erow);
            TextBox8.Text = GetRowData("REACT", erow);
            TextBox9.Text = GetRowData("DOCNUMREACT", erow);
            TextBox10.Text = GetRowData("REACTDATE", erow);
            TextBox11.Text = GetRowData("TIMEPASS", erow);
            TextBox12.Text = GetRowData("GLPI", erow);
            TextBox13.Text = GetRowData("TESTDATE", erow);
            TextBox14.Text = GetRowData("TESTDOC", erow);
            TextBox15.Text = GetRowData("CREATEDATE", erow);
            TextBox16.Text = GetRowData("CREATEDOC", erow);
            TextBox17.Text = GetRowData("COMMENTR", erow);
            TextBox18.Text = GetRowData("ISINBR", erow);
            TextBox19.Text = GetRowData("ISPUPR", erow);
            TextBox20.Text = GetRowData("DAYPASS", erow);
            TextBox21.Text = GetRowData("PLANDATE", erow);
            GridView2.EditIndex = -1;   
        }

        protected int GetMaxID()
        {
            int ID;
            if (GridView2.Rows.Count != 0)
            {
                //считаю кол-во записей (айдишников)
                OleDbConnection con;
                OleDbCommand cmd = new OleDbCommand();
                con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
                cmd.CommandText = "Select MAX(LOGROWID) FROM LOGICS2";
                cmd.Connection = con;
                con.Open();
                object obj = cmd.ExecuteScalar();
                ID = (int)Convert.ChangeType(obj, typeof(int));
                con.Close();
                ID++;
            }
            else
            {
                ID = 1;
            }
            return ID;
        }
        protected string GetRowData(string row, string id)
        {
            string rowdata;
            OleDbConnection con;
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = "Select "+row+ " FROM LOGICS2 WHERE LOGROWID="+id;
            cmd.Connection = con;
            con.Open();
            object obj = cmd.ExecuteScalar();
            rowdata = (string)Convert.ChangeType(obj, typeof(string));
            con.Close();             
            return rowdata;
        }

        // кнопка "добавить"
        protected void Button4_Click(object sender, EventArgs e)
        {
            popupdiv.Visible = true;
            popupPanel.Visible = true;
            GridView2.Visible = false;
            Button4.Visible = false;
            Button5.Visible = false;
            Button3.Visible = true;
        }

        protected void TrimTextbox()
        {
            // очищаю строчечки от лишних пробелов
            TextBox2.Text = TextBox2.Text.Trim();
            TextBox3.Text = TextBox3.Text.Trim();
            TextBox4.Text = TextBox4.Text.Trim();
            TextBox5.Text = TextBox5.Text.Trim();
            TextBox6.Text = TextBox6.Text.Trim();
            TextBox7.Text = TextBox7.Text.Trim();
            TextBox8.Text = TextBox8.Text.Trim();
            TextBox9.Text = TextBox9.Text.Trim();
            TextBox10.Text = TextBox10.Text.Trim();
            TextBox11.Text = TextBox11.Text.Trim();
            TextBox12.Text = TextBox12.Text.Trim();
            TextBox13.Text = TextBox13.Text.Trim();
            TextBox14.Text = TextBox14.Text.Trim();
            TextBox15.Text = TextBox15.Text.Trim();
            TextBox16.Text = TextBox16.Text.Trim();
            TextBox17.Text = TextBox17.Text.Trim();
            TextBox18.Text = TextBox18.Text.Trim();
            TextBox19.Text = TextBox19.Text.Trim();
            TextBox20.Text = TextBox20.Text.Trim();
            TextBox21.Text = TextBox21.Text.Trim();
        }


        //кнопка "добавить" для внесения в таблицу
            protected void Button3_Click(object sender, EventArgs e)
        {
            TrimTextbox();
            //а это высчитывает ожидаему дату реализации логики
            TextBox21.Text=Convert.ToString(AddWorkDays(Convert.ToDateTime(TextBox10.Text), 14));                   
            
            // сам запрос инсерта            
             Com = string.Format("INSERT INTO LOGICS2 (LOGROWID, LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE) VALUES ("+GetMaxID()+",'" + TextBox2.Text+"','"+ TextBox3.Text +"','"+ TextBox4.Text + "','"+ TextBox5.Text + "',TO_DATE('"+TextBox6.Text+"', 'dd.mm.yyyy'),'"+ TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text +  "',TO_DATE('" + TextBox10.Text + "', 'dd.mm.yyyy'),'"+TextBox11.Text + "','" + TextBox12.Text + "',TO_DATE('" + TextBox13.Text + "', 'dd.mm.yyyy'),'" + TextBox14.Text + "',TO_DATE('" + TextBox15.Text + "', 'dd.mm.yyyy'),'" + TextBox16.Text + "','"+ TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "','" +TextBox20.Text + "'," + "TO_DATE('"+TextBox21.Text +"', 'dd.mm.yyyy hh24:mi:ss'))");
               DoEditTable(Com);
             CleanTexbox();

        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {           
            GridView2.EditIndex = -1;
            FillGrid();
        }
                
        //кнопка "апдейт"
        protected void Button5_Click(object sender, EventArgs e)
        {
            string erow = Convert.ToString(Convert.ToInt32(ViewState["row"]) + 1);
            string LOGNAME = TextBox2.Text;
            string RUBRNAMESPD = TextBox3.Text;
            string RUBRNAMEESRD = TextBox4.Text;
            string IDDK = TextBox5.Text;
            string DATEAR = TextBox6.Text;
            string DOCNUM = TextBox7.Text;
            string REACT = TextBox8.Text;
            string DOCNUMREACT = TextBox9.Text;
            string REACTDATE = TextBox10.Text;
            string TIMEPASS = TextBox11.Text;
            string GLPI = TextBox12.Text;
            string TESTDATE = TextBox13.Text;
            string TESTDOC = TextBox14.Text;
            string CREATEDATE = TextBox15.Text;
            string CREATEDOC = TextBox16.Text;
            string COMMENTR = TextBox17.Text;
            string ISINBR = TextBox18.Text;
            string ISPUPR = TextBox19.Text;
            string DAYPASS = TextBox20.Text;
            string PLANDATE = TextBox21.Text;
            TrimTextbox();
            Com = string.Format("UPDATE LOGICS2 SET LOGNAME='" + LOGNAME + "',RUBRNAMESPD='" + RUBRNAMESPD + "',RUBRNAMEESRD='" + RUBRNAMEESRD + "',IDDK='" + IDDK + "',DATEAR=TO_DATE('" + DATEAR + "', 'dd.mm.yyyy hh24:mi:ss'),DOCNUM='" + DOCNUM + "',REACT='" + REACT + "',DOCNUMREACT='" + DOCNUMREACT + "',REACTDATE=TO_DATE('" + REACTDATE + "', 'dd.mm.yyyy hh24:mi:ss'),TIMEPASS='" + TIMEPASS + "',GLPI='" + GLPI + "',TESTDATE=TO_DATE('" + TESTDATE + "', 'dd.mm.yyyy hh24:mi:ss'),TESTDOC='" + TESTDOC + "',CREATEDATE=TO_DATE('" + CREATEDATE + "', 'dd.mm.yyyy hh24:mi:ss'),CREATEDOC='" + CREATEDOC + "',COMMENTR='" + COMMENTR + "',ISINBR='" + ISINBR + "',ISPUPR='" + ISPUPR + "',DAYPASS='" + DAYPASS + "',PLANDATE=TO_DATE('" + PLANDATE + "', 'dd.mm.yyyy hh24:mi:ss') WHERE LOGROWID=" + erow + "");
            DoEditTable(Com);
            TextBox2.Text = GetRowData("LOGNAME", erow);
            ClearTable();
        }                

    }
}


