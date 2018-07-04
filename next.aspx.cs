using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

public partial class next : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label4.Text = (string)Session["budget"];
        Label5.Text = (string)Session["photo"];
        Label6.Text = (string)Session["brand"];
        Label7.Text = (string)Session["location"];


        string[] bodyName = (string[])Session["bodyName"];
        string[] bodyImg = (string[])Session["bodyImage"];
        string[] bodyDet = (string[])Session["bodyDetails"];

        string[] lensName = (string[])Session["lensName"];
        string[] lensImg = (string[])Session["lensImage"];
        string[] lensDet = (string[])Session["lensDetails"];

        double[] price = (double[])Session["price"];

        Label1.Text = Convert.ToString(price[0]);
        bodyName1.Text = bodyName[0];
        bodyImage1.ImageUrl = bodyImg[0];
        bodyDetails1.Text = bodyDet[0];

        Label2.Text = Convert.ToString(price[1]);
        bodyName2.Text = bodyName[1];
        bodyImage2.ImageUrl = bodyImg[1];
        bodyDetails2.Text = bodyDet[1];

        Label3.Text = Convert.ToString(price[2]);
        bodyName3.Text = bodyName[2];
        bodyImage3.ImageUrl = bodyImg[2];
        bodyDetails3.Text = bodyDet[2];

        lensName1.Text = lensName[0];
        lensImage1.ImageUrl = lensImg[0];
        lensDetails1.Text = lensDet[0];

        lensName2.Text = lensName[1];
        lensImage2.ImageUrl = lensImg[1];
        lensDetails2.Text = lensDet[1];

        lensName3.Text = lensName[2];
        lensImage3.ImageUrl = lensImg[2];
        lensDetails3.Text = lensDet[2];
    }
}