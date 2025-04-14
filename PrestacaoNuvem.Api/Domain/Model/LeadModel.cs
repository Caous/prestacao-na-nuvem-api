﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace PrestacaoNuvem.Api.Domain.Model;

public class LeadModel
{
    [BsonElement("_id")]
    [JsonProperty("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Category { get; set; }
    public string Address { get; set; }
    public string TimeOpen { get; set; }
    public string Star { get; set; }
    public string WebSite { get; internal set; }
    public string Email { get; internal set; }
    public string RedeSocial { get; set; }
    public ICollection<HistoricoLead>? Historico { get; set; }
    public string? Observacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? NameRepresentation { get; set; }
    public string? CNPJ { get; set; }
    public ELead Status { get; set; }
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
