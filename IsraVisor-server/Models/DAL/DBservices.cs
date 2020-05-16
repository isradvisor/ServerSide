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

    //GUIDE CLASS **************** GUIDE CLASS ****************
    //GET ALL GUIDES
    public List<Guide> ReadGuides()
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
                if (dr["License"] != System.DBNull.Value)
                {
                    g.License = Convert.ToInt32(dr["License"]);
                }
                if (dr["descriptionGuide"] != System.DBNull.Value)
                {
                    g.DescriptionGuide = (string)dr["descriptionGuide"];
                }
                if (dr["phone"] != DBNull.Value)
                {
                    g.Phone = (string)(dr["phone"]);
                }
                g.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                g.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                if (dr["gender"] != System.DBNull.Value)
                {
                    bool genderGuide = Convert.ToBoolean(dr["gender"]);
                    if (genderGuide)
                    {
                        g.Gender = "male";
                    }
                    else
                    {
                        g.Gender = "female";
                    }
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

    public List<AttractionPointInTrip> GetAttractionsFromSQLByID(int id)
    {
        List<AttractionPointInTrip> attractions = new List<AttractionPointInTrip>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM AttractionPointInTrip_Project ap join Attraction_Project att on ap.AttractionCode = att.AttractionCode where TripPlan_IdPlan = " + id + " order by fromHour";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                AttractionPointInTrip att = new AttractionPointInTrip();
                att.fromHour = Convert.ToDateTime(dr["fromHour"]);
                att.ToHour = Convert.ToDateTime(dr["ToHour"]);
                att.PointInPlanId = Convert.ToInt32(dr["PointInPlanId"]);
                att.AttractionCode = Convert.ToInt32(dr["AttractionCode"]);
                att.AttractionName = (string)(dr["AttractionName"]);
                att.CityName = (string)(dr["AreaName"]);
                att.Opening_Hours = (string)(dr["Opening_Hours"]);
                att.PointNo = Convert.ToInt32(dr["PointNo"]);
                attractions.Add(att);
            }

            return attractions;
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

    public int AddAtraction(AttractionPointInTrip attraction)
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

        String cStr = BuildInsertAttractionCommand(attraction);      // helper method to build the insert string

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

    private string BuildInsertAttractionCommand(AttractionPointInTrip attraction)
    {
        String command;
        DateTime fromHour = Convert.ToDateTime(attraction.fromHour.ToString());
        DateTime toHour = Convert.ToDateTime(attraction.ToHour.ToString());

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1},'{2}','{3}',{4},'{5}','{6}')", attraction.TripPlan_IdPlan,attraction.PointInPlanId,attraction.CityName,attraction.AttractionName,attraction.AttractionCode,fromHour.ToString("yyyy-MM-dd HH:mm:ss"),toHour.ToString("yyyy-MM-dd HH:mm:ss"));
        String prefix = "INSERT INTO AttractionPointInTrip_Project " + "(TripPlan_IdPlan,PointInPlanId,CityName,AttractionName,AttractionCode,fromHour,ToHour)";
        command = prefix + sb.ToString();
        return command;
    }

    public List<AreaPointInTrip> GetCitiesFromSQL(int id)
    {
        List<AreaPointInTrip> cities = new List<AreaPointInTrip>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM AreaPointInTrip_Project where TripPlan_IdPlan = " + id + " order by FromDate";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                AreaPointInTrip city = new AreaPointInTrip();
                city.FromDate = Convert.ToDateTime(dr["FromDate"]);
                city.AreaName = (string)(dr["AreaName"]);
                city.OrderNumber = Convert.ToInt32(dr["OrderNumber"]);
                city.PointInPlanId = Convert.ToInt32(dr["PointInPlanId"]);
                cities.Add(city);
            }

            return cities;
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

    public int AddTripTouristToSQL(AreaPointInTrip trip)
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

        String cStr = BuildInsertTripCommand(trip);      // helper method to build the insert string

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

    public Tourist GetAllDetailsFromSQL(int id)
    {
        Tourist tour = new Tourist();
        List<int> HobbiesList = new List<int>();
        List<int> ExpertisesList = new List<int>();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select t.Id,t.FirstName,t.LastName,t.email,t.yearOfBirth,t.ProfilePic,t.IntrestInGender,t.FirstTimeInIsrael,t.gender,tp.tripType,tp.FromDate,tp.ToDate,tp.EstimateDate,tp.Duration,tp.Budget,lp.LCode,ep.Code,hp.HCode from TouristProject t join Trip_Plan_Project tp on t.email = tp.TouristEmail join TripPlanIntrest_Project tpi on t.Id = tpi.TouristId join Expertise_Project ep on tpi.ExpertiseCode = ep.Code join Tourist_Language_Project tl on t.Id = tl.IdTourist join Language_Project lp on tl.LanguageLCode = lp.LCode join Hobby_Tourist_Project ht on t.Id = ht.TouristId join Hobby_Project hp on ht.HobbyHCode = hp.HCode where t.Id='" + id + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                tour.TouristID = Convert.ToInt32(dr["Id"]);
                tour.FirstName = (string)(dr["FirstName"]);
                tour.LastName = (string)(dr["LastName"]);
                tour.Email = (string)(dr["email"]);
                tour.YearOfBirth = Convert.ToDateTime(dr["yearOfBirth"]).ToString("MM/dd/yyyy");
                if (dr["ProfilePic"] != System.DBNull.Value)
                {
                    tour.ProfilePic = (string)(dr["ProfilePic"]);
                }
                if (dr["IntrestInGender"] != System.DBNull.Value)
                {
                    tour.InterestGender = (string)(dr["IntrestInGender"]);
                }
                if (dr["FirstTimeInIsrael"] != System.DBNull.Value)
                {
                    tour.FirstTimeInIsrael = Convert.ToBoolean(dr["FirstTimeInIsrael"]);
                }
                if (dr["gender"] != System.DBNull.Value)
                {
                    tour.Gender = (string)(dr["gender"]);
                }
                if (dr["tripType"] != System.DBNull.Value)
                {
                    tour.TripType = (string)(dr["tripType"]);
                }
                if (dr["FromDate"] != System.DBNull.Value)
                {
                    tour.FromDate = (string)(dr["FromDate"]);
                }
                if (dr["ToDate"] != System.DBNull.Value)
                {
                    tour.ToDate = (string)(dr["ToDate"]);
                }
                if (dr["EstimateDate"] != System.DBNull.Value)
                {
                    tour.EstimateDate = (string)(dr["EstimateDate"]);
                }
                if (dr["Budget"] != System.DBNull.Value)
                {
                    tour.Budget = (string)(dr["Budget"]);
                }
                if (dr["LCode"] != System.DBNull.Value)
                {
                    tour.LanguageCode = Convert.ToInt32(dr["LCode"]);
                }
                if (dr["Code"] != System.DBNull.Value)
                {
                    int expertise = Convert.ToInt32(dr["Code"]);
                    if (ExpertisesList.Count == 0)
                    {
                        ExpertisesList.Add(expertise);
                    }
                    else
                    {
                        if (ExpertisesList.Contains(expertise))
                        {
                        }
                        else
                        {
                            ExpertisesList.Add(expertise);
                        }
                    }
                }
                if (dr["HCode"] != System.DBNull.Value)
                {
                    int hobby = Convert.ToInt32(dr["HCode"]);
                    if (HobbiesList.Count == 0)
                    {
                        HobbiesList.Add(hobby);
                    }
                    else
                    {
                        if (HobbiesList.Contains(hobby))
                        {

                        }
                        else
                        {
                            HobbiesList.Add(hobby);
                        }
                    }
                }
            }


            //bool existHob = false;
            //bool existExper = false;

            //for (int i = 0; i < touristList.Last().Hobbies.Count; i++) //רץ על התחביבים של התייר האחרון
            //{
            //    int element = touristList.Last().Hobbies[i];
            //    if (element == Convert.ToInt32(dr["HobbyHCode"])) //בודק אם התחביב כבר הוכנס
            //    {
            //        existHob = true;
            //    }
            //}
            //if (!existHob) //אם התחביב לא הוכנס עדיין
            //{
            //    if (dr["HobbyHCode"] != System.DBNull.Value)
            //    {
            //        touristList.Last().Hobbies.Add(Convert.ToInt32(dr["HobbyHCode"]));
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < touristList.Last().Expertises.Count; i++)
            //    {
            //        int element = touristList.Last().Expertises[i];
            //        if (element == Convert.ToInt32(dr["ExpertiseCode"])) //בודק אם ההתמחות הוכנסה
            //        {
            //            existExper = true;
            //        }
            //    }
            //    if (!existExper) //אם ההתמחות לא הוכנסה עדיין
            //    {
            //        if (dr["ExpertiseCode"] != System.DBNull.Value)
            //        {
            //            touristList.Last().Expertises.Add(Convert.ToInt32(dr["ExpertiseCode"]));
            //        }
            //    }
            //}
            tour.Expertises = ExpertisesList;
            tour.Hobbies = HobbiesList;
            return tour;
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

    private string BuildInsertTripCommand(AreaPointInTrip trip)
    {
        String command;
        DateTime fromdate = Convert.ToDateTime(trip.FromDate.ToString());
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}',{2},{3})", trip.AreaName, fromdate.ToString("yyyy-MM-dd HH:mm:ss"), trip.OrderNumber,trip.TripPlan_IdPlan);
        String prefix = "INSERT INTO AreaPointInTrip_Project " + "(AreaName,FromDate,OrderNumber,TripPlan_IdPlan)";
        command = prefix + sb.ToString();
        return command;
    }

    //GET GUIDE BY EMAIL
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
                if (dr["License"] != System.DBNull.Value)
                {
                    guide.License = Convert.ToInt32(dr["License"]);
                }
                if (dr["descriptionGuide"] != System.DBNull.Value)
                {
                    guide.DescriptionGuide = (string)dr["descriptionGuide"];
                }
                if (dr["phone"] != System.DBNull.Value)
                {
                    guide.Phone = (string)(dr["phone"]);
                }
                guide.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                if (dr["gender"] != System.DBNull.Value)
                {
                    bool genderGuide = Convert.ToBoolean(dr["gender"]);
                    if (genderGuide)
                    {
                        guide.Gender = "male";
                    }
                    else
                    {
                        guide.Gender = "female";
                    }
                }
               
                if (dr["Rank"] != System.DBNull.Value)
                {
                    guide.Rank = Convert.ToDouble(dr["Rank"]);
                }
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

    public int UpdateGoogleOrFacebookAccount(Tourist tourist)
    {
        SqlConnection con;
        SqlCommand cmd;
        string cStr;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cStr = "UPDATE TouristProject SET passwordTourist = '" + (tourist.PasswordTourist) + "', yearOfBirth = '" + (tourist.YearOfBirth) + "', gender= '" + (tourist.Gender) + "'  WHERE email = '" + (tourist.Email) + "'";


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

    public Guide GetGuideBygCode(int id)
    {
        Guide guide = new Guide();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM GuideProject where gCode ='" + id + "'";
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
                if (dr["License"] != System.DBNull.Value)
                {
                    guide.License = Convert.ToInt32(dr["License"]);
                }
                if (dr["descriptionGuide"] != System.DBNull.Value)
                {
                    guide.DescriptionGuide = (string)dr["descriptionGuide"];
                }
                if (dr["phone"] != System.DBNull.Value)
                {
                    guide.Phone = (string)(dr["phone"]);
                }
                guide.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                if (dr["gender"] != System.DBNull.Value)
                {
                    bool genderGuide = Convert.ToBoolean(dr["gender"]);
                    if (genderGuide)
                    {
                        guide.Gender = "male";
                    }
                    else
                    {
                        guide.Gender = "female";
                    }
                }

                if (dr["Rank"] != System.DBNull.Value)
                {
                    guide.Rank = Convert.ToDouble(dr["Rank"]);
                }
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

    public Guide GetGuideByLicenseNum(int license)
    {
        Guide guide = new Guide();
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM GuideProject where License ='" + license + "'";
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
                if (dr["License"] != System.DBNull.Value)
                {
                    guide.License = Convert.ToInt32(dr["License"]);
                }
                if (dr["descriptionGuide"] != System.DBNull.Value)
                {
                    guide.DescriptionGuide = (string)dr["descriptionGuide"];
                }
                if (dr["phone"] != System.DBNull.Value)
                {
                    guide.Phone = (string)(dr["phone"]);
                }
                guide.SignDate = Convert.ToDateTime(dr["SignDate"]).ToString("MM/dd/yyyy");
                guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                if (dr["gender"] != System.DBNull.Value)
                {
                    bool genderGuide = Convert.ToBoolean(dr["gender"]);
                    if (genderGuide)
                    {
                        guide.Gender = "male";
                    }
                    else
                    {
                        guide.Gender = "female";
                    }
                }

                if (dr["Rank"] != System.DBNull.Value)
                {
                    guide.Rank = Convert.ToDouble(dr["Rank"]);
                }
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

    //מכניס מדריך ממשרד התיירות
    public int PostGuideToSQLFromGovIL(Guide g)
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

        String cStr = BuildInsertGovCommand(g);      // helper method to build the insert string

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

    private string BuildInsertGovCommand(Guide g)
    {
        string Password = "GovILPass" + g.FirstName;
        g.ProfilePic = "";
        g.BirthDay = g.SignDate;
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}')", g.Email, Password, g.FirstName, g.LastName, g.SignDate, g.ProfilePic, g.License, g.Phone, g.BirthDay, g.DescriptionGuide);
        String prefix = "INSERT INTO GuideProject " + "(email,passwordGuide,firstName,LastName,SignDate,profilePic,License,Phone,BirthDay,descriptionGuide)";
        command = prefix + sb.ToString();
        return command;
    }

    //Update Guide ProfilePic
    public int UpdateGuidePicture(string picPath, int id)
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

        String cStr = BuildUpdatePictureGuide(picPath, id);      // helper method to build the insert string

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

    private string BuildUpdatePictureGuide(string picPath, int id)
    {
        String command = "";

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "UPDATE GuideProject SET profilePic = '" + picPath + "' WHERE gCode = " + id;
        return command;
    }


    //Change Guide Password
    public int ChangePass(string randPass, int gCode)
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

        String cStr = BuildUpdatePassGuide(randPass, gCode);      // helper method to build the insert string

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

    private string BuildUpdatePassGuide(string randPass, int gCode)
    {

        String command = "";

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "UPDATE GuideProject SET passwordGuide = '" + randPass + "' WHERE gCode = " + gCode;
        return command;
    }

    //POST GUIDE
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
    private String BuildInsertCommand(Guide g)
    {
        int GenderGuider = 0;
        if (g.Gender == "male" || g.Gender == "")
        {
            GenderGuider = 1;
        }
        else if (g.Gender == "female")
        {
            GenderGuider = 0;
        }
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}',{9},'{10}')", g.Email, g.PasswordGuide, g.FirstName, g.LastName, g.SignDate, g.ProfilePic, 0, "", "", GenderGuider, g.BirthDay);
        String prefix = "INSERT INTO GuideProject " + "(email,passwordGuide,firstName,LastName,SignDate,profilePic,License,descriptionGuide,Phone,gender,BirthDay)";
        command = prefix + sb.ToString();
        return command;
    }


    //Update Guide's Details 
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
        else if (g.Gender == "female")
        {
            GenderGuider = 0;
        }
        command = "UPDATE GuideProject SET firstName ='" + g.FirstName + "',BirthDay='" + g.BirthDay + "',phone='" + g.Phone + "', LastName='" + g.LastName + "',descriptionGuide='" + g.DescriptionGuide + "', License=" + g.License + ", gender=" + GenderGuider + " WHERE email = '" + g.Email + "'";
        return command;

    }

    //***********RANK*****************
    //Get Guide Rank's By Tourist's ID
    public Guide_Tourist GetGuidesRankByID(int id)
    {
        Guide_Tourist g = new Guide_Tourist();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select * from Guide_Tourist_Project where TouristId =" + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                g.Rank = Convert.ToInt32(dr["Rank"]);
                g.TouristId = Convert.ToInt32(dr["TouristId"]);
                g.guidegCode = Convert.ToInt32(dr["guidegCode"]);
            }
            return g;
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
    //Get Ranks Of Guide To Calculate Rank AVG
    public List<Guide_Tourist> GetAllRanksOfGuide(int id)
    {
        List<Guide_Tourist> gList = new List<Guide_Tourist>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file
            String selectSTR = "select * from Guide_Tourist_Project where guidegCode =" + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Guide_Tourist g = new Guide_Tourist();
                g.Rank = Convert.ToInt32(dr["Rank"]);
                g.TouristId = Convert.ToInt32(dr["TouristId"]);
                g.guidegCode = Convert.ToInt32(dr["guidegCode"]);
                gList.Add(g);
            }
            return gList;
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
    //Update Guide Rank
    public int UpdateRankGuide(int id, double sum)
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

        String cStr = BuildUpdateCommandRankGuide(id, sum);      // helper method to build the insert string

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

    private string BuildUpdateCommandRankGuide(int id, double sum)
    {
        String command = "";

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string

        command = "UPDATE GuideProject SET Rank =" + sum + " WHERE gCode = " + id;
        return command;

    }
    //END GUIDE CLASS ****************END GUIDE CLASS ****************END GUIDE CLASS


    //TOURIST CLASS
    //TOURIST LOG IN WITH FACEBOOK
    public Tourist LogInFacebook(Tourist tourist)
    {
        Tourist t = new Tourist();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from TouristProject where email = '" + (tourist.Email) + "'";


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                t.Email = (string)dr["email"];
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

    //TOURIST TRIP TYPE
    public int TouristTripType(Tourist tourist)
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

        String cStr = "INSERT INTO Trip_Plan_Project (TouristEmail, tripType) VALUES ('" + (tourist.Email) + "','" + (tourist.TripType) + "')";


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
    //TOURIST BUDGET TRIP UPDATE
    public int BudgetUPDATE(Tourist tourist)
    {
        SqlConnection con;
        SqlCommand cmd;
        string cStr;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cStr = "UPDATE Trip_Plan_Project SET Budget = '" + (tourist.Budget) + "' WHERE TouristEmail = '" + (tourist.Email) + "'";


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



    //TOURIST FLIGHT DATES
    public int FlightsDatesUpdate(Tourist tourist)
    {
        SqlConnection con;
        SqlCommand cmd;
        string cStr;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        if (tourist.FromDate == null && tourist.ToDate == null)
        {
            cStr = "UPDATE Trip_Plan_Project SET FromDate = NULL, ToDate = NULL, EstimateDate = '" + (tourist.EstimateDate) + "' WHERE TouristEmail = '" + (tourist.Email) + "'";

        }
        else
        {
            cStr = "UPDATE Trip_Plan_Project SET FromDate = '" + (tourist.FromDate) + "', ToDate = '" + (tourist.ToDate) + "', EstimateDate = NULL WHERE TouristEmail = '" + (tourist.Email) + "'";

        }
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


    //TOURIST FIRST TIME IN ISRAEL
    public int FirstTimeInIsraelUPDATE(Tourist tourist)
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

        String cStr = "UPDATE TouristProject SET FirstTimeInIsrael = '" + (tourist.FirstTimeInIsrael) + "' WHERE email = '" + (tourist.Email) + "'";

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

    //TOURIST LOG IN WITH GOOGLE
    public int AddGoogleAccount(Tourist tourist)
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

        String cStr = BuildGoogleAccountCommand(tourist);      // helper method to build the insert string

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

    private string BuildGoogleAccountCommand(Tourist t)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string


        command = "INSERT INTO TouristProject (FirstName, LastName, email, ProfilePic) VALUES ('" + (t.FirstName) + "','" + (t.LastName) + "', '" + (t.Email) + "', '" + (t.ProfilePic) + "')";

        return command;
    }

    //TOURIST ADD FACEBOOK ACOUNT
    public int AddFacebookAccount(Tourist tourist)
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

        String cStr = BuildFacebookAccountCommand(tourist);      // helper method to build the insert string

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

    private string BuildFacebookAccountCommand(Tourist t)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string


        command = "INSERT INTO TouristProject (FirstName, LastName, email, ProfilePic) VALUES ('" + (t.FirstName) + "','" + (t.LastName) + "', '" + (t.Email) + "', '" + (t.ProfilePic) + "')";

        return command;
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

    //check if tourist is already signed up
    public Tourist CheckIfUserExist(Tourist tourist)
    {
        Tourist t = new Tourist();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from TouristProject where email = '" + (tourist.Email) + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                t.Email = (string)dr["email"];
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

    //TOURIST LOG IN
    public Tourist LogInCheck(Tourist tourist)
    {
        Tourist t = new Tourist();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from TouristProject where email = '" + (tourist.Email) + "' and passwordTourist = '" + (tourist.PasswordTourist) + "'";

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
    public int Interest(int TouristID, int interestId, string table)
    {
        SqlConnection con;
        SqlCommand cmd;
        string cStr;

        try
        {
            con = connect("ConnectionStringName"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cStr = "INSERT INTO " + (table) + " (TouristId, " + (table == "Hobby_Tourist_Project" ? "HobbyHCode" : "ExpertiseCode") + ") VALUES (" + (TouristID) + "," + (interestId) + ")";


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


    //Post Tourist Language CODE To DB
    public int PostTouristLanguageToDB(Tourist tourist)
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

        String cStr = "INSERT INTO Tourist_Language_Project (IdTourist, LanguageLCode) VALUES (" + (tourist.TouristID) + "," + (tourist.LanguageCode) + ")";


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


    public int GetTouristId(Tourist tourist)
    {
        Tourist t = new Tourist();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select Id from TouristProject where email = '" + (tourist.Email) + "'";


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                t.TouristID = Convert.ToInt32(dr["Id"]);
            }

            return t.TouristID;
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

    //Post Rank Of Guide By Tourist
    public int PostRankGuideByTourist(Guide_Tourist guide_Tourist)
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

        String cStr = BuildInsertGuideTouristRank(guide_Tourist);      // helper method to build the insert string

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

    private string BuildInsertGuideTouristRank(Guide_Tourist g)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1},{2},'{3}','{4}')", g.TouristId, g.guidegCode, g.Rank, g.DateOfRanking, g.Comment);
        String prefix = "INSERT INTO Guide_Tourist_Project" + "(TouristId,guidegCode,Rank,DateOfRanking,Comment)";
        command = prefix + sb.ToString();

        return command;
    }
    //**********************************END TOURIST CLASS ******************************************************


    //AREA CLASS ***************************************************************************************************
    //Post Areas To SQL
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

    //GET All Areas From SQL
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
    //POST AREA GUIDE
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
    //LANGUAGE CLASS **************************************************************************
    //Get All Languages List From SQL
    public List<Language> ReadLanguagesFromSQL()
    {
        List<Language> LanguagesList = new List<Language>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Language_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader.....
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Language lan = new Language();
                lan.Id = Convert.ToInt32(dr["LCode"]);
                lan.LName = (string)dr["LName"];
                lan.LNameEnglish = (string)dr["LNameEnglish"];
                lan.LCode = Convert.ToInt32(dr["LCode"]);
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
    //POST LANGUAGE GUIDE
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


    //DELETE All Guide's Languages
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

    //Get All Guide's Languages From SQL
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

    //HOBBY CLASS *****************************************************************

    //GET All Hobbies From SQL
    public IEnumerable<Hobby> GetAllHobbiesFromSQL()
    {
        List<Hobby> hobbieList = new List<Hobby>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Hobby_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Hobby hobby = new Hobby();
                hobby.HCode = Convert.ToInt32(dr["HCode"]);
                hobby.HName = (string)dr["HName"];
                hobby.Picture = (string)dr["Picture"];
                hobby.Type = (string)dr["Type"];
                hobbieList.Add(hobby);
            }
            return hobbieList;
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


    //GET Guide's Hobbies
    public List<Hobby> GetGuideHobbies(int id)
    {
        List<Hobby> hobbieList = new List<Hobby>();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from Hobby_Project h join guide_Hobby_Project g on h.HCode = g.HobbyHCode where g.guidegCode = " + id;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {  // Read till the end of the data into a row
                Hobby hobby = new Hobby();
                hobby.HCode = Convert.ToInt32(dr["HCode"]);
                hobby.HName = (string)dr["HName"];
                hobby.Picture = (string)dr["Picture"];
                hobbieList.Add(hobby);
            }

            return hobbieList;
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


    //POST Guide's Hobbies To SQL
    public int PostGuideHobbiesToSQL(Guide_Hobby guide_Hobby)
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

        String cStr = BuildInsertGuideHobbiesCommand(guide_Hobby);      // helper method to build the insert string

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

    private string BuildInsertGuideHobbiesCommand(Guide_Hobby guide_Hobby)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1})", guide_Hobby.guidegCode, guide_Hobby.HobbyHCode);
        String prefix = "INSERT INTO guide_Hobby_Project " + "(guidegCode,HobbyHCode)";
        command = prefix + sb.ToString();

        return command;
    }

    //DELETE ALL Guide's Hobbies
    public void DeleteAllGuideHobbies(int guidegCode)
    {
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "DELETE FROM guide_Hobby_Project where guidegCode = " + guidegCode;
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
//**************END HOBBIES ***********************************
    //EXPERTISE CLASS **********************************************************

    //GET ALL Expertises From SQL
    public IEnumerable<Expertise> GetAllExpertisesFromSQL()
    {
        List<Expertise> EXList = new List<Expertise>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Expertise_Project";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                Expertise ex = new Expertise();
                ex.Code = Convert.ToInt32(dr["Code"]);
                ex.NameE = (string)dr["NameE"];
                ex.Picture = (string)dr["Picture"];
                ex.Type = (string)dr["Type"];
                EXList.Add(ex);
            }
            return EXList;
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
    //Get Guide's Expertises From SQL
    public List<Expertise> GetGuideExpertisesFromSQL(int id)
    {
        List<Expertise> ExpertiseList = new List<Expertise>();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from Expertise_Project e join guide_Expertise_Project g on e.Code = g.ExpertiseCode where g.guidegCode = " + id;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {  // Read till the end of the data into a row
                Expertise exper = new Expertise();
                exper.Code = Convert.ToInt32(dr["Code"]);
                exper.NameE = (string)dr["NameE"];
                exper.Picture = (string)dr["Picture"];
                ExpertiseList.Add(exper);
            }

            return ExpertiseList;
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
    //POST EXPERTISE GUIDE
    public int PostGuideExpertiseToSQL(Guide_Expertise guide_Expertise)
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

        String cStr = BuildInsertGuideExpertisesCommand(guide_Expertise);      // helper method to build the insert string

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

    private string BuildInsertGuideExpertisesCommand(Guide_Expertise guide_Expertise)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1})", guide_Expertise.guidegCode, guide_Expertise.ExpertiseCode);
        String prefix = "INSERT INTO guide_Expertise_Project " + "(guidegCode,ExpertiseCode)";
        command = prefix + sb.ToString();

        return command;
    }

    //Delete Guide Expertises
    public void DeleteAllGuideExpertiseFromSQL(int id)
    {
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "DELETE FROM guide_Expertise_Project where guidegCode = " + id;
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
    //************************************MATCH CLASS ***************************************************
    //Convert Guide To Match Class
    public Guide GetSpecificGuideDetailsMatch(int id)
    {
        Guide guide = new Guide();
        List<Guide_Language> listLan = new List<Guide_Language>();
        List<Guide_Hobby> listHob = new List<Guide_Hobby>();
        List<Guide_Expertise> listExper = new List<Guide_Expertise>();
        bool First = true;
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select g.BirthDay,g.gCode,gl.LanguageLCode,gh.HobbyHCode,ge.ExpertiseCode,Rank from GuideProject g left join guide_Language_Project gl on g.gCode = gl.guidegCode left join guide_Hobby_Project gh on g.gCode = gh.guidegCode left join guide_Expertise_Project ge on g.gCode = ge.guidegCode where gCode = " + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                if (First) //אם נכנס פעם ראשונה ללולאה
                {
                    guide.gCode = Convert.ToInt32(dr["gCode"]);
                    guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                    guide.Rank = Convert.ToDouble(dr["Rank"]);
                    if (dr["LanguageLCode"] != System.DBNull.Value)
                    {
                        Guide_Language lan = new Guide_Language();
                        lan.Guide_Code = Convert.ToInt32(dr["gCode"]);
                        lan.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                        listLan.Add(lan);
                        guide.gLanguages= listLan;
                    }
                    if (dr["HobbyHCode"] != System.DBNull.Value)
                    {
                        Guide_Hobby hob = new Guide_Hobby();
                        hob.guidegCode = Convert.ToInt32(dr["gCode"]);
                        hob.HobbyHCode = Convert.ToInt32(dr["HobbyHCode"]);
                        listHob.Add(hob);
                        guide.gHobbies = listHob;
                    }
                    if (dr["ExpertiseCode"] != System.DBNull.Value)
                    {
                        Guide_Expertise ge = new Guide_Expertise();
                        ge.guidegCode = Convert.ToInt32(dr["gCode"]);
                        ge.ExpertiseCode = Convert.ToInt32(dr["ExpertiseCode"]);
                        listExper.Add(ge);
                        guide.gExpertises = listExper;
                    }

                }
                else //במידה וכבר נכנס ללולאה
                {
                    bool existLang = false;
                    bool existHob = false;
                    bool existExper = false;
                    for (int i = 0; i < guide.gLanguages.Count; i++)
                    {
                        int element = guide.gLanguages[i].Language_Code;
                        if (element == Convert.ToInt32(dr["LanguageLCode"])) //בודק אם השפה כבר הוכנסה
                        {
                            existLang = true;
                        }
                    }
                    if (!existLang) //אם השפה עדיין לא הוכנסה
                    {
                        if (dr["LanguageLCode"] != System.DBNull.Value)
                        {
                            Guide_Language lan = new Guide_Language();
                            lan.Guide_Code = Convert.ToInt32(dr["gCode"]);
                            lan.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                            listLan.Add(lan);
                            guide.gLanguages = listLan;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < guide.gHobbies.Count; i++)
                        {
                            int element = guide.gHobbies[i].HobbyHCode;
                            if (element == Convert.ToInt32(dr["HobbyHCode"])) //בודק אם התחביב כבר הוכנס
                            {
                                existHob = true;
                            }
                        }
                        if (!existHob) //אם התחביב עוד לא הוכנס
                        {
                            if (dr["HobbyHCode"] != System.DBNull.Value)
                            {
                                Guide_Hobby hob = new Guide_Hobby();
                                hob.guidegCode = Convert.ToInt32(dr["gCode"]);
                                hob.HobbyHCode = Convert.ToInt32(dr["HobbyHCode"]);
                                listHob.Add(hob);
                                guide.gHobbies = listHob;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < guide.gExpertises.Count; i++)
                            {
                                int element = guide.gExpertises[i].ExpertiseCode;
                                if (element == Convert.ToInt32(dr["ExpertiseCode"])) //בודק אם ההתמחות הוכנסה
                                {
                                    existExper = true;
                                }
                            }
                            if (!existExper) //אם ההתמחות עוד לא הוכנסה
                            {
                                if (dr["ExpertiseCode"] != System.DBNull.Value)
                                {
                                    Guide_Expertise ge = new Guide_Expertise();
                                    ge.guidegCode = Convert.ToInt32(dr["gCode"]);
                                    ge.ExpertiseCode = Convert.ToInt32(dr["ExpertiseCode"]);
                                    listExper.Add(ge);
                                    guide.gExpertises = listExper;
                                }
                            }
                        }
                    }
                }
                First = false;
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

   

    //Convert All Guides To Match Class
    public List<Guide> GetGuidesDetailsMatch()
    {
        List<Guide> guideList = new List<Guide>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select Rank, g.BirthDay,g.gCode,gl.LanguageLCode,gh.HobbyHCode,ge.ExpertiseCode from GuideProject g left join guide_Language_Project gl on g.gCode = gl.guidegCode left join guide_Hobby_Project gh on g.gCode = gh.guidegCode left join guide_Expertise_Project ge on g.gCode = ge.guidegCode";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                if (guideList.Count == 0 || guideList.Last().gCode != Convert.ToInt32(dr["gCode"])) //בודק אם נכנס פעם ראשונה ללולאה או אם כבר קיימת מדריך כזה ברשימה
                {
                    Guide guide = new Guide();
                    if (dr["Rank"] != System.DBNull.Value)
                    {
                        guide.Rank = Convert.ToDouble(dr["Rank"]);
                    }
                    guide.gCode = Convert.ToInt32(dr["gCode"]);
                    guide.BirthDay = Convert.ToDateTime(dr["BirthDay"]).ToString("MM/dd/yyyy");
                    List<Guide_Language> listLan = new List<Guide_Language>();
                    List<Guide_Hobby> listHob = new List<Guide_Hobby>();
                    List<Guide_Expertise> listExper = new List<Guide_Expertise>();
                 
                    if (dr["LanguageLCode"] != System.DBNull.Value)
                    {
                        Guide_Language lan = new Guide_Language();
                        lan.Guide_Code = guide.gCode;
                        lan.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                        listLan.Add(lan);
                    }
                    guide.gLanguages = listLan;

                    if (dr["HobbyHCode"] != System.DBNull.Value)
                    {
                        Guide_Hobby hob = new Guide_Hobby();
                        hob.guidegCode = guide.gCode;
                        hob.HobbyHCode = Convert.ToInt32(dr["HobbyHCode"]);
                        listHob.Add(hob);
                    }

                    guide.gHobbies = listHob;
                   
                    if (dr["ExpertiseCode"] != System.DBNull.Value)
                    {
                        Guide_Expertise ge = new Guide_Expertise();
                        ge.guidegCode = guide.gCode;
                        ge.ExpertiseCode = Convert.ToInt32(dr["ExpertiseCode"]);
                        listExper.Add(ge);
                    }
                    guide.gExpertises = listExper;
                    guideList.Add(guide);
                }
                else //במידה והמדריך כבר קיים
                {
                    bool existLang = false;
                    bool existHob = false;
                    bool existExper = false;
                    for (int i = 0; i < guideList.Last().gLanguages.Count; i++) //רץ על רשימת השפות של המדריך האחרון שהוכנס
                    {
                        int element = guideList.Last().gLanguages[i].Language_Code;
                        if (element == Convert.ToInt32(dr["LanguageLCode"])) //בודק אם השפה כבר הוכנסה
                        {
                            existLang = true;
                        }
                    }
                    if (!existLang) //אם השפה עוד לא הוכנסה
                    {
                        if (dr["LanguageLCode"] != System.DBNull.Value)
                        {
                            Guide_Language lan = new Guide_Language();
                            lan.Guide_Code = Convert.ToInt32(dr["gCode"]);
                            lan.Language_Code = Convert.ToInt32(dr["LanguageLCode"]);
                            guideList.Last().gLanguages.Add(lan);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < guideList.Last().gHobbies.Count; i++) //רץ על רשימת התחביבים של המדריך האחרון
                        {
                            int element = guideList.Last().gHobbies[i].HobbyHCode;
                            if (element == Convert.ToInt32(dr["HobbyHCode"])) //בודק אם התחביב כבר הוכנס
                            {
                                existHob = true;
                            }
                        }
                        if (!existHob) //אם התחביב עוד לא הוכנס
                        {
                            if (dr["HobbyHCode"] != System.DBNull.Value)
                            {
                                Guide_Hobby hob = new Guide_Hobby();
                                hob.guidegCode = Convert.ToInt32(dr["gCode"]);
                                hob.HobbyHCode = Convert.ToInt32(dr["HobbyHCode"]);
                                guideList.Last().gHobbies.Add(hob);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < guideList.Last().gExpertises.Count; i++) //רץ על רשימת ההתמחויות של המדריך האחרון
                            {
                                int element = guideList.Last().gExpertises[i].ExpertiseCode;
                                if (element == Convert.ToInt32(dr["ExpertiseCode"])) //בודק אם ההתמחות כבר הוכנסה
                                {
                                    existExper = true;
                                }
                            }
                            if (!existExper) //אם ההתמחות עוד לא הוכנסה
                            {
                                if (dr["ExpertiseCode"] != System.DBNull.Value)
                                {
                                    Guide_Expertise ge = new Guide_Expertise();
                                    ge.guidegCode = Convert.ToInt32(dr["gCode"]);
                                    ge.ExpertiseCode = Convert.ToInt32(dr["ExpertiseCode"]);
                                    guideList.Last().gExpertises.Add(ge);
                                }
                            }
                        }
                    }
                  
                }
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

    //Convert Tourist To Match Class
    public Tourist GetSpecificTouristMatchDetails(int id)
    {
        Tourist tourist = new Tourist();
        List<int> listHob = new List<int>();
        List<int> listExper = new List<int>();
        bool First = true;
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select Id, yearOfBirth, LanguageLCode, HobbyHCode,ExpertiseCode from TouristProject t left join Tourist_Language_Project l on t.Id = l.IdTourist left join Hobby_Tourist_Project h on t.Id = h.TouristId left join TripPlanIntrest_Project tpe on t.Id = tpe.TouristId where Id=" + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row

                if (First) //אם נכנס פעם ראשונה ללולאה
                {
                     tourist.TouristID = Convert.ToInt32(dr["Id"]);
                    if (dr["yearOfBirth"] != System.DBNull.Value)
                    {
                        tourist.YearOfBirth = Convert.ToDateTime(dr["yearOfBirth"]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        tourist.YearOfBirth = "10/10/2020";
                    }
                    if (dr["LanguageLCode"] != System.DBNull.Value)
                    {
                        tourist.LanguageCode = Convert.ToInt32(dr["LanguageLCode"]);
                    }
                    else
                    {
                        tourist.LanguageCode = 0;
                    }

                    if (dr["HobbyHCode"] != System.DBNull.Value)
                    {
                        listHob.Add(Convert.ToInt32(dr["HobbyHCode"]));
                        tourist.Hobbies = listHob;
                    }
                    if (dr["ExpertiseCode"] != System.DBNull.Value)
                    {
                        listExper.Add(Convert.ToInt32(dr["ExpertiseCode"]));
                        tourist.Expertises = listExper;
                    }
                }
                else
                {
                    bool existHob = false;
                    bool existExper = false;
                    if (listHob.Count>0) //אם קיימת רשימת תחביבים
                    {
                        for (int i = 0; i < tourist.Hobbies.Count; i++)
                        {
                            int element = tourist.Hobbies[i];
                            if (element == Convert.ToInt32(dr["HobbyHCode"])) //בודק אם התחביב כבר הוכנס
                            {
                                existHob = true;
                            }
                        }
                    }
                      
                        if (!existHob) //אם התחביב עוד לא הוכנס
                        {
                        if (dr["HobbyHCode"] != System.DBNull.Value)
                        {
                            listHob.Add(Convert.ToInt32(dr["HobbyHCode"]));
                            tourist.Hobbies = listHob;
                        }
                        }
                        else
                        {
                        if (listExper.Count>0) //אם קיימת רשימת התמחויות
                        {
                            for (int i = 0; i < tourist.Expertises.Count; i++)
                            {
                                int element = tourist.Expertises[i];
                                if (element == Convert.ToInt32(dr["ExpertiseCode"])) //בודק אם ההתמחות כבר הוכנסה
                                {
                                    existExper = true;
                                }
                            }
                        }
                           
                            if (!existExper) //אם ההתמחות עוד לא הוכנסה
                            {
                            if (dr["ExpertiseCode"] != System.DBNull.Value)
                            {
                                listExper.Add(Convert.ToInt32(dr["ExpertiseCode"]));
                                tourist.Expertises = listExper;
                            }
                        }
                        }
                }
                First = false;
            }
            return tourist;
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

    //Convert All Tourists To Match Class
    public List<Tourist> GetTouristsMatchDetails()
    {
        List<Tourist> touristList = new List<Tourist>();
        SqlConnection con = null;
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select Id, yearOfBirth, LanguageLCode, HobbyHCode,ExpertiseCode from TouristProject t left join Tourist_Language_Project l on t.Id = l.IdTourist left join Hobby_Tourist_Project h on t.Id = h.TouristId left join TripPlanIntrest_Project tpe on t.Id = tpe.TouristId";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                if (touristList.Count == 0 || touristList.Last().TouristID != Convert.ToInt32(dr["Id"])) //אם נכנס פעם ראשונה ללולאה או אם קיים כבר תייר ברשימה
                {
                    Tourist tourist = new Tourist();
                    tourist.TouristID = Convert.ToInt32(dr["Id"]);
                    if (dr["yearOfBirth"] != System.DBNull.Value)
                    {
                        tourist.YearOfBirth = Convert.ToDateTime(dr["yearOfBirth"]).ToString("MM/dd/yyyy");
                    }
                    List<int> listHob = new List<int>();
                    List<int> listExper = new List<int>();
                    if (dr["LanguageLCode"] != System.DBNull.Value)
                    {
                        tourist.LanguageCode = Convert.ToInt32(dr["LanguageLCode"]);
                    }

                    if (dr["HobbyHCode"] != System.DBNull.Value)
                    {
                        listHob.Add(Convert.ToInt32(dr["HobbyHCode"]));
                    }
                    tourist.Hobbies = listHob;
                    if (dr["ExpertiseCode"] != System.DBNull.Value)
                    {
                        listExper.Add(Convert.ToInt32(dr["ExpertiseCode"]));
                    }
                    tourist.Expertises = listExper;
                    touristList.Add(tourist);
                }
                else //אם התייר כבר קיים ברשימה
                {
                    bool existHob = false;
                    bool existExper = false;
                   
                        for (int i = 0; i < touristList.Last().Hobbies.Count; i++) //רץ על התחביבים של התייר האחרון
                        {
                            int element = touristList.Last().Hobbies[i];
                            if (element == Convert.ToInt32(dr["HobbyHCode"])) //בודק אם התחביב כבר הוכנס
                            {
                                existHob = true;
                            }
                        }
                        if (!existHob) //אם התחביב לא הוכנס עדיין
                        {
                            if (dr["HobbyHCode"] != System.DBNull.Value)
                            {
                            touristList.Last().Hobbies.Add(Convert.ToInt32(dr["HobbyHCode"]));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < touristList.Last().Expertises.Count; i++)
                            {
                                int element = touristList.Last().Expertises[i];
                                if (element == Convert.ToInt32(dr["ExpertiseCode"])) //בודק אם ההתמחות הוכנסה
                                {
                                    existExper = true;
                                }
                            }
                            if (!existExper) //אם ההתמחות לא הוכנסה עדיין
                            {
                                if (dr["ExpertiseCode"] != System.DBNull.Value)
                                {
                                touristList.Last().Expertises.Add(Convert.ToInt32(dr["ExpertiseCode"]));
                                }
                            }
                        }
                    }

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
    //***********************************END MATCH CLASS ******************************************************
   

    

    public void deleteAllGuideLinks(int id)
    {
        SqlConnection con = null;

        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "DELETE FROM Link_Project where guidegCode = " + id;
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

    public int PostGuideListLinks(Link links)
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

        String cStr = BuildInsertLinksCommand(links);      // helper method to build the insert string

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
    private string BuildInsertLinksCommand(Link l)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1},'{2}')", l.guidegCode, l.LinksCategoryLCode,l.linkPath);
        String prefix = "INSERT INTO Link_Project " + "(guidegCode,LinksCategoryLCode,linkPath)";
        command = prefix + sb.ToString();

        return command;
    }


    public List<Link> GetGuideLinksFromSQL(int id)
    {
        List<Link> listLinks = new List<Link>();
        SqlConnection con = null;
        String selectSTR = "";
        try
        {
            con = connect("ConnectionStringName"); // create a connection to the database using the connection String defined in the web config file

            selectSTR = "select * from Link_Project where guidegCode=" + id;


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {  // Read till the end of the data into a row
                Link l = new Link();
                l.guidegCode = Convert.ToInt32(dr["guidegCode"]);
                l.LinkCode = Convert.ToInt32(dr["LinkCode"]);
                l.LinksCategoryLCode = Convert.ToInt32(dr["LinksCategoryLCode"]);
                l.linkPath = (string)(dr["linkPath"]);
                listLinks.Add(l);
            }

            return listLinks;
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
   
  

}
