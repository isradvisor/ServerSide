using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using IsraVisor_server.Models;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;

    public List<Language> ReadLanguagesFromSQL()
    {
        List<Language> LanguagesList = new List<Language>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Language_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Language lan = new Language();
                lan.LName = (string)dr["LName"];
                lan.LNameEnglish = (string)dr["LNameEnglish"];
                LanguagesList.Add(lan);
            }

            return LanguagesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public int SignUp(Tourist tourist)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTouristSignUp(tourist);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertCommandTouristSignUp(Tourist t)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string


        command = "INSERT INTO TouristProject (FirstName, LastName, email, passwordTourist, gender,yearOfBirth) VALUES ('" + (t.FirstName) + "', '" + (t.LastName) + "', '" + (t.Email) + "','" + (t.PasswordTourist) + "', '" + (t.Gender) + "', '" + (t.YearOfBirth) + "')";

        return command;
    }
    public Tourist LogInCheck(Tourist tourist)
    {
        Tourist t = new Tourist();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from TouristProject where email = '" + (tourist.Email) + "' and passwordTourist = '" + (tourist.PasswordTourist) + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                t.FirstName = (string)dr["FirstName"];
                t.LastName = (string)dr["LastName"];
                t.Email = (string)dr["email"];
                t.PasswordTourist = (string)dr["passwordTourist"];
                t.Gender = (string)dr["gender"];
                t.YearOfBirth = Convert.ToDateTime(dr["yearOfBirth"]).ToString("MM/dd/yyyy");

            }

            return t;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public Guide GetGuideByEmailFromSQL(string email)
    {
        Guide guide = new Guide();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM GuideProject where email ='" + email + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                guide.gCode = Convert.ToInt32(dr["gCode"]);
                guide.Email = (string)dr["email"];
                guide.PasswordGuide = (string)dr["PasswordGuide"];
                guide.FirstName = (string)dr["firstName"];
                guide.LastName = (string)dr["LastName"];
                guide.ProfilePic = (string)dr["profilePic"];
                guide.License = Convert.ToInt32(dr["License"]);
                guide.DescriptionGuide = (string)dr["descriptionGuide"];
                guide.Phone = (string)(dr["phone"]);
                guide.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                //g.BirthDay = g.BirthDay.ToString();
                bool genderGuide = Convert.ToBoolean(dr["gender"]);
                if (genderGuide)
                {
                    guide.Gender = "male";
                }
                else
                {
                    guide.Gender = "female";
                }
                //g.Rank = Convert.ToDouble(dr["Rank"]);
            }

            return guide;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public int PostGuideAreasToSQL(Guide_Area guide_Area)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandGuideArea(guide_Area);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertCommandGuideArea(Guide_Area guide_Area)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1})", guide_Area.Guide_Code, guide_Area.Area_Code);
        String prefix = "INSERT INTO guide_Area_Project " + "(guidegCode,areaCode)";
        command = prefix + sb.ToString();

        return command;
    }

    public void DeleteAllGuideAreas(int guide_Code)
    {
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "DELETE FROM guide_Area_Project where guidegCode = " + guide_Code;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<Guide_Area> GetAreasByGuideFromSQL(int id)
    {
        List<Guide_Area> GuideAreas = new List<Guide_Area>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM guide_Area_Project where guidegCode = " + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Guide_Area gArea = new Guide_Area();

                gArea.Guide_Code = Convert.ToInt32(dr["guidegCode"]);
                gArea.Area_Code = Convert.ToInt32(dr["areaCode"]);
                GuideAreas.Add(gArea);
            }

            return GuideAreas;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<Area> GetAreasFromSQL()
    {
        List<Area> AreaList = new List<Area>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Area_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Area City = new Area();
                City.AreaName = (string)dr["areaName"];
                AreaList.Add(City);
            }

            return AreaList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public int PostCitiesToSQL(Area City)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCitiesCommand(City);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private String BuildInsertCitiesCommand(Area City)
    {
        String command;
       
            command = @"INSERT INTO Area_Project (areaName) Values ('" + City.AreaName + "') ";
        return command;
    }

    public void DeleteAllGuideLanguages(int guide_Code)
    {
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "DELETE FROM guide_Language_Project where guidegCode = " + guide_Code;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<Guide_Language> ReadGuideAllLanguagesFromSQL(int id)
    {
        List<Guide_Language> GuideLangs = new List<Guide_Language>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM guide_Language_Project where guidegCode = " + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Guide_Language gLang = new Guide_Language();

                gLang.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                gLang.Guide_Code = Convert.ToInt32(dr["guidegCode"]);
                GuideLangs.Add(gLang);
            }

            return GuideLangs;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<Guide_Language> GetGuideLangsFromSQL()
    {
        List<Guide_Language> GuideLangs = new List<Guide_Language>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM guide_Language_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Guide_Language gLang = new Guide_Language();

                gLang.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                gLang.Guide_Code = Convert.ToInt32(dr["guidegCode"]);
                GuideLangs.Add(gLang);
            }

            return GuideLangs;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public int PostGuideLanguagesToSQL(Guide_Language guidesLanguages)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandGuideLanguages(guidesLanguages);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertCommandGuideLanguages(Guide_Language g)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1})", g.Guide_Code, g.Language_Code);
        String prefix = "INSERT INTO guide_Language_Project " + "(guidegCode,LanguageLCode)";
        command = prefix + sb.ToString();

        return command;
    }

    public DataTable dt;


    public DBservices()
    {

        //
        // TODO: Add constructor logic here
        //
    }

    public List<Tourist> readTourist()
    {
        List<Tourist> touristList = new List<Tourist>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM TouristProject";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Tourist t = new Tourist();
                t.Email = (string)dr["email"];
                t.PasswordTourist = (string)dr["passwordTourist"];
                touristList.Add(t);
            }

            return touristList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public int PostTouristToSQL(Tourist tourist)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommandTourist(tourist);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal int UpdateGuideSQL(Guide g)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateCommand(g);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

   
    public int PostGuideToSQL(Guide g)
    {
    
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("ConnectionStringName"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(g);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
    }

    internal List<Guide> ReadGuides()
    {
        List<Guide> guideList = new List<Guide>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM GuideProject";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Guide g = new Guide();
                g.gCode = Convert.ToInt32(dr["gCode"]);
                g.Email = (string)dr["email"];
                g.PasswordGuide = (string)dr["PasswordGuide"];
                g.FirstName = (string)dr["firstName"];
                g.LastName = (string)dr["LastName"];
                g.ProfilePic = (string)dr["profilePic"];
                g.License = Convert.ToInt32(dr["License"]);
                g.DescriptionGuide = (string)dr["descriptionGuide"];
                g.Phone = (string)(dr["phone"]);
                g.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                g.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                //g.BirthDay = g.BirthDay.ToString();
                bool genderGuide = Convert.ToBoolean(dr["gender"]);
                if (genderGuide)
                {
                    g.Gender = "male";
                }
                else
                {
                    g.Gender = "female";
                }
                //g.Rank = Convert.ToDouble(dr["Rank"]);

                guideList.Add(g);
            }

            return guideList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    private String BuildInsertCommand(Guide g)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}',{9},'{10}')", g.Email,g.PasswordGuide,g.FirstName,g.LastName,g.SignDate,g.ProfilePic,0,"","",1,"10/10/2020");
        String prefix = "INSERT INTO GuideProject " + "(email,passwordGuide,firstName,LastName,SignDate,profilePic,License,descriptionGuide,Phone,gender,BirthDay)";
        command = prefix + sb.ToString();

        return command;
    }

    private String BuildInsertCommandTourist(Tourist t)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}')", t.Email, t.PasswordTourist,t.FirstName,t.LastName);
        String prefix = "INSERT INTO TouristProject " + "(email,passwordTourist,FirstName,LastName)";
        command = prefix + sb.ToString();

        return command;
    }

    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

  
    ////---------------------------------------------------------------------------------
    //// Create the SqlCommand
    ////---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }



    public void update() {
        da.Update(dt);
    }
   
    private string BuildUpdateCommand(Guide g)
    {
        String command = "";

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        int GenderGuider = 0;
        if (g.Gender == "male")
        {
            GenderGuider = 1;
        }
        else if(g.Gender == "female")
        {
            GenderGuider = 0;
        }
        command = "UPDATE GuideProject SET firstName ='" + g.FirstName + "',BirthDay='"+ g.BirthDay + "',phone='"+g.Phone+"', LastName='" + g.LastName + "',descriptionGuide='" + g.DescriptionGuide + "', License=" + g.License + ", gender=" + GenderGuider +  " WHERE email = '" + g.Email + "'";
        return command;

    }

}
