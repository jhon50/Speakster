using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Speakster.Models;

namespace Speakster.Controllers
{

    public class PayPalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Ipn()
        {
            //Post back to either sandbox or live
            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            //string strLive = "https://www.paypal.com/cgi-bin/webscr";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.DefaultConnectionLimit = 9999;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] Param = Request.BinaryRead(System.Web.HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(Param);
            strRequest = strRequest + "&cmd=_notify-validate";
            req.ContentLength = strRequest.Length;

            //Send the request to PayPal and get the response
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            if (strResponse == "VERIFIED")
            {
                //Processa pagamento
                string transaction_type = getRequest("txn_type");
                string user_Id = getRequest("custom");
                UserPayment user_payment = new UserPayment();
                bool alreadyRecurring = false;

                if (findUserRecurrencyPayment(user_Id) != null)
                {
                    user_payment = findUserRecurrencyPayment(user_Id);
                    alreadyRecurring = true;
                }

                switch (transaction_type)
                {
                    case "subscr_payment":
                        user_payment = setPaymentInfo(user_payment);
                        if (alreadyRecurring)
                        {
                            /*
                              * Se chegar até este ponto o aluno já possui
                              * recorrência, portanto, os seus dados serão apenas
                              * atualizados.
                            */
                            db.Entry(user_payment).State = EntityState.Modified;
                        }
                        else
                        {
                            /*
                               * Se chegar até este ponto o aluno ainda não 
                               * possui recorrência. Será adicionada uma nova
                               * entrada de pagamento para o usuário com seu
                               * respectivo User_ID
                             */
                            db.UserPayments.Add(user_payment);
                        }
                        break;

                    //Cancelamento da recorrência no site do PayPal
                    case "subscr_cancel":
                        user_payment.Payment_status = "Cancelled";
                        db.Entry(user_payment).State = EntityState.Modified;
                        break;

                    //Recorrência expirou
                    case "subscr_eot":
                        user_payment.Payment_status = "Expired";
                        db.Entry(user_payment).State = EntityState.Modified;

                        break;
                }

                db.SaveChanges();

                //db.PayPals.Add(paypal);
                //db.SaveChanges();
                //check the payment_status is Completed
                //check that txn_id has not been previously processed
                //check that receiver_email is your Primary PayPal email
                //check that payment_amount/payment_currency are correct
                //process payment
            }
            else if (strResponse == "INVALID")
            {
                //log for manual investigation
            }
            else
            {
                //Response wasn't VERIFIED or INVALID, log for manual investigation
            }
            return View();
        }

        private UserPayment setPaymentInfo(UserPayment user_payment)
        {
            user_payment.User_id = getRequest("custom");
            user_payment.Payment_status = getRequest("payment_status");
            user_payment.Address_city = getRequest("address_city");
            user_payment.Address_country = getRequest("address_country");
            user_payment.Address_state = getRequest("address_state");
            user_payment.Address_zip = getRequest("address_zip");
            user_payment.First_name = getRequest("first_name");
            user_payment.Last_name = getRequest("last_name");
            user_payment.Payer_email = getRequest("payer_email");
            user_payment.Payment_date = getRequest("payment_date");
            user_payment.Receiver_email = getRequest("receiver_email");
            //user_payment.Subscr_date = Request["subscr_date"].ToString();
            user_payment.Txn_id = getRequest("txn_id");
            user_payment.Txn_type = getRequest("txn_type");
            return user_payment;
        }

        private string getRequest(string value)
        {
            var data = Request[value];
            return data;
        }

        private UserPayment findUserRecurrencyPayment(string user_Id)
        {
            UserPayment user_payment = db.UserPayments.Find(user_Id);
            return user_payment;
        }
    }
}