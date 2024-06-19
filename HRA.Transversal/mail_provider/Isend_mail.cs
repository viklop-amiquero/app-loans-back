using App.BackEndTransversal.Transversal_entidad;

namespace HRA.Transversal.mail_provider
{
    public interface Isend_mail
    {
        void SendGmail<T>(_Email<T> _Email);
        void SendOutlook<T>(_Email<T> _Email);
    }
}
