using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JARVIS.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace JARVIS.Controllers
{
    public class ValuesController : ApiController
    {
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ValuesController));
        public void Index()
        {
            Log.Info("Values controller Index!!!");
        }
       [HttpGet]
        public IHttpActionResult StatusDownload()
        {
           

            string jsonData=@"{
                'requests': {
                    'request_status': 'completed',
    'owner_email': 'pamruta0826@gmail.com',
    'document_ids': [
      {
        'document_name': 'Total Value Proposition.pdf',
        'document_size': 614271,
        'document_order': '1',
        'total_pages': 1,
        'document_id': '16025000000024004'
      },
      {
        'document_name': 'LOA (3).pdf',
        'document_size': 67514,
        'document_order': '0',
        'total_pages': 1,
        'document_id': '16025000000024001'
      }
    ],
    'self_sign': false,
    'owner_id': '16025000000009003',
    'request_name': 'download.pdf',
    'modified_time': 1614406648026,
    'action_time': 1614407157695,
    'is_deleted': false,
    'is_sequential': true,
    'owner_first_name': 'Amruta',
    'request_type_name': 'Others',
    'owner_last_name': 'Patil',
    'request_id': '16025000000024013',
    'request_type_id': '16025000000000171',
    'actions': [
      {
        'verify_recipient': false,
        'action_type': 'SIGN',
        'action_id': '16025000000024029',
        'is_revoked': false,
        'recipient_email': 'priyavamshi40@gmail.com',
        'is_embedded': false,
        'signing_order': 2,
        'recipient_name': 'vamshi priya',
        'allow_signing': false,
        'recipient_phonenumber': '',
        'recipient_countrycode': '+91',
        'action_status': 'SIGNED'
      },
      {
        'verify_recipient': false,
        'action_type': 'SIGN',
        'action_id': '16025000000024021',
        'is_revoked': false,
        'recipient_email': 'amrutakrishnapatil@gmail.com',
        'is_embedded': true,
        'signing_order': 1,
        'recipient_name': 'amruta patil',
        'allow_signing': false,
        'recipient_phonenumber': '',
        'recipient_countrycode': '',
        'action_status': 'SIGNED'
      }
    ]
  },
  'notifications': {
    'performed_by_email': 'System Generated',
    'performed_at': 1614407157967,
    'country': 'INDIA',
    'activity': 'Document has been completed',
    'operation_type': 'RequestCompleted',
    'latitude': 12.97623,
    'performed_by_name': 'System Generated',
    'ip_address': '223.237.231.76',
    'longitude': 77.60329
  }
            }";

            dynamic json = JObject.Parse(jsonData);
            //dynamic json = JsonConvert.DeserializeObject(jsonData);
            string request_status = json.requests.request_status;
            int signing_order = 1;
            string recipient_email;
            string recipient_name;
            if (request_status == "completed")
            {
                foreach (JObject recipients in json.requests.SelectToken("actions"))
                {
                    int order = (int)recipients.SelectToken("signing_order");
                    if (signing_order == order)
                    {
                        recipient_email = (string)recipients.SelectToken("recipient_email");
                        recipient_name = (string)recipients.SelectToken("recipient_name");
                        Debug.WriteLine(recipient_name + "'s documnet is completed");

                        string dir = @"D:\ZohoDocs\" + recipient_name;
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);

                        }

                        //int reqId = ;//to be changed
                        string accessToken = "1000.cd9e44b42c87d16f500a8b455ed27e8a.bde984a04a0a1030d62dff471101f7a1";//to be changed
                        foreach (JObject document in json.requests.SelectToken("document_ids"))
                        {
                            String docId = (string)document.SelectToken("document_id");
                            string docName = (string)document.SelectToken("document_name");

                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                            Log.Info("Download Api call");
                            Log.Debug("Request for download"+"https://sign.zoho.in/api/v1/requests/" + 16025000000024013 + "/documents/" + docId + "/pdf");
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://sign.zoho.in/api/v1/requests/" + 16025000000024013 + "/documents/" + docId + "/pdf");
                            httpWebRequest.Method = "GET";
                            httpWebRequest.Headers["Authorization"] = "Zoho-oauthtoken " + accessToken;

                            using (HttpWebResponse response2 = (HttpWebResponse)httpWebRequest.GetResponse())
                            {

                                Log.Debug("Response for download:"+response2);
                                using (Stream output = System.IO.File.OpenWrite("D:/ZohoDocs/" + recipient_name + "/" + docName))
                                using (Stream input = response2.GetResponseStream())
                                {
                                    input.CopyTo(output);
                                    output.Close();
                                }
                                Debug.WriteLine(docName + " done");
                            }

                            //var Client1 = new RestClient("https://sign.zoho.com/api/v1/requests/" + reqId + "/documents/" + docId + "/pdf");
                            //HttpEntity entity = response.getEntity();
                            //InputStream is = entity.getContent();
                            //String filePath = "C:\\Users\\windowsUserName\\Downloads\\" + documentIds.getJSONObject(i).getString("document_name");
                            //FileOutputStream fos = new FileOutputStream(new File(filePath));
                            //byte[] buffer = new byte[5600];
                            //int inByte;
                            //while ((inByte = is.read(buffer)) > 0)
                            //    fos.write(buffer, 0, inByte);
                            //    is.close();
                            //fos.close();
                        }
                    }

                }

            }
            return Ok();
        }
    }
}