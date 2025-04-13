using PrestacaoNuvem.Api.Domain.Interfacesk;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace PrestacaoNuvem.Api.Domain.Services
{
    public class EmailManager : IEmailManager
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> PostPropostaEmailAsync(EmailRequestPropostaDto request)
        {

            int configConta = int.Parse(request.BoxTo);
            Email emailConfig = new Email();
            ConfigEmailSender(emailConfig, configConta);

            if (!ValidationEmails(emailConfig.ConfigHost.UserName, request.To.Split(";")))
                return false;

            request.Content = GenerateContentProposta(request, emailConfig);

            if (request.Content.IsMissing())
                return false;

            if (request.Subject.IsMissing())
                return false;
            try
            {
                foreach (var item in request.To.Split(";"))
                {

                    using (MailMessage mensagemEmail = new MailMessage(emailConfig.ConfigHost.UserName, item, request.Subject, request.Content))
                    {
                        mensagemEmail.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient(emailConfig.ConfigHost.Host, emailConfig.ConfigHost.Port);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(emailConfig.ConfigHost.UserName, emailConfig.ConfigHost.Password);
                        smtpClient.Timeout = 30000;
                        smtpClient.Send(mensagemEmail);
                    }

                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private string? GenerateContentProposta(EmailRequestPropostaDto request, Email emailConfig)
        {

            var linhasServicos = "";
            int contador = 1;
            var preco = request.OrdemServico.PrecoOrdem.Value;
            var cultura = new CultureInfo("pt-BR");
            var precoFormatado = preco.ToString("C", cultura);
            foreach (var servico in request.OrdemServico.Servicos ?? Enumerable.Empty<ServicoDto>())
            {
                string valorFormatado = servico.Valor.ToString("C2", new CultureInfo("pt-BR"));
                linhasServicos += $@"
                                    <tr style=""border-bottom: 1px solid #ccc;"">
                                        <td style=""padding: 10px; white-space: nowrap;"">{contador}</td>
                                        <td style=""padding: 10px; white-space: nowrap;"">{servico.Descricao}</td>
                                        <td style=""padding: 10px; white-space: nowrap;"">{valorFormatado}</td>
                                        <td style=""padding: 10px; white-space: nowrap;"">{valorFormatado}</td>
                                    </tr>";
                contador++;
            }

            string message = $@"<!DOCTYPE html>
                            <html lang=""pt-BR"">
                            <head>
                                <meta charset=""UTF-8"" />
                                <title>Proposta InnovaSfera</title>
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                            </head>

                            <body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #fffef9;"">
                                <div style=""background-color: #0c1c33; color: white; padding: 20px 20px 60px 20px;"">
                                    <div style=""max-width: 700px; margin: 0 auto;"">
                                        <img src=""https://www.innovasfera.com.br/logo-large.svg"" alt=""Logo InnovaSfera"" width=""120""
                                            style=""display: block; margin-bottom: 15px;"" />
                                        <div style=""text-align: left;"">
                                            <h1 style=""color: #9ae600; font-size: 26px; margin: 10px 0; font-weight: bold;"">
                                                Sua proposta Innovadora chegou!
                                            </h1>
                                            <p style=""font-size: 16px; margin: 0 auto; max-width: 90%; color: white;"">
                                                {request.MessageBox}
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div style=""
                                max-width: 700px;
                                width: 94%;
                                margin: 0 auto;
                                margin-top: -30px;
                                background-color: #fffef9;
                                box-shadow: 0 12px 30px rgba(0, 0, 0, 0.25);
                                border-radius: 8px;
                                overflow: hidden;
                                padding-bottom: 40px;
                                position: relative;
                                z-index: 2;"">
                                    <div
                                        style=""background-color: #0c1c33; color: white; padding: 20px; display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap;"">
                                        <img src=""https://www.innovasfera.com.br/logo-large.svg"" alt=""Logo""
                                            style=""width: 120px; height: auto; margin-bottom: 10px;"" />
                                        <div style=""color: #9ae600; font-size: 18px; font-weight: 600;"">PROPOSTA</div>
                                    </div>
                                    <div
                                        style=""background-color: #bef264; padding: 16px; display: flex; justify-content: space-between; flex-wrap: wrap; font-weight: bold;"">
                                        <div style=""width: 100%; max-width: 48%;"">Aos Cuidados:<br /> {request.OrdemServico.Cliente.Nome ?? request.OrdemServico.Cliente.NomeRepresentante}</div>
                                        <div style=""width: 100%; max-width: 48%; text-align: right;"">Proposta: #{request.OrdemServico.Referencia}<br /> Data:
                                            {request.OrdemServico.DataCadastro.Value.ToString("dd/MM/yyyy")}</div>
                                    </div>
                                    <div style=""padding: 20px;"">
                                        <div style=""overflow-x: auto; width: 100%;"">
                                            <table style=""min-width: 500px; width: 100%; border-collapse: collapse; margin-top: 20px;"">
                                                <thead>
                                                    <tr style=""background-color: #0c1c33; color: white;"">
                                                        <th style=""padding: 10px; text-align: left; white-space: nowrap;"">Item</th>
                                                        <th style=""padding: 10px; text-align: left; white-space: nowrap;"">Serviço</th>
                                                        <th style=""padding: 10px; text-align: left; white-space: nowrap;"">Preço</th>
                                                        <th style=""padding: 10px; text-align: left; white-space: nowrap;"">Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody style=""color: #374151;"">
                                                    {linhasServicos}
                                                </tbody>
                                            </table>
                                        </div>
                                        <div
                                            style=""display: flex; justify-content: space-between; flex-wrap: wrap; margin-top: 24px; font-weight: 500;"">
                                            <div style=""width: 100%; max-width: 50%; margin-bottom: 10px;"">
                                                Estamos super animados em realizar este projeto!
                                            </div>
                                            <div style=""width: 100%; max-width: 45%; text-align: right;"">
                                                <p>Subtotal: {precoFormatado}</p>
                                                <p>Desconto: {request.OrdemServico.DescontoPercentual}%</p>
                                                <p style=""font-weight: bold;"">Total: {precoFormatado}</p>
                                            </div>
                                        </div>
                                        <div style=""margin-top: 24px; font-size: 14px;"">
                                            <p style=""font-weight: bold;"">Forma de pagamento</p>
                                            <p>{ObterFormaPagamentoFormatada(request.OrdemServico.FormaPagamento)}</p>
                                        </div>
                                        <div style=""margin-top: 32px; font-size: 14px;"">
                                            <p style=""font-weight: bold;"">Termos e Condições</p>
                                            <p>
                                                Esta proposta está sujeita à aprovação final. Os valores podem variar de acordo com a demanda
                                                adicional.
                                                Prazo de execução e forma de entrega a serem combinados com o cliente.
                                            </p>
                                        </div>
                                        <div style=""margin-top: 40px; text-align: right; font-weight: 500;"">
                                            {ObterNomeEmail(int.Parse(request.BoxTo))}

                                        </div>
                                    </div>
                                </div>
                            </body>
                            </html>";


            return message;

        }

        public async Task<bool> SendEmailSmtpAsync(Email emailConfig)
        {
            if (!ValidationEmails(emailConfig.FromEmail, emailConfig.ToEmail))
                return false;

            if (emailConfig.Menssage.IsMissing())
                return false;

            if (emailConfig.Subject.IsMissing())
                return false;
            try
            {
                foreach (var item in emailConfig.ToEmail)
                {

                    using (MailMessage mensagemEmail = new MailMessage(emailConfig.FromEmail, item, emailConfig.Subject, emailConfig.Menssage))
                    {
                        mensagemEmail.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient(emailConfig.ConfigHost.Host, emailConfig.ConfigHost.Port);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(emailConfig.ConfigHost.UserName, emailConfig.ConfigHost.Password);
                        smtpClient.Timeout = 30000;
                        smtpClient.Send(mensagemEmail);
                    }

                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool ValidationEmails(string fromEmail, string[] toEmail)
        {
            if (!Email.Validation(fromEmail))
                return false;
            for (int i = 0; i < toEmail.Length; i++)
                if (!Email.Validation(toEmail[i])) return false;

            return true;
        }

        private void ConfigEmailSender(Email request, int configConta)
        {
            switch (configConta)
            {
                case 0:
                    request.ConfigHost = new EmailConfigHost(
                    _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:Host"),
                    _configuration.GetValue<int>("EmailConfiguration:ComercialGeral:Port"),
                    _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:UserName"),
                   _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:Password"));
                    break;
                case 1:
                    request.ConfigHost = new EmailConfigHost(
                    _configuration.GetValue<string>("EmailConfiguration:FelipeEmail:Host"),
                    _configuration.GetValue<int>("EmailConfiguration:FelipeEmail:Port"),
                    _configuration.GetValue<string>("EmailConfiguration:FelipeEmail:UserName"),
                   _configuration.GetValue<string>("EmailConfiguration:FelipeEmail:Password"));
                    break;
                case 2:
                    request.ConfigHost = new EmailConfigHost(
                    _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:Host"),
                    _configuration.GetValue<int>("EmailConfiguration:ComercialGeral:Port"),
                    _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:UserName"),
                    _configuration.GetValue<string>("EmailConfiguration:ComercialGeral:Password"));
                    break;
                case 3:
                    request.ConfigHost = new EmailConfigHost(
                    _configuration.GetValue<string>("EmailConfiguration:GustavoEmail:Host"),
                    _configuration.GetValue<int>("EmailConfiguration:GustavoEmail:Port"),
                    _configuration.GetValue<string>("EmailConfiguration:GustavoEmail:UserName"),
                    _configuration.GetValue<string>("EmailConfiguration:GustavoEmail:Password"));
                    break;
                default:
                    break;
            }
        }

        public static string ObterFormaPagamentoFormatada(EFormaPagamento formaPagamento)
        {
            return formaPagamento switch
            {
                EFormaPagamento.AvistaPix => "À vista no Pix",
                EFormaPagamento.AvistaBoleto => "À vista no Boleto",
                EFormaPagamento.ParceladoBoleto => "Parcelado no Boleto",
                EFormaPagamento.CartaoCreditoAvista => "À vista no Cartão de Crédito",
                EFormaPagamento.CartaoCreditoParcelado => "Parcelado no Cartão de Crédito",
                EFormaPagamento.CartaoDebito => "No Cartão de Débito",
                _ => "Forma de pagamento não informada"
            };
        }

        public static string ObterNomeEmail(int caixaEmail)
        {
            switch (caixaEmail)
            {
                case 0:
                    return "Guilherm Oliveira,<br /> Gerente Comercial";
                case 1:
                    return "Felipe Santana,<br /> Gerente Operação";
                case 2:
                    return "Gerente comercial,<br /> Time Comercial";
                case 3:
                    return "Gustavo Nascimento,<br /> CEO";
                default:
                    return "Equipe InnovaSfera";
            }
        }

        public async Task<bool> PostPropostaEmailComAnexoAsync(EmailRequestContratoFileDto request, byte[] file)
        {
            int configConta = int.Parse(request.BoxTo);
            Email emailConfig = new Email();
            ConfigEmailSender(emailConfig, configConta);

            if (!ValidationEmails(emailConfig.ConfigHost.UserName, request.To.Split(";")))
                return false;

            request.Content = GenerateContentContratoPdf(request, emailConfig);

            if (request.Content.IsMissing() || request.Subject.IsMissing())
                return false;

            try
            {
                foreach (var item in request.To.Split(";"))
                {
                    using (var mensagemEmail = new MailMessage(emailConfig.ConfigHost.UserName, item, request.Subject, request.Content))
                    {
                        mensagemEmail.IsBodyHtml = true;

                        using var stream = new MemoryStream(file);
                        var anexo = new Attachment(stream, "Proposta.pdf", "application/pdf");
                        mensagemEmail.Attachments.Add(anexo);

                        var smtpClient = new SmtpClient(emailConfig.ConfigHost.Host, emailConfig.ConfigHost.Port)
                        {
                            EnableSsl = true,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(emailConfig.ConfigHost.UserName, emailConfig.ConfigHost.Password),
                            Timeout = 30000
                        };

                        smtpClient.Send(mensagemEmail);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private string? GenerateContentContratoPdf(EmailRequestContratoFileDto request, Email emailConfig)
        {

            string message = $@"<!DOCTYPE html>
                                        <html lang=""pt-BR"">

                                        <head>
                                            <meta charset=""UTF-8"" />
                                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                                            <title>InnovaSfera - Assinatura de Contrato</title>
                                        </head>

                                        <body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f9f1e4; color: #0f193a;"">
                                            <div style=""text-align: center; background-color: #0f193a; padding: 30px 20px;"">
                                                <img src=""https://www.innovasfera.com.br/logo-large.svg"" alt=""Foguete InnovaSfera""
                                                    style=""width: 100%; max-width: 480px;"" />
                                                <h2 style=""color: #fff; margin-top: 30px;"">Seu projeto está prestes a decolar!</h2>
                                            </div>
                                            <!-- Mensagem de boas-vindas -->
                                            <div style=""padding: 40px 20px; text-align: center;"">
                                                <h2 style=""font-size: 24px; color: #0f193a;"">Olá, {request.MessageBox}!</h2>
                                                <p style=""font-size: 16px; max-width: 600px; margin: 0 auto;"">
                                                    Seu contrato personalizado chegou com sucesso! 🎉<br />
                                                    Agora é hora de revisar, tirar dúvidas e assinar digitalmente com total segurança através do GOV.BR.
                                                </p>
                                            </div>
                                            <!-- Etapas de assinatura -->
                                            <div style=""padding: 60px 20px; max-width: 700px; margin: 0 auto;"">
                                                <h3 style=""font-size: 22px; font-weight: bold; text-align: center; margin-bottom: 40px;"">Passo a Passo para
                                                    Assinatura:</h3>

                                                <div style=""display: flex; align-items: flex-start; margin-bottom: 30px;"">
                                                    <div
                                                        style=""min-width: 40px; min-height: 40px; background-color: #a2ef1b; border-radius: 50%; color: #0f193a; display: flex; align-items: center; justify-content: center; font-weight: bold; margin-right: 16px;"">
                                                        1</div>
                                                    <div>
                                                        <strong>Revisar o contrato com atenção</strong><br />
                                                        Leia o documento e verifique se todas as informações estão corretas.
                                                    </div>
                                                </div>

                                                <div style=""display: flex; align-items: flex-start; margin-bottom: 30px;"">
                                                    <div
                                                        style=""min-width: 40px; min-height: 40px; background-color: #a2ef1b; border-radius: 50%; color: #0f193a; display: flex; align-items: center; justify-content: center; font-weight: bold; margin-right: 16px;"">
                                                        2</div>
                                                    <div>
                                                        <strong>Tire dúvidas, se necessário</strong><br />
                                                        Nossa equipe está à disposição para responder qualquer pergunta.
                                                    </div>
                                                </div>

                                                <div style=""display: flex; align-items: flex-start; margin-bottom: 30px;"">
                                                    <div
                                                        style=""min-width: 40px; min-height: 40px; background-color: #a2ef1b; border-radius: 50%; color: #0f193a; display: flex; align-items: center; justify-content: center; font-weight: bold; margin-right: 16px;"">
                                                        3</div>
                                                    <div>
                                                        <strong>Acesse o portal de assinatura Gov.BR</strong><br />
                                                        Clique no link abaixo para iniciar a assinatura digital.
                                                    </div>
                                                </div>

                                                <div style=""display: flex; align-items: flex-start; margin-bottom: 30px;"">
                                                    <div
                                                        style=""min-width: 40px; min-height: 40px; background-color: #a2ef1b; border-radius: 50%; color: #0f193a; display: flex; align-items: center; justify-content: center; font-weight: bold; margin-right: 16px;"">
                                                        4</div>
                                                    <div>
                                                        <strong>Finalize a assinatura:</strong><br />
                                                        Ao acessar o portal, faça login com sua conta GOV.BR, envie o contrato, clique em <em>“Avançar”</em> e
                                                        posicione sua assinatura sobre seu nome no documento.
                                                    </div>
                                                </div>

                                                <div style=""text-align: center; margin-top: 40px;"">
                                                    <a href=""https://assinador.iti.br/assinatura/index.xhtml"" target=""_blank""
                                                        style=""display: inline-block; background-color: #a2ef1b; color: #0f193a; padding: 14px 30px; border-radius: 30px; font-weight: bold; text-decoration: none;"">
                                                        Assinar contrato agora
                                                    </a>
                                                </div>
                                            </div>

                                            <!-- Rodapé -->
                                            <div style=""text-align: center; padding: 30px; background-color: #0f193a; color: #f9f1e4;"">
                                                <p style=""margin: 0; font-size: 14px;"">InnovaSfera © 2025 - Todos os direitos reservados</p>
                                            </div>

                                        </body>

                                        </html>";


            return message;

        }
    }
}
