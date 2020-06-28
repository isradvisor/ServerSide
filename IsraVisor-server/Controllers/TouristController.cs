using IsraVisor_server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace IsraVisor_server.Controllers
{
    public class TouristController : ApiController
    {
        // GET api/<controller>
        public List<Tourist> Get()
        {
            Tourist t = new Tourist();
            return t.readTourist();
        }
        //Log In Check
        // POST api/<controller>
        public Tourist Post([FromBody]Tourist tourist)
        {
            Tourist t = new Tourist();
            return t.LogIn(tourist);
        }
        //Sign In
        // POST api/<controller>/Register
        [HttpPost]
        [Route("api/Tourist/Register")]
        public int Post2([FromBody]Tourist tourist)
        {


            return tourist.SignUp(tourist);

        }
        //Sign In with facebook
        // POST api/<controller>/FacebookUser
        [HttpPost]
        [Route("api/Tourist/FacebookUser")]
        public int Post3([FromBody]Tourist tourist)
        {


            return tourist.SignUpFacebook(tourist);

        }
        //Sign In with google
        // POST api/<controller>/GoogleUser
        [HttpPost]
        [Route("api/Tourist/GoogleUser")]
        public int Post4([FromBody]Tourist tourist)
        {


            return tourist.SignUpGoogle(tourist);

        }

        //What is your Trip Type Screen
        // POST api/<controller>/TripType
        [HttpPost]
        [Route("api/Tourist/TripType")]
        public int Post5([FromBody]Tourist tourist)
        {


            return tourist.TouristTripType(tourist);

        }

        //What is the user's interest (expertises & hobbies)
        // POST api/<controller>/Interest
        [HttpPost]
        [Route("api/Tourist/Interest")]
        public List<int> Post6([FromBody]Tourist tourist)
        {


            return tourist.Interest(tourist);

        }

        //First Time in Israel Screen UPDATE (true or false)
        // PUT api/<controller>/Register
        [HttpPut]
        [Route("api/Tourist/FirstTimeInIsrael")]
        public int Put([FromBody]Tourist tourist)
        {
            return tourist.FirstTimeInIsraelUPDATE(tourist);
        }

        //First Sign Up with Google or Facebook Account
        // PUT api/<controller>/Register
        [HttpPut]
        [Route("api/Tourist/GoogleFacebookSignUpFirstTime")]
        public int Put1([FromBody]Tourist tourist)
        {
            return tourist.GoogleFacebookSignUpFirstTime(tourist);
        }

        //Flights Dates UPDATE (FromDate, EndDate / EstimateDate)
        // PUT api/<controller>/FlightsDates
        [HttpPut]
        [Route("api/Tourist/FlightsDates")]
        public int Put2([FromBody]Tourist tourist)
        {
            return tourist.FlightsDates(tourist);
        }

        //Budget UPDATE (string)
        // PUT api/<controller>/Budget
        [HttpPut]
        [Route("api/Tourist/Budget")]
        public int Put3([FromBody]Tourist tourist)
        {
            return tourist.SetBudget(tourist);
        }

        //Edit Profile (Email, FirstName, LastName, SecondEmail)
        // PUT api/<controller>/EditProfile
        [HttpPut]
        [Route("api/Tourist/EditProfile")]
        public int Put5([FromBody]Tourist tourist)
        {
            return tourist.EditProfile(tourist);
        }

        //GetAllTouristDetailsByEmailTourist
        //[HttpGet]
        //[Route("api/Tourist/GetDetails/{id}")]
        public Tourist Get(string email)
        {
            Tourist t = new Tourist();
            return t.GetAllDetails(email);
        }

        [HttpGet]
        [Route("api/Tourist/GetAllTokensOfUsersInChat")]
        public List<Guide_Tourist> GetAllUsersInChat()
        {
            Guide_Tourist gt = new Guide_Tourist();
           return gt.GetAllUsersInChatToken();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("api/Tourist/Push")]
        public string send(PushNotData pnd)
        {

           return PostNotif(pnd);
        }

        [HttpPost]
        [Route("api/Tourist/Reset")]
        public int Reset([FromBody]string email)
        {
            Tourist tour = new Tourist();
           return tour.ResetPassword(email);
        }

        [Route("sendpushnotification")]
        public string PostNotif([FromBody]PushNotData pnd)
        {
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("https://exp.host/--/api/v2/push/send");
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            var objectToSend = new
            {
                to = pnd.to,
                title = pnd.title,
                body = pnd.body,
                sound = pnd.sound,
               data = pnd.data//new { name = "nir", grade = 100 }
                               //badge = pnd.badge,
            };
            string postData = new JavaScriptSerializer().Serialize(objectToSend);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            string returnStatus = ((HttpWebResponse)response).StatusDescription;
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.WriteLine("Succeess");
            return "success:) --- " + responseFromServer + ", " + returnStatus;
        }
    }
}