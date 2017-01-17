using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;

namespace ACHE.Model
{
    public enum EmailTemplate
    {
        Contacto,
        ConsultaPropiedad
    }

    public static class EmailHelper
    {
        //public static readonly string HOST = ConfigurationManager.AppSettings["Email.Host"] ?? "No hay host definido";
        //public static readonly int PORT = int.Parse(ConfigurationManager.AppSettings["Email.Port"]);

        public static bool SendMessage(EmailTemplate template, ListDictionary replacements, string to, string subject)
        {
			string emailFrom = ConfigurationManager.AppSettings["Email.From"];
			string emailCC = ConfigurationManager.AppSettings["Email.CC"];
			MailMessage mailMessage = CreateMessage(template, replacements, to, emailFrom, emailCC, subject);

            return SendMailMessage(mailMessage);
        }

        private static MailMessage CreateMessage(EmailTemplate template, ListDictionary replacements, string to, string from, string cc, string subject)
        {
            string physicalPath = HttpContext.Current.Server.MapPath("~/App_Data/EmailTemplates/" + template.ToString() + ".txt");
            if (!File.Exists(physicalPath))
                throw new ArgumentException("Invalid EMail Template Passed into the NotificationManager", "template");

            string html = File.ReadAllText(physicalPath);
            foreach (DictionaryEntry param in replacements)
            {
                string val = param.Value == null ? "" : param.Value.ToString();
                html = html.Replace(param.Key.ToString(), val);
            }

			MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
		    mailMessage.Subject = subject;
            mailMessage.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc))
                mailMessage.CC.Add(new MailAddress(cc));
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = html;

            return mailMessage;
        }

        private static bool SendMailMessage(MailMessage mailMessage)
        {
            bool send = true;
            
            try
            {
                SmtpClient client = new SmtpClient();
                //client.UseDefaultCredentials = true;
                //client.Credentials = new NetworkCredential("wajid849@gmail.com", "password");
                //client.EnableSsl = false;                
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                send = false;
                //throw;// new Exception();
            }

            return send;
        }
    }


}