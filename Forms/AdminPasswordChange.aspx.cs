using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AdminPasswordChange : System.Web.UI.Page
{
    clsServiceHandler obJclsServiceHandler = new clsServiceHandler();
    clsItemDependency obJclsItemDependency = new clsItemDependency();
 

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        string pass="";
        string uid="";


        if (txtLogName.Text.Equals(""))
        {
            lblMsg.Text = "Input LogName";
            txtNewPassword1.Focus();
            return;

        }


        if (txtNewPassword1.Text.Equals(""))
        {   
            lblMsg.Text = "Input Password1";
            txtNewPassword1.Focus();
            return;

        }
          string sqlString = " select * from userList where Logname ='" + txtLogName.Text+"'" ;
        DataSet UserList = obJclsServiceHandler.ExecuteQuery(sqlString);

        if (UserList.Tables["Table1"].Rows.Count != 1)
        {
            lblMsg.Text = "User not found";
            return;
        }
        foreach (DataRow prows in UserList.Tables["Table1"].Rows)
        {
           pass=  prows["password"].ToString();
            uid=  prows["userid"].ToString();
          if (pass != txtOldPassword.Text.ToString())
          {
              lblMsg.Text = "Old Password not correct";
              return;
  
  
          }

          if (txtNewPassword1.Text != txtNewPassword2.Text)
          {
              lblMsg.Text = "Booth password should be matched";
              return;
          }
 
        
        }

     string    result=obJclsItemDependency.UpdatePassword(pass, uid);


     if (result == "Successfull")
     {
         lblMsg.Text = "Update Successfull";

     }


        

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}