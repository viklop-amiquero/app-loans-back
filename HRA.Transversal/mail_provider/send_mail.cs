using App.BackEndTransversal.Transversal_entidad;
using HRA.Transversal.Interfaces;
using System.Net;
using System.Net.Mail;

namespace HRA.Transversal.mail_provider
{
    public class send_mail : Isend_mail
    {
        private readonly ICredential _credentials;
        public send_mail(ICredential credentials)
        {
            _credentials = credentials;
        }
        public void SendGmail<T>(_Email<T> _Email)
        {
            Random random = new Random();
            var remitente_email = _credentials.Gmail.OrderBy(x => random.Next()).FirstOrDefault();

            using (var client = new SmtpClient(_credentials.ClientG, int.Parse(_credentials.Port)))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(remitente_email.email, remitente_email.password);

                MailAddress from = new MailAddress(remitente_email.email, _Email.remitente);
                MailAddress to = new MailAddress(_Email.destinatario_email);
                MailMessage message = new MailMessage(from, to);

                if (!string.IsNullOrWhiteSpace(_Email.destinatario_copia))
                {
                    string[] copias = _Email.destinatario_copia.Split(',');

                    if (copias.Length > 1)
                    {
                        foreach (string c in copias)
                        {
                            message.CC.Add(c.Trim());
                        }
                    }
                    else
                    {
                        message.CC.Add(_Email.destinatario_copia.Trim());
                    }
                }

                if (!string.IsNullOrWhiteSpace(_credentials.Cco))
                {
                    string[] copias = _credentials.Cco.Split(',');

                    if (copias.Length > 1)
                    {
                        foreach (string c in copias)
                        {
                            message.Bcc.Add(c.Trim());
                        }
                    }
                    else
                    {
                        message.Bcc.Add(_credentials.Cco.Trim());
                    }
                }

                message.Body = _Email.plantilla;
                message.IsBodyHtml = true;
                message.Subject = _Email.titulo;

                try
                {
                    client.Send(message);
                }
                catch (SmtpException ex)
                {
                    throw new Exception("Error al enviar el correo electrónico.", ex);
                }
                finally
                {
                    message.Dispose();
                }
            }
        }
        public void SendOutlook<T>(_Email<T> _Email)
        {
            Random random = new Random();
            var remitente_email = _credentials.Outlook.OrderBy(x => random.Next()).FirstOrDefault();

            using (var client = new SmtpClient(_credentials.ClientO, int.Parse(_credentials.Port)))
            { 
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(remitente_email.email, remitente_email.password);

                MailAddress from = new MailAddress(remitente_email.email, _Email.remitente);
                MailAddress to = new MailAddress(_Email.destinatario_email);
                MailMessage message = new MailMessage(from, to);

                if (!string.IsNullOrWhiteSpace(_Email.destinatario_copia))
                {
                    string[] copias = _Email.destinatario_copia.Split(',');

                    if (copias.Length > 1)
                    {
                        foreach (string c in copias)
                        {
                            message.CC.Add(c.Trim());
                        }
                    }
                    else
                    {
                        message.CC.Add(_Email.destinatario_copia.Trim());
                    }
                }

                if (!string.IsNullOrWhiteSpace(_credentials.Cco))
                {
                    string[] copias = _credentials.Cco.Split(',');

                    if (copias.Length > 1)
                    {
                        foreach (string c in copias)
                        {
                            message.Bcc.Add(c.Trim());
                        }
                    }
                    else
                    {
                        message.Bcc.Add(_credentials.Cco.Trim());
                    }
                }

                message.Body = _Email.plantilla;
                message.IsBodyHtml = true;
                message.Subject = _Email.titulo;

                try
                {
                    client.Send(message);
                }
                catch (SmtpException ex)
                {
                    throw new Exception("Error al enviar el correo electrónico.", ex);
                }
                finally
                {
                    message.Dispose();
                }
            }
        }
    }
}