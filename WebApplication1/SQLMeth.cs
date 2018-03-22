using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Globalization;

namespace ASGuf2.DAL
{
    class SqlMeth
    {
        public OracleCommand OraQueryProcFunc(OracleConnection conn, string FuncName, 
            OracleParameter[] p, OracleTransaction trans)
        {
            OracleCommand ComOra = new OracleCommand(FuncName, conn);
            ComOra.Transaction = trans;
            ComOra.CommandType = CommandType.StoredProcedure;
            ComOra.Connection = conn;
            ComOra.Parameters.AddRange(p);
            ComOra.ExecuteNonQuery();
            return ComOra;
        }

        public DataTable GetCursor(OracleConnection conn, OracleTransaction tran, string query)
        {
            OracleCommand ComOra = new OracleCommand(query, conn);
            ComOra.Transaction = tran;
            ComOra.Connection = conn;
            OracleDataReader reader = ComOra.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader, LoadOption.PreserveChanges);
            return dt;
        }

        public string OraQuery(OracleConnection conn, OracleTransaction tran, string Command)
        {
            OracleCommand ComOra = new OracleCommand(Command, conn, tran);
            object result = ComOra.ExecuteScalar();
            return !result.Equals(DBNull.Value) ? (string)result : "";
        }

        public DataTable GetGrid(OracleConnection conn, OracleTransaction tran, string GridName)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.GetGrid" + GridName, p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string GetFullNameFromLogin(OracleConnection conn, OracleTransaction tran, string Login)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("Login", OracleType.VarChar); p[1].Value = Login; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2.asguf2_pkg.GetFullNameFromLogin", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string GetPositionByLogin(OracleConnection conn, OracleTransaction tran, string Login)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("Login", OracleType.VarChar); p[1].Value = Login; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetPositionByLogin", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public DataSet GetVisibleBlocksByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetVisibleBlocksByRubrID", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetVisibleBlockDocsByIDandC(OracleConnection conn, OracleTransaction tran, int RubrID, int CustomerID)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("CustomerID_", OracleType.Number); p[2].Value = CustomerID; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetVisibleBlockDocsByIDandC", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetDocNameTypes(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 0];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetDocNameTypes", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAvailableCustomers(OracleConnection conn, OracleTransaction tran, int RubrID)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetAvailableCustomers", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public string RegInDocControl(OracleConnection conn, OracleTransaction tran, string outcomenumber, string authororgname, 
            DateTime outcomedate, string author, string docdescr, string barcode, string pometki, int idrubr, string ind, string addr,
            string phone, string mail, string onhand, string appltype, string cadnum, int klass, int indexi, string asguf_num,
            ref int docid, ref string docnum, ref DateTime docdate, int typerequest = 1)
        {
            OracleParameter[] p = new OracleParameter[22 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("outcomenumber", OracleType.VarChar); p[1].Value = outcomenumber; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("authororgname", OracleType.VarChar); p[2].Value = authororgname; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("outcomedate", OracleType.DateTime); p[3].Value = outcomedate; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("author", OracleType.VarChar); p[4].Value = author; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("typerequest", OracleType.Number); p[5].Value = typerequest; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("docdescr", OracleType.VarChar); p[6].Value = docdescr; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("barcode", OracleType.VarChar); p[7].Value = barcode; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("pometki", OracleType.VarChar); p[8].Value = pometki; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("idrubr", OracleType.Number); p[9].Value = idrubr; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("ind", OracleType.VarChar); p[10].Value = ind; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("addr", OracleType.VarChar); p[11].Value = addr; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("phone", OracleType.VarChar); p[12].Value = phone; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("mail", OracleType.VarChar); p[13].Value = mail; p[13].Direction = ParameterDirection.Input;
            if (!onhand.Equals(""))
            {
                p[14] = new OracleParameter("onhand", OracleType.Number); p[14].Value = Convert.ToInt32(onhand); p[14].Direction = ParameterDirection.Input;
            }
            else
            {
                p[14] = new OracleParameter("onhand", OracleType.Number); p[14].Value = DBNull.Value; p[14].Direction = ParameterDirection.Input;
            }
            p[15] = new OracleParameter("appltype", OracleType.VarChar); p[15].Value = appltype; p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("cadnum", OracleType.VarChar); p[16].Value = cadnum; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("klass", OracleType.Number); p[17].Value = klass; p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("indexi", OracleType.Number); p[18].Value = indexi; p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("asguf_num", OracleType.VarChar); p[19].Value = asguf_num; p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("docid", OracleType.Number); p[20].Direction = ParameterDirection.Output;
            p[21] = new OracleParameter("docnum", OracleType.VarChar); p[21].Size = 2048; p[21].Direction = ParameterDirection.Output;
            p[22] = new OracleParameter("docdate", OracleType.DateTime); p[22].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegInDocControl", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            docid = Convert.ToInt32(p[20].Value);
            docnum = p[21].Value.ToString();
            docdate = Convert.ToDateTime(p[22].Value);

            return Convert.ToString(p[0].Value);
        }

        public string RegChildInDocControl(OracleConnection conn, OracleTransaction tran, string parentnum, string outcomenumber, string authororgname,
            DateTime outcomedate, string author, string docdescr, string barcode, string pometki, int idrubr, string ind, string addr,
            string phone, string mail, string onhand, string appltype, string cadnum, int klass, int indexi,
            ref int docid, ref string docnum, ref DateTime docdate, int typerequest = 1)
        {
            OracleParameter[] p = new OracleParameter[22 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentnum", OracleType.VarChar); p[1].Value = parentnum; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("outcomenumber", OracleType.VarChar); p[2].Value = outcomenumber; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("authororgname", OracleType.VarChar); p[3].Value = authororgname; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("outcomedate", OracleType.DateTime); p[4].Value = outcomedate; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("author", OracleType.VarChar); p[5].Value = author; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("typerequest", OracleType.Number); p[6].Value = typerequest; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("docdescr", OracleType.VarChar); p[7].Value = docdescr; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("barcode", OracleType.VarChar); p[8].Value = barcode; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("pometki", OracleType.VarChar); p[9].Value = pometki; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("idrubr", OracleType.Number); p[10].Value = idrubr; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("ind", OracleType.VarChar); p[11].Value = ind; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("addr", OracleType.VarChar); p[12].Value = addr; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("phone", OracleType.VarChar); p[13].Value = phone; p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("mail", OracleType.VarChar); p[14].Value = mail; p[14].Direction = ParameterDirection.Input;
            if (!onhand.Equals(""))
            {
                p[15] = new OracleParameter("onhand", OracleType.Number); p[15].Value = Convert.ToInt32(onhand); p[15].Direction = ParameterDirection.Input;
            }
            else
            {
                p[15] = new OracleParameter("onhand", OracleType.Number); p[15].Value = DBNull.Value; p[15].Direction = ParameterDirection.Input;
            }
            p[16] = new OracleParameter("appltype", OracleType.VarChar); p[16].Value = appltype; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("cadnum", OracleType.VarChar); p[17].Value = cadnum; p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("klass", OracleType.Number); p[18].Value = klass; p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("indexi", OracleType.Number); p[19].Value = indexi; p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("docid", OracleType.Number); p[20].Direction = ParameterDirection.Output;
            p[21] = new OracleParameter("docnum", OracleType.VarChar); p[21].Size = 2048; p[21].Direction = ParameterDirection.Output;
            p[22] = new OracleParameter("docdate", OracleType.DateTime); p[22].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegChildInDocControl", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            docid = Convert.ToInt32(p[20].Value);
            docnum = p[21].Value.ToString();
            docdate = Convert.ToDateTime(p[22].Value);

            return Convert.ToString(p[0].Value);
        }

        public string RegNote(OracleConnection conn, OracleTransaction tran, string docnum, string barcode, string SignatureFIO, ref string vidandate, ref string docdate)
        {
            OracleParameter[] p = new OracleParameter[1 + 6];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum", OracleType.VarChar); p[1].Value = docnum; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("barcode", OracleType.VarChar); p[2].Value = barcode; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("SignatureFIO", OracleType.VarChar); p[3].Value = SignatureFIO; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("out_docnum", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("out_docdate", OracleType.DateTime); p[5].Direction = ParameterDirection.Output;
            p[6] = new OracleParameter("docdate", OracleType.DateTime); p[6].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegNote", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            vidandate = Convert.ToDateTime(p[5].Value).ToString("dd.MM.yyyy");
            docdate = Convert.ToDateTime(p[6].Value).ToString("dd.MM.yyyy");

            return Convert.ToString(p[0].Value);
        }

        public DataSet GetDocControlInfo(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetDocControlInfo", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }


        public DataSet GetRubrInfo(OracleConnection conn, OracleTransaction tran, int gosn)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("gosn", OracleType.Number); p[1].Value = gosn; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRubrInfo", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetDenyInfo(OracleConnection conn, OracleTransaction tran, string rn_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("rn_", OracleType.VarChar); p[1].Value = rn_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetDenyInfo", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public string UpdateDocControl(OracleConnection conn, OracleTransaction tran, int id_documents, string outcomenumber, 
            DateTime outcomedate, string authororgname, string author, string docdescr, string barcode, string pometki, string ind, string addr,
            string phone, string mail, string onhand, string appltype, string cadnum, string asguf_num, int typerequest = 1)
        {
            OracleParameter[] p = new OracleParameter[17 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("outcomenumber_", OracleType.VarChar); p[2].Value = outcomenumber; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("outcomedate_", OracleType.DateTime); p[3].Value = outcomedate; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("authororgname_", OracleType.VarChar); p[4].Value = authororgname; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("author_", OracleType.VarChar); p[5].Value = author; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("typerequest_", OracleType.Number); p[6].Value = typerequest; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("docdescr_", OracleType.VarChar); p[7].Value = docdescr; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("barcode_", OracleType.VarChar); p[8].Value = barcode; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("pometki_", OracleType.VarChar); p[9].Value = pometki; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("ind_", OracleType.VarChar); p[10].Value = ind; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("addr_", OracleType.VarChar); p[11].Value = addr; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("phone_", OracleType.VarChar); p[12].Value = phone; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("mail_", OracleType.VarChar); p[13].Value = mail; p[13].Direction = ParameterDirection.Input;
            if (!onhand.Equals(""))
            {
                p[14] = new OracleParameter("onhand_", OracleType.Number); p[14].Value = Convert.ToInt32(onhand); p[14].Direction = ParameterDirection.Input;
            }
            else
            {
                p[14] = new OracleParameter("onhand_", OracleType.Number); p[14].Value = DBNull.Value; p[14].Direction = ParameterDirection.Input;
            }
            p[15] = new OracleParameter("appltype_", OracleType.VarChar); p[15].Value = appltype; p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("cadnum_", OracleType.VarChar); p[16].Value = cadnum; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("asguf_num_", OracleType.VarChar); p[17].Value = asguf_num; p[17].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.UpdateDocControl", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string OpenResolution(OracleConnection conn, OracleTransaction tran, int IDReg)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.DateTime); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("IDReg", OracleType.Number); p[1].Value = IDReg; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.OpenResolution", p, tran);

            string result = IsDate(p[0].Value.ToString()) ? Convert.ToDateTime(p[0].Value).ToString("dd.MM.yyyy") : "";

            return result;
        }

        public string SetVariativeSrok(OracleConnection conn, OracleTransaction tran, int IDReg, string DeadLine, int IncType = 0)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.DateTime); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("IDReg", OracleType.Number); p[1].Value = IDReg; p[1].Direction = ParameterDirection.Input;
            if (DeadLine.Equals(""))
            { p[2] = new OracleParameter("DeadLine", OracleType.DateTime); p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input; }
            else
            { p[2] = new OracleParameter("DeadLine", OracleType.DateTime); p[2].Value = Convert.ToDateTime(DeadLine); p[2].Direction = ParameterDirection.Input; }
            p[3] = new OracleParameter("IncType", OracleType.Number); p[3].Value = IncType; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.SetVariativeSrok", p, tran);

            return p[0].Value.ToString();
        }

        public string CloseResolution(OracleConnection conn, OracleTransaction tran, int IDReg, DateTime date)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("IDReg", OracleType.Number); p[1].Value = IDReg; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("Ispoln_Date", OracleType.DateTime); p[2].Value = date; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CloseResolution", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string SetTodayIspolData(OracleConnection conn, OracleTransaction tran, int IDReg, string userldap_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("IDReg", OracleType.Number); p[1].Value = IDReg; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("userldap_", OracleType.VarChar); p[2].Value = userldap_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.SetTodayIspolData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string SetDeny(OracleConnection conn, OracleTransaction tran, int IDReg, int IDDeny, string SignatureFIO, int Otkaz_type)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("IDReg", OracleType.Number); p[1].Value = IDReg; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("IDDeny", OracleType.Number); p[2].Value = IDDeny; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("FIO", OracleType.VarChar); p[3].Value = SignatureFIO; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("Otkaz_type", OracleType.Number); p[4].Value = Otkaz_type; p[4].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.SetDeny", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string GetNextBarcode(OracleConnection conn, OracleTransaction tran)
        {
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetNextBarcode", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterClassAttributes(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_view_, int id_content_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_view_", OracleType.Number); p[2].Value = id_view_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_content_", OracleType.VarChar); p[3].Value = id_content_; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "ClassAttributes", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterClassKontrAttributes(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string kontrname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("kontrname_", OracleType.VarChar); p[2].Value =kontrname_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "ClassKontrAttributes", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterMainBlock(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_,
            string username_, string userldap_, string id_dest_okrug_, string id_reg_okrug_, string mfc_date_, string docs_recieved_, string id_income_type_)
        {
            OracleParameter[] p = new OracleParameter[1 + 8];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("username_", OracleType.VarChar); p[2].Value = username_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("userldap_", OracleType.VarChar); p[3].Value = userldap_; p[3].Direction = ParameterDirection.Input;

            if (!id_dest_okrug_.Equals(""))
            {
                p[4] = new OracleParameter("id_dest_okrug_", OracleType.Number); p[4].Value = Convert.ToInt32(id_dest_okrug_); p[4].Direction = ParameterDirection.Input;
            }
            else
            {
                p[4] = new OracleParameter("id_dest_okrug_", OracleType.Number); p[4].Value = DBNull.Value; p[4].Direction = ParameterDirection.Input;
            }
            if (!id_reg_okrug_.Equals(""))
            {
                p[5] = new OracleParameter("id_reg_okrug_", OracleType.Number); p[5].Value = Convert.ToInt32(id_reg_okrug_); p[5].Direction = ParameterDirection.Input;
            }
            else
            {
                p[5] = new OracleParameter("id_reg_okrug_", OracleType.Number); p[5].Value = DBNull.Value; p[5].Direction = ParameterDirection.Input;
            }

            if (IsDate(mfc_date_))
            {
                p[6] = new OracleParameter("mfc_date_", OracleType.DateTime); p[6].Value = Convert.ToDateTime(mfc_date_); p[6].Direction = ParameterDirection.Input;
            }
            else
            {
                p[6] = new OracleParameter("mfc_date_", OracleType.DateTime); p[6].Value = DBNull.Value; p[6].Direction = ParameterDirection.Input;
            }

            if (IsDate(docs_recieved_))
            {
                p[7] = new OracleParameter("docs_recieved_", OracleType.DateTime); p[7].Value = Convert.ToDateTime(docs_recieved_); p[7].Direction = ParameterDirection.Input;
            }
            else
            {
                p[7] = new OracleParameter("docs_recieved_", OracleType.DateTime); p[7].Value = DBNull.Value; p[7].Direction = ParameterDirection.Input;
            }

            if (!id_income_type_.Equals(""))
            {
                p[8] = new OracleParameter("id_income_type_", OracleType.Number); p[8].Value = Convert.ToInt32(id_income_type_); p[8].Direction = ParameterDirection.Input;
            }
            else
            {
                p[8] = new OracleParameter("id_income_type_", OracleType.Number); p[8].Value = DBNull.Value; p[8].Direction = ParameterDirection.Input;
            }


            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "MainBlock", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock1(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string name_, string ogrn_, string inn_,
            string fio_f_, string fio_i_, string fio_o_, string snils_, int sex_, string contactphone_, string smsphone_,
            string email_, string type_subj_, int type_subj2_,
            string serial_, string num_, string vidan_, string vidan_date_,
            bool Custom, string index_n_,
            string Guid1, string Guid2, string Guid3, string Guid4, string Guid5,
            string Guid6, string flat_office_, string CustomAddress, int non_resident_, string birth_date_, int other_subj_, int ip_register_, string position_,
            string username_, string userldap_, string id_okrug_, string id_reg_okrug_, string mfc_date_, string type_, int tab_, int no_fias_ = 0)
        {
            OracleParameter[] p = new OracleParameter[1 + 41];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("name_", OracleType.VarChar); p[2].Value = name_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("ogrn_", OracleType.VarChar); p[3].Value = ogrn_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("inn_", OracleType.VarChar); p[4].Value = inn_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("fio_f_", OracleType.VarChar); p[5].Value = fio_f_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("fio_i_", OracleType.VarChar); p[6].Value = fio_i_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("fio_o_", OracleType.VarChar); p[7].Value = fio_o_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("snils_", OracleType.VarChar); p[8].Value = snils_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("sex_", OracleType.Number); p[9].Value = sex_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("contactphone_", OracleType.VarChar); p[10].Value = contactphone_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("smsphone_", OracleType.VarChar); p[11].Value = smsphone_; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("email_", OracleType.VarChar); p[12].Value = email_; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("type_subj_", OracleType.VarChar); p[13].Value = type_subj_; p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("type_subj2_", OracleType.Number); p[14].Value = type_subj2_; p[14].Direction = ParameterDirection.Input;
            p[15] = new OracleParameter("serial_", OracleType.VarChar); p[15].Value = serial_; p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("num_", OracleType.VarChar); p[16].Value = num_; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("vidan_", OracleType.VarChar); p[17].Value = vidan_; p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("vidan_date_", OracleType.VarChar); p[18].Value = vidan_date_; p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("Custom", OracleType.Number); p[19].Value = Convert.ToInt32(Custom); ; p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("index_n_", OracleType.VarChar); p[20].Value = index_n_; p[20].Direction = ParameterDirection.Input;
            p[21] = new OracleParameter("Guid1", OracleType.VarChar); p[21].Value = Guid1; p[21].Direction = ParameterDirection.Input;
            p[22] = new OracleParameter("Guid2", OracleType.VarChar); p[22].Value = Guid2; p[22].Direction = ParameterDirection.Input;
            p[23] = new OracleParameter("Guid3", OracleType.VarChar); p[23].Value = Guid3; p[23].Direction = ParameterDirection.Input;
            p[24] = new OracleParameter("Guid4", OracleType.VarChar); p[24].Value = Guid4; p[24].Direction = ParameterDirection.Input;
            p[25] = new OracleParameter("Guid5", OracleType.VarChar); p[25].Value = Guid5; p[25].Direction = ParameterDirection.Input;
            p[26] = new OracleParameter("Guid6", OracleType.VarChar); p[26].Value = Guid6; p[26].Direction = ParameterDirection.Input;
            p[27] = new OracleParameter("flat_office_", OracleType.VarChar); p[27].Value = flat_office_; p[27].Direction = ParameterDirection.Input;
            p[28] = new OracleParameter("CustomAddress", OracleType.VarChar); p[28].Value = CustomAddress; p[28].Direction = ParameterDirection.Input;
            p[29] = new OracleParameter("non_resident_", OracleType.Number); p[29].Value = non_resident_; p[29].Direction = ParameterDirection.Input;
            p[30] = new OracleParameter("birth_date_", OracleType.VarChar); p[30].Value = birth_date_; p[30].Direction = ParameterDirection.Input;
            p[31] = new OracleParameter("other_subj_", OracleType.Number); p[31].Value = other_subj_; p[31].Direction = ParameterDirection.Input;
            p[32] = new OracleParameter("ip_register_", OracleType.Number); p[32].Value = ip_register_; p[32].Direction = ParameterDirection.Input;
            p[33] = new OracleParameter("position_", OracleType.VarChar); p[33].Value = position_; p[33].Direction = ParameterDirection.Input;
            p[34] = new OracleParameter("username_", OracleType.VarChar); p[34].Value = username_; p[34].Direction = ParameterDirection.Input;
            p[35] = new OracleParameter("userldap_", OracleType.VarChar); p[35].Value = userldap_; p[35].Direction = ParameterDirection.Input;
            if (!id_okrug_.Equals(""))
            {
                p[36] = new OracleParameter("id_dest_okrug_", OracleType.Number); p[36].Value = Convert.ToInt32(id_okrug_); p[36].Direction = ParameterDirection.Input;
            }
            else
            {
                p[36] = new OracleParameter("id_dest_okrug_", OracleType.Number); p[36].Value = DBNull.Value; p[36].Direction = ParameterDirection.Input;
            }
            if (!id_reg_okrug_.Equals(""))
            {
                p[37] = new OracleParameter("id_reg_okrug_", OracleType.Number); p[37].Value = Convert.ToInt32(id_reg_okrug_); p[37].Direction = ParameterDirection.Input;
            }
            else
            {
                p[37] = new OracleParameter("id_reg_okrug_", OracleType.Number); p[37].Value = DBNull.Value; p[37].Direction = ParameterDirection.Input;
            }
            p[38] = new OracleParameter("mfc_date_", OracleType.VarChar); p[38].Value = mfc_date_; p[38].Direction = ParameterDirection.Input;
            p[39] = new OracleParameter("type_", OracleType.VarChar); p[39].Value = type_; p[39].Direction = ParameterDirection.Input;
            p[40] = new OracleParameter("tab_", OracleType.Number); p[40].Value = tab_; p[40].Direction = ParameterDirection.Input;
            p[41] = new OracleParameter("no_fias_", OracleType.Number); p[41].Value = no_fias_; p[41].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block1", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock2(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_type_predst_,
            string fio_f_, string fio_i_, string fio_o_, string snils_,
            string serial_, string num_, string vidan_, string vidan_date_, int tab_)
        {
            OracleParameter[] p = new OracleParameter[1 + 11];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_type_predst_", OracleType.Number); p[2].Value = id_type_predst_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("fio_f_", OracleType.VarChar); p[3].Value = fio_f_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("fio_i_", OracleType.VarChar); p[4].Value = fio_i_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("fio_o_", OracleType.VarChar); p[5].Value = fio_o_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("snils_", OracleType.VarChar); p[6].Value = snils_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("serial_", OracleType.VarChar); p[7].Value = serial_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("num_", OracleType.VarChar); p[8].Value = num_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("vidan_", OracleType.VarChar); p[9].Value = vidan_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("vidan_date_", OracleType.VarChar); p[10].Value = vidan_date_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("tab_", OracleType.Number); p[11].Value = tab_; p[11].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block2", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock3_5(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_type_object_,
            string s_object_, string placement_, string floor_, string rooms_, string kadnum_, string uslnum_, string unom_, string addr_orientir_,
            string kod_region_, int id_purpose_, string descr_, int Custom, string Guid1, string Guid2, string Guid3, string Guid4, string Guid5, string Guid6)
        {
            OracleParameter[] p = new OracleParameter[1 + 20];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_type_object_", OracleType.Number); p[2].Value = id_type_object_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("s_object_", OracleType.Number); 
            if (!s_object_.Equals(""))
                p[3].Value = ConvToDouble(s_object_);
            else
                p[3].Value = DBNull.Value; 
            p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("placement_", OracleType.VarChar); p[4].Value = placement_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("floor_", OracleType.VarChar); p[5].Value = floor_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("rooms_", OracleType.VarChar); p[6].Value = rooms_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("kadnum_", OracleType.VarChar); p[7].Value = kadnum_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("uslnum_", OracleType.VarChar); p[8].Value = uslnum_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("unom_", OracleType.Number);
            if (!unom_.Equals(""))
                p[9].Value = Convert.ToInt32(unom_);
            else
                p[9].Value = DBNull.Value;
            p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("addr_orientir_", OracleType.VarChar); p[10].Value = addr_orientir_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("kod_region_", OracleType.VarChar); p[11].Value = kod_region_; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("id_purpose_", OracleType.VarChar); p[12].Value = id_purpose_; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("descr_", OracleType.VarChar); p[13].Value = descr_; p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("Custom", OracleType.Number); p[14].Value = Convert.ToInt32(Custom); ; p[14].Direction = ParameterDirection.Input;
            p[15] = new OracleParameter("Guid1", OracleType.VarChar); p[15].Value = Guid1; p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("Guid2", OracleType.VarChar); p[16].Value = Guid2; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("Guid3", OracleType.VarChar); p[17].Value = Guid3; p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("Guid4", OracleType.VarChar); p[18].Value = Guid4; p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("Guid5", OracleType.VarChar); p[19].Value = Guid5; p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("Guid6", OracleType.VarChar); p[20].Value = Guid6; p[20].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block3_5", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock4(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_object_name_,
            string addr_orientir_, string descr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_object_name_", OracleType.Number); p[2].Value = id_object_name_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("addr_orientir_", OracleType.VarChar); p[3].Value = addr_orientir_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("descr_", OracleType.VarChar); p[4].Value = descr_; p[4].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block4", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock6_7_9_16_17(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_doctype_, string docnum_, string docdate_,
            string docdeadline_, string govauthorityname_, string constructobjname_, int block_number_, string subnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 9];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_doctype_", OracleType.Number); p[2].Value = id_doctype_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("docnum_", OracleType.VarChar); p[3].Value = docnum_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("docdate_", OracleType.VarChar); p[4].Value = docdate_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("docdeadline_", OracleType.VarChar); p[5].Value = docdeadline_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("govauthorityname_", OracleType.VarChar); p[6].Value = govauthorityname_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("constructobjname_", OracleType.VarChar); p[7].Value = constructobjname_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("block_number_", OracleType.Number); p[8].Value = block_number_; p[8].Direction = ParameterDirection.Input;
            if (!subnum_.Trim().Equals(""))
            {
                p[9] = new OracleParameter("subnum_", OracleType.Number); p[9].Value = Convert.ToInt32(subnum_); p[9].Direction = ParameterDirection.Input;
            }
            else
            {
                p[9] = new OracleParameter("subnum_", OracleType.Number); p[9].Value = DBNull.Value; p[9].Direction = ParameterDirection.Input;
            }

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block6_7_9_16_17", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock8(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string bankname_, string kpp_,
            string okpo_, string curracc_, string corracc_, string bik_, int id_type_signer_, string snils_, string signer_position_,
            int id_type_basedocname_, string doc_description_, string docdate_, string docnum_, int tab_)
        {
            OracleParameter[] p = new OracleParameter[1 + 15];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("bankname_", OracleType.VarChar); p[2].Value = bankname_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("kpp_", OracleType.VarChar); p[3].Value = kpp_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("okpo_", OracleType.VarChar); p[4].Value = okpo_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("curracc_", OracleType.VarChar); p[5].Value = curracc_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("corracc_", OracleType.VarChar); p[6].Value = corracc_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("bik_", OracleType.VarChar); p[7].Value = bik_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("id_type_signer_", OracleType.Number); p[8].Value = id_type_signer_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("snils_", OracleType.VarChar); p[9].Value = snils_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("signer_position_", OracleType.VarChar); p[10].Value = signer_position_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("id_type_basedocname_", OracleType.Number); p[11].Value = id_type_basedocname_; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("doc_description_", OracleType.VarChar); p[12].Value = doc_description_; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("docdate_", OracleType.VarChar); p[13].Value = docdate_; p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("docnum_", OracleType.VarChar); p[14].Value = docnum_; p[14].Direction = ParameterDirection.Input;
            p[15] = new OracleParameter("tab_", OracleType.Number); p[15].Value = tab_; p[15].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block8", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock10(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_type_changes_, int id_type_sidechangebase_,
            string docnum_salecontract_, string date_salecontract_, string sidechangebasedesc_, string using_purpose_, string deadline_, string reqschangedesc_,
            string new_square_, string new_room_nums_, string cur_chval_, string new_chval_)
        {
            OracleParameter[] p = new OracleParameter[1 + 13];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_type_changes_", OracleType.Number); p[2].Value = id_type_changes_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_type_sidechangebase_", OracleType.Number); p[3].Value = id_type_sidechangebase_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("docnum_salecontract_", OracleType.VarChar); p[4].Value = docnum_salecontract_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("date_salecontract_", OracleType.VarChar); p[5].Value = date_salecontract_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("sidechangebasedesc_", OracleType.VarChar); p[6].Value = sidechangebasedesc_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("using_purpose_", OracleType.VarChar); p[7].Value = using_purpose_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("deadline_", OracleType.VarChar); p[8].Value = deadline_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("reqschangedesc_", OracleType.VarChar); p[9].Value = reqschangedesc_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("new_square_", OracleType.Number);
            if (!new_square_.Equals(""))
                p[10].Value = ConvToDouble(new_square_);
            else
                p[10].Value = DBNull.Value;
            p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("new_room_nums_", OracleType.VarChar); p[11].Value = new_room_nums_; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("cur_chval_", OracleType.VarChar); p[12].Value = cur_chval_; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("new_chval_", OracleType.VarChar); p[13].Value = new_chval_; p[13].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block10", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock11(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, bool Custom, string index_n_,
            string Guid1, string Guid2, string Guid3, string Guid4, string Guid5,
            string Guid6, string flat_office_, string CustomAddress, int tab_, int no_fias_ = 0)
        {
            OracleParameter[] p = new OracleParameter[1 + 13];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("Custom", OracleType.Number); p[2].Value = Convert.ToInt32(Custom); ; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("index_n_", OracleType.VarChar); p[3].Value = index_n_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("Guid1", OracleType.VarChar); p[4].Value = Guid1; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("Guid2", OracleType.VarChar); p[5].Value = Guid2; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("Guid3", OracleType.VarChar); p[6].Value = Guid3; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("Guid4", OracleType.VarChar); p[7].Value = Guid4; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("Guid5", OracleType.VarChar); p[8].Value = Guid5; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("Guid6", OracleType.VarChar); p[9].Value = Guid6; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("flat_office_", OracleType.VarChar); p[10].Value = flat_office_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("CustomAddress", OracleType.VarChar); p[11].Value = CustomAddress; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("tab_", OracleType.Number); p[12].Value = tab_; p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("no_fias_", OracleType.Number); p[13].Value = no_fias_; p[13].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block11", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock12(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int prefpayorder_, int payfreq_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("prefpayorder_", OracleType.VarChar); p[2].Value = prefpayorder_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("payfreq_", OracleType.VarChar); p[3].Value = payfreq_; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block12", p, tran);
            return Convert.ToString(p[0].Value);
        }


        public string RegisterBlock13(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string docnum_, string docdate_, string flsnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("docnum_", OracleType.VarChar); p[2].Value = docnum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("docdate_", OracleType.VarChar); p[3].Value = docdate_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("flsnum_", OracleType.VarChar); p[4].Value = flsnum_; p[4].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block13", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock14(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_type_claim_, string statusreq_date_,
            string flsnum_, string kbk_, string rent_, string penalties_, string debtstart_date_, string debtstart_p_date_, string calcsend_date_, string dissolution_basedoc_,
            string sqrooms_, string proc_debt_, string dolg_sum_, string receipts_dolg_sum_, string interest_sum_, string receipts_interest_sum_, string installments_period_,
            string payment_frequency_, string period_po_dkp_, string nyear_rate_, string nyear_rent_, string nyear_, string using_target_, string cyear_rate_,
            string nyear_mrate_, string corr_rate_coef_, string nosq_subarend_, string sq_subarend_, string nyear_avrate_, string calctype_, string cyear_)
        {
            OracleParameter[] p = new OracleParameter[1 + 32];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_type_claim_", OracleType.Number); p[2].Value = id_type_claim_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("statusreq_date_", OracleType.VarChar); p[3].Value = statusreq_date_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("flsnum_", OracleType.VarChar); p[4].Value = flsnum_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("kbk_", OracleType.VarChar); p[5].Value = kbk_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("rent_", OracleType.VarChar); p[6].Value = rent_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("penalties_", OracleType.VarChar); p[7].Value = penalties_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("debtstart_date_", OracleType.VarChar); p[8].Value = debtstart_date_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("debtstart_p_date_", OracleType.VarChar); p[9].Value = debtstart_p_date_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("calcsend_date_", OracleType.VarChar); p[10].Value = calcsend_date_; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("dissolution_basedoc_", OracleType.VarChar); p[11].Value = dissolution_basedoc_; p[11].Direction = ParameterDirection.Input;

            p[12] = new OracleParameter("sqrooms_", OracleType.Number);
            if (!sqrooms_.Equals(""))
                p[12].Value = ConvToDouble(sqrooms_);
            else
                p[12].Value = DBNull.Value;
            p[12].Direction = ParameterDirection.Input;

            p[13] = new OracleParameter("proc_debt_", OracleType.VarChar); p[13].Value = proc_debt_; p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("dolg_sum_", OracleType.VarChar); p[14].Value = dolg_sum_; p[14].Direction = ParameterDirection.Input;
            p[15] = new OracleParameter("receipts_dolg_sum_", OracleType.VarChar); p[15].Value = receipts_dolg_sum_; p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("interest_sum_", OracleType.VarChar); p[16].Value = interest_sum_; p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("receipts_interest_sum_", OracleType.VarChar); p[17].Value = receipts_interest_sum_; p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("installments_period_", OracleType.VarChar); p[18].Value = installments_period_; p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("payment_frequency_", OracleType.VarChar); p[19].Value = payment_frequency_; p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("period_po_dkp_", OracleType.VarChar); p[20].Value = period_po_dkp_; p[20].Direction = ParameterDirection.Input;
            p[21] = new OracleParameter("nyear_rate_", OracleType.VarChar); p[21].Value = nyear_rate_; p[21].Direction = ParameterDirection.Input;
            p[22] = new OracleParameter("nyear_rent_", OracleType.VarChar); p[22].Value = nyear_rent_; p[22].Direction = ParameterDirection.Input;
            p[23] = new OracleParameter("nyear_", OracleType.Number);

            if (!nyear_.Equals(""))
                p[23].Value = Convert.ToInt32(nyear_);
            else
                p[23].Value = DBNull.Value;

            p[23].Direction = ParameterDirection.Input;

            p[24] = new OracleParameter("using_target_", OracleType.VarChar); p[24].Value = using_target_; p[24].Direction = ParameterDirection.Input;
            p[25] = new OracleParameter("cyear_rate_", OracleType.VarChar); p[25].Value = cyear_rate_; p[25].Direction = ParameterDirection.Input;
            p[26] = new OracleParameter("nyear_mrate_", OracleType.VarChar); p[26].Value = nyear_mrate_; p[26].Direction = ParameterDirection.Input;
            p[27] = new OracleParameter("corr_rate_coef_", OracleType.VarChar); p[27].Value = corr_rate_coef_; p[27].Direction = ParameterDirection.Input;
            p[28] = new OracleParameter("nosq_subarend_", OracleType.VarChar); p[28].Value = nosq_subarend_; p[28].Direction = ParameterDirection.Input;
            p[29] = new OracleParameter("sq_subarend_", OracleType.VarChar); p[29].Value = sq_subarend_; p[29].Direction = ParameterDirection.Input;
            p[30] = new OracleParameter("nyear_avrate_", OracleType.VarChar); p[30].Value = nyear_avrate_; p[30].Direction = ParameterDirection.Input;
            p[31] = new OracleParameter("calctype_", OracleType.VarChar); p[31].Value = calctype_; p[31].Direction = ParameterDirection.Input;
            p[32] = new OracleParameter("cyear_", OracleType.Number);

            if (!cyear_.Equals(""))
                p[32].Value = Convert.ToInt32(cyear_);
            else
                p[32].Value = DBNull.Value;

            p[32].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block14", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock15(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string content_, string comment_text_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("content_", OracleType.VarChar); p[2].Value = content_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("comment_text_", OracleType.VarChar); p[3].Value = comment_text_; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block15", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock18(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string lawsuitnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("lawsuitnum_", OracleType.VarChar); p[2].Value = lawsuitnum_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block18", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock19(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int type_, string name_,
            string ogrn_, string inn_, string contactphone_, string email_)
        {
            OracleParameter[] p = new OracleParameter[1 + 7];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("type_", OracleType.Number); p[2].Value = type_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("name_", OracleType.VarChar); p[3].Value = name_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("ogrn_", OracleType.VarChar); p[4].Value = ogrn_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("inn_", OracleType.VarChar); p[5].Value = inn_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("contactphone_", OracleType.VarChar); p[6].Value = contactphone_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("email_", OracleType.VarChar); p[7].Value = email_; p[7].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block19", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock20(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string flsnum_,
            string periodstart_date_, string periodend_date_, string kvartal_year_, string statusreq_date_)
        {
            OracleParameter[] p = new OracleParameter[1 + 6];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("flsnum_", OracleType.VarChar); p[2].Value = flsnum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("periodstart_date_", OracleType.VarChar); p[3].Value = periodstart_date_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("periodend_date_", OracleType.VarChar); p[4].Value = periodend_date_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("kvartal_year_", OracleType.VarChar); p[5].Value = kvartal_year_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("statusreq_date_", OracleType.VarChar); p[6].Value = statusreq_date_; p[6].Direction = ParameterDirection.Input;
            
            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block20", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock21(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int id_user_,
            string file_name_, string file_descr_, ref int id_blob_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_user_", OracleType.Number); p[2].Value = id_user_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("file_name_", OracleType.VarChar); p[3].Value = file_name_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("file_descr_", OracleType.VarChar); p[4].Value = file_descr_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("id_blob_", OracleType.Number); p[5].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block21", p, tran);

            if (p[0].Value.ToString().Equals(""))
                id_blob_ = Convert.ToInt32(p[5].Value);

            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock22(OracleConnection conn, OracleTransaction tran, int id_documents_, string rn_appeal_on_, string rn_hyperlink_on_,
            DateTime appeal_date_, int id_appeal_linktype_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("rn_hyperlink_on_", OracleType.VarChar); p[2].Value = rn_hyperlink_on_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("rn_appeal_on_", OracleType.VarChar); p[3].Value = rn_appeal_on_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("appeal_date_", OracleType.DateTime); p[4].Value = appeal_date_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("id_appeal_linktype_", OracleType.Number); p[5].Value = id_appeal_linktype_; p[5].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegisterBlock22", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public Boolean IsDate(string stringToTest)
        {
            DateTime result;
            return DateTime.TryParse(stringToTest, out result);
        }

        public bool IsDate2(string stringToTest)
        {
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;
            string[] formats = { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy", "dd.MM.yy HH:mm:ss" };
            return DateTime.TryParseExact(stringToTest, formats, provider, DateTimeStyles.None, out result);
        }

        public string RegisterBlock23(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, 
            string meetingtimehour_, string meetingtimeminute_, string meetingdate_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            if (!meetingdate_.Equals("") && !meetingtimehour_.Equals("") && IsDate(meetingdate_))
            {
                p[2] = new OracleParameter("meetingdatetime_", OracleType.DateTime); 
                p[2].Value = Convert.ToDateTime(meetingdate_).AddHours(Convert.ToInt32(meetingtimehour_)).AddMinutes(Convert.ToInt32(meetingtimeminute_)); 
                p[2].Direction = ParameterDirection.Input;
            }
            else
            {
                p[2] = new OracleParameter("meetingdatetime_", OracleType.DateTime); p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input;
            }
           
            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block23", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock24(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, 
            string lawsuitnum_, string id_lawcourt_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("lawsuitnum_", OracleType.VarChar); p[2].Value = lawsuitnum_; p[2].Direction = ParameterDirection.Input;
            if (!id_lawcourt_.Equals(""))
            {
                p[3] = new OracleParameter("id_lawcourt_", OracleType.Number); p[3].Value = id_lawcourt_; p[3].Direction = ParameterDirection.Input;
            }
            else
            {
                p[3] = new OracleParameter("id_lawcourt_", OracleType.Number); p[3].Value = DBNull.Value; p[3].Direction = ParameterDirection.Input;
            }

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block24", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock25(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_,
            string id_rate_purpose_, string id_rate_using_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            if (!id_rate_purpose_.Equals(""))
            {
                p[2] = new OracleParameter("id_rate_purpose_", OracleType.Number); p[2].Value = id_rate_purpose_; p[2].Direction = ParameterDirection.Input;
            }
            else
            {
                p[2] = new OracleParameter("id_rate_purpose_", OracleType.Number); p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input;
            }
            if (!id_rate_using_.Equals(""))
            {
                p[3] = new OracleParameter("id_rate_using_", OracleType.Number); p[3].Value = id_rate_using_; p[3].Direction = ParameterDirection.Input;
            }
            else
            {
                p[3] = new OracleParameter("id_rate_using_", OracleType.Number); p[3].Value = DBNull.Value; p[3].Direction = ParameterDirection.Input;
            }

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block25", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock26(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int accept_auction_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("accept_auction_", OracleType.Number); p[2].Value = accept_auction_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block26", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock27(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int family_count_,
            int rooms_count_, string live_room_square_, string common_square_, string live_square_, string realty_addr_, string deadline_ )
        {
            OracleParameter[] p = new OracleParameter[1 + 8];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("family_count_", OracleType.Number); p[2].Value = family_count_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("rooms_count_", OracleType.Number); p[3].Value = rooms_count_; p[3].Direction = ParameterDirection.Input;

            p[4] = new OracleParameter("live_room_square_", OracleType.Number);
            if (!live_room_square_.Equals(""))
                p[4].Value = ConvToDouble(live_room_square_);
            else
                p[4].Value = DBNull.Value; 
            p[4].Direction = ParameterDirection.Input;

            p[5] = new OracleParameter("common_square_", OracleType.Number);
            if (!common_square_.Equals(""))
                p[5].Value = ConvToDouble(common_square_);
            else
                p[5].Value = DBNull.Value; 
            p[5].Direction = ParameterDirection.Input;

            p[6] = new OracleParameter("live_square_", OracleType.Number);
            if (!live_square_.Equals(""))
                p[6].Value = ConvToDouble(live_square_);
            else
                p[6].Value = DBNull.Value; 
            p[6].Direction = ParameterDirection.Input;

            p[7] = new OracleParameter("realty_addr_", OracleType.VarChar); p[7].Value = realty_addr_; p[7].Direction = ParameterDirection.Input;

            if (IsDate(deadline_))
            {
                p[8] = new OracleParameter("deadline_", OracleType.DateTime); p[8].Value = Convert.ToDateTime(deadline_); p[8].Direction = ParameterDirection.Input;
            }
            else
            {
                p[8] = new OracleParameter("deadline_", OracleType.DateTime); p[8].Value = DBNull.Value; p[8].Direction = ParameterDirection.Input;
            }

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block27", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlock28(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, string transition_type_,
            string target_aftertrans_, string splitmerge_, string splitmerge_nums_, string reconf_, string registered_in_egrp_, string reg_num_, string reg_date_,
            string neighboring_, string neighboringnums_, string mkdsubjinfo_)
        {
            OracleParameter[] p = new OracleParameter[1 + 12];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("transition_type_", OracleType.VarChar); p[2].Value = transition_type_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("target_aftertrans_", OracleType.VarChar); p[3].Value = target_aftertrans_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("splitmerge_", OracleType.Number); p[4].Value = IsNumeric(splitmerge_) ? Convert.ToInt32(splitmerge_) : -1; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("splitmerge_nums_", OracleType.VarChar); p[5].Value = splitmerge_nums_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("reconf_", OracleType.Number); p[6].Value = IsNumeric(reconf_) ? Convert.ToInt32(reconf_) : -1; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("registered_in_egrp_", OracleType.Number); p[7].Value = IsNumeric(registered_in_egrp_) ? Convert.ToInt32(registered_in_egrp_) : -1; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("reg_num_", OracleType.VarChar); p[8].Value = reg_num_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("reg_date_", OracleType.DateTime); p[9].Value = reg_date_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("neighboring_", OracleType.Number); p[10].Value = IsNumeric(neighboring_) ? Convert.ToInt32(neighboring_) : -1; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("neighboringnums_", OracleType.VarChar); p[11].Value = neighboringnums_; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("mkdsubjinfo_", OracleType.VarChar); p[12].Value = mkdsubjinfo_; p[12].Direction = ParameterDirection.Input;
            for (int i = 2; i < p.Count(); i++)
            {
                if (p[i].Value.Equals("") || p[i].Value.ToString().Equals("-1"))
                    p[i].Value = DBNull.Value;
            }

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Block28", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterOtkaz310(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int not_storage_, int not_udo_, int not_gosusl_,
            string request_doc_name_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("not_storage_", OracleType.Number); p[2].Value = not_storage_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("not_udo_", OracleType.Number); p[3].Value = not_udo_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("not_gosusl_", OracleType.Number); p[4].Value = not_gosusl_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("request_doc_name_", OracleType.VarChar); p[5].Value = request_doc_name_; p[5].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Otkaz310", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterOtkaz(OracleConnection conn, OracleTransaction tran, string OperationType, int id_documents_, int invalid_proxy_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("invalid_proxy_", OracleType.Number); p[2].Value = invalid_proxy_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg." + OperationType + "Otkaz", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string RegisterBlockDocs(OracleConnection conn, OracleTransaction tran, int id_documents_, int id_doc_, int required_, int notarial_check_,
            string descr_, int notarial_, int count_, string docnum_, string docdate_, int count_check_)
        {
            OracleParameter[] p = new OracleParameter[1 + 10];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_doc_", OracleType.Number); p[2].Value = id_doc_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("required_", OracleType.Number); p[3].Value = required_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("notarial_check_", OracleType.Number); p[4].Value = notarial_check_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("descr_", OracleType.VarChar); p[5].Value = descr_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("notarial_", OracleType.Number); p[6].Value = notarial_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("count_", OracleType.Number); p[7].Value = count_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("docnum_", OracleType.VarChar); p[8].Value = docnum_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("docdate_", OracleType.VarChar); p[9].Value = docdate_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("count_check_", OracleType.Number); p[10].Value = count_check_; p[10].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegisterBlockDocs", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public DataSet GetBlockData(OracleConnection conn, OracleTransaction tran, int id_documents_, int block_number_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("block_number_", OracleType.Number); p[2].Value = block_number_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetBlockData", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetBlockData2(OracleConnection conn, OracleTransaction tran, int id_documents_, int block_number_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("block_number_", OracleType.Number); p[2].Value = block_number_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetBlockData2", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetBlockDocsData(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetBlockDocsData", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataTable GetDocClassification(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetDocClassification", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetDocClassificationKontr(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetDocClassificationKontr", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string GetRubrIDByRN(OracleConnection conn, OracleTransaction tran, string RN_, ref int RubrID, ref int id_documents)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RN_", OracleType.VarChar); p[1].Value = RN_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_documents_", OracleType.Number); p[2].Direction = ParameterDirection.Output;
            p[3] = new OracleParameter("RubrID_", OracleType.Number); p[3].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRubrIDByRN", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            id_documents = Convert.ToInt32(p[2].Value);
            RubrID = Convert.ToInt32(p[3].Value);

            return Convert.ToString(p[0].Value);
        }

        public string GetRubrNameByRN(OracleConnection conn, OracleTransaction tran, string RN_, ref string Rubr, ref int id_documents)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RN_", OracleType.VarChar); p[1].Value = RN_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_documents_", OracleType.Number); p[2].Direction = ParameterDirection.Output;
            p[3] = new OracleParameter("Rubr_", OracleType.VarChar); p[3].Size = 2048; p[3].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRubrNameByRN", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            id_documents = Convert.ToInt32(p[2].Value);
            Rubr = Convert.ToString(p[3].Value);

            return Convert.ToString(p[0].Value);
        }

        public Boolean IsNumeric(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }

        public string GetGosAndBasisPunkts(OracleConnection conn, OracleTransaction tran, int RubrID,
            ref string GOS_PUNKT, ref string BASIS_PUNKT_INFO, ref string BASIS_PUNKT_DOCS, ref string BASIS_PUNKT_APPLICANT,
            ref string BASIS_PUNKT_PROXY, ref string BASIS_PUNKT_PORTAL, ref string BASIS_PUNKT_BTI_ADR, ref string REGL_NAME)
        {
            OracleParameter[] p = new OracleParameter[1 + 9];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("GOS_PUNKT", OracleType.VarChar); p[2].Size = 2048; p[2].Direction = ParameterDirection.Output;
            p[3] = new OracleParameter("BASIS_PUNKT_INFO", OracleType.VarChar); p[3].Size = 2048; p[3].Direction = ParameterDirection.Output;
            p[4] = new OracleParameter("BASIS_PUNKT_DOCS", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("BASIS_PUNKT_APPLICANT", OracleType.VarChar); p[5].Size = 2048; p[5].Direction = ParameterDirection.Output;
            p[6] = new OracleParameter("BASIS_PUNKT_PROXY", OracleType.VarChar); p[6].Size = 2048; p[6].Direction = ParameterDirection.Output;
            p[7] = new OracleParameter("BASIS_PUNKT_PORTAL", OracleType.VarChar); p[7].Size = 2048; p[7].Direction = ParameterDirection.Output;
            p[8] = new OracleParameter("BASIS_PUNKT_BTI_ADR", OracleType.VarChar); p[8].Size = 2048; p[8].Direction = ParameterDirection.Output;
            p[9] = new OracleParameter("REGL_NAME", OracleType.VarChar); p[9].Size = 2048; p[9].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosAndBasisPunkts2", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            GOS_PUNKT = p[2].Value.ToString();
            BASIS_PUNKT_INFO = p[3].Value.ToString();
            BASIS_PUNKT_DOCS = p[4].Value.ToString();
            BASIS_PUNKT_APPLICANT = p[5].Value.ToString();
            BASIS_PUNKT_PROXY = p[6].Value.ToString();
            BASIS_PUNKT_PORTAL = p[7].Value.ToString();
            BASIS_PUNKT_BTI_ADR = p[8].Value.ToString();
            REGL_NAME = p[9].Value.ToString();

            return Convert.ToString(p[0].Value);
        }

        public int GetGosNByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosNByRubrID", p, tran);

            return Convert.ToInt32(p[0].Value);
        }

        public int GetClassIDByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetClassIDByRubrID", p, tran);

            return Convert.ToInt32(p[0].Value);
        }

        public string GetGosUslReglNameByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosUslReglNameByRubrID", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string GetGosUslReglNameByRubrname(OracleConnection conn, OracleTransaction tran, string Rubrname)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("Rubrname_", OracleType.VarChar); p[1].Value = Rubrname; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosUslReglNameByRubrname", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string FixAdminRegFact(OracleConnection conn, OracleTransaction tran, int id_documents_, string username_, DateTime adm_date_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("username_", OracleType.VarChar); p[2].Value = username_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("adm_date_", OracleType.DateTime); p[3].Value = adm_date_; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.FixAdminRegFact", p, tran);

            return Convert.ToString(p[0].Value);
        }


        public DataTable GetAdmRegInfo(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetAdmRegInfo", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string ClearBlock(OracleConnection conn, OracleTransaction tran, int id_documents_, int block_number_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("block_number_", OracleType.Number); p[2].Value = block_number_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.ClearBlock", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public bool CheckSuperUserOK(OracleConnection conn, OracleTransaction tran, string Username_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("Username_", OracleType.VarChar); p[1].Value = Username_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckSuperUserOK", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public string AddSPDItem(OracleConnection conn, OracleTransaction tran, int id_documents_, DateTime date_insert_,
            bool b1_, bool b2_, bool b3_, bool b4_, bool b5_, bool b6_, bool b7_, bool b8_, bool b9_, bool b10_,
            bool b11_, bool b12_, bool b13_, bool b14_, bool b15_, bool b16_, bool b17_, bool b18_, bool b19_, bool b20_,
            bool b21_, bool b22_, bool b23_, bool b24_, bool b25_, bool b26_, bool b27_, bool b28_)
        {
            OracleParameter[] p = new OracleParameter[1 + 30];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("date_insert_", OracleType.DateTime); p[2].Value = date_insert_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("b1_", OracleType.Number); p[3].Value = Convert.ToInt32(b1_); p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("b2_", OracleType.Number); p[4].Value = Convert.ToInt32(b2_); p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("b3_", OracleType.Number); p[5].Value = Convert.ToInt32(b3_); p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("b4_", OracleType.Number); p[6].Value = Convert.ToInt32(b4_); p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("b5_", OracleType.Number); p[7].Value = Convert.ToInt32(b5_); p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("b6_", OracleType.Number); p[8].Value = Convert.ToInt32(b6_); p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("b7_", OracleType.Number); p[9].Value = Convert.ToInt32(b7_); p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("b8_", OracleType.Number); p[10].Value = Convert.ToInt32(b8_); p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("b9_", OracleType.Number); p[11].Value = Convert.ToInt32(b9_); p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("b10_", OracleType.Number); p[12].Value = Convert.ToInt32(b10_); p[12].Direction = ParameterDirection.Input;
            p[13] = new OracleParameter("b11_", OracleType.Number); p[13].Value = Convert.ToInt32(b11_); p[13].Direction = ParameterDirection.Input;
            p[14] = new OracleParameter("b12_", OracleType.Number); p[14].Value = Convert.ToInt32(b12_); p[14].Direction = ParameterDirection.Input;
            p[15] = new OracleParameter("b13_", OracleType.Number); p[15].Value = Convert.ToInt32(b13_); p[15].Direction = ParameterDirection.Input;
            p[16] = new OracleParameter("b14_", OracleType.Number); p[16].Value = Convert.ToInt32(b14_); p[16].Direction = ParameterDirection.Input;
            p[17] = new OracleParameter("b15_", OracleType.Number); p[17].Value = Convert.ToInt32(b15_); p[17].Direction = ParameterDirection.Input;
            p[18] = new OracleParameter("b16_", OracleType.Number); p[18].Value = Convert.ToInt32(b16_); p[18].Direction = ParameterDirection.Input;
            p[19] = new OracleParameter("b17_", OracleType.Number); p[19].Value = Convert.ToInt32(b17_); p[19].Direction = ParameterDirection.Input;
            p[20] = new OracleParameter("b18_", OracleType.Number); p[20].Value = Convert.ToInt32(b18_); p[20].Direction = ParameterDirection.Input;
            p[21] = new OracleParameter("b19_", OracleType.Number); p[21].Value = Convert.ToInt32(b19_); p[21].Direction = ParameterDirection.Input;
            p[22] = new OracleParameter("b20_", OracleType.Number); p[22].Value = Convert.ToInt32(b20_); p[22].Direction = ParameterDirection.Input;
            p[23] = new OracleParameter("b21_", OracleType.Number); p[23].Value = Convert.ToInt32(b21_); p[23].Direction = ParameterDirection.Input;
            p[24] = new OracleParameter("b22_", OracleType.Number); p[24].Value = Convert.ToInt32(b22_); p[24].Direction = ParameterDirection.Input;
            p[25] = new OracleParameter("b23_", OracleType.Number); p[25].Value = Convert.ToInt32(b23_); p[25].Direction = ParameterDirection.Input;
            p[26] = new OracleParameter("b24_", OracleType.Number); p[26].Value = Convert.ToInt32(b24_); p[26].Direction = ParameterDirection.Input;
            p[27] = new OracleParameter("b25_", OracleType.Number); p[27].Value = Convert.ToInt32(b25_); p[27].Direction = ParameterDirection.Input;
            p[28] = new OracleParameter("b26_", OracleType.Number); p[28].Value = Convert.ToInt32(b26_); p[28].Direction = ParameterDirection.Input;
            p[29] = new OracleParameter("b27_", OracleType.Number); p[29].Value = Convert.ToInt32(b27_); p[29].Direction = ParameterDirection.Input;
            p[30] = new OracleParameter("b28_", OracleType.Number); p[30].Value = Convert.ToInt32(b28_); p[30].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.AddSPDItem", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public string UndoSPDItem(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.UndoSPDItem", p, tran);
            return Convert.ToString(p[0].Value);
        }

        public DataSet GetCustomersByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID_", OracleType.Number); p[1].Value = RubrID_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetCustomersByRubrID", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public string CheckDolgForPrivatization2(OracleConnection conn, OracleTransaction tran, string REG_N_, DateTime DOC_DATE_,
            ref double NEDOPL, ref double PENI)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("REG_N_", OracleType.VarChar); p[1].Value = REG_N_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("DOC_DATE_", OracleType.DateTime); p[2].Value = DOC_DATE_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("NEDOPL", OracleType.Number); p[3].Direction = ParameterDirection.Output;
            p[4] = new OracleParameter("PENI", OracleType.Number); p[4].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckDolgForPrivatization2", p, tran);

            if (p[3].Value.Equals(DBNull.Value))
                NEDOPL = 0;
            else 
                NEDOPL = Convert.ToDouble(p[3].Value);

            if (p[4].Value.Equals(DBNull.Value))
                PENI = 0;
            else 
                PENI = Convert.ToDouble(p[4].Value);

            return Convert.ToString(p[0].Value);
        }

        public bool CheckDocStatusLand(OracleConnection conn, OracleTransaction tran, string doc_num_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("doc_num_", OracleType.VarChar); p[1].Value = doc_num_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckDocStatusLand", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public bool CheckDocStatusLandByCadNum(OracleConnection conn, OracleTransaction tran, string cad_num_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("cad_num_", OracleType.VarChar); p[1].Value = cad_num_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckDocStatusLandByCadNum", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public bool CheckDocStatusRealty(OracleConnection conn, OracleTransaction tran, string doc_num_, string doc_date_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("doc_num_", OracleType.VarChar); p[1].Value = doc_num_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("doc_date_", OracleType.VarChar); p[2].Value = doc_date_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckDocStatusRealty", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public bool CheckApplicant(OracleConnection conn, OracleTransaction tran, string appltype_, int gosn_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("appltype_", OracleType.VarChar); p[1].Value = appltype_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("gosn_", OracleType.Number); p[2].Value = gosn_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckApplicant", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public bool CheckCustomersIsPortalOnly(OracleConnection conn, OracleTransaction tran, int gosn_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("gosn_", OracleType.Number); p[1].Value = gosn_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckCustomersIsPortalOnly", p, tran);
            return Convert.ToBoolean(p[0].Value);
        }

        public DataSet GetGosUslList(OracleConnection conn, OracleTransaction tran, string ActLogin, int actual)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("ActLogin", OracleType.VarChar); p[1].Value = ActLogin; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("actual", OracleType.Number); p[2].Value = actual; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosUslList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetIndexListByRubrID(OracleConnection conn, OracleTransaction tran, int RubrID, int actual)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("RubrID", OracleType.Number); p[1].Value = RubrID; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("actual", OracleType.Number); p[2].Value = actual; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetIndexListByRubrID", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetContentListByViewID(OracleConnection conn, OracleTransaction tran, int id_view)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("ViewID", OracleType.Number); p[1].Value = id_view; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetContentListByViewID", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public string RegChildRejectInDocControl(OracleConnection conn, OracleTransaction tran, string docnum, string authororgname,
            string author, ref int replydocid, ref string replydocnum, ref DateTime replydocdate, ref bool existdoc, ref bool existdeny, string SignatureFIO)
        {
            OracleParameter[] p = new OracleParameter[1 + 9];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum", OracleType.VarChar); p[1].Value = docnum; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("authororgname", OracleType.VarChar); p[2].Value = authororgname; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("author", OracleType.VarChar); p[3].Value = author; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("REPLYDOCNUM", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("REPLYDOCDATE", OracleType.DateTime); p[5].Direction = ParameterDirection.Output;
            p[6] = new OracleParameter("REPLYIDDOC", OracleType.Number); p[6].Direction = ParameterDirection.Output;
            p[7] = new OracleParameter("EXISTDOC", OracleType.Number); p[7].Direction = ParameterDirection.Output;
            p[8] = new OracleParameter("EXISTDENY", OracleType.Number); p[8].Direction = ParameterDirection.Output;
            p[9] = new OracleParameter("SignatureFIO", OracleType.VarChar); p[9].Value = SignatureFIO; p[9].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegChildRejectInDocControl", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            existdoc = Convert.ToBoolean(p[7].Value);
            existdeny = Convert.ToBoolean(p[8].Value);
            if (existdoc && !existdeny)
            {
                replydocid = Convert.ToInt32(p[6].Value);
                replydocnum = p[4].Value.ToString();
                replydocdate = Convert.ToDateTime(p[5].Value);
            }

            return Convert.ToString(p[0].Value);
        }

        public int GetIDDocByDocnum(OracleConnection conn, OracleTransaction tran, string docnum)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum", OracleType.VarChar); p[1].Value = docnum; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetIDDocByDocnum", p, tran);

            return Convert.ToInt32(p[0].Value);
        }

        public string GetParentNumByChildID(OracleConnection conn, OracleTransaction tran, int id_child_doc, ref string parent_docnum)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_child_doc", OracleType.Number); p[1].Value = id_child_doc; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("parent_docnum", OracleType.VarChar); p[2].Size = 2048; p[2].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetParentNumByChildID", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            parent_docnum = Convert.ToString(p[2].Value);

            return Convert.ToString(p[0].Value);
        }

        public string GetParentIDByChildID(OracleConnection conn, OracleTransaction tran, int id_child_doc, ref int parent_id)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_child_doc", OracleType.Number); p[1].Value = id_child_doc; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("parent_id", OracleType.Number); p[2].Size = 2048; p[2].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetParentIDByChildID", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            parent_id = Convert.ToInt32(p[2].Value);

            return Convert.ToString(p[0].Value);
        }

        public string GetStatus(OracleConnection conn, OracleTransaction tran, int id_documents_, ref string status_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("status_", OracleType.VarChar); p[2].Size = 2048; p[2].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetStatus", p, tran);

            if (Convert.ToString(p[0].Value).Equals(""))
                status_ = Convert.ToString(p[2].Value);

            return Convert.ToString(p[0].Value);
        }

        public string GetStatus2(OracleConnection conn, OracleTransaction tran, string rn_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("rn_", OracleType.VarChar); p[1].Value = rn_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.interface_pkg.GetStatus2", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public bool IsSendedToSPD(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsSendedToSPD", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsRubrOfReturnDoc(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsRubrOfReturnDoc", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsReturnDoc(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsReturnDoc", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsRubrOfNoteDoc(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsRubrOfNoteDoc", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsRubrOfVariativeSrok(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsRubrOfVariativeSrok", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsRubrOfMFCSrok(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsRubrOfMFCSrok", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsRubrLawDefinition(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsRubrLawDefinition", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public int GetRubrIDOfVerifyIspol(OracleConnection conn, OracleTransaction tran, int GosN_)
        {
            OracleParameter[] p = new OracleParameter[0 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("GosN_", OracleType.Number); p[1].Value = GosN_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRubrIDOfVerifyIspol", p, tran);

            return Convert.ToInt32(p[0].Value);
        }

        public string CheckDocExistenz(OracleConnection conn, OracleTransaction tran, string docnum_, string base_, ref bool exist_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum_", OracleType.VarChar); p[1].Value = docnum_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("base_", OracleType.VarChar); p[2].Value = base_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("exist_", OracleType.Number); p[3].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckDocExistenz", p, tran);

            if (Convert.ToString(p[0].Value).Equals(""))
                exist_ = Convert.ToBoolean(Convert.ToInt32(p[3].Value));

            return Convert.ToString(p[0].Value);
        }

        public DataSet GetAutocompleteBySNILS(OracleConnection conn, OracleTransaction tran, string snils_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("snils_", OracleType.VarChar); p[1].Value = snils_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteBySNILS", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteByPassport(OracleConnection conn, OracleTransaction tran, string serial_, string num_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("serial_", OracleType.VarChar); p[1].Value = serial_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("num_", OracleType.VarChar); p[2].Value = num_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteByPassport", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteByINN(OracleConnection conn, OracleTransaction tran, string inn_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("inn_", OracleType.VarChar); p[1].Value = inn_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteByINN", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteFLSByINN(OracleConnection conn, OracleTransaction tran, string inn_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("inn_", OracleType.VarChar); p[1].Value = inn_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteFLSByINN", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteRealty(OracleConnection conn, OracleTransaction tran, string docnum_, string doc_date_, string number_sub_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum_", OracleType.VarChar); p[1].Value = docnum_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("docdate_", OracleType.VarChar); p[2].Value = doc_date_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("number_sub_", OracleType.VarChar); p[3].Value = number_sub_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("realty_data_", OracleType.Cursor); p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("land_data_", OracleType.Cursor); p[5].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteRealty", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("data_");
                adapter.Fill(myset);
            }
            myset.Tables[0].TableName = "Realty";
            myset.Tables[1].TableName = "Land";

            return myset;
        }

        public DataSet GetAutocompleteByDocnum(OracleConnection conn, OracleTransaction tran, string registr_n_, string registr_date_, string number_sub_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("registr_n_", OracleType.VarChar); p[1].Value = registr_n_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("registr_date_", OracleType.VarChar); p[2].Value = registr_date_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("number_sub_", OracleType.VarChar); p[3].Value = number_sub_; p[3].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteByDocnum", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteFLSByDocnum(OracleConnection conn, OracleTransaction tran, string registr_n_, string registr_date_, string number_sub_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("registr_n_", OracleType.VarChar); p[1].Value = registr_n_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("registr_date_", OracleType.VarChar); p[2].Value = registr_date_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("number_sub_", OracleType.VarChar); p[3].Value = number_sub_; p[3].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteFLSByDocnum", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetSubjectsByDocnum(OracleConnection conn, OracleTransaction tran, string registr_n_, string registr_date_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("registr_n_", OracleType.VarChar); p[1].Value = registr_n_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("registr_date_", OracleType.VarChar); p[2].Value = registr_date_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetSubjectsByDocnum", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetAutocompleteByBankName(OracleConnection conn, OracleTransaction tran, string bankname_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("bankname_", OracleType.VarChar); p[1].Value = bankname_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetAutocompleteByBankName", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public bool CheckDate(string DateToTest)
        {
            DateTime TestDate;
            if (!DateTime.TryParse(DateToTest, out TestDate))
                return false;
            return true;
        }

        public DataSet GetDoc_RecordsByAll(OracleConnection conn, OracleTransaction tran, string rn_, string rn_data1, string rn_data2,
            string rubrname_, string customer_, string kontr_data1, string kontr_data2, string ispol_data1, string ispol_data2,
            string vidan_data1, string vidan_data2, string status_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 12];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("rn_", OracleType.VarChar); p[1].Value = rn_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("rn_data1", OracleType.DateTime); if (CheckDate(rn_data1)) p[2].Value = Convert.ToDateTime(rn_data1).Date; else p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("rn_data2", OracleType.DateTime); if (CheckDate(rn_data2)) p[3].Value = Convert.ToDateTime(rn_data2).Date; else p[3].Value = DBNull.Value; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("rubrname_", OracleType.VarChar); p[4].Value = rubrname_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("customer_", OracleType.VarChar); p[5].Value = customer_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("kontr_data1", OracleType.DateTime); if (CheckDate(kontr_data1)) p[6].Value = Convert.ToDateTime(kontr_data1).Date; else p[6].Value = DBNull.Value; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("kontr_data2", OracleType.DateTime); if (CheckDate(kontr_data2)) p[7].Value = Convert.ToDateTime(kontr_data2).Date; else p[7].Value = DBNull.Value; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("ispol_data1", OracleType.DateTime); if (CheckDate(ispol_data1)) p[8].Value = Convert.ToDateTime(ispol_data1).Date; else p[8].Value = DBNull.Value; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("ispol_data2", OracleType.DateTime); if (CheckDate(ispol_data2)) p[9].Value = Convert.ToDateTime(ispol_data2).Date; else p[9].Value = DBNull.Value; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("vidan_data1", OracleType.DateTime); if (CheckDate(vidan_data1)) p[10].Value = Convert.ToDateTime(vidan_data1).Date; else p[10].Value = DBNull.Value; p[10].Direction = ParameterDirection.Input;
            p[11] = new OracleParameter("vidan_data2", OracleType.DateTime); if (CheckDate(vidan_data2)) p[11].Value = Convert.ToDateTime(vidan_data2).Date; else p[11].Value = DBNull.Value; p[11].Direction = ParameterDirection.Input;
            p[12] = new OracleParameter("status_", OracleType.VarChar); p[12].Value = status_ ; p[12].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetDoc_RecordsByAll", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataSet GetKontrDates(OracleConnection conn, OracleTransaction tran, int id_gosn_, DateTime reg_date_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_gosn_", OracleType.Number); p[1].Value = id_gosn_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("reg_date_", OracleType.DateTime); p[2].Value = reg_date_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetKontrDates", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataTable GetLevel1(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 0];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel1", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLevel2(OracleConnection conn, OracleTransaction tran, string parentGuid_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentGuid_", OracleType.VarChar); p[1].Value = parentGuid_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel2", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLevel3(OracleConnection conn, OracleTransaction tran, string parentGuid_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentGuid_", OracleType.VarChar); p[1].Value = parentGuid_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel3", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLevel4(OracleConnection conn, OracleTransaction tran, string parentGuid_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentGuid_", OracleType.VarChar); p[1].Value = parentGuid_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel4", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLevel5(OracleConnection conn, OracleTransaction tran, string parentGuid_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentGuid_", OracleType.VarChar); p[1].Value = parentGuid_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel5", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLevel6(OracleConnection conn, OracleTransaction tran, string parentGuid_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("parentGuid_", OracleType.VarChar); p[1].Value = parentGuid_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetLevel6", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string GetAddrByGuides(OracleConnection conn, OracleTransaction tran, string Guid1, string Guid2, string Guid3,
            string Guid4, string Guid5, string Guid6, string FlatOffice, string index_n)
        {
            OracleParameter[] p = new OracleParameter[1 + 8];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("Guid1", OracleType.VarChar); p[1].Value = Guid1; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("Guid2", OracleType.VarChar); p[2].Value = Guid2; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("Guid3", OracleType.VarChar); p[3].Value = Guid3; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("Guid4", OracleType.VarChar); p[4].Value = Guid4; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("Guid5", OracleType.VarChar); p[5].Value = Guid5; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("Guid6", OracleType.VarChar); p[6].Value = Guid6; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("FlatOffice", OracleType.VarChar); p[7].Value = FlatOffice; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("index_n", OracleType.VarChar); p[8].Value = index_n; p[8].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.fias_pkg.GetAddrByGuides", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string MFCDocsRecievedInfo(OracleConnection conn, OracleTransaction tran, int id_documents_, ref bool is_recieved_, ref string recieve_date_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("is_recieved_", OracleType.Number); p[2].Direction = ParameterDirection.Output;
            p[3] = new OracleParameter("recieve_date_", OracleType.DateTime); p[3].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.MFCDocsRecievedInfo", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            is_recieved_ = Convert.ToBoolean(Convert.ToInt32(p[2].Value));
            if (is_recieved_)
            {
                recieve_date_ = Convert.ToDateTime(p[3].Value).ToString("dd.MM.yyyy");
            }

            return Convert.ToString(p[0].Value);
        }

        public string GetRootDocnum(OracleConnection conn, OracleTransaction tran, string docnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum_", OracleType.VarChar); p[1].Value = docnum_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRootDocnum", p, tran);

            return Convert.ToString(p[0].Value);
        }
        

        public double ConvToDouble(object val)
        {
            string culture
                    = System.Globalization.CultureInfo.CurrentCulture.Name;
            string numberDecimalSeparator
                    = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            System.Globalization.CultureInfo ci
                    = new System.Globalization.CultureInfo(culture);   //System.Globalization.CultureInfo.CurrentCulture;
            ci.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;

            return ConvToDouble(val, ci);
        }

        public double ConvToDouble(object val, System.Globalization.CultureInfo ci)
        {
            double v;
            try
            {
                v = Convert.ToDouble(val, ci);
            }
            catch
            {
                if (ci.NumberFormat.NumberDecimalSeparator == ".")
                    ci.NumberFormat.NumberDecimalSeparator = ",";
                else
                {
                    if (ci.NumberFormat.NumberDecimalSeparator == ",")
                        ci.NumberFormat.NumberDecimalSeparator = ".";
                }
                v = Convert.ToDouble(val, ci);
            }
            return v;
        }

        public DataSet GetChildDocs(OracleConnection conn, OracleTransaction tran, int id_documents_, bool WithParent)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("withparent_", OracleType.Number); p[2].Value = Convert.ToInt32(WithParent); p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetChildDocs", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public DataTable GetObrazByBarcode(OracleConnection conn, OracleTransaction tran, string barcode_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("barcode_", OracleType.Number); p[1].Value = barcode_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetObrazByBarcode", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetSPDObrazByRN(OracleConnection conn, OracleTransaction tran, string rn_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("rn_", OracleType.VarChar); p[1].Value = rn_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetSPDObrazByRN", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }


        public bool CheckFLSExistence(OracleConnection conn, OracleTransaction tran, string FLSNum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("FLSNum_", OracleType.VarChar); p[1].Value = FLSNum_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckFLSExistence", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public DataTable GetRegistryByIDReestr(OracleConnection conn, OracleTransaction tran, int id_reestr_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reestr_", OracleType.Number); p[1].Value = id_reestr_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRegistryByIDReestr", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetTransferredDocs(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetTransferredDocs", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetDifficultyTotalState(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.difficulty_statistics_pkg.GetTotalState", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetRegTimeStat(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetRegTimeStat", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetTotalState(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_,
            int stat_type_, int filter_type_, ref int id_reg_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("stat_type_", OracleType.Number); p[3].Value = stat_type_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("filter_type_", OracleType.Number); p[4].Value = filter_type_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("id_reg_", OracleType.Number); p[5].Value = id_reg_; p[5].Direction = ParameterDirection.InputOutput;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetTotalState2", p, tran);

            if (!p[5].Value.ToString().Equals(""))
                id_reg_ = Convert.ToInt32(p[5].Value);
            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string CreateStatistics(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.CreateStatistics3", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public DataTable GetStatRegInfoByIDReg(OracleConnection conn, OracleTransaction tran, int id_reg_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reg_", OracleType.Number); p[1].Value = id_reg_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetStatRegInfoByIDReg", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetRegListByRubrAndColumn(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_,
            string rubrname_, string ispname_, string depid_, int column_n_, int stat_type_, int filter_type_, int id_reg_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 9];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("rubrname_", OracleType.VarChar); p[3].Value = rubrname_; p[3].Direction = ParameterDirection.Input;
            if (ispname_.Equals(""))
            { p[4] = new OracleParameter("ispname_", OracleType.VarChar); p[4].Value = DBNull.Value; p[4].Direction = ParameterDirection.Input; }
            else
            { p[4] = new OracleParameter("ispname_", OracleType.VarChar); p[4].Value = ispname_; p[4].Direction = ParameterDirection.Input; }
            if (depid_.Equals(""))
            { p[5] = new OracleParameter("depid_", OracleType.VarChar); p[5].Value = DBNull.Value; p[5].Direction = ParameterDirection.Input; }
            else
            { p[5] = new OracleParameter("depid_", OracleType.VarChar); p[5].Value = depid_; p[5].Direction = ParameterDirection.Input; }
            p[6] = new OracleParameter("column_n_", OracleType.Number); p[6].Value = column_n_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("stat_type_", OracleType.Number); p[7].Value = stat_type_; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("filter_type_", OracleType.Number); p[8].Value = filter_type_; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("id_reg_", OracleType.Number); p[9].Value = id_reg_; p[9].Direction = ParameterDirection.Input;
            
            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetRegListByRubrAndColumn", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetRegTimeListByRubrAndFam(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_,
            string fam_, string rubrname_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("fam_", OracleType.VarChar); p[3].Value = fam_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("rubrname_", OracleType.VarChar); p[4].Value = rubrname_; p[4].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetRegTimeListByRubrAndFam", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetRegistryInfoByIDReestr(OracleConnection conn, OracleTransaction tran, int id_reestr_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reestr_", OracleType.Number); p[1].Value = id_reestr_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetRegistryInfoByIDReestr", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string AddDocNumToReestr(OracleConnection conn, OracleTransaction tran, int id_documents_, string id_reestr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.VarChar); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            if (id_reestr_.Equals(""))
            {
                p[2] = new OracleParameter("id_reestr_", OracleType.VarChar); p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input;
            }
            else
            {
                p[2] = new OracleParameter("id_reestr_", OracleType.VarChar); p[2].Value = id_reestr_; p[2].Direction = ParameterDirection.Input;
            }

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.AddDocNumToReestr", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string CreateReestr(OracleConnection conn, OracleTransaction tran, string reestrname_, string username_, ref int id_reestr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("reestrname_", OracleType.VarChar); p[1].Value = reestrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("username_", OracleType.VarChar); p[2].Value = username_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_reestr_", OracleType.Number); p[3].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CreateReestr", p, tran);

            if (Convert.ToString(p[0].Value).Equals(""))
                id_reestr_ = Convert.ToInt32(p[3].Value);

            return Convert.ToString(p[0].Value);
        }

        public bool DocIsForSentToSPD(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.DocIsForSentToSPD", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsSPDRubr(OracleConnection conn, OracleTransaction tran, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_rubr_", OracleType.Number); p[1].Value = id_rubr_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsSPDRubr", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public bool IsDocExistInSPD(OracleConnection conn, OracleTransaction tran, string docnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum_", OracleType.VarChar); p[1].Value = docnum_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsDocExistInSPD", p, tran);

            return Convert.ToBoolean(p[0].Value);
        }

        public string ChangeRubr(OracleConnection conn, OracleTransaction tran, int id_documents_, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_rubr_", OracleType.Number); p[2].Value = id_rubr_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.ChangeRubr", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string ChangeAnyRubr(OracleConnection conn, OracleTransaction tran, int id_documents_, int id_rubr_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("id_rubr_", OracleType.Number); p[2].Value = id_rubr_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.ChangeAnyRubr", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string LogToAudit(OracleConnection conn, OracleTransaction tran, string CommandType, string ObjectName, string ObjectParam, string Username)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("type_event_", OracleType.VarChar); p[1].Value = CommandType; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("object_name_", OracleType.VarChar); p[2].Value = ObjectName; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("object_param_", OracleType.VarChar); p[3].Value = ObjectParam; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("username_", OracleType.VarChar); p[4].Value = Username; p[4].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.LogToAudit", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string FixRegTime(OracleConnection conn, OracleTransaction tran, int id_documents_, string rn_, DateTime rn_data_, int id_rubr_,
            string username_, int total_regtime_, DateTime end_regtime_)
        {
            OracleParameter[] p = new OracleParameter[1 + 7];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("rn_", OracleType.VarChar); p[2].Value = rn_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("rn_data_", OracleType.DateTime); p[3].Value = rn_data_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("id_rubr_", OracleType.Number); p[4].Value = id_rubr_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("username_", OracleType.VarChar); p[5].Value = username_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("total_regtime_", OracleType.Number); p[6].Value = total_regtime_; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("end_regtime_", OracleType.DateTime); p[7].Value = end_regtime_; p[7].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.FixRegTime", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public DataTable GetControlDoc(OracleConnection conn, OracleTransaction tran, string DateFrom, string DateTill, int id_uprav_, int filter_type_, int control_type_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[5 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("DateFrom", OracleType.VarChar); p[1].Value = DateFrom; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("DateTill", OracleType.VarChar); p[2].Value = DateTill; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_uprav_", OracleType.Number); p[3].Value = id_uprav_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("filter_type_", OracleType.Number); p[4].Value = filter_type_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("control_type_", OracleType.Number); p[5].Value = control_type_; p[5].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetControlDoc", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetStopped(OracleConnection conn, OracleTransaction tran, string DateFrom, string DateTill, int id_uprav_, int filter_type_,
            string DateStopStartFrom, string DateStopStartTill, string DateStopEndFrom, string DateStopEndTill, string etap_, int control_type)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[10 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("DateFrom", OracleType.VarChar); p[1].Value = DateFrom; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("DateTill", OracleType.VarChar); p[2].Value = DateTill; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_uprav_", OracleType.Number); p[3].Value = id_uprav_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("filter_type_", OracleType.Number); p[4].Value = filter_type_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("DateStopStartFrom", OracleType.VarChar); p[5].Value = DateStopStartFrom; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("DateStopStartTill", OracleType.VarChar); p[6].Value = DateStopStartTill; p[6].Direction = ParameterDirection.Input;
            p[7] = new OracleParameter("DateStopEndFrom", OracleType.VarChar); p[7].Value = DateStopEndFrom; p[7].Direction = ParameterDirection.Input;
            p[8] = new OracleParameter("DateStopEndTill", OracleType.VarChar); p[8].Value = DateStopEndTill; p[8].Direction = ParameterDirection.Input;
            p[9] = new OracleParameter("etap_", OracleType.VarChar); p[9].Value = etap_; p[9].Direction = ParameterDirection.Input;
            p[10] = new OracleParameter("control_type_", OracleType.Number); p[10].Value = control_type; p[10].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetStopped", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetRubricatorsByTypeAndUprav(OracleConnection conn, OracleTransaction tran, int id_uprav_, int filter_type_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_uprav_", OracleType.Number); p[1].Value = id_uprav_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("filter_type_", OracleType.Number); p[2].Value = filter_type_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetRubricatorsByTypeAndUprav", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetIPRegister(OracleConnection conn, OracleTransaction tran, int id_gosn_, DateTime start_period_, DateTime end_period_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_gosn_", OracleType.Number); p[1].Value = id_gosn_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("start_period_", OracleType.DateTime); p[2].Value = start_period_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("end_period_", OracleType.DateTime); p[3].Value = end_period_; p[3].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetIPRegister", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetPGURegister(OracleConnection conn, OracleTransaction tran, DateTime start_period_, DateTime end_period_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("start_period_", OracleType.DateTime); p[1].Value = start_period_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("end_period_", OracleType.DateTime); p[2].Value = end_period_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetPGURegister", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataSet GetGosUslListIPReg(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetGosUslListIPReg", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public string SaveProcessCommonData(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int elementnum_, string content_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("elementnum_", OracleType.Number); p[2].Value = elementnum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("content_", OracleType.VarChar); p[3].Value = content_; p[3].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveProcessCommonData", p, tran);

            return Convert.ToString(p[0].Value);
        }


        public string SaveProcessProcData(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int procnum_, int elementnum_, string content_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("procnum_", OracleType.Number); p[2].Value = procnum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("elementnum_", OracleType.Number); p[3].Value = elementnum_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("content_", OracleType.VarChar); p[4].Value = content_; p[4].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveProcessProcData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string SaveProcessIndicatorsData(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int indicatornum_,
            string indicatorname_, string id_indview_, string calculation_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("indicatornum_", OracleType.Number); p[2].Value = indicatornum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("indicatorname_", OracleType.VarChar); p[3].Value = indicatorname_; p[3].Direction = ParameterDirection.Input;
            if (id_indview_.Equals(""))
            {
                p[4] = new OracleParameter("id_indview_", OracleType.Number); p[4].Value = DBNull.Value; p[4].Direction = ParameterDirection.Input;
            }
            else
            {
                p[4] = new OracleParameter("id_indview_", OracleType.Number); p[4].Value = Convert.ToInt32(id_indview_); p[4].Direction = ParameterDirection.Input;
            }
            p[5] = new OracleParameter("calculation_", OracleType.VarChar); p[5].Value = calculation_; p[5].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveProcessIndicatorsData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string SaveProcessFileData(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int idfile_,
            string id_docname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("idfile_", OracleType.Number); p[2].Value = idfile_; p[2].Direction = ParameterDirection.Input;
            if (id_docname_.Equals(""))
            {
                p[3] = new OracleParameter("id_docname_", OracleType.Number); p[3].Value = DBNull.Value; p[3].Direction = ParameterDirection.Input;
            }
            else
            {
                p[3] = new OracleParameter("id_docname_", OracleType.Number); p[3].Value = id_docname_; p[3].Direction = ParameterDirection.Input;
            }

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveProcessFileData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string AddProc(OracleConnection conn, OracleTransaction tran, int spd_rubrname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.AddProc", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string AddIndicator(OracleConnection conn, OracleTransaction tran, int spd_rubrname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.AddIndicator", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string RemoveProc(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int procnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("procnum_", OracleType.Number); p[2].Value = procnum_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.RemoveProc", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string RemoveIndicator(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int indicatornum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("indicatornum_", OracleType.Number); p[2].Value = indicatornum_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.RemoveIndicator", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string RemoveFile(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int idfile_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("idfile_", OracleType.Number); p[2].Value = idfile_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.RemoveFile", p, tran);

            return Convert.ToString(p[0].Value);
        }
 
        public DataTable GetProcessDataByRubr(OracleConnection conn, OracleTransaction tran, string GridName, int spd_rubrname_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.GetProcess" + GridName + "DataByRubr", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetProcessesList(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.GetProcessesList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetDifficultyReg(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.difficulty_statistics_pkg.GetDifficultyReg", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string GetPrNameBySPDrubrname(OracleConnection conn, OracleTransaction tran, int spd_rubrname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.GetPrNameBySPDrubrname", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string UploadProcessFile(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, string id_docname_, string filetype_, string login_, byte[] filedata_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            if (id_docname_.Equals(""))
            {
                p[2] = new OracleParameter("id_docname_", OracleType.Number); p[2].Value = DBNull.Value; p[2].Direction = ParameterDirection.Input;
            }
            else
            {
                p[2] = new OracleParameter("id_docname_", OracleType.Number); p[2].Value = id_docname_; p[2].Direction = ParameterDirection.Input;
            }
            p[3] = new OracleParameter("filetype_", OracleType.VarChar); p[3].Value = filetype_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("login_", OracleType.VarChar); p[4].Value = login_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("filedata_", OracleType.Blob); p[5].Value = filedata_; p[5].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.UploadProcessFile", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string GetProcessFile(OracleConnection conn, OracleTransaction tran, int spd_rubrname_, int idfile_, ref byte[] filedata_, ref string filetype_, ref string docname_)
        {
            OracleParameter[] p = new OracleParameter[1 + 5];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("spd_rubrname_", OracleType.Number); p[1].Value = spd_rubrname_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("idfile_", OracleType.Number); p[2].Value = idfile_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("filedata_", OracleType.Blob); p[3].Direction = ParameterDirection.Output;
            p[4] = new OracleParameter("filetype_", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("docname_", OracleType.VarChar); p[5].Size = 2048; p[5].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.GetProcessFile", p, tran);

            if (Convert.ToString(p[0].Value).Equals(""))
            {
                OracleLob blob = (OracleLob)p[3].Value;
                filedata_ = new byte[blob.Length];
                blob.Read(filedata_, 0, Convert.ToInt32(blob.Length));
                filetype_ = p[4].Value.ToString();
                docname_ = p[5].Value.ToString();
            }

            return Convert.ToString(p[0].Value);
        }

        public DataTable GetLawLevel1(OracleConnection conn, OracleTransaction tran)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetLawLevel1", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetLawData(OracleConnection conn, OracleTransaction tran, string id_lawcourt_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_lawcourt_", OracleType.Number); p[1].Value = Convert.ToInt32(id_lawcourt_); p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.interface_pkg.GetLawData", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public bool CheckLawsuitNumExistence(OracleConnection conn, OracleTransaction tran, string lawsuitnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("lawsuitnum_", OracleType.VarChar); p[1].Value = lawsuitnum_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckLawsuitNumExistence", p, tran);

            return Convert.ToBoolean(Convert.ToInt32(p[0].Value));
        }

        public bool CheckMeetingExistence(OracleConnection conn, OracleTransaction tran, DateTime meetingtime_, string lawsuitnum_, int id_lawcourt_, ref string docnum_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("meetingdatetime_", OracleType.DateTime); p[1].Value = meetingtime_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("lawsuitnum_", OracleType.VarChar); p[2].Value = lawsuitnum_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("id_lawcourt_", OracleType.Number); p[3].Value = id_lawcourt_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("docnum_", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.CheckMeetingExistence", p, tran);

            docnum_ = p[4].Value.ToString();

            return Convert.ToBoolean(Convert.ToInt32(p[0].Value));
        }

        public string RegMeetingDoubleForInfo(OracleConnection conn, OracleTransaction tran, string docnum, string barcode, string SignatureFIO, ref string out_docnum, ref DateTime out_docdate)
        {
            OracleParameter[] p = new OracleParameter[1 + 6];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum", OracleType.VarChar); p[1].Value = docnum; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("barcode", OracleType.VarChar); p[2].Value = barcode; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("SignatureFIO", OracleType.VarChar); p[3].Value = SignatureFIO; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("out_docnum", OracleType.VarChar); p[4].Size = 2048; p[4].Direction = ParameterDirection.Output;
            p[5] = new OracleParameter("out_docdate", OracleType.DateTime); p[5].Direction = ParameterDirection.Output;
            p[6] = new OracleParameter("docdate", OracleType.DateTime); p[6].Direction = ParameterDirection.Output;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.RegMeetingDoubleForInfo", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            out_docnum = p[4].Value.ToString();
            out_docdate = Convert.ToDateTime(p[5].Value);

            return Convert.ToString(p[0].Value);
        }

        public DataTable GetGosCounts(OracleConnection conn, OracleTransaction tran, int gosn)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("gosn", OracleType.Number); p[1].Value = gosn; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.GetGosCounts", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetSettingsUpravZam(OracleConnection conn, OracleTransaction tran, string GridName)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.Get" + GridName, p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public string SaveUpravData(OracleConnection conn, OracleTransaction tran, int id_uprav_, string name_uprav_,
            string id_zam_, string phone_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_uprav_", OracleType.Number); p[1].Value = id_uprav_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("name_uprav_", OracleType.VarChar); p[2].Value = name_uprav_; p[2].Direction = ParameterDirection.Input;
            if (id_zam_.Equals(""))
            {
                p[3] = new OracleParameter("id_zam_", OracleType.Number); p[3].Value = DBNull.Value; p[3].Direction = ParameterDirection.Input;
            }
            else
            {
                p[3] = new OracleParameter("id_zam_", OracleType.Number); p[3].Value = Convert.ToInt32(id_zam_); p[3].Direction = ParameterDirection.Input;
            }
            p[4] = new OracleParameter("phone_", OracleType.VarChar); p[4].Value = phone_; p[4].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveUpravData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string SaveZamData(OracleConnection conn, OracleTransaction tran, int id_zam_, string name_zam_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_zam_", OracleType.Number); p[1].Value = id_zam_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("name_zam_", OracleType.VarChar); p[2].Value = name_zam_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.SaveZamData", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string RemoveUprav(OracleConnection conn, OracleTransaction tran, int id_uprav_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_uprav_", OracleType.Number); p[1].Value = id_uprav_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.RemoveUprav", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string RemoveZam(OracleConnection conn, OracleTransaction tran, int id_zam_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_zam_", OracleType.Number); p[1].Value = id_zam_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.admin_pkg.RemoveZam", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public bool MFCnumIsDoubled(OracleConnection conn, OracleTransaction tran, string docnum)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("docnum", OracleType.VarChar); p[1].Value = docnum; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.asguf2_pkg.MFCnumIsDoubled", p, tran);

            return Convert.ToBoolean(Convert.ToInt32(p[0].Value));
        }

        public string AddToSPDLog(OracleConnection conn, OracleTransaction tran, int id_documents_, DateTime logtime_, string problem_desc_)
        {
            OracleParameter[] p = new OracleParameter[1 + 3];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("logtime_", OracleType.DateTime); p[2].Value = logtime_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("problem_desc_", OracleType.VarChar); p[3].Value = problem_desc_; p[3].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2_pkg.AddToSPDLog", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string ChangeSPDItemStatus(OracleConnection conn, OracleTransaction tran, int id_documents_, int status_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("status_", OracleType.Number); p[2].Value = status_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2_pkg.ChangeSPDItemStatus", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public DataTable GetObrazByIdDoc(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2_pkg.GetObrazByIdDoc", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetBlobIdListByIdDoc(OracleConnection conn, OracleTransaction tran, int id_documents_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_documents_", OracleType.Number); p[1].Value = id_documents_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2_pkg.GetBlobIdListByIdDoc", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetBlobObrazById(OracleConnection conn, OracleTransaction tran, int id_blob_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_blob_", OracleType.Number); p[1].Value = id_blob_; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2_pkg.GetBlobObrazById", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetNormalList(OracleConnection conn, OracleTransaction tran, string adm_okrug_, string mun_region_, string street_,
            string square_from_, string square_till_, DateTime docend_date_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 6];
            double dsquare_from_, dsquare_till_;
            if (!square_from_.Equals(""))
                dsquare_from_ = ConvToDouble(square_from_);
            else
                dsquare_from_ = 0;
            if (!square_till_.Equals(""))
                dsquare_till_ = ConvToDouble(square_till_);
            else
                dsquare_till_ = 99999999999999999;

            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("adm_okrug_", OracleType.VarChar); p[1].Value = adm_okrug_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("mun_region_", OracleType.VarChar); p[2].Value = mun_region_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("street_", OracleType.VarChar); p[3].Value = street_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("square_from_", OracleType.Number); p[4].Value = dsquare_from_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("square_till_", OracleType.Number); p[5].Value = dsquare_till_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("docend_date_", OracleType.DateTime); p[6].Value = docend_date_; p[6].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.undistributed_pkg.GetNormalList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }

        public DataTable GetDetailedList(OracleConnection conn, OracleTransaction tran, string adm_okrug_, string mun_region_, string street_,
            string square_from_, string square_till_, DateTime docend_date_)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 6];
            double dsquare_from_, dsquare_till_;
            if (!square_from_.Equals(""))
                dsquare_from_ = ConvToDouble(square_from_);
            else
                dsquare_from_ = 0;
            if (!square_till_.Equals(""))
                dsquare_till_ = ConvToDouble(square_till_);
            else
                dsquare_till_ = 99999999999999999;

            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("adm_okrug_", OracleType.VarChar); p[1].Value = adm_okrug_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("mun_region_", OracleType.VarChar); p[2].Value = mun_region_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("street_", OracleType.VarChar); p[3].Value = street_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("square_from_", OracleType.Number); p[4].Value = dsquare_from_; p[4].Direction = ParameterDirection.Input;
            p[5] = new OracleParameter("square_till_", OracleType.Number); p[5].Value = dsquare_till_; p[5].Direction = ParameterDirection.Input;
            p[6] = new OracleParameter("docend_date_", OracleType.DateTime); p[6].Value = docend_date_; p[6].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.undistributed_pkg.GetDetailedList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset.Tables[0];
        }


        public DataSet GetMassGosUslList(OracleConnection conn, OracleTransaction tran, string ActLogin)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("ActLogin", OracleType.VarChar); p[1].Value = ActLogin; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.mass_transact_pkg.GetMassGosUslList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }


        public string AddMassReg(OracleConnection conn, OracleTransaction tran, string userldap_, string reg_name_, string reg_table_, ref int id_reg_)
        {
            OracleParameter[] p = new OracleParameter[1 + 4];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("userldap_", OracleType.VarChar); p[1].Value = userldap_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("reg_name_", OracleType.VarChar); p[2].Value = reg_name_; p[2].Direction = ParameterDirection.Input;
            p[3] = new OracleParameter("reg_table_", OracleType.NClob); p[3].Value = reg_table_; p[3].Direction = ParameterDirection.Input;
            p[4] = new OracleParameter("id_reg_", OracleType.Number); p[4].Direction = ParameterDirection.Output;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.mass_transact_pkg.AddMassReg", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            id_reg_ = Convert.ToInt32(p[4].Value);

            return Convert.ToString(p[0].Value);
        }

        public string UpdateMassReg(OracleConnection conn, OracleTransaction tran, int id_reg_, string reg_table_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reg_", OracleType.Number); p[1].Value = id_reg_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("reg_table_", OracleType.NClob); p[2].Value = reg_table_; p[2].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.mass_transact_pkg.UpdateMassReg", p, tran);

            return Convert.ToString(p[0].Value);
        }

        public string GetMassReg(OracleConnection conn, OracleTransaction tran, int id_reg_, ref string reg_table_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.VarChar); p[0].Size = 2048; p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reg_", OracleType.Number); p[1].Value = id_reg_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("reg_table_", OracleType.NClob); p[2].Direction = ParameterDirection.Output;
            
            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.mass_transact_pkg.GetMassReg", p, tran);

            if (!Convert.ToString(p[0].Value).Equals(""))
                return Convert.ToString(p[0].Value);

            reg_table_ = ((System.Data.OracleClient.OracleLob)(p[2].Value)).Value.ToString();

            return Convert.ToString(p[0].Value);
        }

        public DataSet GetRegsList(OracleConnection conn, OracleTransaction tran, int actual)
        {
            DataSet myset;
            OracleParameter[] p = new OracleParameter[2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Cursor); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("actual", OracleType.Number); p[1].Value = actual; p[1].Direction = ParameterDirection.Input;

            OracleCommand Com = OraQueryProcFunc(conn, "asguf2.statistics_pkg.GetRegsList", p, tran);

            using (OracleDataAdapter adapter = new OracleDataAdapter(Com))
            {
                myset = new DataSet("RETURN_VALUE");
                adapter.Fill(myset);
            }
            return myset;
        }

        public bool IsDocDateRequired(OracleConnection conn, OracleTransaction tran, int id_doctype_)
        {
            OracleParameter[] p = new OracleParameter[1 + 1];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_doctype_", OracleType.Number); p[1].Value = id_doctype_; p[1].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.asguf2_pkg.IsDocDateRequired", p, tran);

            return Convert.ToBoolean(Convert.ToInt32(p[0].Value));
        }

        public string MarkArchive(OracleConnection conn, OracleTransaction tran, int id_reg_, int isarchive_)
        {
            OracleParameter[] p = new OracleParameter[1 + 2];
            p[0] = new OracleParameter("RETURN_VALUE", OracleType.Number); p[0].Direction = ParameterDirection.ReturnValue;
            p[1] = new OracleParameter("id_reg_", OracleType.Number); p[1].Value = id_reg_; p[1].Direction = ParameterDirection.Input;
            p[2] = new OracleParameter("isarchive_", OracleType.Number); p[2].Value = isarchive_; p[2].Direction = ParameterDirection.Input;

            OraQueryProcFunc(conn, "asguf2.statistics_pkg.MarkArchive", p, tran);

            return Convert.ToString(p[0].Value);
        }
    }
}
