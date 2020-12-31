using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
     //DataClassesDataContext db = new DataClassesDataContext();
     clsServiceHandler objServiceHandler = new clsServiceHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadWelcomeMsg();
        loadFooterMsg();
        loadLogo();
        if(!IsPostBack)
        { 
       
        LoadBranchList();

        }
    }

    private void loadWelcomeMsg()
    {
        string welcomeMsg = objServiceHandler.ReturnString("SELECT WelcomeMSG FROM CompanyInformation WHERE [Default]=1");
        lblwelcomeMsg.Text = welcomeMsg;
    }
    private void loadFooterMsg()
    {
        string year = DateTime.Now.ToString("yyyy");
        string footerMsg = objServiceHandler.ReturnString("SELECT FooterMSG FROM CompanyInformation WHERE [Default]=1");
        lblFooterMsg.Text = year + " " + footerMsg;
    }

    private void loadLogo()
    {
        string logoName = objServiceHandler.ReturnString("SELECT LogoName FROM CompanyInformation WHERE [Default]=1");
        logo.ImageUrl = "Images/" + logoName;
    }
    private void LoadBranchList()
    {
        try
        {

            string strSql = "SELECT BRID,BRNAME FROM BRANCHNAME  ORDER BY BRNAME ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlBranch.DataSource = oDs;
            ddlBranch.DataValueField = "BRID";
            ddlBranch.DataTextField = "BRNAME";
            ddlBranch.DataBind();

            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--"));
        }
        catch (Exception)
        {

            lblMsg.Text = "Something went wrong.";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/RESET_PASSWORD.aspx");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedValue = "0";
        txtUserId.Text = "";
        txtPassword.Text = "";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        try
        {
            DataSet userInfo = new DataSet();
            DataSet branchInfo = new DataSet();
            string strSql = "";
            string BranchId=ddlBranch.SelectedValue;
            string BranchName = ddlBranch.SelectedItem.Text;

            string username = txtUserId.Text;
            string password = txtPassword.Text;
            if(ddlBranch.SelectedIndex.Equals(0))
            {
                lblMsg.Text = "Please Select Branch";
                return;
            }
            if (username.Equals(""))
            {
                lblMsg.Text = "Please input Username";
                return;
            }
            if (password.Equals(""))
            {
                lblMsg.Text = "Please input login Password";
                return;
            }
            


            strSql = "SELECT * FROM USERLIST WHERE LogName='" + username + "' AND PASSWORD='" + password + "'";
            userInfo = objServiceHandler.ExecuteQuery(strSql);

            if (userInfo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in userInfo.Tables["Table1"].Rows)
                {
                        Session["UserID"] = prow["userId"].ToString();
                        Session["LoginName"] = prow["LogName"].ToString();
                        Session["Designation"] = prow["Desigantion"].ToString();
                        Session["Department"] = prow["Department"].ToString();
                        Session["Password"] = prow["Password"].ToString();
                        Session["UserFullName"] = prow["FullName"].ToString();
                        Session["Access"] = prow["Access"].ToString();
                        Session["BrId"] = prow["BrId"].ToString();
                 }

                string userId = Session["UserID"].ToString();
                strSql = "SELECT * FROM BRPERMISSION WHERE BRID='" + BranchId + "' AND USERID='" + userId + "'";
                branchInfo = objServiceHandler.ExecuteQuery(strSql);
                if (branchInfo.Tables["Table1"].Rows.Count == 1)
                {
                    Session["BranchID"] = BranchId;
                    Session["BranchName"] = BranchName;
                    Response.Redirect("~/Forms/WelcomePage.aspx");

                }
                else 
                {
                    lblMsg.Text = "Your are not allowed for this Branch !";
                    return;
                }
                
             }

               
            else
            {
                lblMsg.Text = "Please input Valid Username and Password ";
            }
        }
        catch (Exception )
        {
            lblMsg.Text = "Somthing went wrong !";
        }

    }
    
}