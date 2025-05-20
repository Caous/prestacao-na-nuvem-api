using MongoDB.Bson;
using MongoDB.Driver;

namespace PrestacaoNuvem.Api.Infrastructure.Repositories.Services;

public class LeadRepository : ILeadRepository
{
    private const string CollectionsGroupMessages = "leadsinnova";
    private readonly IMongoDatabase _dataBase;
    public LeadRepository(IMongoDatabase database)
    {
        _dataBase = database;
    }

    public async Task<bool> DeleteLeadAsync(ObjectId id)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var filter = Builders<LeadModel>.Filter.Eq("_id", id);

        var deleteResult = await collection.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<LeadModel> GetLeadByFilterId(LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var filters = new List<FilterDefinition<LeadModel>>();

        if (!string.IsNullOrEmpty(request.Name))
            filters.Add(Builders<LeadModel>.Filter.Eq("Name", request.Name));

        if (!string.IsNullOrEmpty(request.PhoneNumber))
            filters.Add(Builders<LeadModel>.Filter.Eq("PhoneNumber", request.PhoneNumber));

        if (!string.IsNullOrEmpty(request.Category))
            filters.Add(Builders<LeadModel>.Filter.Eq("Category", request.Category));

        var combinedFilter = filters.Count > 0
            ? Builders<LeadModel>.Filter.And(filters)
            : Builders<LeadModel>.Filter.Empty;

        var lead = await collection
            .Find(combinedFilter)
            .FirstOrDefaultAsync();

        return lead;
    }

    public async Task<ICollection<LeadModel>> GetLeadsAsync()
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        var group = await collection
            .Find(Builders<LeadModel>.Filter.Empty)
            .ToListAsync();

        return group.OrderByDescending(x => x.DataCriacao).ToArray();
    }

    public async Task<LeadModel> PostLeadAsync(LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        if (request != null)
            collection.InsertOneAsync(request).GetAwaiter().GetResult();

        return request;
    }

    public async Task<LeadModel> PutLeadAsync(ObjectId id, LeadModel request)
    {
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);

        if (request != null)
        {
            var filter = Builders<LeadModel>.Filter.Eq("_id", id);

            var xpto = await collection.FindAsync(filter);

            var updateDefinition = Builders<LeadModel>.Update
                                       .Set(x => x.Name, request.Name)
                                       .Set(x => x.Category, request.Category)
                                       .Set(x => x.Address, request.Address)
                                       .Set(x => x.TimeOpen, request.TimeOpen)
                                       .Set(x => x.Star, request.Star)
                                       .Set(x => x.WebSite, request.WebSite)
                                       .Set(x => x.Email, request.Email)
                                       .Set(x => x.RedeSocial, request.RedeSocial)
                                       .Set(x => x.Observacao, request.Observacao)
                                       .Set(x => x.Status, request.Status);

            var updateResult = await collection.ReplaceOneAsync(filter, request);

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return request;
            }
            else if (updateResult.IsAcknowledged && updateResult.UpsertedId != null)
            {
                return request;
            }
        }

        return null;
    }

    public async Task<int> GetLeadsCountByMonthAsync()
    {
        var inicioDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        return (int)await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).CountDocumentsAsync(x => x.DataCriacao >= inicioDoMes);
    }

    public async Task<int> GetLeadsCountByWeekAsync()
    {
        var inicioDaSemana = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
        return (int)await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).CountDocumentsAsync(x => x.DataCriacao >= inicioDaSemana);
    }

    public async Task<int> GetMeetingsCountAsync()
    {
        return (int)await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).CountDocumentsAsync(x => x.Status == Domain.Model.ELead.Reuniao);
    }

    public async Task<(int enviados, int abertos, int respondidos)> GetEmailStatsAsync()
    {
        // Simulação, ajuste conforme a lógica real do envio de e-mails
        var enviados = (int)await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).CountDocumentsAsync(x => !string.IsNullOrEmpty(x.Email));
        var abertos = (int)(enviados * 0.7); // exemplo
        var respondidos = (int)(abertos * 0.4); // exemplo
        return (enviados, abertos, respondidos);
    }

    public async Task<string?> GetTopCategoriaAsync()
    {
        var pipeline = new BsonDocument[]
        {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$Category" },
            { "count", new BsonDocument("$sum", 1) }
        }),
        new BsonDocument("$sort", new BsonDocument("count", -1)),
        new BsonDocument("$limit", 1)
        };

        var result = await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
        return result?["_id"]?.AsString;
    }

    public async Task<TimeSpan?> GetAverageConversionTimeAsync()
    {
        var convertidos = await _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages).Find(x => x.Status == Domain.Model.ELead.Convertido && x.Historico != null).ToListAsync();

        if (convertidos.Count == 0) return null;

        var tempos = convertidos
            .Select(x =>
            {
                var dataInicio = x.DataCriacao;
                var dataFim = x.Historico
                    .Where(h => h.Assunto?.ToLower().Contains("conversão") == true || h.Descricao?.ToLower().Contains("conversão") == true)
                    .Select(h => h.DataAtualizacao)
                    .OrderBy(d => d)
                    .FirstOrDefault();

                return dataFim.HasValue ? (TimeSpan?)(dataFim.Value - dataInicio) : null;
            })
            .Where(x => x.HasValue)
            .Select(x => x.Value)
            .ToList();


        return tempos.Count > 0 ? TimeSpan.FromTicks((long)tempos.Average(t => t.Ticks)) : null;
    }

    public async Task<int> GetLeadsCountByStatusAsync(int status)
    {
        if (!Enum.IsDefined(typeof(Domain.Model.ELead), status))
            throw new ArgumentException("Status inválido");

        var enumStatus = (Domain.Model.ELead)status;
        var collection = _dataBase.GetCollection<LeadModel>(CollectionsGroupMessages);


        return (int)await collection.CountDocumentsAsync(x => x.Status == enumStatus);
    }

}