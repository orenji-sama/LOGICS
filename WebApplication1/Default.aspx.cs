using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Drawing;
using System.Web;
using System.Text;
using ASGuf2.DAL;
using System.Globalization;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page

    {
        string Com;
        // стандартный селект для полного отображения таблицы
        string standartCom = "WITH timepass_inf AS (SELECT logrowid, CASE WHEN (PLANDATE >= CURRENT_DATE) AND CREATEDATE IS NULL THEN 'Реализуется в срок - '||TO_CHAR(PLANDATE, 'dd.MM.yyyy') WHEN(PLANDATE >= CREATEDATE) THEN 'Выполнено в срок' WHEN(PLANDATE<CURRENT_DATE) AND CREATEDATE IS NULL THEN 'Превышен срок реализации' WHEN(PLANDATE<CREATEDATE) THEN 'Выполнено с нарушением срока' else '-' END as TIMEPASS FROM vinogradovamb.LOGICS21) SELECT l.LOGROWID, l.LOGNAME, l.RUBRNAMESPD, l.RUBRNAMEESRD, l.IDDK, TO_CHAR(l.DATEAR, 'dd.MM.yyyy') DATEAR, l.DOCNUM, l.REACT, l.ISNEW, l.DOCNUMREACT, TO_CHAR(l.REACTDATE, 'dd.MM.yyyy') REACTDATE, GLPI, TO_CHAR(l.TESTDATE, 'dd.MM.yyyy') TESTDATE, l.TESTDOC, TO_CHAR(l.CREATEDATE, 'dd.MM.yyyy') CREATEDATE, l.CREATEDOC, l.COMMENTR, l.ISINBR, l.ISPUPR,case when TO_CHAR(l.PLANDATE, 'dd.MM.yyyy') is null then '-' else TO_CHAR(l.PLANDATE, 'dd.MM.yyyy') end as PLANDATE, case when((l.testdate IS NOT NULL) AND(l.createdate IS NULL)) then ''||TRUNC(l.testdate-l.reactdate) when((l.testdate IS NULL) AND(l.createdate IS NOT NULL) OR(l.react= 'Логика не принята')) then '-' when((l.testdate IS NOT NULL) AND(l.createdate IS NOT NULL)) then ''||TRUNC(l.testdate-l.reactdate) else ''||(SELECT TRUNC(SYSDATE - l.reactdate) FROM dual)  end as daypasstest, i.timepass as timepass, case when(l.createdate IS NOT NULL) AND(l.testdate IS NOT NULL) THEN ''||TRUNC(l.createdate-l.testdate) when(l.createdate IS NULL) AND(l.testdate IS NOT NULL)  THEN ''||(SELECT TRUNC(SYSDATE - l.testdate) FROM dual) else '-' end as createdaypass, case when(l.createdate IS NULL) AND (l.react='Логика принята') THEN ''||(SELECT TRUNC(SYSDATE - l.reactdate) FROM dual) when (l.createdate IS NOT NULL) AND (l.react='Логика принята')  THEN ''||TRUNC(l.createdate-l.reactdate) else '-' end as daystaken FROM vinogradovamb.logics21 l, timepass_inf i WHERE l.logrowid = i.logrowid ";
        string StandartOrderBy = "ORDER BY LOGROWID DESC";

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
        protected void DoEditTable(string Com)
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
            Com = string.Format(standartCom + StandartOrderBy);
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
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox12.Text = "";
            TextBox13.Text = "";
            TextBox14.Text = "";
            TextBox15.Text = "";
            TextBox16.Text = "";
            TextBox17.Text = "";
            TextBox18.Text = "";
            TextBox19.Text = "";
            //TextBox20.Text = "";
            TextBox21.Text = "";
            TextBox30.Text = "Заполняется автоматически";
            TextBox31.Text = "Заполняется автоматически";
            TextBox32.Text = "Заполняется автоматически";
            TextBox33.Text = "Заполняется автоматически";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["AllowAccess"] != "true")
            {
                Response.Redirect("login.aspx");
            }
            GridView2.Visible = true;
            popupPanel.Visible = false;
            popupdiv.Visible = false;
            //legenddiv.Visible = true;
            TextBox30.Text = "Заполняется автоматически";
            TextBox31.Text = "Заполняется автоматически";
            TextBox32.Text = "Заполняется автоматически";
            TextBox33.Text = "Заполняется автоматически";
            if (CheckBox1.Checked)
            {
                Label1.Visible = false;
                Label3.Visible = true;
            }
            else
            {
                Label1.Visible = true;
                Label3.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                Com = string.Format(standartCom + StandartOrderBy);
                Command(Com);
                Label12.Visible = false;
            }

            if (Convert.ToString(Session["Access"]) != "True")
            {
                Button4.Visible = false;
                GridView2.Columns[22].Visible = false;
            }
        /*    if (Convert.ToString(Session["Filtr"]) != "")
            {
                Labfiltr.Text = "Нет активных фильтров";
            }
            else
            {
                Labfiltr.Text = Convert.ToString(Session["Filtr"]);
            }
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

        // счетчик страниц
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        //сбросить все нафиг, отобразить изначальный грид
        void ClearTable()
        {
            GridView2.EditIndex = -1;
            GridView2.Visible = true;
            popupPanel.Visible = false;
            navpanel.Visible = true;
            TextBox1.Text = "";
            Com = string.Format(standartCom + StandartOrderBy);
            Command(Com);
            CleanTexbox();
            Button4.Visible = true;
            Button7.Visible = true;
            Button2.Visible = true;
            Labfiltr.Text = "Нет активных фильтров";
        }

        //кнопка поиска
        protected void Button1_Click(object sender, EventArgs e)
        {
            Button4.Visible = true;
            Button7.Visible = true;
            Button2.Visible = true;
            GridView2.Visible = true;
            popupPanel.Visible = false;
            TextBox1.Text = TextBox1.Text.Trim();

            if (!CheckBox1.Checked)
            {
                if (TextBox1.Text != "")
                {
                    Com = string.Format(standartCom + " and UPPER(l.RubrnameSPD||' '||l.LOGNAME) LIKE UPPER('%" + TextBox1.Text + "%') ORDER BY LOGROWID DESC");
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
                request = request.Replace(',', ' ');
                newrequest = request.Replace(" ", "' OR RUBRNAMESPD = '");
                if (TextBox1.Text != "")
                {
                    Com = string.Format(standartCom + " and l.RUBRNAMESPD = '" + newrequest + "'");
                    Command(Com);
                }
                else
                {
                    // надо бы какую-нить ошибульку запилить
                }
            }
            if (GridView2.Rows.Count == 0)
            {
                Label12.Visible = true;
            }
            else
            {
                Label12.Visible = false;
            }
        }

        //заполнение грида
        void FillGrid()
        {
            GridView2.DataSource = Session["tbl"];
            GridView2.DataBind();
        }

        //кнопка "сброс", "отмена"
        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearTable();
            GridView2.EditIndex = -1;
        }

        //удалениие строки
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetTextBoxes(e.RowIndex);
            int Eid = Convert.ToInt32(ViewState["row"]);
            Com = string.Format("DELETE FROM LOGICS21 WHERE LOGROWID=" + Eid + "");
            DoEditTable(Com);
            SaveLogs(Com + ", Название процесса:  " + TextBox2.Text);
            for (int i = Eid; i <= GetMaxID(); i++)
            {
                Com = string.Format("UPDATE LOGICS21 SET LOGROWID=" + (i - 1) + " WHERE LOGROWID=" + i + "");
                DoEditTable(Com);
            }
            ClearTable();
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            popupdiv.Visible = true;
            popupPanel.Visible = true;
            navpanel.Visible = false;
            GridView2.Visible = false;
            Button4.Visible = false;
            Button5.Visible = true;
            Button3.Visible = false;
            Button7.Visible = false;
            Button2.Visible = false;
            int rowid = (Convert.ToInt32(e.NewEditIndex));
            SetTextBoxes(rowid);
            GridView2.EditIndex = -1;

        }
        protected void SetTextBoxes(int rowid)
        {
            TextBox2.Text = GetRowData(rowid, 0, "Label1"); //Название процесса в СПД
            TextBox3.Text = GetRowData(rowid, 1, "Label3"); //Номер процесса в СПД
            TextBox4.Text = GetRowData(rowid, 2, "Label4"); //Рубрика в ЕСРД/ДК
            TextBox5.Text = GetRowData(rowid, 3, "Label5"); //ID DK
            if (GetRowData(rowid, 4, "Label6") != "")
            {
                TextBox6.Text = Convert.ToDateTime(GetRowData(rowid, 4, "Label6")).ToString("yyyy-MM-dd"); //Дата получения логики
            }
            else
            {
                TextBox6.Text = "";
            }
            TextBox7.Text = GetRowData(rowid, 5, "HyperLink3"); //Номер служебной записки, по которой логика направ- лялась
            DropDownList1.Text = GetRowData(rowid, 6, "Label8"); //Реакция Управления информа- тизации
            if (GetRowData(rowid, 7, "Label34") == "")
            {
                DropDownList2.Text = "Реализация";
            }
            else
            {
                DropDownList2.Text = GetRowData(rowid, 7, "Label34"); //Тип логики процесса
            }
            string strDropDownList2 = GetRowData(rowid, 7, "Label34");
            // DropDownList2.Text = GetRowData(rowid, 7, "Label34"); //Тип логики процесса
            if (GetRowData(rowid, 8, "Label9") != "")
            {
                TextBox9.Text = Convert.ToDateTime(GetRowData(rowid, 8, "Label9")).ToString("yyyy-MM-dd"); //Дата ответной служебной записки Управления информа- тизации
            }
            TextBox10.Text = GetRowData(rowid, 9, "HyperLink4"); //Номер ответной служебной записки Управления информа- тизации
            TextBox12.Text = GetRowData(rowid, 10, "HyperLink1"); //Номер GLPI
            if (GetRowData(rowid, 11, "Label11") != "")
            {
                TextBox13.Text = Convert.ToDateTime(GetRowData(rowid, 11, "Label11")).ToString("yyyy-MM-dd"); //Дата Акта передаче на тестиро- вание (дата реализа- ции процесса)
            }
            else
            {
                TextBox13.Text = "";
            }
            TextBox14.Text = GetRowData(rowid, 12, "HyperLink5"); //Номер Акта о передаче на тестирование
            if (GetRowData(rowid, 13, "Label13") != "")
            {
                TextBox15.Text = Convert.ToDateTime(GetRowData(rowid, 13, "Label13")).ToString("yyyy-MM-dd"); //Дата Акта (служебной записки) о вводе в эксплуат- ацию
            }
            else
            {
                TextBox15.Text = "";
            }

            TextBox16.Text = GetRowData(rowid, 14, "HyperLink6"); //Номер Акта (служебной записки) о вводе в эксплуата- цию
            TextBox17.Text = GetRowData(rowid, 15, "Label15"); //Комментарий
                                                               // DropDownList2.Text = GetRowData(rowid, 15, "Label16"); //Наличие логики в БР             
            TextBox19.Text = GetRowData(rowid, 17, "Label17"); //Управление - исполнитель по логике  
            TextBox18.Text = "";
            if (GetRowData(rowid, 18, "Label18") != "" && GetRowData(rowid, 18, "Label18") != "-")
            {
                TextBox21.Text = Convert.ToDateTime(GetRowData(rowid, 18, "Label18")).ToString("yyyy-MM-dd"); //Плановая дата
            }
            else
            {
                TextBox21.Text = "";
            }
            TextBox30.Text = GetRowData(rowid, 19, "Label19"); //Время, завтра- ченное на реали- зацию процесса (передачу на тести- рование)
            TextBox31.Text = GetRowData(rowid, 20, "Label2"); //Превышение срока реализации(14 рабочих дней)
            TextBox32.Text = GetRowData(rowid, 21, "Label20"); //Время, затраченное на ввод в эксплуа- тацию после тести- рования (реали- зации) процесса 
            TextBox33.Text = GetRowData(rowid, 22, "Label21"); //Общее время от передачи на реали- зацию Логики до ввода в эксплуа- тацию
            string rowids = GetRowId(TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, DropDownList1.Text, strDropDownList2, TextBox10.Text, TextBox9.Text, TextBox12.Text, TextBox13.Text, TextBox14.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, TextBox18.Text, TextBox19.Text, TextBox21.Text);
            ViewState["row"] = rowids;
            Com = string.Format("SELECT ISINBR FROM LOGICS21 WHERE LOGROWID='" + rowids + "'");
            OleDbConnection con;
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = Com;
            cmd.Connection = con;
            con.Open();
            object obj = cmd.ExecuteScalar();
            ID = (string)Convert.ChangeType(obj, typeof(string));
            con.Close();
            TextBox18.Text = ID;
        }


        protected string GetRowId(string LOGNAME, string RUBRNAMESPD, string RUBRNAMEESRD, string IDDK, string DATEAR, string DOCNUM, string REACT, string ISNEW, string DOCNUMREACT, string REACTDATE, string GLPI, string TESTDATE, string TESTDOC, string CREATEDATE, string CREATEDOC, string COMMENTR, string ISINBR, string ISPUPR, string PLANDATE)
        {
            string[] data = new string[] { LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, ISNEW, DOCNUMREACT, REACTDATE, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, PLANDATE };
            for (int i = 0; i < data.Length; i++)
            {
                DateTime dDate;
                if (data[i] == "" || data[i] == "-")
                {
                    data[i] = " IS NULL";
                }
                else if ((DateTime.TryParse(data[i], out dDate)) && data[i].Length > 6)
                {
                    data[i] = "=TO_DATE('" + data[i] + "', 'yyyy.MM.dd')";
                }
                else
                {
                    data[i] = "='" + data[i] + "'";
                }
            }
            Com = string.Format("SELECT LOGROWID FROM LOGICS21 WHERE LOGNAME" + data[0] + " and RUBRNAMESPD" + data[1] + " and RUBRNAMEESRD" + data[2] + " and IDDK" + data[3] + " and DATEAR" + data[4] + " and DOCNUM" + data[5] + " and REACT" + data[6] + " and ISNEW" + data[7] + " and DOCNUMREACT" + data[8] + " and REACTDATE" + data[9] + " and GLPI" + data[10] + " and TESTDATE" + data[11] + " and TESTDOC" + data[12] + " and CREATEDATE" + data[13] + " and CREATEDOC" + data[14] + " and COMMENTR" + data[15] + " and ISPUPR" + data[17] + " and PLANDATE" + data[18]);
            OleDbConnection con;
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = Com;
            cmd.Connection = con;
            con.Open();
            object obj = cmd.ExecuteScalar();
            ID = (string)Convert.ChangeType(obj, typeof(string));
            con.Close();
            return ID;
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
                cmd.CommandText = "Select MAX(LOGROWID) FROM LOGICS21";
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
        protected string GetRowData(int rowid, int cell, string ctype)
        {
            string rowdata = "";
            if (ctype.Contains("Label"))
            {
                System.Web.UI.WebControls.Label logrow = (System.Web.UI.WebControls.Label)GridView2.Rows[rowid].Cells[cell].FindControl(ctype);
                rowdata = logrow.Text;

            }
            else if (ctype.Contains("HyperLink"))
            {
                System.Web.UI.WebControls.HyperLink logrow = (System.Web.UI.WebControls.HyperLink)GridView2.Rows[rowid].Cells[cell].FindControl(ctype);
                rowdata = logrow.Text;

            }
            return rowdata;
        }


        // кнопка "добавить"
        protected void Button4_Click(object sender, EventArgs e)
        {
            popupdiv.Visible = true;
            popupPanel.Visible = true;
            navpanel.Visible = false;
            GridView2.Visible = false;
            Button2.Visible = false;
            Button4.Visible = false;
            Button5.Visible = false;
            Button3.Visible = true;
            Button7.Visible = false;
        }

        protected void TrimTextbox()
        {
            // очищаю строчечки от лишних пробелов
            if (TextBox2.Text == "&nbsp;") // Название процесса в СПД
            {
                TextBox2.Text = "";
            }
            else
            {
                TextBox2.Text = TextBox2.Text.Trim();
            }

            if (TextBox3.Text == "&nbsp;") // Номер процесса в СПД
            {
                TextBox3.Text = "";
            }
            else
            {
                TextBox3.Text = TextBox3.Text.Trim();
            }

            if (TextBox4.Text == "&nbsp;") // Рубрика в ЕСРД/ДК:
            {
                TextBox4.Text = "";
            }
            else
            {
                TextBox4.Text = TextBox4.Text.Trim();
            }

            if (TextBox5.Text == "&nbsp;") //ID DK:
            {
                TextBox5.Text = "";
            }
            else
            {
                TextBox5.Text = TextBox5.Text.Trim();
            }

            if ((TextBox6.Text != "") && (TextBox6.Text != "&nbsp;")) //Дата получения логики:
            {
                TextBox6.Text = Convert.ToDateTime(TextBox6.Text.Replace("0:00:00", "").Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                TextBox6.Text = "";
            }

            if (TextBox7.Text == "&nbsp;") //Номер служебной записки, по которой логика направлялась
            {
                TextBox7.Text = "";
            }
            else
            {
                TextBox7.Text = TextBox7.Text.Trim();
            }

            if ((TextBox9.Text != "") && (TextBox9.Text != "&nbsp;")) //Дата ответной служебной записки Управления информатизации
            {
                TextBox9.Text = Convert.ToDateTime(TextBox9.Text.Replace("0:00:00", "").Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                TextBox9.Text = "";
            }

            if (TextBox10.Text == "&nbsp;") //Номер ответной служебной записки Управления информатизации:
            {
                TextBox10.Text = "";
            }
            else
            {
                TextBox10.Text = TextBox10.Text.Trim();
            }
            if (TextBox12.Text == "&nbsp;") // Номер GLPI:
            {
                TextBox12.Text = "";
            }
            else
            {
                TextBox12.Text = TextBox12.Text.Trim();
            }

            if ((TextBox13.Text != "") && (TextBox13.Text != "&nbsp;")) //Дата Акта передаче на тестирование
            {
                TextBox13.Text = Convert.ToDateTime(TextBox13.Text.Replace("0:00:00", "").Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                TextBox13.Text = "";
            }

            if (TextBox14.Text == "&nbsp;") //Номер Акта о передаче на тестирование
            {
                TextBox14.Text = "";
            }
            else
            {
                TextBox14.Text = TextBox14.Text.Trim();
            }

            if ((TextBox15.Text != "") && (TextBox15.Text != "&nbsp;")) //Дата Акта (служебной записки) о вводе в эксплуатацию
            {
                TextBox15.Text = Convert.ToDateTime(TextBox15.Text.Replace("0:00:00", "").Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                TextBox15.Text = "";
            }

            if (TextBox16.Text == "&nbsp;") //Номер Акта (служебной записки) о вводе в эксплуатацию
            {
                TextBox16.Text = "";
            }
            else
            {
                TextBox16.Text = TextBox16.Text.Trim();
            }

            if (TextBox17.Text == "&nbsp;") //Комментарий
            {
                TextBox17.Text = "";
            }
            else
            {
                TextBox17.Text = TextBox17.Text.Trim();
            }

            if (TextBox18.Text == "&nbsp;") //Наличие логики в БР 
            {
                TextBox18.Text = "";
            }
            else
            {
                TextBox18.Text = TextBox18.Text.Trim();
            }

            if (TextBox19.Text == "&nbsp;") //Управление-исполнитель по логике
            {
                TextBox19.Text = "";
            }
            else
            {
                TextBox19.Text = TextBox19.Text.Trim();
            }

            if ((TextBox21.Text != "") && (TextBox21.Text != "&nbsp;")) //Плановая дата
            {
                TextBox21.Text = Convert.ToDateTime(TextBox21.Text.Replace("0:00:00", "").Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                TextBox21.Text = "";
            }
        }

        //кнопка "добавить" для внесения в таблицу
        protected void Button3_Click(object sender, EventArgs e)
        {
            TrimTextbox();
            //а это высчитывает ожидаему дату реализации логики
            if ((CheckBox2.Checked) && (DropDownList1.Text == "Логика принята") && (TextBox9.Text != ""))
            {
                TextBox21.Text = Convert.ToString(AddWorkDays(Convert.ToDateTime(TextBox9.Text), 14));
            }
            // сам запрос инсерта    
            try
            {
                Com = string.Format("INSERT INTO LOGICS21 (LOGROWID, LOGNAME, RUBRNAMESPD, RUBRNAMEESRD, IDDK, DATEAR, DOCNUM, REACT, ISNEW, DOCNUMREACT, REACTDATE, GLPI, TESTDATE, TESTDOC, CREATEDATE, CREATEDOC, COMMENTR, ISINBR, ISPUPR, PLANDATE) VALUES (" + GetMaxID() + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "',TO_DATE('" + TextBox6.Text + "', 'yyyy-mm-dd'),'" + TextBox7.Text + "','" + DropDownList1.Text + "','" + DropDownList2.Text + "','" + TextBox10.Text + "',TO_DATE('" + TextBox9.Text + "', 'yyyy-mm-dd'),'" + TextBox12.Text + "',TO_DATE('" + TextBox13.Text + "', 'yyyy-mm-dd'),'" + TextBox14.Text + "',TO_DATE('" + TextBox15.Text + "', 'yyyy-mm-dd'),'" + TextBox16.Text + "','" + TextBox17.Text + "','" + TextBox18.Text + "','" + TextBox19.Text + "'," + "TO_DATE('" + TextBox21.Text + "', 'dd.mm.yyyy hh24:mi:ss'))");
                DoEditTable(Com);
                SaveLogs(Com);
                ClearTable();
            }
            catch (Exception ex)
            {
                Err.Text = Convert.ToString(ex);
            }
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            FillGrid();
        }

        //кнопка "апдейт"
        protected void Button5_Click(object sender, EventArgs e)
        {
            string PLANDATE = "";
            string LOGNAME = TextBox2.Text;
            string RUBRNAMESPD = TextBox3.Text;
            string RUBRNAMEESRD = TextBox4.Text;
            string IDDK = TextBox5.Text;
            string DATEAR = TextBox6.Text;
            string DOCNUM = TextBox7.Text;
            string REACT = DropDownList1.Text;
            string ISNEW = DropDownList2.Text;
            string DOCNUMREACT = TextBox10.Text;
            string REACTDATE = TextBox9.Text;
            string GLPI = TextBox12.Text;
            string TESTDATE = TextBox13.Text;
            string TESTDOC = TextBox14.Text;
            string CREATEDATE = TextBox15.Text;
            string CREATEDOC = TextBox16.Text;
            string COMMENTR = TextBox17.Text;
            string ISINBR = TextBox18.Text;
            string ISPUPR = TextBox19.Text;
            if ((CheckBox2.Checked) && (DropDownList1.Text == "Логика принята") && (TextBox9.Text != ""))
            {
                TextBox21.Text = Convert.ToString(AddWorkDays(Convert.ToDateTime(TextBox9.Text), 14));
                PLANDATE = TextBox21.Text;
            }
            else if (DropDownList1.Text == "Логика принята")
            {
                PLANDATE = Convert.ToString((Convert.ToDateTime(TextBox21.Text)));
            }
            else PLANDATE = "";

            Com = string.Format("UPDATE LOGICS21 SET LOGNAME='" + LOGNAME + "',RUBRNAMESPD='" + RUBRNAMESPD + "',RUBRNAMEESRD='" + RUBRNAMEESRD + "',IDDK='" + IDDK + "',DATEAR=TO_DATE('" + DATEAR + "', 'yyyy-mm-dd'),DOCNUM='" + DOCNUM + "',REACT='" + REACT + "',ISNEW='" + ISNEW + "',DOCNUMREACT='" + DOCNUMREACT + "',REACTDATE=TO_DATE('" + REACTDATE + "', 'yyyy-mm-dd'), GLPI='" + GLPI + "',TESTDATE=TO_DATE('" + TESTDATE + "', 'yyyy-mm-dd'),TESTDOC='" + TESTDOC + "',CREATEDATE=TO_DATE('" + CREATEDATE + "', 'yyyy-mm-dd'),CREATEDOC='" + CREATEDOC + "',COMMENTR='" + COMMENTR + "',ISINBR='" + ISINBR + "',ISPUPR='" + ISPUPR + "',PLANDATE=TO_DATE('" + PLANDATE + "', 'dd.mm.yyyy hh24:mi:ss') WHERE LOGROWID=" + Convert.ToString(ViewState["row"]) + "");
            DoEditTable(Com);
            SaveLogs(Com);
            ClearTable();
        }

        //выгрузка
        protected void Button7_Click(object sender, EventArgs e)
        {
            //ExportToExcel();
            GridView2.AllowPaging = false;
            GridView2.Columns.Remove(GridView2.Columns[23]);
            FillGrid();
            Response.Clear();
            Response.Buffer = true;
            string ExtractionDateTime = Convert.ToString(DateTime.Now);
            string temp_subdir = "Temp\\";
            Response.AddHeader("content-disposition", "attachment;filename=_LogicsRegistry_" + ExtractionDateTime.Replace(" ", "_").Replace(":", " - ") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                    cell.Height = 100;
                    cell.BorderStyle = BorderStyle.Solid;
                    cell.BorderWidth = 1;
                    cell.BorderColor = Color.Black;

                    System.Web.UI.WebControls.TextBox Head = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox23");
                    System.Web.UI.WebControls.TextBox SPD = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox24");
                    System.Web.UI.WebControls.TextBox ESRD = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox25");
                    System.Web.UI.WebControls.TextBox DK = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox26");
                    System.Web.UI.WebControls.TextBox DATEAR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox27");
                    System.Web.UI.WebControls.TextBox ARNUM = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox28");
                    DropDownList REACT = (DropDownList)GridView2.HeaderRow.FindControl("DRL1");
                    DropDownList ISNEW = (DropDownList)GridView2.HeaderRow.FindControl("DRL4");
                    System.Web.UI.WebControls.TextBox REACTDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox29");
                    System.Web.UI.WebControls.TextBox DOCNUMREACT = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox30");
                    System.Web.UI.WebControls.TextBox GLPI = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox31");
                    System.Web.UI.WebControls.TextBox TESTDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox32");
                    System.Web.UI.WebControls.TextBox TESTDOC = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox33");
                    System.Web.UI.WebControls.TextBox CREATEDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox34");
                    System.Web.UI.WebControls.TextBox CREATEDOC = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox35");
                    System.Web.UI.WebControls.TextBox COMMENTR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox36");
                    DropDownList ISINBR = (DropDownList)GridView2.HeaderRow.FindControl("DRL2");
                    System.Web.UI.WebControls.TextBox ISPUPR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox38");
                    System.Web.UI.WebControls.TextBox PLANDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox39");
                    System.Web.UI.WebControls.TextBox daypasstest = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox40");
                    DropDownList TIMEPASS = (DropDownList)GridView2.HeaderRow.FindControl("DRL3");
                    System.Web.UI.WebControls.TextBox createdaypass = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox41");
                    System.Web.UI.WebControls.TextBox daystaken = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox42");

                    Head.Visible = false;
                    SPD.Visible = false;
                    ESRD.Visible = false;
                    DK.Visible = false;
                    DATEAR.Visible = false;
                    ARNUM.Visible = false;
                    REACT.Visible = false;
                    ISNEW.Visible = false;
                    REACTDATE.Visible = false;
                    DOCNUMREACT.Visible = false;
                    GLPI.Visible = false;
                    TESTDATE.Visible = false;
                    TESTDOC.Visible = false;
                    CREATEDATE.Visible = false;
                    CREATEDOC.Visible = false;
                    COMMENTR.Visible = false;
                    ISINBR.Visible = false;
                    ISPUPR.Visible = false;
                    PLANDATE.Visible = false;
                    daypasstest.Visible = false;
                    TIMEPASS.Visible = false;
                    createdaypass.Visible = false;
                    daystaken.Visible = false;
                }
                GridView2.HeaderRow.Cells[0].Text = "Название процесса в СПД";
                GridView2.HeaderRow.Cells[1].Text = "Номер процесса в СПД";
                GridView2.HeaderRow.Cells[2].Text = "Рубрика в ЕСРД/ДК";
                GridView2.HeaderRow.Cells[3].Text = "ID DK";
                GridView2.HeaderRow.Cells[4].Text = "Дата получения логики";
                GridView2.HeaderRow.Cells[5].Text = "Номер служебной записки, по которой логика направлялась";
                GridView2.HeaderRow.Cells[6].Text = "Реакция Управления информатизации";
                GridView2.HeaderRow.Cells[7].Text = "Реакция Управления информатизации";
                GridView2.HeaderRow.Cells[8].Text = "Дата ответной служебной записки Управления информатизации";
                GridView2.HeaderRow.Cells[9].Text = "Номер ответной служебной записки Управления информатизации";
                GridView2.HeaderRow.Cells[10].Text = "Номер GLPI";
                GridView2.HeaderRow.Cells[11].Text = "Дата Акта о передаче на тестирование (дата реализации процесса)";
                GridView2.HeaderRow.Cells[12].Text = "Номер Акта о передаче на тестирование";
                GridView2.HeaderRow.Cells[13].Text = "Дата Акта (служебной записки) о вводе в эксплуатацию";
                GridView2.HeaderRow.Cells[14].Text = "Номер Акта (служебной записки) о вводе в эксплуатацию";
                GridView2.HeaderRow.Cells[15].Text = "Комментарий";
                GridView2.HeaderRow.Cells[16].Text = "Наличие логики в БР";
                GridView2.HeaderRow.Cells[17].Text = "Управление - исполнитель по логике";
                GridView2.HeaderRow.Cells[18].Text = "Плановая дата";
                GridView2.HeaderRow.Cells[19].Text = "Время, завтраченное на реализацию процесса (передачу на тестирование)";
                GridView2.HeaderRow.Cells[20].Text = "Статус реализации (Норматив - 14 рабочих дней):";
                GridView2.HeaderRow.Cells[21].Text = "Время, затраченное на ввод в эксплуатацию после тестирования (реализации) процесса";
                GridView2.HeaderRow.Cells[22].Text = "Общее время от передачи на реализацию Логики до ввода в эксплуатацию";

                foreach (GridViewRow row in GridView2.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        {
                            cell.BorderStyle = BorderStyle.Solid;
                            cell.BorderWidth = 1;
                            cell.BorderColor = Color.Black;
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                        List<Control> controls = new List<Control>();

                        //Add controls to be removed to Generic List
                        foreach (Control control in cell.Controls)
                        {
                            controls.Add(control);
                        }

                        //Loop through the controls to be removed and replace then with Literal
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "HyperLink":
                                    cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                    break;
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as System.Web.UI.WebControls.TextBox).Text });
                                    break;
                                case "LinkButton":
                                    cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                    break;
                                case "CheckBox":
                                    cell.Controls.Add(new Literal { Text = (control as System.Web.UI.WebControls.CheckBox).Text });
                                    break;
                                case "RadioButton":
                                    cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                    break;
                                case "Label":
                                    cell.Controls.Add(new Literal { Text = (control as System.Web.UI.WebControls.Label).Text });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }
                if (GridView2.Controls.Count > 0)
                {
                    GridView2.RenderControl(hw);
                }
                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                string file = temp_subdir + "_LogicsRegistry_" + ExtractionDateTime.Replace(" ", "_").Replace(":", "-") + ".xls";
                ReturnResponse(file);
                SaveLogs("Выполнена выгрузка " + file);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        private void ReturnResponse(string file)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/word";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            Response.WriteFile(file);
            Response.End();
            Response.Close();
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + file);
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lnk2 = (HyperLink)e.Row.FindControl("HyperLink2");
                if (((DataRowView)e.Row.DataItem)["ISINBR"].ToString() != "")
                {
                    if (((DataRowView)e.Row.DataItem)["ISINBR"].ToString().ToLower() == "да")
                    {
                        lnk2.NavigateUrl = "";
                        lnk2.Text = "Да, необходимо указать ссылку";
                    }
                    else
                    {
                        lnk2.NavigateUrl = ((DataRowView)e.Row.DataItem)["ISINBR"].ToString();
                        lnk2.Text = "Да";
                    }
                }
                else
                {
                    lnk2.NavigateUrl = "";
                    lnk2.Text = "Нет";
                }

                var lnk3 = (HyperLink)e.Row.FindControl("HyperLink3");
                if (((DataRowView)e.Row.DataItem)["DOCNUM"].ToString() != "")
                {
                    if (((DataRowView)e.Row.DataItem)["DOCNUM"].ToString().Contains("ДГИ") || ((DataRowView)e.Row.DataItem)["DOCNUM"].ToString().Contains("согл"))
                    {
                        string text = ((DataRowView)e.Row.DataItem)["DOCNUM"].ToString();
                        var win1251 = Encoding.GetEncoding("windows-1251");
                        var strDec = HttpUtility.UrlEncode(text, Encoding.GetEncoding(1251));
                        lnk3.NavigateUrl = "https://mosedo.ru/?DNSID=vLljms3OUNg0JuiAizNxXg&frmsearch=get&search=" + strDec + "&find=%CD%E0%E9%F2%E8";
                        lnk3.Text = ((DataRowView)e.Row.DataItem)["DOCNUM"].ToString();
                    }
                    else
                    {
                        lnk3.NavigateUrl = "";
                        lnk3.Text = ((DataRowView)e.Row.DataItem)["DOCNUM"].ToString();
                    }
                }
                else
                {
                    lnk3.NavigateUrl = "";
                    lnk3.Text = "";
                }
                var lnk4 = (HyperLink)e.Row.FindControl("HyperLink4");
                if (((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString() != "")
                {
                    if (((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString().Contains("ДГИ") || ((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString().Contains("согл"))
                    {
                        string text = ((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString();
                        var win1251 = Encoding.GetEncoding("windows-1251");
                        var strDec = HttpUtility.UrlEncode(text, Encoding.GetEncoding(1251));
                        lnk4.NavigateUrl = "https://mosedo.ru/?DNSID=vLljms3OUNg0JuiAizNxXg&frmsearch=get&search=" + strDec + "&find=%CD%E0%E9%F2%E8";
                        lnk4.Text = ((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString();
                    }
                    else
                    {
                        lnk4.NavigateUrl = "";
                        lnk4.Text = ((DataRowView)e.Row.DataItem)["DOCNUMREACT"].ToString();
                    }
                }
                else
                {
                    lnk4.NavigateUrl = "";
                    lnk4.Text = "";
                }
                var lnk5 = (HyperLink)e.Row.FindControl("HyperLink5");
                if (((DataRowView)e.Row.DataItem)["TESTDOC"].ToString() != "")
                {
                    if (((DataRowView)e.Row.DataItem)["TESTDOC"].ToString().Contains("ДГИ") || ((DataRowView)e.Row.DataItem)["TESTDOC"].ToString().Contains("согл"))
                    {
                        string text = ((DataRowView)e.Row.DataItem)["TESTDOC"].ToString();
                        var win1251 = Encoding.GetEncoding("windows-1251");
                        var strDec = HttpUtility.UrlEncode(text, Encoding.GetEncoding(1251));
                        lnk5.NavigateUrl = "https://mosedo.ru/?DNSID=vLljms3OUNg0JuiAizNxXg&frmsearch=get&search=" + strDec + "&find=%CD%E0%E9%F2%E8";
                        lnk5.Text = ((DataRowView)e.Row.DataItem)["TESTDOC"].ToString();
                    }
                    else
                    {
                        lnk5.NavigateUrl = "";
                        lnk5.Text = ((DataRowView)e.Row.DataItem)["TESTDOC"].ToString();
                    }
                }
                else
                {
                    lnk5.NavigateUrl = "";
                    lnk5.Text = "";
                }
                var lnk6 = (HyperLink)e.Row.FindControl("HyperLink6");
                if (((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString() != "")
                {
                    if (((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString().Contains("ДГИ") || ((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString().Contains("согл"))
                    {
                        string text = ((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString();
                        var win1251 = Encoding.GetEncoding("windows-1251");
                        var strDec = HttpUtility.UrlEncode(text, Encoding.GetEncoding(1251));
                        lnk6.NavigateUrl = "https://mosedo.ru/?DNSID=vLljms3OUNg0JuiAizNxXg&frmsearch=get&search=" + strDec + "&find=%CD%E0%E9%F2%E8";
                        lnk6.Text = ((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString();
                    }
                    else
                    {
                        lnk6.NavigateUrl = "";
                        lnk6.Text = ((DataRowView)e.Row.DataItem)["CREATEDOC"].ToString();
                    }
                }
                else
                {
                    lnk6.NavigateUrl = "";
                    lnk6.Text = "";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((DataBinder.Eval(e.Row.DataItem, "REACT") as string) == "Логика не принята")
                {

                    e.Row.CssClass = "SEDO2";
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#d0c6c6'";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#eedfdf'";
                }
                else
                {
                    if ((DataBinder.Eval(e.Row.DataItem, "TIMEPASS") as string) == "Выполнено в срок")
                    {
                        e.Row.CssClass = "CompletedRowStyle";
                        e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#c0cca9'";
                        e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#e3f1c7'";
                    }
                    else if ((DataBinder.Eval(e.Row.DataItem, "TIMEPASS") as string) == "Выполнено с нарушением срока")
                    {
                        e.Row.CssClass = "RegAndCorrected";
                        e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#dbd499'";
                        e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#fff7b4'";
                    }
                    else if ((DataBinder.Eval(e.Row.DataItem, "TIMEPASS") as string) == "Превышен срок реализации")
                    {
                        e.Row.CssClass = "RegAndError";
                        e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#d1a29c'";
                        e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#ffc6bf'";
                    }
                    else
                    {
                        e.Row.CssClass = "SEDO";
                        e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#d1d1d1'";
                        e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='#ededed'";
                    }
                }
                //    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#d9e8f7'";
            }
        }

        //фильтр
        protected void Button8_Click(object sender, EventArgs e)
        {
            string filtr = "";
            System.Web.UI.WebControls.TextBox Head = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox23");
            System.Web.UI.WebControls.TextBox SPD = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox24");
            System.Web.UI.WebControls.TextBox ESRD = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox25");
            System.Web.UI.WebControls.TextBox DK = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox26");
            System.Web.UI.WebControls.TextBox DATEAR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox27");
            System.Web.UI.WebControls.TextBox ARNUM = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox28");
            DropDownList REACT = (DropDownList)GridView2.HeaderRow.FindControl("DRL1");
            DropDownList ISNEW = (DropDownList)GridView2.HeaderRow.FindControl("DRL4");
            System.Web.UI.WebControls.TextBox REACTDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox29");
            System.Web.UI.WebControls.TextBox DOCNUMREACT = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox30");
            System.Web.UI.WebControls.TextBox GLPI = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox31");
            System.Web.UI.WebControls.TextBox TESTDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox32");
            System.Web.UI.WebControls.TextBox TESTDOC = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox33");
            System.Web.UI.WebControls.TextBox CREATEDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox34");
            System.Web.UI.WebControls.TextBox CREATEDOC = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox35");
            System.Web.UI.WebControls.TextBox COMMENTR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox36");
            DropDownList ISINBR = (DropDownList)GridView2.HeaderRow.FindControl("DRL2");
            System.Web.UI.WebControls.TextBox ISPUPR = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox38");
            System.Web.UI.WebControls.TextBox PLANDATE = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox39");
            System.Web.UI.WebControls.TextBox daypasstest = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox40");
            DropDownList TIMEPASS = (DropDownList)GridView2.HeaderRow.FindControl("DRL3");
            System.Web.UI.WebControls.TextBox createdaypass = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox41");
            System.Web.UI.WebControls.TextBox daystaken = (System.Web.UI.WebControls.TextBox)GridView2.HeaderRow.FindControl("Textbox42");
            string Headstr = Head.Text.Trim();
            string SPDstr = SPD.Text.Trim();
            string ESRDstr = ESRD.Text.Trim();
            string DKstr = DK.Text.Trim();
            string DATEARstr = DATEAR.Text.Trim();
            string ARNUMstr = ARNUM.Text.Trim();
            string REACTstr = REACT.Text.Trim();
            string ISNEWstr = ISNEW.Text.Trim();
            string REACTDATEstr = REACTDATE.Text.Trim();
            string DOCNUMREACTstr = DOCNUMREACT.Text.Trim();
            string GLPIstr = GLPI.Text.Trim();
            string TESTDATEstr = TESTDATE.Text.Trim();
            string TESTDOCstr = TESTDOC.Text.Trim();
            string CREATEDATEstr = CREATEDATE.Text.Trim();
            string CREATEDOCstr = CREATEDOC.Text.Trim();
            string COMMENTRstr = COMMENTR.Text.Trim();
            string ISINBRstr = ISINBR.Text.Trim();
            string ISPUPRstr = ISPUPR.Text.Trim();
            string PLANDATEstr = PLANDATE.Text.Trim();
            string daypassteststr = daypasstest.Text.Trim();
            string TIMEPASSstr = TIMEPASS.Text.Trim();
            string createdaypassstr = createdaypass.Text.Trim();
            string daystakenstr = daystaken.Text.Trim();
            bool empty = true;
            string a = "";
            if ((Headstr != "") || (SPDstr != "") || (ESRDstr != "") || (DKstr != "") || (DATEARstr != "") || (ARNUMstr != "") || (REACTstr != "") || (ISNEWstr != "") || (REACTDATEstr != "") || (DOCNUMREACTstr != "") || (GLPIstr != "") || (TESTDATEstr != "") || (TESTDOCstr != "") || (CREATEDATEstr != "") || (CREATEDOCstr != "") || (COMMENTRstr != "") || (ISINBRstr != "") || (ISPUPRstr != "") || (PLANDATEstr != "") || (daypassteststr != "") || (TIMEPASSstr != "") || (createdaypassstr != "") || (daystakenstr != ""))
            {
                DateTime dateValue;
                if (Headstr != "")
                {
                    filtr = "Название процесса в СПД: " + Headstr +";"+ Environment.NewLine;
                    Headstr = " AND LOGNAME LIKE '%" + Headstr + "%' ";
                    a = a + Headstr;
                }

                if (SPDstr != "")
                {
                    filtr = filtr + "Номер процесса в СПД: " + SPDstr + ";" + Environment.NewLine;
                    SPDstr = " AND RUBRNAMESPD LIKE '%" + SPDstr + "%' ";
                    a = a + SPDstr;
                }

                if (ESRDstr != "")
                {
                    filtr = filtr+  "Рубрика в ЕСРД/ДК: " + ESRDstr + ";" + Environment.NewLine;
                    ESRDstr = " AND RUBRNAMEESRD LIKE '%" + ESRDstr + "%' ";
                    a = a + ESRDstr;
                }
                if (DKstr != "")
                {
                    filtr = filtr + "ID DK: " + DKstr + ";" + Environment.NewLine;
                    DKstr = " AND IDDK LIKE '%" + DKstr + "%' ";
                    a = a + DKstr;
                }

                if (DATEARstr != "") 
                {
                    DateTime dt = DateTime.ParseExact(DATEARstr, "yyyy-MM-d", CultureInfo.InvariantCulture);
                    DATEARstr = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    filtr = filtr + "Дата получения логики: " + DATEARstr + ";" + Environment.NewLine;
                    if (DateTime.TryParse(DATEARstr, out dateValue))
                    {
                        DATEARstr = " AND DATEAR  = to_date('" + DATEARstr + " ', 'dd.mm.yyyy') ";
                        a = a + DATEARstr;
                    }
                }
                if (ARNUMstr != "")
                {
                    filtr = filtr + "Номер служебной записки, по которой логика направ- лялась: " + ARNUMstr + ";" + Environment.NewLine;
                    ARNUMstr = " AND DOCNUM LIKE '%" + ARNUMstr + "%' ";
                    a = a + ARNUMstr;
                }

                if (REACTstr != "")
                {
                    filtr = filtr + "Реакция Управления информатизации: " + REACTstr + ";" + Environment.NewLine;
                    REACTstr = " AND REACT LIKE '%" + REACTstr + "%' ";
                    a = a + REACTstr;
                }

                if (ISNEWstr != "")
                {
                    filtr = filtr + "Тип логики процесса: " + ISNEWstr + ";" + Environment.NewLine;
                    ISNEWstr = " AND ISNEW LIKE '%" + ISNEWstr + "%' ";
                    a = a + ISNEWstr;
                }
                if (REACTDATEstr != "") 
                {
                    DateTime dt = DateTime.ParseExact(REACTDATEstr, "yyyy-MM-d", CultureInfo.InvariantCulture);
                    REACTDATEstr = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    filtr = filtr + "Дата ответной служебной записки Управления информатизации: " + REACTDATEstr + ";" + Environment.NewLine;
                    if (DateTime.TryParse(REACTDATEstr, out dateValue))
                    {
                        REACTDATEstr = " AND REACTDATE  = to_date('" + REACTDATEstr + " ', 'dd.mm.yyyy') ";
                        a = a + REACTDATEstr;
                    }
                }
                if (DOCNUMREACTstr != "")
                {
                    filtr = filtr + "Номер ответной служебной записки Управления информатизации: " + DOCNUMREACTstr + ";" + Environment.NewLine;
                    DOCNUMREACTstr = " AND DOCNUMREACT LIKE '%" + DOCNUMREACTstr + "%' ";
                    a = a + DOCNUMREACTstr;
                }
                if (GLPIstr != "")
                {
                    filtr = filtr + "Номер GLPI: " + GLPIstr + ";" + Environment.NewLine;
                    GLPIstr = " AND GLPI LIKE '%" + GLPIstr + "%' ";
                    a = a + GLPIstr;
                }
                if (TESTDATEstr != "") 
                {
                    DateTime dt = DateTime.ParseExact(TESTDATEstr, "yyyy-MM-d", CultureInfo.InvariantCulture);
                    TESTDATEstr = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    filtr = filtr + "Дата Акта о передаче на тестирование (дата реализации процесса): " + TESTDATEstr + ";" + Environment.NewLine;
                    if (DateTime.TryParse(TESTDATEstr, out dateValue))
                        {
                        TESTDATEstr = " AND  TESTDATE = to_date('" + TESTDATEstr + " ', 'dd.mm.yyyy') ";
                        a = a + TESTDATEstr;
                    }
                }
                if (TESTDOCstr != "")
                {
                    filtr = filtr + "Номер Акта о передаче на тестирование: " + TESTDOCstr + ";" + Environment.NewLine;
                    TESTDOCstr = " AND TESTDOC LIKE '%" + TESTDOCstr + "%' ";
                    a = a + TESTDOCstr;
                }
                if (CREATEDATEstr != "") 
                {
                    DateTime dt = DateTime.ParseExact(CREATEDATEstr, "yyyy-MM-d", CultureInfo.InvariantCulture);
                    CREATEDATEstr = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    filtr = filtr + "Дата Акта (слу- жебной записки) о вводе в эксплуатацию: " + CREATEDATEstr + ";" + Environment.NewLine;
                    if (DateTime.TryParse(CREATEDATEstr, out dateValue))
                    {
                        CREATEDATEstr = " AND CREATEDATE = to_date('" + CREATEDATEstr + " ', 'dd.mm.yyyy') ";
                        a = a + CREATEDATEstr;
                    }
                }
                if (CREATEDOCstr != "")
                {
                    filtr = filtr + "Номер Акта (служебной записки) о вводе в эксплуатацию: " + CREATEDOCstr + ";" + Environment.NewLine;
                    CREATEDOCstr = " AND CREATEDOC LIKE '%" + CREATEDOCstr + "%' ";
                    a = a + CREATEDOCstr;
                }
                if (COMMENTRstr != "")
                {
                    filtr = filtr + "Комментарий: " + COMMENTRstr + ";" + Environment.NewLine;
                    COMMENTRstr = " AND  COMMENTR LIKE '%" + COMMENTRstr + "%' ";
                    a = a + COMMENTRstr;
                }
                if (ISINBRstr == "Есть")
                {
                    filtr = filtr + "Наличие логики в БР: " + ISINBRstr + ";" + Environment.NewLine;
                    ISINBRstr = " AND ISINBR IS NOT NULL ";
                    a = a + ISINBRstr;
                }
                if (ISINBRstr == "Нет")
                {
                    filtr = filtr + "Наличие логики в БР: " + ISINBRstr + ";" + Environment.NewLine;
                    ISINBRstr = " AND ISINBR IS NULL ";
                    a = a + ISINBRstr;
                }
                if (ISPUPRstr != "")
                {
                    filtr = filtr + "Управление - исполнитель по логике: " + ISPUPRstr + ";" + Environment.NewLine;
                    ISPUPRstr = " AND ISPUPR LIKE '%" + ISPUPRstr + "%' ";
                    a = a + ISPUPRstr;
                }
                if (PLANDATEstr != "") 
                {
                    DateTime dt = DateTime.ParseExact(PLANDATEstr, "yyyy-MM-d", CultureInfo.InvariantCulture);
                    PLANDATEstr = dt.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                    filtr = filtr + "Плановая дата: " + PLANDATEstr + ";" + Environment.NewLine;
                    if (DateTime.TryParse(PLANDATEstr, out dateValue))
                    {
                        PLANDATEstr = " AND PLANDATE= to_date('" + PLANDATEstr + " ', 'dd.mm.yyyy') ";
                        a = a + PLANDATEstr;
                    }
                }

                /*  if (daypassteststr != "")
                  {
                      daypassteststr = " AND  daypasstest LIKE '%" + daypassteststr + "%'";
                      a = a + daypassteststr;
                  }
                 */

                if (TIMEPASSstr != "")
                {
                    filtr = filtr + "Время, завтраченное на реализацию процесса (передачу на тестирование): " + TIMEPASSstr + ";" + Environment.NewLine;
                    TIMEPASSstr = " AND  TIMEPASS LIKE '%" + TIMEPASSstr + "%' ";
                    a = a + TIMEPASSstr;

                }
                /*  if (createdaypassstr != "")
                  {
                      createdaypassstr = " AND  createdaypass LIKE '%" + createdaypassstr + "%'";
                      a = a + createdaypassstr;
                  }
                  if (daystakenstr != "")
                  {
                      daystakenstr = " AND daystaken LIKE '%" + daystakenstr + "%'";
                      a = a + daystakenstr;
                  }    */

                empty = false;
            }
            else
            {
                empty = true;
            }

            if (!empty)
            {
                Com = standartCom + a + StandartOrderBy;
            }
            else
            {
                Com = standartCom + StandartOrderBy;
            }
            Command(Com);
            if (GridView2.Rows.Count == 0)
            {
                Label12.Visible = true;
            }
            else
            {
                Label12.Visible = false;
            }
            Labfiltr.Text = filtr;
           // Session["Filtr"] = a;
        }

        private void SaveLogs(string message)
        {
            message = message.Replace("'", "");
            int ID;
            //считаю кол-во записей (айдишников)
            OleDbConnection con;
            OleDbCommand cmd = new OleDbCommand();
            con = new OleDbConnection(ConfigurationSettings.AppSettings["connect"]);
            cmd.CommandText = "Select MAX(idlog) FROM logicslogs";
            cmd.Connection = con;
            con.Open();
            object obj = cmd.ExecuteScalar();
            ID = (int)Convert.ChangeType(obj, typeof(int));
            con.Close();
            ID++;

            string command = "Insert into logicslogs values (" + ID + ", '" + HttpContext.Current.Session["ActLogin"] + "', to_date('" + Convert.ToString(DateTime.Now) + "', 'dd.MM.yyyy hh24:mi:ss'), '" + message + "')";
            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.CommandText = Com;
            cmd2.Connection = con;
            con.Open();
            DoEditTable(command);
            con.Close();
        }

        /*
private void ExportToExcel()
{
   Excel.Application exApp = new Excel.Application();
   exApp.Visible = false;
   Excel._Workbook rBook = exApp.Workbooks.Add();
   Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
   workSheet.Cells.ColumnWidth = 25;
   workSheet.Cells[1, 1] = "Название процесса в СПД";
   workSheet.Cells[1, 2] = "Номер процесса в СПД";
   workSheet.Cells[1, 3] = "Рубрика в ЕСРД/ДК";
   workSheet.Cells[1, 4] = "ID DK";
   workSheet.Cells[1, 5] = "Дата получения логики";
   workSheet.Cells[1, 6] = "Номер служебной записки, по которой логика направлялась";
   workSheet.Cells[1, 7] = "Реакция Управления информатизации";            
   workSheet.Cells[1, 8] = "Дата ответной служебной записки Управления информатизации";
   workSheet.Cells[1, 9] = "Номер ответной служебной записки Управления информатизации";
   workSheet.Cells[1, 10] = "Номер GLPI";
   workSheet.Cells[1, 11] = "Дата Акта передаче на тестирование (дата реализации процесса)";
   workSheet.Cells[1, 12] = "Номер Акта о передаче на тестирование";
   workSheet.Cells[1, 13] = "Дата Акта (служебной записки) о вводе в эксплуатацию";
   workSheet.Cells[1, 14] = "Номер Акта (служебной записки) о вводе в эксплуатацию";
   workSheet.Cells[1, 15] = "Комментарий";
   workSheet.Cells[1, 16] = "Наличие логики в БР";
   workSheet.Cells[1, 17] = "Управление - исполнитель по логике";
   workSheet.Cells[1, 18] = "Плановая дата";
   workSheet.Cells[1, 19] = "Время, завтраченное на реализацию процесса (передачу на тестирование)";
   workSheet.Cells[1, 20] = "Превышение срока реализации (14 рабочих дней)";
   workSheet.Cells[1, 21] = "Время, затраченное на ввод в эксплуатацию после тестирования (реализации) процесса";
   workSheet.Cells[1, 21] = "Общее время от передачи на реализацию Логики до ввода в эксплуатацию";
   int rowExcel = 2;
   for (int i = 0; i < GridView2.Rows.Count; i++)
   {
       workSheet.Cells[rowExcel, "A"] = GridView2.Rows[i].Cells[0].Text;
       workSheet.Cells[rowExcel, "B"] = GridView2.Rows[i].Cells[1].Text;
       workSheet.Cells[rowExcel, "C"] = GridView2.Rows[i].Cells[2].Text;
       workSheet.Cells[rowExcel, "D"] = GridView2.Rows[i].Cells[3].Text;
       workSheet.Cells[rowExcel, "E"] = GridView2.Rows[i].Cells[4].Text;
       workSheet.Cells[rowExcel, "F"] = GridView2.Rows[i].Cells[5].Text;
       workSheet.Cells[rowExcel, "G"] = GridView2.Rows[i].Cells[6].Text;
       workSheet.Cells[rowExcel, "H"] = GridView2.Rows[i].Cells[7].Text;
       workSheet.Cells[rowExcel, "I"] = GridView2.Rows[i].Cells[8].Text;
       workSheet.Cells[rowExcel, "J"] = GridView2.Rows[i].Cells[9].Text;
       workSheet.Cells[rowExcel, "K"] = GridView2.Rows[i].Cells[10].Text;
       workSheet.Cells[rowExcel, "L"] = GridView2.Rows[i].Cells[11].Text;
       workSheet.Cells[rowExcel, "M"] = GridView2.Rows[i].Cells[12].Text;
       workSheet.Cells[rowExcel, "N"] = GridView2.Rows[i].Cells[13].Text;
       workSheet.Cells[rowExcel, "O"] = GridView2.Rows[i].Cells[14].Text;
       workSheet.Cells[rowExcel, "P"] = GridView2.Rows[i].Cells[15].Text;
       workSheet.Cells[rowExcel, "Q"] = GridView2.Rows[i].Cells[16].Text;
       workSheet.Cells[rowExcel, "R"] = GridView2.Rows[i].Cells[17].Text;
       workSheet.Cells[rowExcel, "S"] = GridView2.Rows[i].Cells[18].Text;
       workSheet.Cells[rowExcel, "T"] = GridView2.Rows[i].Cells[19].Text;
       workSheet.Cells[rowExcel, "U"] = GridView2.Rows[i].Cells[20].Text;
       workSheet.Cells[rowExcel, "V"] = GridView2.Rows[i].Cells[21].Text;
       ++rowExcel;
   }
   string ExtractionDateTime = Convert.ToString(DateTime.Now);
   string temp_subdir = "Temp\\";
   string file = temp_subdir + "_LogicsRegistry_" + ExtractionDateTime.Replace(" ", "_").Replace(":", "-") + ".xls";
   rBook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + file);
   rBook.Close(false);
   exApp.Quit();
   ReturnResponse(file);
}
*/
    }
}


