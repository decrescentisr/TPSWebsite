using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class useraccessinfo : System.Web.UI.Page
    {
        DataLayer dataLayer;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Security"].ToString() == "Manager")
            {
                //Do nothing
            }
            else if (Session["Security"].ToString() == "Staff" || Session["Security"].ToString() == "Client")
            {
                Server.Transfer("index.aspx", true);
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            dataLayer = new DataLayer();

            ds = dataLayer.grabDataSet("SELECT * FROM login");
            user_grid.DataSource = ds;
            user_grid.DataBind();
        }

        protected void btn_AddUsers_Click(object sender, EventArgs e)
        {
            var userIdTxt = userid.Text.Trim();
            if (!password.Text.Trim().Equals(string.Empty) && !userIdTxt.Equals(string.Empty))
            {
                if (new DataLayer().IsUserExists(userIdTxt))
                {
                    lblMessage.Text = "UserId(" + userIdTxt + ") already exists.";

                    bool Done = new DataLayer().addUser(userIdTxt, password.Text, DropDownList_Level.SelectedValue);
                    if (Done)
                    {
                        if (DropDownList_Level.SelectedValue.Equals("S"))
                        {
                            dataLayer.addStaff(userIdTxt, "", "", "", "", "", "", "", "");
                        }

                        lblMessage.Text = "User(" + userIdTxt + ") was successfully added!";
                        ds = dataLayer.grabDataSet("SELECT * FROM  [login]");
                        user_grid.DataSource = ds;
                        user_grid.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "The user could not be added!";
                    }

                }
                else
                {
                    lblMessage.Text = "Please enter valid userName and password!";
                }
            }
        }

        protected void user_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in user_grid.Rows)
            {
                if (row.RowIndex == user_grid.SelectedIndex)
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5b7ef0");
                    row.ToolTip = string.Empty;

                    userid.Text = row.Cells[0].Text;
                    password.Text = row.Cells[1].Text;
                    DropDownList_Level.SelectedValue = row.Cells[2].Text;
                }
                else
                {
                    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#001a28");
                    row.ToolTip = "Click to select this row.";

                }
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            var dataLayer = new DataLayer();
            if (!userid.Text.Trim().Equals(string.Empty))
            {
                var userIdTxt = userid.Text.Trim();
                if (dataLayer.deleteUser(userIdTxt))
                {
                    if (DropDownList_Level.SelectedValue.Equals("S"))
                    {
                        dataLayer.deleteStaff(userIdTxt);
                    }

                    lblMessage.Text = "UserId(" + userIdTxt + ") was successfully deleted.";
                    ds = dataLayer.grabDataSet("SELECT * FROM [login]");
                    user_grid.DataSource = ds;
                    user_grid.DataBind();
                }
            }
            else
            {
                lblMessage.Text = "Invalid UserId.";
            }
            userid.Text = "";
            password.Text = "";
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            var dataLayer = new DataLayer();
            var userIdTxt = userid.Text.Trim();
            if (!userIdTxt.Equals(string.Empty))
            {
                if (!new DataLayer().IsUserExists(userIdTxt))
                {
                    lblMessage.Text = "Update :: UserId(" + userIdTxt + ") not found.";
                    return;
                }

                if (dataLayer.updateUser(userIdTxt, password.Text, DropDownList_Level.SelectedValue))
                {
                    lblMessage.Text = "UserId(" + userIdTxt + ") successfully updated.";
                    ds = dataLayer.grabDataSet("SELECT * FROM [login]");
                    user_grid.DataSource = ds;
                    user_grid.DataBind();
                }
                else
                {
                    lblMessage.Text = "Failed to update UserId(" + userIdTxt + ").";
                }
            }

            else
            {
                lblMessage.Text = "Invalid UserId.";
            }
        }
    }
}