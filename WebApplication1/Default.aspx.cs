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
        string standartCom = "SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Реализуется в срок ' || PLANDATE    WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') <= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Превышен срок реализации'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is not NULL THEN 'Выполнено с нарушением срока'    END as TIMEPASS,GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 ORDER BY LOGROWID";

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
            // GridView2.DataSource = ds;                               
            Session["tbl"] = ds;
            GridView2.DataSource = Session["tbl"];
            FillGrid();
            GridView2.DataBind();
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
           // da.Fill(ds);
            con.Open();
            cmd.ExecuteNonQuery();            
            con.Close();
            Com = string.Format(standartCom);
            Command(Com);

        }

        protected void SetGridWidth ()
        {
            // костыль для выравнивания под строку ввода1
            /*
            GridView2.Columns[0].ItemStyle.Width = 40;
            GridView2.Columns[1].ItemStyle.Width = 158;
            GridView2.Columns[2].ItemStyle.Width = 62;
            GridView2.Columns[3].ItemStyle.Width = 82;
            GridView2.Columns[5].ItemStyle.Width = 70;
            GridView2.Columns[6].ItemStyle.Width = 93;
            GridView2.Columns[7].ItemStyle.Width = 108;
            GridView2.Columns[8].ItemStyle.Width = 107;
            GridView2.Columns[9].ItemStyle.Width = 108;
            GridView2.Columns[10].ItemStyle.Width = 86;
            GridView2.Columns[11].ItemStyle.Width = 45;
            GridView2.Columns[12].ItemStyle.Width = 89;
            GridView2.Columns[13].ItemStyle.Width = 97;
            GridView2.Columns[14].ItemStyle.Width = 96;
            GridView2.Columns[15].ItemStyle.Width = 103;
            GridView2.Columns[16].ItemStyle.Width = 98;
            GridView2.Columns[17].ItemStyle.Width = 58;
            GridView2.Columns[18].ItemStyle.Width = 115;
            GridView2.Columns[19].ItemStyle.Width = 78;
            GridView2.Columns[20].ItemStyle.Width = 67;
        */
            // костыль для выравнивания под строку ввода   2       
            GridView2.Columns[0].ItemStyle.Width = 198;
            GridView2.Columns[1].ItemStyle.Width = 62;
            GridView2.Columns[2].ItemStyle.Width = 82;
            GridView2.Columns[4].ItemStyle.Width = 70;
            GridView2.Columns[5].ItemStyle.Width = 93;
            GridView2.Columns[6].ItemStyle.Width = 108;
            GridView2.Columns[7].ItemStyle.Width = 107;
            GridView2.Columns[8].ItemStyle.Width = 108;
            GridView2.Columns[9].ItemStyle.Width = 86;
            GridView2.Columns[10].ItemStyle.Width = 45;
            GridView2.Columns[11].ItemStyle.Width = 89;
            GridView2.Columns[12].ItemStyle.Width = 97;
            GridView2.Columns[13].ItemStyle.Width = 96;
            GridView2.Columns[14].ItemStyle.Width = 103;
            GridView2.Columns[15].ItemStyle.Width = 98;
            GridView2.Columns[16].ItemStyle.Width = 58;
            GridView2.Columns[17].ItemStyle.Width = 115;
            GridView2.Columns[18].ItemStyle.Width = 78;
            GridView2.Columns[19].ItemStyle.Width = 67;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //редирект
          /*  if (HttpContext.Current.Session["AllowAccess"] != "true")
            {
                Response.Redirect("/login.aspx");
            }
            */
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
               // Com = string.Format("SELECT * FROM LOGICS2 ORDER BY LOGROWID");

                Com = string.Format(standartCom);
                Command(Com);               
            }            

            SetGridWidth();

            //чтобы ручками не писать тест
            /*
              TextBox2.Text = "Название1";
              TextBox3.Text = "600";
              TextBox4.Text = "800";
              TextBox5.Text = "2548";
              TextBox6.Text = "22.12.2017";
              TextBox7.Text = "текст";
              TextBox8.Text = "текст2";
              TextBox9.Text = "текст3";
              TextBox10.Text = "25.12.2017";
              TextBox11.Text = " текст4";
              TextBox12.Text = " текст5";
              TextBox13.Text = "25.12.2017";
              TextBox14.Text = "текст7";
              TextBox15.Text = "25.12.2017";
              TextBox16.Text = "текст9";
              TextBox17.Text = "текст10";
              TextBox18.Text = "текст11";
              TextBox19.Text = "текст12";
              TextBox20.Text = "текст13";
            TextBox21.Text = Convert.ToString(AddWorkDays(Convert.ToDateTime(TextBox10.Text), 14));
            */
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

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;           
            FillGrid();
        }
        void ClearTable ()
        {           
            TextBox1.Text = "";                    
             Com = string.Format(standartCom);       
             Command(Com);          
         }

        //кнопка поиска
        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text=TextBox1.Text.Trim();
            
            if (!CheckBox1.Checked)
            {
                if (TextBox1.Text != "") 
                {                    
                        Com = string.Format("SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Реализуется в срок ' || PLANDATE    WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') <= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Превышен срок реализации'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is not NULL THEN 'Выполнено с нарушением срока'    END as TIMEPASS,GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 WHERE RUBRNAMESPD like '%" + TextBox1.Text + "%' ORDER BY LOGROWID");
                        Command(Com);                    
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
                   Com = string.Format("SELECT LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, CASE    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') >= to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Реализуется в срок ' || PLANDATE    WHEN to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') <= to_date(CREATEDATE, 'dd.mm.yyyy hh24:mi:ss') THEN 'Выполнено в срок'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND  CREATEDATE is NULL THEN 'Превышен срок реализации'    WHEN(to_date(PLANDATE, 'dd.mm.yyyy hh24:mi:ss') < to_date(CURRENT_DATE, 'dd.mm.yyyy hh24:mi:ss')) AND CREATEDATE is not NULL THEN 'Выполнено с нарушением срока'    END as TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE FROM LOGICS2 WHERE RUBRNAMESPD = '" + newrequest + "'");
                  Command(Com);  
               }
            }
        }

        void FillGrid()
        {
            GridView2.DataSource = Session["tbl"];            
            GridView2.DataBind();
        }        

        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearTable();
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

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SetGridWidth();
            int index = GridView2.EditIndex;
            GridViewRow row = GridView2.Rows[index];
            int Eid = e.RowIndex + 1;
            string LOGNAME = ((TextBox)row.Cells[0].Controls[0]).Text.ToString().Trim();
            string RUBRNAMESPD = ((TextBox)row.Cells[1].Controls[0]).Text.ToString().Trim();
            string RUBRNAMEESRD = ((TextBox)row.Cells[2].Controls[0]).Text.ToString().Trim();
            string IDDK = ((TextBox)row.Cells[3].Controls[0]).Text.ToString().Trim();
            string DATEAR = ((TextBox)row.Cells[4].Controls[0]).Text.ToString().Trim();
            string DOCNUM = ((TextBox) row.Cells[5].Controls[0]).Text.ToString().Trim();
            string REACT = ((TextBox)row.Cells[6].Controls[0]).Text.ToString().Trim();
            string DOCNUMREACT = ((TextBox)row.Cells[7].Controls[0]).Text.ToString().Trim();
            string REACTDATE = ((TextBox)row.Cells[8].Controls[0]).Text.ToString().Trim();
            string TIMEPASS = ((TextBox)row.Cells[9].Controls[0]).Text.ToString().Trim();
            string GLPI = ((TextBox)row.Cells[10].Controls[0]).Text.ToString().Trim();
            string TESTDATE = ((TextBox)row.Cells[11].Controls[0]).Text.ToString().Trim();
            string TESTDOC = ((TextBox)row.Cells[12].Controls[0]).Text.ToString().Trim();
            string CREATEDATE = ((TextBox)row.Cells[13].Controls[0]).Text.ToString().Trim();
            string CREATEDOC = ((TextBox)row.Cells[14].Controls[0]).Text.ToString().Trim();
            string COMMENTR = ((TextBox)row.Cells[15].Controls[0]).Text.ToString().Trim();
            string ISINBR = ((TextBox)row.Cells[16].Controls[0]).Text.ToString().Trim();
            string ISPUPR = ((TextBox)row.Cells[17].Controls[0]).Text.ToString().Trim();
            string DAYPASS = ((TextBox)row.Cells[18].Controls[0]).Text.ToString().Trim();
            string PLANDATE = ((TextBox)row.Cells[19].Controls[0]).Text.ToString().Trim();

            Com = string.Format("UPDATE LOGICS2 SET LOGNAME='" + LOGNAME + "',RUBRNAMESPD='" + RUBRNAMESPD + "',IDDK='" + IDDK + "',DATEAR=TO_DATE('" + DATEAR + "', 'dd.mm.yyyy hh24:mi:ss'),DOCNUM='" + DOCNUM + "',REACT='" + REACT + "',DOCNUMREACT='" + DOCNUMREACT + "',REACTDATE=TO_DATE('" + REACTDATE + "', 'dd.mm.yyyy hh24:mi:ss'),TIMEPASS='" + TIMEPASS + "',GLPI='" + GLPI + "',TESTDATE=TO_DATE('" + TESTDATE + "', 'dd.mm.yyyy hh24:mi:ss'),TESTDOC='" + TESTDOC + "',CREATEDATE=TO_DATE('" + CREATEDATE + "', 'dd.mm.yyyy hh24:mi:ss'),CREATEDOC='" + CREATEDOC + "',COMMENTR='" + COMMENTR+ "',ISINBR='" + ISINBR + "',ISPUPR='" + ISPUPR + "',DAYPASS='" + DAYPASS + "',PLANDATE=TO_DATE('" + PLANDATE + "', 'dd.mm.yyyy hh24:mi:ss') WHERE LOGROWID=" + Eid + "");                    
            DoEditTable(Com);         
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {            
            GridView2.EditIndex = e.NewEditIndex;
            FillGrid();           
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
        protected void Button3_Click(object sender, EventArgs e)
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
            //а это высчитывает ожидаему дату реализации логики
            TextBox21.Text=Convert.ToString(AddWorkDays(Convert.ToDateTime(TextBox10.Text), 14));                   
            
            // сам запрос инсерта            
             Com = string.Format("INSERT INTO LOGICS2 (LOGROWID, LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, DOCNUMREACT, REACTDATE, TIMEPASS, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, DAYPASS, PLANDATE) VALUES ("+GetMaxID()+",'" + TextBox2.Text+"','"+ TextBox3.Text +"','"+ TextBox4.Text + "','"+ TextBox5.Text + "',TO_DATE('"+TextBox6.Text+"', 'dd.mm.yyyy'),'"+ TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text +  "',TO_DATE('" + TextBox10.Text + "', 'dd.mm.yyyy'),'"+TextBox11.Text + "','" + TextBox12.Text + "',TO_DATE('" + TextBox13.Text + "', 'dd.mm.yyyy'),'" + TextBox14.Text + "',TO_DATE('" + TextBox15.Text + "', 'dd.mm.yyyy'),'" + TextBox16.Text + "','"+ TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "','" +TextBox20.Text + "'," + "TO_DATE('"+TextBox21.Text +"', 'dd.mm.yyyy hh24:mi:ss'))");
               DoEditTable(Com);

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

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SetGridWidth();
            GridView2.EditIndex = -1;
            FillGrid();
        }

      /*  Вот это пока ковыряю, ругается
       * protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox textBox = ((TextBox)e.Row.FindControl("GLPI"));
                HyperLink HyperLink1 = ((TextBox)e.Row.FindControl("GLPI"));
            }
        }
        */
    }
}


