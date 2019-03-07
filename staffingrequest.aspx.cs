﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class staffingrequest : System.Web.UI.Page
    {
        DataLayer dataLayer;
        DataSet ds;
        string[] selectedStaff = { "", "", "" };

        //Handles when page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check
            if (Session["Security"].ToString() == "Admin")
            {
                Server.Transfer("index.html", true);
            }
            else if (Session["Security"].ToString() == "Staff" || Session["Security"].ToString() == "Client" || Session["Security"].ToString() == "Manager")
            {
                // Do nothing
            }
            else
            {
                Server.Transfer("login.aspx", true);
            }

            //Grab a dataset
            dataLayer = new DataLayer();
            ds = dataLayer.grabDataSet("SELECT staff.[userid]AS [ID], [full_name] AS [Name], [experience] AS [Experience (Years)], [degree] AS [Degree], [salary] AS [Salary], [city] AS [City], [state] AS [State] FROM staff, login WHERE login.[userid] = staff.[userid] AND [security] = 'S'");
            //Check for postback data in case of changes
            if (!Page.IsPostBack)
            {
                Session["DataView"] = new DataView(ds.Tables[0]);
                Session["Staff1"] = "";
                Session["Staff2"] = "";
                Session["Staff3"] = "";
                Staff1.Text = "";
                Staff2.Text = "";
                Staff3.Text = "";
                staff_grid.DataSource = ds;
            }
            else
            {
                selectedStaff[0] = Session["Staff1"].ToString();
                selectedStaff[1] = Session["Staff2"].ToString();
                selectedStaff[2] = Session["Staff3"].ToString();
                staff_grid.DataSource = (DataView)Session["DataView"];
            }

            //Bind data
            staff_grid.DataBind();
        }

        // Method to handle tooltips and cursor for rows, as well as selection method
        protected void RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(staff_grid, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';";
                e.Row.ToolTip = "Click to select row";

            }
        }

        //Sorts datagridview when clicking headers
        protected void SortDataSet(object sender, GridViewSortEventArgs e)
        {
            // Dataview allows for sorting given data
            DataView dv = new DataView(ds.Tables[0]);

            // Change according to session variable column header, according to last known direction
            if (Session[e.SortExpression.ToString()] != null && Session[e.SortExpression.ToString()].ToString() == "ASC")
            {
                dv.Sort = e.SortExpression.ToString() + " DESC";
                Session[e.SortExpression.ToString()] = "DESC";
            }
            else
            {
                dv.Sort = e.SortExpression.ToString() + " ASC";
                Session[e.SortExpression.ToString()] = "ASC";
            }

            //Bind new data and set postback data
            staff_grid.DataSource = dv;
            staff_grid.DataBind();
            Session["DataView"] = dv;
            //Call to show current selected staff
            updateSelected();
        }

        protected void staff_grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Grab the userid
            string userid = staff_grid.Rows[staff_grid.SelectedIndex].Cells[0].Text;

            // Algorithm to set current staff listing
            for (int i = 0; i < selectedStaff.Length; i++)
            {
                if (selectedStaff[i] == userid)
                {
                    if (i == 0)
                    {
                        selectedStaff[0] = selectedStaff[1];
                        Session["Staff1"] = selectedStaff[1];
                        selectedStaff[1] = selectedStaff[2];
                        Session["Staff2"] = selectedStaff[2];
                    }
                    else if (i == 1)
                    {
                        selectedStaff[1] = selectedStaff[2];
                        Session["Staff2"] = selectedStaff[2];
                    }

                    selectedStaff[2] = "";
                    Session["Staff3"] = "";
                    updateSelected();
                    return;
                }
                else if (selectedStaff[i] == "")
                {
                    selectedStaff[i] = userid;
                    Session["Staff" + (i + 1).ToString()] = userid;
                    updateSelected();
                    return;
                }
            }
            updateSelected();

        }

        // Method that handles which rows are highlighted as well as label text
        protected void updateSelected()
        {
            //Set known staff values
            Staff1.Text = selectedStaff[0];
            Staff2.Text = selectedStaff[1];
            Staff3.Text = selectedStaff[2];

            //Set row backcolor
            foreach (GridViewRow row in staff_grid.Rows)
            {
                for (int i = 0; i < selectedStaff.Length; i++)
                {
                    if (row.Cells[0].Text == selectedStaff[i])
                    {
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4286f4");
                    }
                }
            }
        }

        protected void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            try
            {
                //Error handling checks
                if (tbLocation.Text != "" && tbSalary.Text != "" && selectedStaff[0] != "")
                {
                    string stafflist = selectedStaff[0];

                    if (selectedStaff[1] != "")
                    {
                        stafflist += "," + selectedStaff[1];

                        if (selectedStaff[2] != "")
                        {
                            stafflist += "," + selectedStaff[2];
                        }
                    }

                    //Add request and redirect
                    dataLayer.addRequest(Session["UserID"].ToString(), stafflist, tbLocation.Text, ddlWork.SelectedValue, tbSalary.Text);
                    Session["Title"] = "<h1>Request Created</h1>";
                    Session["Message"] = "Your request has been created and is up for review.<br /><br />Your Request ID is: " + dataLayer.grabRequestNumber(Session["UserID"].ToString()) + "<br /><br />Be sure to save this page for referencing at a later date. Please contact TPS for any and all questions. Thank you and have a great day! You will be redirected within 20 seconds!";
                }


                


            }
            catch (Exception ex)
            {
                lblMessage.Text = "Something went wrong. Please try again."; //posts exception if system error
                throw;
            }
            finally
            {
                
                Response.AddHeader("REFRESH", "20;URL=portal.html");
            }
        }
    }
}