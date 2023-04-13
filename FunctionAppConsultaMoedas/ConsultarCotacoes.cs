using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppConsultaMoedas.Data;

namespace FunctionAppConsultaMoedas;

public class ConsultarCotacoes
{
    private readonly ILogger _logger;
    private readonly CotacoesRepository _repository;

    public ConsultarCotacoes(ILoggerFactory loggerFactory,
        CotacoesRepository repository)
    {
        _logger = loggerFactory.CreateLogger<ConsultarCotacoes>();
        _repository = repository;
    }

    [Function(nameof(ConsultarCotacoes))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("Consultando cotacoes ja cadastradas...");

        var dados = _repository.GetAll();
        _logger.LogInformation($"Numero de cotacoes encontradas = {dados.Count()}");

        var response = req.CreateResponse();
        response.StatusCode = HttpStatusCode.OK;
        await response.WriteAsJsonAsync(dados);
        return response;
    }
}