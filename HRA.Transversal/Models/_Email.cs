namespace App.BackEndTransversal.Transversal_entidad
{
    public class _Email<T>
    {
        private T Data;
        public T data
        {
            get { return Data; }
            set { this.Data = value; }
        }
        public string remitente { get; set; } = string.Empty;
        public string destinatario_email { get; set; } = string.Empty;
        public string destinatario_copia { get; set; } = string.Empty;
        public string titulo { get; set; } = string.Empty;
        public string fecha { get; set; } = string.Empty;
        public string plantilla { get; set; } = string.Empty;
        public string app_host_url { get; set; } = string.Empty;
    }

    public class _Auxiliar
    {
        public string Cco { get; set; } = string.Empty;
    }
}
