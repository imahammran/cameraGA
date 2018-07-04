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
using System.Text;
using System.IO;

public partial class index : System.Web.UI.Page
{
    string connectionString = "server=localhost;uid=root;pwd=;database=dslr;";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindcheckboxlist();
        }
        //bindData();
    }

    private void bindcheckboxlist()
    {
        MySqlConnection con = new MySqlConnection(connectionString);
        con.Open();

        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "select styleName from style";
        cmd.Connection = con;

        MySqlDataReader reader = cmd.ExecuteReader();
        cboxListPhoto.DataSource = reader;
        cboxListPhoto.DataTextField = "styleName";
        cboxListPhoto.DataValueField = "styleName";

        cboxListPhoto.DataBind();

    }

    protected void reset_onClick(object sender, EventArgs e)
    {
        txtBudget.Text = string.Empty;
        cboxListPhoto.ClearSelection();
        dropBrand.SelectedIndex = 0;
        dropLocation.SelectedIndex = 0;
    }

    protected void recommend_onClick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        MySqlConnection conn = new MySqlConnection(connectionString);

        //open database table lens and store in lens object list
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("select * from lens", conn);
        MySqlDataReader reader = cmd.ExecuteReader();

        List<Lens> lens = new List<Lens>();

        while (reader.Read())
        {
            string lensID = reader.GetValue(0) + "";
            string lensName = reader.GetValue(1) + "";
            string lensBrand = reader.GetValue(2) + "";
            double lensPrice = Convert.ToDouble(reader.GetValue(3) + "");
            string[] flengthID = (reader.GetValue(4) + "").Split('-');
            string[] fstopID = (reader.GetValue(5) + "").Split('-');
            string llocation = reader.GetValue(6) + "";
            string limg = reader.GetValue(7) + "";

            Lens temp = new Lens(lensID, lensName, lensBrand, lensPrice, flengthID, fstopID, llocation,limg);
            lens.Add(temp);
        }
        conn.Close();

        //open database table body and store in cbody object list
        conn.Open();
        cmd = new MySqlCommand("select * from body", conn);
        reader = cmd.ExecuteReader();

        List<Cbody> cbody = new List<Cbody>();

        while (reader.Read())
        {
            string bodyID = reader.GetValue(0) + "";
            string bodyName = reader.GetValue(1) + "";
            string bodyBrand = reader.GetValue(2) + "";
            double bodyPrice = Convert.ToDouble(reader.GetValue(3) + "");
            double bodyMP = Convert.ToDouble(reader.GetValue(4) + "");
            string bodySize = reader.GetValue(5) + "";
            string location = reader.GetValue(6) + "";
            string bimg = reader.GetValue(7) + "";

            Cbody temp = new Cbody(bodyID, bodyName, bodyBrand, bodyPrice, bodyMP, bodySize, location, bimg);
            cbody.Add(temp);
        }
        conn.Close();

        //GENETIC ALGORITHM START
        //POPULATION GENERATION
        
        int population = 100; //bilangan gabungan lens + body
        int generation = 100; //bilangan loop yg generate population

        Random rnd = new Random();
        Chromosome[] chrome = new Chromosome[population];

        for (int g = 0; g < generation; g++)
        {
            if (g == 0) //first generation
            {
                for (int i = 0; i < population; i++)
                {
                    chrome[i] = new Chromosome(cbody[rnd.Next(cbody.Count)], lens[rnd.Next(lens.Count)], 0);

                    //FITNESS EVALUATION
                    int fitness = fitnessFunction(chrome[i]);
                }
            }
            else //for second and next generation
            {
                for (int i = population / 100; i < population; i++)
                {
                    chrome[i] = new Chromosome(cbody[rnd.Next(cbody.Count)], lens[rnd.Next(lens.Count)], 0);

                    //FITNESS EVALUATION
                    int fitness = fitnessFunction(chrome[i]);
                }
            }

            Array.Sort(chrome, delegate(Chromosome x, Chromosome y) { return y.fitness.CompareTo(x.fitness); });

            //CROSSOVER
            int first = chrome.Length / 2; //take top 50 population only

            for (int k = 0; k < chrome.Length / 2; k++)
            {
                Chromosome nextP = CrossOver(chrome[k], chrome[k + 1]);

                //MUTATION
                mutation(chrome[first], cbody, lens);

                nextP.fitness = fitnessFunction(nextP);
                chrome[first] = nextP;
                first++;
            }

            //SELECTION
            Array.Sort(chrome, delegate(Chromosome x, Chromosome y) { return y.fitness.CompareTo(x.fitness); });

        }
        
        //to send data to next page
        //display body result
        List<string> bodyNameL = new List<string>();
        List<string> bodyImgL = new List<string>();
        List<string> bodyDetL = new List<string>();

        List<string> lensNameL = new List<string>();
        List<string> lensImgL = new List<string>();
        List<string> lensDetL = new List<string>();

        double[] price = new double[3];
        for (int z = 0; z < 3; z++)
        {
            String bName = chrome[z].cbody.bodyName;
            String bBrand = chrome[z].cbody.bodyBrand;
            String bPrice = Convert.ToString("RM" + chrome[z].cbody.bodyPrice);
            String bMP = Convert.ToString(chrome[z].cbody.bodyMP);
            String bSize = chrome[z].cbody.bodySize;
            String bLoc = chrome[z].cbody.location;
            String bImg = chrome[z].cbody.bimg;
            String bDet = "Brand: " + bBrand + "\nPrice: " + bPrice + "\nMegapixel: " + bMP + "MP\nSensor Size: " + bSize + "\nLocation: " + bLoc;

            bodyNameL.Add(bName);
            bodyImgL.Add(bImg);
            bodyDetL.Add(bDet);

            //display lens result
            String lName = chrome[z].lens.lensName;
            String lBrand = chrome[z].lens.lensBrand;
            String lPrice = Convert.ToString("RM" + chrome[z].lens.lensPrice);
            String lf1 = chrome[z].lens.flengthID.First();
            String lf2 = chrome[z].lens.flengthID.Last();
            String lfs1 = chrome[z].lens.fstopID.First();
            String lfs2 = chrome[z].lens.fstopID.Last();


            //retrieve flength and fstop details
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            cmd = new MySqlCommand("select flengthDesc from flength where flengthID=@lf1 or flengthID=@lf2", con);
            cmd.Parameters.AddWithValue("@lf1", lf1);
            cmd.Parameters.AddWithValue("@lf2", lf2);
            reader = cmd.ExecuteReader();
            List<string> al = new List<string>();
            while (reader.Read())
            {
                al.Add(reader[0].ToString() + "");
            }
            conn.Close();

            conn.Open();
            cmd = new MySqlCommand("select fstopDesc from fstop where fstopID=@lfs1 or fstopID=@lfs2", conn);
            cmd.Parameters.AddWithValue("@lfs1", lfs1);
            cmd.Parameters.AddWithValue("@lfs2", lfs2);
            reader = cmd.ExecuteReader();
            List<string> al1 = new List<string>();
            while (reader.Read())
            {
                al1.Add(reader[0].ToString() + "");
            }
            conn.Close();

            string lf;
            if (al.Count == 1)
                lf = Convert.ToString(al[0]);
            else
                lf = Convert.ToString(al[0] + "-" + al[1]);

            string lfs;
            if (al1.Count == 1)
                lfs = Convert.ToString(al1[0]);
            else
                lfs = Convert.ToString(al1[0] + "-" + al1[1]);

            String lLoc = chrome[z].lens.llocation;
            String lImg = chrome[z].lens.limg;
            String lDet = "Brand: " + lBrand + "\nPrice: " + lPrice +
                            "\nFocal Length: " + lf +
                            "\nAperture: " + lfs +
                            "\nLocation: " + lLoc;

            lensNameL.Add(lName);
            lensImgL.Add(lImg);
            lensDetL.Add(lDet);

            price[z] = chrome[z].cbody.bodyPrice + chrome[z].lens.lensPrice;
        }

        //store details in array session
        Session["bodyName"] = bodyNameL.ToArray();
        Session["bodyImage"] = bodyImgL.ToArray();
        Session["bodyDetails"] = bodyDetL.ToArray();

        Session["lensName"] = lensNameL.ToArray();
        Session["lensImage"] = lensImgL.ToArray();
        Session["lensDetails"] = lensDetL.ToArray();

        Session["price"] = price.ToArray<double>();

        Session["budget"] = txtBudget.Text;

        var pstyle = new List<string>();
        foreach (ListItem item in cboxListPhoto.Items)
        {
            if (item.Selected)
                pstyle.Add(item.Value);
        }

        Session["photo"] = string.Join(", ", pstyle);
        Session["brand"] = dropBrand.SelectedValue;
        Session["location"] = dropLocation.SelectedValue;

        //redirect to recommendation result page
        Response.Redirect("next.aspx");
    }

    //fitness function
    public int fitnessFunction(Chromosome x)
    {
        int fitness = 0;

        //collection from user input
        //budget
        double budget = Convert.ToDouble(txtBudget.Text);

        //photography selection
        var pstyle = new List<string>();
        foreach (ListItem item in cboxListPhoto.Items)
        {
            if (item.Selected)
                pstyle.Add(item.Value);
        }

        //brand
        var bra = new List<string>();
        foreach (ListItem item in dropBrand.Items)
        {
            if (item.Selected)
                bra.Add(item.Value);
        }

        //location
        var loc = new List<string>();
        foreach (ListItem item in dropLocation.Items)
        {
            if (item.Selected)
                loc.Add(item.Value);
        }

        //fitness calculation from the user input
        //budget fitness
        double totalPrice = x.cbody.bodyPrice + x.lens.lensPrice;

        if (budget < totalPrice)
            fitness = fitness + Convert.ToInt32(budget - totalPrice);
        else if (budget == totalPrice)
            fitness = fitness + 200;
        else
        {
            if (budget - totalPrice <= 100)
                fitness = fitness + 100;
            else if (totalPrice - budget <= 200)
                fitness = fitness + 50;
            else if (totalPrice - budget <= 300)
                fitness = fitness + 10;
            else
                fitness = fitness + Convert.ToInt32(totalPrice - budget);
        }
        
        //photography selection fitness
        var photo = pstyle.ToArray();
        for (int p = 0; p < photo.Length; p++)
        {
            string cfl = ConvertStringArrayToStringJoin(x.lens.flengthID); //join flength[]
            string cfs = ConvertStringArrayToStringJoin(x.lens.fstopID); //join fstop[]

            //open database table style in st object list
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select * from style", conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Style> st = new List<Style>();

            while (reader.Read())
            {
                int id = Convert.ToInt16(reader.GetValue(0));
                string name = reader.GetValue(1) + "";
                string flength = reader.GetValue(2) + "";
                string fstop = reader.GetValue(3) + "";

                Style s;

                foreach (string tempp in photo)
                {
                    if (tempp.TrimEnd() == name.TrimEnd()) //end any spacebar etc. and check if the input style name equal with the style name of the table
                    {
                        s = new Style(id, name, flength, fstop);
                        st.Add(s);
                    }
                }// to insert the details of the style only if it match with the input

                
            }
            conn.Close();

            foreach (Style tempo in st)
            {
                //read string flength and fstop from style and split it into array
                string[] matcher = tempo.flength.Split('-');
                string[] matcher2 = tempo.fstop.Split('-');

                foreach (string a in matcher)
                {
                    if (x.lens.flengthID.Contains(a)) //check if the style flength[] contains same flength[] with the chromosomes
                    {
                        fitness = fitness + 100;
                    }
                    else
                        fitness = fitness - 100;
                }

                foreach (string b in matcher2)
                {
                    if (x.lens.fstopID.Contains(b)) //check if the style fstop[] contains same fstop[] with the chromosomes
                    {
                        fitness = fitness + 100;
                    }
                    else
                        fitness = fitness - 100;
                }
            }
        }

        //location fitness
        var locate = loc.ToArray();
        for (int l = 0; l < locate.Length; l++)
        {
            string locB = x.cbody.location;
            string locL = x.lens.llocation;

            for (int ll = 0; ll < 100; ll++)
            {
                if (locB == locate[l] & locL == locate[l])
                    fitness = fitness + 100;
                else if (locB == locate[l] & locL != locate[l])
                    fitness = fitness + 50;
                else if (locB != locate[l] & locL == locate[l])
                    fitness = fitness + 50;
                else
                    fitness = fitness - 100;
            }
        }

        //brand fitness
        var brand = bra.ToArray();
        for (int l = 0; l < brand.Length; l++)
        {
            string braB = x.cbody.bodyBrand;
            string braL = x.lens.lensBrand;

            for (int ll = 0; ll < 100; ll++)
            {
                if (braB == brand[l] & braL == brand[l])
                    fitness = fitness + 100;
                else if (braB == brand[l] & braL != brand[l])
                    fitness = fitness + 50;
                else if (braB != brand[l] & braL == brand[l])
                    fitness = fitness + 50;
                else
                    fitness = fitness - 100;
            }
        }

        return fitness;
    }

    //cbody from x crossover with cbody from y & lens from x crossover with lens from y
    public Chromosome CrossOver(Chromosome x, Chromosome y)
    {
        Chromosome temp = new Chromosome(x.cbody, y.lens, 0);
        return temp;
    }

    //randomly change cbody/lens
    public Chromosome mutation(Chromosome x, List<Cbody> cbody, List<Lens> lens)
    {
        Random rnd = new Random();
        int random = rnd.Next(0, 5); //0.4 probabilty of mutation

        if (random == 1)
        {
            x.cbody = cbody[rnd.Next(cbody.Count)]; //change body
            return x;
        }
        else if (random == 2)
        {
            x.lens = lens[rnd.Next(lens.Count)]; //change lens
            return x;
        }
        else
            return x; //no changes
    }

    public string ConvertStringArrayToStringJoin(string[] array)
    {
        string result = string.Join("-", array);
        return result;
    }

    public class Cbody
    {
        public string bodyID;
        public string bodyName;
        public string bodyBrand;
        public double bodyPrice;
        public double bodyMP;
        public string bodySize;
        public string location;
        public string bimg;

        public Cbody(string bodyID, string bodyName, string bodyBrand, double bodyPrice, double bodyMP, string bodySize, string location, string bimg)
        {
            this.bodyID = bodyID;
            this.bodyName = bodyName;
            this.bodyBrand = bodyBrand;
            this.bodyPrice = bodyPrice;
            this.bodyMP = bodyMP;
            this.bodySize = bodySize;
            this.location = location;
            this.bimg = bimg;
        }
    }

    public class Lens
    {
        public string lensID;
        public string lensName;
        public string lensBrand;
        public double lensPrice;
        public string[] flengthID;
        public string[] fstopID;
        public string llocation;
        public string limg;

        public Lens(string lensID, string lensName, string lensBrand, double lensPrice, string[] flengthID, string[] fstopID, string llocation, string limg)
        {
            this.lensID = lensID;
            this.lensName = lensName;
            this.lensBrand = lensBrand;
            this.lensPrice = lensPrice;
            this.flengthID = flengthID;
            this.fstopID = fstopID;
            this.llocation = llocation;
            this.limg = limg;
        }
    }

    public class Chromosome
    {
        public Cbody cbody;
        public Lens lens;
        public int fitness;


        public Chromosome(Cbody bodyID, Lens lensID, int fitness)
        {
            this.cbody = bodyID;
            this.lens = lensID;
            this.fitness = 0;
        }
    }
}

public class Style
{
    public int id;
    public string name;
    public string flength;
    public string fstop;

    public Style(int id, string name, string flength, string fstop)
    {
        this.id = id;
        this.name = name;
        this.flength = flength;
        this.fstop = fstop;
    }
}
