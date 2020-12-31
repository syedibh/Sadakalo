using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AdminNewUser : System.Web.UI.Page
{
    string useridString = "";

    clsServiceHandler objServiceHandler = new clsServiceHandler();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());



    private void NewUserId()
    {

        try
        {


            string strSql = "";
            strSql = "SELECT MAX(userId)+1 AS VNO FROM UserList";
            string userid = objServiceHandler.ReturnString(strSql);
            if (userid.Equals(""))
            {
                useridString = "1";

            }
            else
            {
                useridString = userid.ToString();


            }
        }
        catch
        {
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (txtEmailId.Equals(""))
        {
            lblMsg.Text = "First input emailId";
            return;
        }

        string sqlString = "Select top 1 logname from userlist where logname='" + txtEmailId.Text + "'";
        string MailExist = objServiceHandler.ReturnString(sqlString);
        if (!MailExist.Equals(""))
        {
            lblMsg.Text = "Allready exist this Id.";
            return;

        }


        string userid = objServiceHandler.ReturnString("select max(userid)+1 from userlist");
        if (userid.Equals(null))
        {
            userid = "1";
        }



        string result = AddUser(userid, txtEmailId.Text, txtDesignation.Text, txtJoiningDate.Text, txtMobileNo.Text, txtFullname.Text);




        if (result.Equals("Successful"))
        {
            lblMsg.Text = "Successfully added";
        }
        else
        {
            lblMsg.Text = "Problem";

        }

    }

    public string AddUser(string userid, string logname, string designation, string joiningdate, string mobileno, string fullname)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT * FROM UserList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "UserList";
            oOrderRow = oDS.Tables["UserList"].NewRow();

            oOrderRow["userId"] = userid;
            oOrderRow["LogName"] = logname;
            oOrderRow["Desigantion"] = designation;
            oOrderRow["JoiningDate"] = joiningdate;
            oOrderRow["Mobileno"] = mobileno;
            oOrderRow["FullName"] = fullname;
            oOrderRow["Access"] = 1;





            oDS.Tables["UserList"].Rows.Add(oOrderRow);
            oda.Update(oDS, "UserList");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}