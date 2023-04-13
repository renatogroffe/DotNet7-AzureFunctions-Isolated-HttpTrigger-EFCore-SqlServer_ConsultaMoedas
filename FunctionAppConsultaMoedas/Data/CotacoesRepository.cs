using FunctionAppConsultaMoedas.Models;

namespace FunctionAppConsultaMoedas.Data;

public class CotacoesRepository
{
    private readonly MoedasContext _context;

    public CotacoesRepository(MoedasContext context)
    {
        _context = context;
    }

    public IEnumerable<DadosCotacao> GetAll()
    {
        return _context.Cotacoes!.OrderByDescending(c => c.Id)
            .Select(c =>
                new DadosCotacao()
                {
                    Sigla = c.Sigla,
                    Horario = c.Horario,
                    Valor= c.Valor
                }).ToArray();
    }
}