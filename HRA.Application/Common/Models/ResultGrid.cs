namespace HRA.Application.Common.Models
{
    public class ResultGrid<T>
    {
        private T Data;
        private double PAGE_SIZE;
        public int Total_paginas { get; set; }
        public int Total_registros
        {
            get { return (int)PAGE_SIZE; }
            set
            {
                PAGE_SIZE = value;
                Total_paginas = (int)Math.Ceiling(PAGE_SIZE / Total_paginas);
            }
        }

        public T data
        {
            get { return Data; }
            set { this.Data = value; }
        }
        public ResultGrid()
        {
        }
    }

}
