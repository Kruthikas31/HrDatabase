using JARVIS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace JARVIS.Controllers
{


    class ZohoSignWIthCurl
    {
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ZohoSignWIthCurl));

        public string getToken()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
              delegate { return true; }
            );
            var url = "https://accounts.zoho.in/oauth/v2/token?refresh_token=1000.4aebfb4712862510c1541ed5322d7ad1.378cd3fa47075b4216573a5cc563c78b&client_id=1000.0P79VDWM466RYJLTD31D76CKNHGSPR&client_secret=d78abdaa3cda76f284040638ec703fdc1fe415a482&redirect_uri=https%3A%2F%2Fsign.zoho.com&grant_type=refresh_token";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            // httpRequest.Headers["Cookie"] = "_zcsr_tmp=61d574b6-2d7f-406f-8b91-39fc588ae4ad; e274bf66cd=a666ff609d879e0aed399e8010732d82; iamcsr=61d574b6-2d7f-406f-8b91-39fc588ae4ad";


            string result;
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            Log.Debug("1 tokenResp : " + httpResponse.StatusCode);

            dynamic json1 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            Log.Debug("1 Resp : " + json1.access_token);
            return Convert.ToString(json1.access_token);
        }

        public ZohoUrl getDocument(string token)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
              delegate { return true; }
            );
            var url = "https://sign.zoho.in/api/v1/templates/16803000000012181/createdocument";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "Zoho-oauthtoken " + token;// 1000.d055c9cfadd77fdbf61ca5a5584bbfe4.6e26c3f9e51a6909863a89a331fd6e28";
            httpRequest.ContentType = "application/json";

            var data = @"{
                ""templates"": {
                ""field_data"": {
                ""field_text_data"": {},
                ""field_boolean_data"": {},
                ""field_date_data"": {}
                },
                ""actions"": [
                {
                    ""recipient_name"": ""amruta"",
                    ""recipient_email"": ""amrutakrishnapatil@gmail.com"",
                    ""is_embedded"":true,
                    ""action_id"": ""16803000000012234"",
                    ""signing_order"": 1,
                    ""verify_recipient"": false,
                    ""private_notes"": """"
                },
                {
                    ""recipient_name"": ""vamshi"",
                    ""recipient_email"": ""vamshipriya2998@gmail.com"",
                    ""is_embedded"":false,
                    ""action_id"": ""16803000000012236"",
                    ""signing_order"": 2,
                    ""verify_recipient"": false,
                    ""private_notes"": """"
                },
                {
                    ""recipient_name"": ""sagar"",
                    ""recipient_email"": ""sagarmr2006@gmail.com"",
                    ""is_embedded"":false,
                    ""action_id"": ""16803000000012236"",
                    ""signing_order"": 3,
                    ""verify_recipient"": false,
                    ""private_notes"": """"
                }
                ],
                ""notes"": """"
                }
            }";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            dynamic json2 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

            // storing request and action ID
            dynamic reqObj = json2.requests;
            string reqId = reqObj.request_id;
            dynamic actionObj = reqObj.actions;
            dynamic firstObj = actionObj[0];
            string actionId2 = (string)firstObj.GetValue("action_id");

            ZohoUrl obj = new ZohoUrl { actionId = actionId2, requestId = reqId };
            Log.Debug("2 getDocument : " + httpResponse.StatusCode);
            Log.Debug("2 getDocument : " + obj);
            return obj;


        }
        public string signUrl(string token, ZohoUrl docObj)
        {

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
            (
              delegate { return true; }
            );
            var url = "https://sign.zoho.in/api/v1/requests/" + docObj.requestId + "/actions/" + docObj.actionId + "/embedtoken?host=https://hrportalqc.vitalaxis.com/";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Headers["Authorization"] = "Zoho-oauthtoken " + token;//1000.d055c9cfadd77fdbf61ca5a5584bbfe4.6e26c3f9e51a6909863a89a331fd6e28";
                                                                              // httpRequest.Headers["Cookie"] = "997e0da029=5121b7c3aaf96aeb85c3ce66bd973e3a; JSESSIONID=639D4AF81F5964B1DFEB670B0C1B60E5; _zcsr_tmp=92717f2c-be0e-425f-92c7-e9be851d3942; f7e721de59=4805f5d121c13f64868c37c5dfcf9dce; zscsrfcookie=92717f2c-be0e-425f-92c7-e9be851d3942";
                                                                              //httpRequest.Headers["Content-Length"] = "0";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            string signUrl;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                signUrl = streamReader.ReadToEnd();
            }

            //            Console.WriteLine(httpResponse.StatusCode);
            dynamic jsonResp = Newtonsoft.Json.JsonConvert.DeserializeObject(signUrl);
            Log.Debug("3 signUrl : " + httpResponse.StatusCode);
            string temp = jsonResp.sign_url;
            Log.Debug("3 signUrl : " + temp);
            return temp;
            //Process.Start(temp);

        }
    }
    public class SignController : Controller
    {
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SignController));
        // GET: Sign
        public ActionResult Index()
        {
            Log.Info("Sign controller Index!!!");
            return View();
        }
        public ActionResult SignAgreements(Recipient obj)
        {
            Log.Info("__________________________________________________________________________________");
            Log.Info("calling methods of class ZohoSignWIthCurl");
            //ZohoSign zohoObj = new ZohoSign();
            ZohoSignWIthCurl zohoObject = new ZohoSignWIthCurl();
            string finalsignUrl = null;
            try
            {
                string token = zohoObject.getToken();
                ZohoUrl getDoc = zohoObject.getDocument(token);
                finalsignUrl = zohoObject.signUrl(token, getDoc);

            }
            catch (Exception e)
            {
                Log.Error("e : -" + e);
                Log.Error("e msg : -" + e.Message);
            }
            ViewBag.url = finalsignUrl;
            return View();
            /*
            string req = "", res = "";
            var name = obj.name; // Form name 
            var email = obj.email; // form mail
            var level = obj.level; // level fresher/EXP
            string template; // For template ID 
            var flevel = "fresher";

            if (level == flevel) //if fresher
            {
                template = "16803000000012181"; // set template id if fresher
                                                //(String)ConfigurationManager.AppSettings["FresherTemplate"];
            }
            else
            {
                template = "16803000000012001"; // set template if for exp 
                                                //(String)ConfigurationManager.AppSettings["ExperienceTemplate"];

            }

            string accessToken = "";
            RestClient Client1, Client2, Client3; // TO make 3 calls for API 



            // if fresher
            // for unique action ID to add inside request parameter
            string templateActionId1, templateActionId2, templateActionId3;
            if (level == "fresher")
            {
                templateActionId1 = "16803000000012234";
                templateActionId2 = "16803000000012236";
                templateActionId3 = "16803000000012238";

            }
            else
            {
                templateActionId1 = "16803000000012064";
                templateActionId2 = "16803000000012066";
                templateActionId3 = "16803000000012068";
            }

            string jsonData = @"{
                    'templates': {
                    'field_data': {
                    'field_text_data': { },
                    'field_boolean_data': { },
                    'field_date_data': { }
                         },
                    'actions': [
                     {
                    'recipient_name':'" + name + @"',
                    'recipient_email': '" + email + @"',
                    'is_embedded':true,
                    'action_id':'" + templateActionId1 + @"',
                    'signing_order': 1,
                    'role': '',
                    'verify_recipient': false,
                    'private_notes': ''
                    },
                     {
                    'recipient_name':'Vamshi Priya',
                    'recipient_email': 'priyavamshi40@gmail.com',
                    'is_embedded':false,
                    'action_id':'" + templateActionId2 + @"',
                    'signing_order': 2,
                    'role': '',
                    'verify_recipient': false,
                    'private_notes': ''
                    },
                    {
                    'recipient_name':'Sagar MR',
                    'recipient_email':'sagarmr2006@gmail.com',
                    'is_embedded':false,
                    'action_id':'" + templateActionId3 + @"',
                    'signing_order': 3,
                    'role': '',
                    'verify_recipient': false,
                    'private_notes': ''
                    }
                    ],
                    'notes': ''
                    }
                }";


            dynamic jsonDATA = JsonConvert.DeserializeObject(jsonData);


            dynamic json = jsonDATA.templates;
            zohoObj.callingRequestTwo(template, jsonDATA);
            string ggg = "AAA";
            */

        }
    }
}