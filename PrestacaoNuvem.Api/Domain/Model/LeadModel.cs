using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace PrestacaoNuvem.Api.Domain.Model;

public class LeadModel
{
    [BsonElement("_id")]
    [JsonProperty("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TimeOpen { get; set; } = string.Empty;
    public string Star { get; set; } = string.Empty;
    public string WebSite { get; internal set; } = string.Empty;
    public string Email { get; internal set; } = string.Empty;
    public string RedeSocial { get; set; } = string.Empty;
    public ICollection<HistoricoLead>? Historico { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? NameRepresentation { get; set; }
    public string? CNPJ { get; set; }
    public ELead? Status { get; set; }
    public EPlataformaRequest? Plataforma { get; set; }
}

public enum ELead
{
    Novo,
    BuscandoInfo,
    Prospeccao,
    AguardandoDecisao,
    Negociacao,
    Reuniao,
    Cancelado,
    Requalificacao,
    Qualificado,
    Convertido
}

public class HistoricoLead
{
    public string? Assunto { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

}
