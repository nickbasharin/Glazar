using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

    public class mono_gmail : MonoBehaviour
    {
    

        public void SendMail(string mailToSent, string mailSubject, string mailText)
        {
            MailMessage mail = new MailMessage();

		mail.From = new MailAddress("glazarsender@gmail.com)");
            mail.To.Add(mailToSent);
            mail.Subject = mailSubject;
        
            //rnd= UnityEngine.Random.Range(1000, 9999).ToString();
            mail.Body = mailText;
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("glazarsender@gmail.com", "01aaaa01") as ICredentialsByHost; //642780ge
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpServer.Send(mail);
            Debug.Log("success");

        }
    }
