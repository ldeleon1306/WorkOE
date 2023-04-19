using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using WorkerOE.Models;
using static WorkerOE.Models.CollectionMongo;

namespace WorkerOE.Mail
{
   public class Mail
    {
        public void mail(string _mailFrom, string _mailTo, string _smtpServer)
        {
            dynamic l = Listas.ListaM;
            string fecha = DateTime.Now.ToString("yyMMdd");
            string myfile = @"/tmp/MODIF-ACCIONES-F"+ fecha + ".txt";

            // Overwriting to the above existing file
            using (StreamWriter sw = File.CreateText(myfile))
            {
                foreach (var item in l)
                {
                    sw.WriteLine(item);
                }              
            }
            // Opening the file for reading
            using (StreamReader sr = File.OpenText(myfile))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine(myfile);

            MailMessage mail = new MailMessage();  
            mail.From = new MailAddress(_mailFrom);
            //mail.To.Add(_mailTo); //para un mail
            var multiple = _mailTo.Split(';');
            foreach (var to in multiple)
            {
                if (to != string.Empty)
                    mail.To.Add(to);
            }
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(myfile);
            mail.Attachments.Add(attachment);
    
            string subject = string.Format($"MODIF - ACCIONES - F"+ fecha + ".txt");
            string bodyMsg = string.Format($"Se adjunta reporte a la fecha.");
            mail.Subject = subject;
            mail.Body = bodyMsg;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(_smtpServer);
            smtp.EnableSsl = false;
            smtp.Port = 25;
            smtp.UseDefaultCredentials = true;
        
            smtp.Send(mail);
        }
     
    }
}
