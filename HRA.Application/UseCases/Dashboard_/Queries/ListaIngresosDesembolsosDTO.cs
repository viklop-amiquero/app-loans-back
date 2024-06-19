using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Dashboard_.Queries
{
    public record class ListaIngresosDesembolsosDTO : IMapFrom<Cuota>
    {
        public double V_MAX_INCOME_DAY { get; set; }
        public double V_MAX_DISBURSEMENT_DAY { get; set; }
        public double V_SUM_INCOMES_DAY {  get; set; }
        public double V_SUM_DISBURSEMENTS_DAY { get; set; }
        public double V_SUM_EXPENSES_DAY { get; set; }
        public List<double> V_INCOMES_DAY { get; set; }
        public List<double> V_DISBURSEMENTS_DAY { get; set; }
        public List<double> V_EXPENSES_DAY { get; set; }
        public double V_INCOMES_MONTH { get; set; }
        public double V_DISBURSEMENTS_MONTH { get; set; }
        public double V_EXPENSES_MONTH { get; set; }

        public void Mapping(Profile profile)
        {
        }
    }
}
