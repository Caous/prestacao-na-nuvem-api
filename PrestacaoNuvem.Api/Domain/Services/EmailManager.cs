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
                                        <td style=""padding: 10px; white-space: nowrap;"">-</td>
                                        <td style=""padding: 10px; white-space: nowrap;"">-</td>
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
                                        <img src=""data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTY4IiBoZWlnaHQ9IjQ4IiB2aWV3Qm94PSIwIDAgMTY4IDQ4IiBmaWxsPSJub25lIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPgo8cGF0aCBkPSJNMTQwLjYxOSAxNi4zNDY3QzE0MS42ODQgMTcuMzM5OSAxNDIuMzI2IDE4Ljc5NDggMTQyLjQ0MiAyMC4yNDM0QzE0Mi40NDggMjAuNDQ3NCAxNDIuNDUgMjAuNjUwOSAxNDIuNDUgMjAuODU1QzE0Mi40NTEgMjAuOTMxNCAxNDIuNDUxIDIxLjAwNzggMTQyLjQ1MiAyMS4wODY1QzE0Mi40NTMgMjEuMjUwNyAxNDIuNDU0IDIxLjQxNDkgMTQyLjQ1NCAyMS41NzkxQzE0Mi40NTUgMjEuODM5NCAxNDIuNDU2IDIyLjA5OTYgMTQyLjQ1OCAyMi4zNTk4QzE0Mi40NjMgMjMuMDA1NiAxNDIuNDY2IDIzLjY1MTMgMTQyLjQ2OSAyNC4yOTcxQzE0Mi40NzIgMjQuODQ0MiAxNDIuNDc1IDI1LjM5MTQgMTQyLjQ3OSAyNS45Mzg2QzE0Mi40ODEgMjYuMTk0MyAxNDIuNDgyIDI2LjQ1MDEgMTQyLjQ4MiAyNi43MDU5QzE0Mi40ODMgMjYuODYzMSAxNDIuNDg1IDI3LjAyMDQgMTQyLjQ4NiAyNy4xNzc3QzE0Mi40ODYgMjcuMjQ4MyAxNDIuNDg2IDI3LjMxODkgMTQyLjQ4NiAyNy4zOTE2QzE0Mi40OSAyNy44MDcgMTQyLjUzIDI4LjE1NzQgMTQyLjY3OCAyOC41NDc0QzE0My4yMTEgMjguOTAyNiAxNDMuNjkgMjguODMyNiAxNDQuMzYxIDI4Ljg4NDJDMTQ0LjM2MSAyOS45OTU4IDE0NC4zNjEgMzEuMTA3NCAxNDQuMzYxIDMyLjI1MjdDMTQzLjY2MyAzMi4yNjMxIDE0My42NjMgMzIuMjYzMSAxNDIuOTUyIDMyLjI3MzdDMTQyLjgwNyAzMi4yNzY4IDE0Mi42NjMgMzIuMjc5OSAxNDIuNTE0IDMyLjI4M0MxNDEuNDIxIDMyLjI5NjcgMTQwLjU1NiAzMi4xNTUxIDEzOS43NDUgMzEuMzYzOEMxMzkuMyAzMC44NTI3IDEzOS4wNjQgMzAuMzM3NSAxMzkuMDM0IDI5LjY2MTlDMTM5LjAyOSAyOS41NDk1IDEzOS4wMjkgMjkuNTQ5NSAxMzkuMDIzIDI5LjQzNDlDMTM5LjAyIDI5LjM1NzQgMTM5LjAxNyAyOS4yNzk5IDEzOS4wMTMgMjkuMkMxMzkuMDEgMjkuMTIxMiAxMzkuMDA2IDI5LjA0MjQgMTM5LjAwMiAyOC45NjEyQzEzOC45OTQgMjguNzY3MSAxMzguOTg1IDI4LjU3MzEgMTM4Ljk3NyAyOC4zNzlDMTM4Ljk0NSAyOC40NDAyIDEzOC45MTMgMjguNTAxNCAxMzguODggMjguNTY0NUMxMzcuOTU4IDMwLjI1MDcgMTM2LjU0MiAzMS4yNjM1IDEzNC43MTggMzEuODUyN0MxMzIuMjc0IDMyLjU1MjcgMTI5LjYwMiAzMi4zNTg0IDEyNy4zNSAzMS4xNTlDMTI3LjAxMyAzMC45Njg5IDEyNi43MjYgMzAuNzQ1OCAxMjYuNDQyIDMwLjQ4NDJDMTI2LjM3OCAzMC40MzAyIDEyNi4zMTUgMzAuMzc2MSAxMjYuMjQ5IDMwLjMyMDRDMTI1LjU0OCAyOS42ODQyIDEyNC45OTcgMjguNzI2OCAxMjQuOTExIDI3Ljc2OTlDMTI0LjkwMiAyNy41NjAzIDEyNC45IDI3LjM1MTkgMTI0LjkwMSAyNy4xNDIxQzEyNC45MDIgMjcuMDY3NyAxMjQuOTAyIDI2Ljk5MzIgMTI0LjkwMyAyNi45MTY1QzEyNC45MjQgMjUuNzY0NyAxMjUuMjYyIDI0LjgyOTYgMTI2LjA0NCAyMy45NzA0QzEyOC4wOSAyMi4wMjMyIDEzMS40OTQgMjIuMDgzNSAxMzQuMTM0IDIyLjAyMTFDMTM0Ljg4MiAyMi4wMDM0IDEzNS42MjkgMjEuOTgyMyAxMzYuMzc2IDIxLjk0MTJDMTM2LjQzMyAyMS45MzgyIDEzNi40ODkgMjEuOTM1MyAxMzYuNTQ4IDIxLjkzMjNDMTM3LjM0MiAyMS44ODU0IDEzOC4xNjkgMjEuODEzOSAxMzguODA4IDIxLjMwNTNDMTM5LjA0IDIwLjk4IDEzOC45ODIgMjAuNjgxNSAxMzguOTM3IDIwLjMwMDdDMTM4Ljc4NyAxOS40MzQxIDEzOC40ODkgMTguNjcyNSAxMzcuNzc1IDE4LjEyNjdDMTM2LjYyMyAxNy4zOTMyIDEzNS4zNzQgMTcuMjMyMyAxMzQuMDM0IDE3LjIzNjZDMTMzLjg2MiAxNy4yMzY5IDEzMy42OSAxNy4yMzQ5IDEzMy41MTcgMTcuMjMyOEMxMzIuMDgxIDE3LjIyNTkgMTMwLjYzMyAxNy40NjI5IDEyOS41NjUgMTguNDk5QzEyOC45OTggMTkuMTE5NyAxMjguNzU2IDE5LjgwOTQgMTI4LjYyOSAyMC42MzE2QzEyNy45MzQgMjAuNTkzNSAxMjcuMjQ3IDIwLjUxNDggMTI2LjU1OCAyMC40MjExQzEyNi40NzYgMjAuNDEwMSAxMjYuMzk0IDIwLjM5OTEgMTI2LjMxIDIwLjM4NzhDMTI2LjE5NCAyMC4zNzE3IDEyNi4xOTQgMjAuMzcxNyAxMjYuMDc2IDIwLjM1NTNDMTI2LjAwNyAyMC4zNDU3IDEyNS45MzcgMjAuMzM2MSAxMjUuODY2IDIwLjMyNjJDMTI1LjY4NSAyMC4yOTQ4IDEyNS42ODUgMjAuMjk0OCAxMjUuNDMyIDIwLjIxMDZDMTI1LjQ2OSAxOC43NzQgMTI2LjEyMiAxNy41MTAyIDEyNy4wODkgMTYuNDc5QzEyOC40MTQgMTUuMjI2MSAxMzAuMjA3IDE0LjU1NTkgMTMxLjk5NCAxNC4zMTU4QzEzMi4wOTUgMTQuMzAxOCAxMzIuMDk1IDE0LjMwMTggMTMyLjE5NyAxNC4yODc1QzEzNS4xMTUgMTMuOTUzNCAxMzguMzIxIDE0LjQxNjYgMTQwLjYxOSAxNi4zNDY3Wk0xMzguNTMgMjMuODE1OEMxMzcuMDYyIDI0LjM1MSAxMzUuNDAzIDI0LjMwMyAxMzMuODYzIDI0LjM0MzlDMTMxLjE3MiAyNC4zNTU5IDEzMS4xNzIgMjQuMzU1OSAxMjguODg0IDI1LjU3NkMxMjguNDQ4IDI2LjA2NjYgMTI4LjM2NCAyNi42NDUxIDEyOC4zNzcgMjcuMjg0MkMxMjguNDc4IDI4LjAxNDMgMTI4LjgzMyAyOC40ODA1IDEyOS4zODkgMjguOTQyMUMxMzAuNzk4IDI5Ljk2NTcgMTMyLjY5MiAyOS45MTU5IDEzNC4zMzcgMjkuNjcyQzEzNS45MDEgMjkuNDEzMiAxMzcuNTQ5IDI4Ljc3NTYgMTM4LjUyNCAyNy40NzM3QzEzOC45NTkgMjYuODQ1NSAxMzkuMDcyIDI2LjI4MTIgMTM5LjA2OSAyNS41MjE3QzEzOS4wNjkgMjUuNDEzMiAxMzkuMDY4IDI1LjMwNDYgMTM5LjA2OCAyNS4xOTI4QzEzOS4wNjcgMjUuMDc5NCAxMzkuMDY3IDI0Ljk2NiAxMzkuMDY2IDI0Ljg1MjdDMTM5LjA2NiAyNC43MzczIDEzOS4wNjUgMjQuNjIyIDEzOS4wNjUgMjQuNTA2NkMxMzkuMDY0IDI0LjIyNTUgMTM5LjA2MyAyMy45NDQzIDEzOS4wNjEgMjMuNjYzMkMxMzguODUgMjMuNjYzMiAxMzguNzI0IDIzLjczMjkgMTM4LjUzIDIzLjgxNThaIiBmaWxsPSIjRjlGMUU0Ii8+CjxwYXRoIGQ9Ik00Mi44NDEgMTUuMjEzMkM0NC42OTcyIDE2Ljc5NDYgNDUuMTgyIDE5LjMzNDYgNDUuMzczOCAyMS42NDU1QzQ1LjQ1MzkgMjIuNzAyNyA0NS40NDkyIDIzLjc1ODkgNDUuNDQ0NiAyNC44MTg1QzQ1LjQ0NDMgMjUuMDM0NiA0NS40NDQxIDI1LjI1MDcgNDUuNDQzOSAyNS40NjY4QzQ1LjQ0MzMgMjUuOTcxNCA0NS40NDIgMjYuNDc1OSA0NS40NDAzIDI2Ljk4MDVDNDUuNDM4NCAyNy41NTY0IDQ1LjQzNzUgMjguMTMyNCA0NS40MzY3IDI4LjcwODRDNDUuNDM0OSAyOS44ODk4IDQ1LjQzMTggMzEuMDcxMiA0NS40MjgyIDMyLjI1MjdDNDQuMjM0NCAzMi4yNTI3IDQzLjA0MDcgMzIuMjUyNyA0MS44MTA4IDMyLjI1MjdDNDEuODA4OSAzMS43OTcxIDQxLjgwODkgMzEuNzk3MSA0MS44MDcgMzEuMzMyM0M0MS44MDI4IDMwLjMyNDMgNDEuNzk3MiAyOS4zMTYzIDQxLjc5MDkgMjguMzA4M0M0MS43ODcyIDI3LjY5NzYgNDEuNzgzOCAyNy4wODcgNDEuNzgxNiAyNi40NzY0QzQxLjc3OTcgMjUuOTQzNCA0MS43NzY4IDI1LjQxMDUgNDEuNzczIDI0Ljg3NzZDNDEuNzcxIDI0LjU5NjEgNDEuNzY5NiAyNC4zMTQ1IDQxLjc2ODkgMjQuMDMzQzQxLjc3MjMgMjEuMTM5NiA0MS43NzIzIDIxLjEzOTYgNDAuNDgyNSAxOC42MTgxQzM5LjY5ODQgMTcuODc2MiAzOC44NjA1IDE3LjcxOTcgMzcuODI1IDE3Ljc0MThDMzcuMjk3IDE3Ljc2NzcgMzYuODI5OCAxNy45MDQgMzYuMzQyNiAxOC4xMDUzQzM2LjIzMTUgMTguMTUxIDM2LjIzMTUgMTguMTUxIDM2LjExODEgMTguMTk3N0MzNC43NDIzIDE4LjgyNzYgMzMuODQ5OSAxOS45ODgzIDMzLjI3MSAyMS4zNTIzQzMyLjg0ODYgMjIuNDkxMSAzMi42MzE2IDIzLjYyMjkgMzIuNjIzNyAyNC44MzQxQzMyLjYyMjkgMjQuOTE0NyAzMi42MjIxIDI0Ljk5NTMgMzIuNjIxMiAyNS4wNzgzQzMyLjYxODYgMjUuMzQyNiAzMi42MTY1IDI1LjYwNjggMzIuNjE0NCAyNS44NzExQzMyLjYxMjcgMjYuMDU1MSAzMi42MTA5IDI2LjIzOTIgMzIuNjA5MSAyNi40MjMzQzMyLjYwNDUgMjYuOTA2MSAzMi42MDAzIDI3LjM4OSAzMi41OTYyIDI3Ljg3MTlDMzIuNTkxMiAyOC40NTIxIDMyLjU4NTcgMjkuMDMyMyAzMi41ODAyIDI5LjYxMjVDMzIuNTcyIDMwLjQ5MjYgMzIuNTY0NSAzMS4zNzI2IDMyLjU1NjkgMzIuMjUyN0MzMS4zNjMxIDMyLjI1MjcgMzAuMTY5NCAzMi4yNTI3IDI4LjkzOTUgMzIuMjUyN0MyOC45Mzk1IDI2LjM2MTMgMjguOTM5NSAyMC40Njk5IDI4LjkzOTUgMTQuNEMzMC4xMzMyIDE0LjQgMzEuMzI3IDE0LjQgMzIuNTU2OSAxNC40QzMyLjU1NjkgMTUuOTAwNyAzMi41NTY5IDE3LjQwMTMgMzIuNTU2OSAxOC45NDc0QzMyLjY5NTcgMTguNjEzOSAzMi44MzQ1IDE4LjI4MDUgMzIuOTc3NSAxNy45MzY5QzMzLjkyNDQgMTYuMjM3OCAzNS40ODE3IDE0Ljg3NzUgMzcuMzUyMSAxNC4zMTU4QzM5LjI3MTIgMTMuODI3NyA0MS4yNDE5IDEzLjk1MzkgNDIuODQxIDE1LjIxMzJaTTMyLjM4ODYgMTkuMzY4NUMzMi40NzI4IDE5LjUzNjkgMzIuNDcyOCAxOS41MzY5IDMyLjQ3MjggMTkuNTM2OUwzMi4zODg2IDE5LjM2ODVaIiBmaWxsPSIjRjlGMUU0Ii8+CjxwYXRoIGQ9Ik02MS4yNTMgMTUuMTk1NEM2MS42NDA3IDE1LjUzOCA2MS45NTI4IDE1LjkxNzEgNjIuMjUzNiAxNi4zMzY5QzYyLjMyMTkgMTYuNDMwOCA2Mi4zMjE5IDE2LjQzMDggNjIuMzkxNiAxNi41MjY3QzYzLjk4MjUgMTguOTM2NCA2My44ODA4IDIyLjIwODIgNjMuODY4NCAyNC45NjY1QzYzLjg2ODEgMjUuMTc4NiA2My44Njc5IDI1LjM5MDcgNjMuODY3NyAyNS42MDI4QzYzLjg2NzEgMjYuMTUyOSA2My44NjU0IDI2LjcwMzEgNjMuODYzNiAyNy4yNTMyQzYzLjg2MTggMjcuODE3OCA2My44NjExIDI4LjM4MjQgNjMuODYwMiAyOC45NDcxQzYzLjg1ODQgMzAuMDQ4OSA2My44NTU1IDMxLjE1MDggNjMuODUyIDMyLjI1MjdDNjIuNjU4MyAzMi4yNTI3IDYxLjQ2NDUgMzIuMjUyNyA2MC4yMzQ2IDMyLjI1MjdDNjAuMjMyNyAzMS44MDA5IDYwLjIzMjcgMzEuODAwOSA2MC4yMzA5IDMxLjM0QzYwLjIyNjYgMzAuMzQwNCA2MC4yMjEgMjkuMzQwOCA2MC4yMTQ4IDI4LjM0MTJDNjAuMjExIDI3LjczNTcgNjAuMjA3NyAyNy4xMzAxIDYwLjIwNTQgMjYuNTI0NkM2MC4yMDM1IDI1Ljk5NjEgNjAuMjAwNyAyNS40Njc3IDYwLjE5NjggMjQuOTM5MkM2MC4xOTQ4IDI0LjY2IDYwLjE5MzQgMjQuMzgwOCA2MC4xOTI3IDI0LjEwMTZDNjAuMTkxOSAyMS4xMDM2IDYwLjE5MTkgMjEuMTAzNiA1OC44MDQ0IDE4LjUyNjNDNTguMTcwNiAxNy45ODUgNTcuNDc4IDE3LjcyMDEgNTYuNjQ4NyAxNy43MzE2QzU2LjU4MjkgMTcuNzMyMyA1Ni41MTcxIDE3LjczMyA1Ni40NDk0IDE3LjczMzdDNTUuMTIzOSAxNy43NzE3IDUzLjk4NTggMTguMzI5MiA1My4wMzEzIDE5LjIzNzJDNTAuNjg3OSAyMS43NjQzIDUwLjk1NTMgMjUuMzgyMSA1MC45MzAyIDI4LjU4NTNDNTAuOTI2MSAyOS4wNzMxIDUwLjkyMSAyOS41NjA5IDUwLjkxNiAzMC4wNDg2QzUwLjkwODcgMzAuNzgzMyA1MC45MDI4IDMxLjUxOCA1MC44OTY2IDMyLjI1MjdDNDkuNzMwNiAzMi4yNTI3IDQ4LjU2NDYgMzIuMjUyNyA0Ny4zNjMzIDMyLjI1MjdDNDcuMzYzMyAyNi4zNjEzIDQ3LjM2MzMgMjAuNDY5OSA0Ny4zNjMzIDE0LjRDNDguNTI5MyAxNC40IDQ5LjY5NTMgMTQuNCA1MC44OTY2IDE0LjRDNTAuOTI0MyAxNS44NzI5IDUwLjk1MjEgMTcuMzQ1NyA1MC45ODA3IDE4Ljg2MzJDNTEuMjAyOCAxOC40MTg2IDUxLjQyNDkgMTcuOTczOSA1MS42NTM3IDE3LjUxNThDNTIuODM2NCAxNS43ODMgNTQuMzcwNCAxNC41NzA4IDU2LjQ0MzcgMTQuMTU3OUM1OC4xNTQ4IDEzLjg1NzkgNTkuODY2MiAxNC4xMDg0IDYxLjI1MyAxNS4xOTU0Wk01MC44MTI1IDE5LjI4NDJDNTAuODk2NiAxOS40NTI3IDUwLjg5NjYgMTkuNDUyNyA1MC44OTY2IDE5LjQ1MjdMNTAuODEyNSAxOS4yODQyWiIgZmlsbD0iI0Y5RjFFNCIvPgo8cGF0aCBkPSJNODMuNjIzOSAxNS43NDIyQzg2LjUxMDMgMTUuNjY2NyA4OS4zMjE3IDE2LjMxMDQgOTEuNDk0OSAxOC4zMDUzQzkyLjkwNzUgMTkuNjY2MiA5My44MDM0IDIxLjU3NTcgOTMuODk5OCAyMy41NDY1QzkzLjk0NjUgMjYuNDc1OSA5My45NDY1IDI2LjQ3NTkgOTMuNTQ4NCAyNy43MDUzQzkzLjUyMiAyNy43ODg1IDkzLjUyMiAyNy43ODg1IDkzLjQ5NTEgMjcuODczNUM5Mi44MjQ2IDI5LjkxMTYgOTEuMzMyOSAzMS40OTA5IDg5LjQ0ODMgMzIuNDY2MkM4Ny44Mjg2IDMzLjI3MzUgODYuMTMzMiAzMy41MzU1IDg0LjM0MTkgMzMuNTM2OUM4NC4yNzc3IDMzLjUzNjkgODQuMjEzNSAzMy41MzcgODQuMTQ3NCAzMy41MzcxQzgzLjIxOTIgMzMuNTM1NyA4Mi4zMzUxIDMzLjUwODIgODEuNDM0MiAzMy4yNjMyQzgxLjM3NzQgMzMuMjQ3OCA4MS4zMjA1IDMzLjIzMjUgODEuMjYxOSAzMy4yMTY3QzgwLjUzMzggMzMuMDE2MSA3OS44Mzc4IDMyLjc2MjUgNzkuMTYyOCAzMi40MjExQzc5LjA4NyAzMi4zODQxIDc5LjAxMTIgMzIuMzQ3MSA3OC45MzMxIDMyLjMwODlDNzguMzI3MSAzMS45OTgzIDc3LjgyMDEgMzEuNjA5OCA3Ny4zMTIxIDMxLjE1NzlDNzcuMjUyMyAzMS4xMDUyIDc3LjE5MjYgMzEuMDUyNCA3Ny4xMzEgMzAuOTk4MUM3NS43MTMxIDI5LjY2NTkgNzQuODcwMiAyNy42NTM5IDc0Ljc3MzQgMjUuNzI0NUM3NC43MjY0IDIyLjg3NjcgNzQuNzI2NCAyMi44NzY3IDc1LjEyNDggMjEuNjQyMUM3NS4xNTE4IDIxLjU1OCA3NS4xNTE4IDIxLjU1OCA3NS4xNzk0IDIxLjQ3MjJDNzUuODQ4MiAxOS40Nzg2IDc3LjIyMjYgMTcuOTA0OSA3OS4wNDc4IDE2Ljg5NDRDODAuNDc1MSAxNi4yMDY2IDgyLjAzODQgMTUuNzkwNyA4My42MjM5IDE1Ljc0MjJaTTgyLjg1MDYgMTYuNzQ0NEM4Mi4zMTggMTcuMzcxNyA4Mi4xMjU3IDE4LjEzNjMgODEuOTAyMiAxOC45MTA2QzgxLjU0NTcgMjAuMTM5MSA4MC45OTM1IDIxLjEwMzQgNzkuODczNCAyMS43ODg0Qzc5LjM3NTEgMjIuMDQ1OSA3OC44MjQ2IDIyLjE5MDkgNzguMjkxNSAyMi4zNTk0Qzc3LjQ1MjUgMjIuNjM1NCA3Ni43MjM4IDIyLjk5NjMgNzYuMjE4NCAyMy43NDc0Qzc1Ljk3MjYgMjQuMjYyIDc1Ljk3MTggMjQuODMxIDc2LjE1MDEgMjUuMzY4NUM3Ni4yODI4IDI1LjY4MjggNzYuNDYzMSAyNS44ODIyIDc2LjcyMzIgMjYuMTA1M0M3Ni43ODIyIDI2LjE2MDkgNzYuODQxMiAyNi4yMTY1IDc2LjkwMTkgMjYuMjczN0M3Ny40Mjk2IDI2LjY0MTEgNzguMDE5IDI2Ljg1MTIgNzguNjI4MiAyNy4wNDI4Qzc5Ljg3MjcgMjcuNDM1MiA4MC44MDQ3IDI4LjAwNDQgODEuNDc4MSAyOS4xNDcxQzgxLjY5MDQgMjkuNTYwOCA4MS44MTUxIDI5Ljk5NjggODEuOTQ0MyAzMC40NDIxQzgyLjM2MjkgMzEuOTM2MiA4Mi4zNjI5IDMxLjkzNjIgODMuNDE2NSAzMi45OTQ4QzgzLjk2MTEgMzMuMjQ2OSA4NC40NTI1IDMzLjI4ODIgODUuMDI3IDMzLjEwNzNDODUuNzM0NyAzMi43OTUgODYuMTA0OSAzMi4yNjY0IDg2LjM5ODcgMzEuNTY4MUM4Ni41MSAzMS4yNDQ0IDg2LjU5NDQgMzAuOTE1MiA4Ni42ODE2IDMwLjU4NDJDODcuMDQwNiAyOS4yNDIyIDg3LjYzMTEgMjguMjU0NiA4OC44MjkyIDI3LjUwOTVDODkuMzQ3NSAyNy4yMjg5IDg5LjkwOTUgMjcuMDc1MSA5MC40NzA3IDI2LjkwM0M5MS4zMDM3IDI2LjYzNDEgOTIuMDI2NCAyNi4yODU3IDkyLjQ2ODIgMjUuNDk4N0M5Mi42ODUxIDI1LjAzNDQgOTIuNjk3IDI0LjU0MzggOTIuNTcxNCAyNC4wNDk3QzkyLjMxNDYgMjMuMzg5NSA5MS44NDA5IDIyLjk5ODIgOTEuMjEzOSAyMi42OTQ4QzkwLjkxMTUgMjIuNTY0OSA5MC42MDU3IDIyLjQ1NTggOTAuMjkzMSAyMi4zNTRDODguODg2MSAyMS44OTQgODcuODMgMjEuMzY1NSA4Ny4xMjI2IDE5Ljk4MzNDODYuOTIzMyAxOS41MzM1IDg2Ljc3NzMgMTkuMDcxNCA4Ni42MzU0IDE4LjYwMTNDODYuMzQ2NSAxNy42NDggODYuMDY4NSAxNi43NTI3IDg1LjEzNTggMTYuMjUyN0M4NC4yODM3IDE1LjkzMzIgODMuNDk4IDE2LjEyMDcgODIuODUwNiAxNi43NDQ0WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNMTA0LjgyMSAxNC40QzEwNi4wOTggMTQuNCAxMDcuMzc1IDE0LjQgMTA4LjY5MSAxNC40QzEwOS4xNjQgMTUuNTM2OSAxMDkuMTY0IDE1LjUzNjkgMTA5LjMzNSAxNS45OTIzQzEwOS4zNzMgMTYuMDk1NiAxMDkuNDEyIDE2LjE5ODkgMTA5LjQ1MiAxNi4zMDU0QzEwOS40OTMgMTYuNDE2IDEwOS41MzQgMTYuNTI2NiAxMDkuNTc2IDE2LjYzNzJDMTA5LjYyIDE2Ljc1NjMgMTA5LjY2NSAxNi44NzU0IDEwOS43MSAxNi45OTQ2QzEwOS44MjkgMTcuMzE0MiAxMDkuOTQ5IDE3LjYzMzggMTEwLjA2OCAxNy45NTM1QzExMC4xNjggMTguMjIxMyAxMTAuMjY4IDE4LjQ4OSAxMTAuMzY4IDE4Ljc1NjdDMTEwLjYyOCAxOS40NTI4IDExMC44ODkgMjAuMTQ4OSAxMTEuMTQ4IDIwLjg0NTJDMTExLjMyOCAyMS4zMjU4IDExMS41MDcgMjEuODA2NSAxMTEuNjg3IDIyLjI4NzFDMTExLjkyOCAyMi45MzEgMTEyLjE2OCAyMy41NzQ5IDExMi40MDkgMjQuMjE4OUMxMTIuNDk5IDI0LjQ2IDExMi41ODkgMjQuNzAxMiAxMTIuNjc5IDI0Ljk0MjRDMTEzLjQ3NiAyNy4wNzY5IDExMy40NzYgMjcuMDc2OSAxMTMuNzkgMjcuOTU1M0MxMTMuODI3IDI4LjA1ODkgMTEzLjg2NCAyOC4xNjI1IDExMy45MDIgMjguMjY5M0MxMTQuMDI4IDI4LjY2NDcgMTE0LjEwOCAyOS4wNjIyIDExNC4xNTkgMjkuNDczN0MxMTQuMTk0IDI5LjM1MTcgMTE0LjE5NCAyOS4zNTE3IDExNC4yMjkgMjkuMjI3MkMxMTQuMjYyIDI5LjExMjkgMTE0LjI5NSAyOC45OTg2IDExNC4zMjggMjguODg0MkMxMTQuMzQ0IDI4LjgyNTIgMTE0LjM2MSAyOC43NjYxIDExNC4zNzkgMjguNzA1M0MxMTQuNjY5IDI3LjY5NDEgMTE1IDI2LjcwNTQgMTE1LjM3MSAyNS43MjFDMTE1LjUyOCAyNS4zMDM3IDExNS42ODMgMjQuODg1NyAxMTUuODM5IDI0LjQ2NzhDMTE1Ljg3MiAyNC4zNzc4IDExNS45MDYgMjQuMjg3OCAxMTUuOTQgMjQuMTk1MUMxMTYuMjA5IDIzLjQ3MTcgMTE2LjQ3NSAyMi43NDcgMTE2LjczOSAyMi4wMjE3QzExNi43NjkgMjEuOTM4MiAxMTYuNzY5IDIxLjkzODIgMTE2LjggMjEuODUyOUMxMTYuOTA0IDIxLjU2ODcgMTE3LjAwNyAyMS4yODQ2IDExNy4xMTEgMjEuMDAwNEMxMTcuOTEyIDE4Ljc5NjcgMTE4LjcyMyAxNi41OTY5IDExOS41NDMgMTQuNEMxMjAuODIgMTQuNCAxMjIuMDk3IDE0LjQgMTIzLjQxMyAxNC40QzEyMi44MDkgMTYuMDUyOSAxMjIuMjAxIDE3LjcwMzggMTIxLjU4NSAxOS4zNTI1QzEyMS41MTkgMTkuNTMwMyAxMjEuNDUzIDE5LjcwODIgMTIxLjM4NiAxOS44ODYxQzEyMC42MTIgMjEuOTYyMyAxMTkuODM1IDI0LjAzNzUgMTE5LjA1NyAyNi4xMTI0QzExOC4yOSAyOC4xNTc4IDExNy41MjcgMzAuMjA0NiAxMTYuNzY3IDMyLjI1MjdDMTE1LjAxOCAzMi4yNTI3IDExMy4yNjkgMzIuMjUyNyAxMTEuNDY3IDMyLjI1MjdDMTEwLjY5NyAzMC4zMjQ0IDExMC42OTcgMzAuMzI0NCAxMTAuNDE5IDI5LjU1ODJDMTEwLjM1MiAyOS4zNzUxIDExMC4yODUgMjkuMTkyIDExMC4yMTggMjkuMDA4OUMxMTAuMTg0IDI4LjkxNDMgMTEwLjE0OSAyOC44MTk3IDExMC4xMTQgMjguNzIyMkMxMDkuOTIyIDI4LjE5NzggMTA5LjczIDI3LjY3MzcgMTA5LjUzNyAyNy4xNDk3QzEwOS41MTcgMjcuMDk0NSAxMDkuNDk3IDI3LjAzOTMgMTA5LjQ3NiAyNi45ODI1QzEwOC45NjEgMjUuNTgxOSAxMDguNDM0IDI0LjE4NTYgMTA3LjkwOCAyMi43ODk1QzEwNy44NjEgMjIuNjY0OCAxMDcuODE0IDIyLjU0MDEgMTA3Ljc2NyAyMi40MTU0QzEwNy43NDMgMjIuMzU0IDEwNy43MiAyMi4yOTI2IDEwNy42OTYgMjIuMjI5M0MxMDcuNTgyIDIxLjkyNSAxMDcuNDY3IDIxLjYyMDggMTA3LjM1MiAyMS4zMTY1QzEwNy4zMyAyMS4yNTc1IDEwNy4zMDggMjEuMTk4NiAxMDcuMjg1IDIxLjEzNzlDMTA2LjgyNiAxOS45MjE1IDEwNi4zNzIgMTguNzAzNCAxMDUuOTIxIDE3LjQ4NDFDMTA1Ljg3MyAxNy4zNTUyIDEwNS44MjUgMTcuMjI2MyAxMDUuNzc4IDE3LjA5NzRDMTA1Ljc1NSAxNy4wMzY1IDEwNS43MzMgMTYuOTc1NyAxMDUuNzA5IDE2LjkxM0MxMDUuNTc4IDE2LjU1OSAxMDUuNDQ0IDE2LjIwNiAxMDUuMzA5IDE1Ljg1MzZDMTA1LjI3OCAxNS43NzI0IDEwNS4yNDcgMTUuNjkxMiAxMDUuMjE1IDE1LjYwNzZDMTA1LjE1NCAxNS40NDkyIDEwNS4wOTMgMTUuMjkwOSAxMDUuMDMxIDE1LjEzMjdDMTA1LjAwNCAxNS4wNjE1IDEwNC45NzYgMTQuOTkwMyAxMDQuOTQ4IDE0LjkxN0MxMDQuOTI0IDE0Ljg1MzcgMTA0Ljg5OSAxNC43OTA0IDEwNC44NzQgMTQuNzI1MkMxMDQuODIxIDE0LjU2ODQgMTA0LjgyMSAxNC41Njg0IDEwNC44MjEgMTQuNFoiIGZpbGw9IiNGOUYxRTQiLz4KPHBhdGggZD0iTTIzLjMwMjcgNy4yNDIwN0MyNC40OTY1IDcuMjQyMDcgMjUuNjkwMiA3LjI0MjA3IDI2LjkyMDIgNy4yNDIwN0MyNi45MjAyIDE1LjQ5NTUgMjYuOTIwMiAyMy43NDkgMjYuOTIwMiAzMi4yNTI2QzI1LjcyNjQgMzIuMjUyNiAyNC41MzI3IDMyLjI1MjYgMjMuMzAyNyAzMi4yNTI2QzIzLjMwMjcgMjMuOTk5MSAyMy4zMDI3IDE1Ljc0NTYgMjMuMzAyNyA3LjI0MjA3WiIgZmlsbD0iI0Y5RjFFNCIvPgo8cGF0aCBkPSJNMTEyLjg3NiAzNC45NjMyQzExMy40MSAzNS40MDQ4IDExMy43OCAzNS45NDMzIDExMy45MDcgMzYuNjMxNkMxMTMuOTA3IDM2LjgyNjEgMTEzLjkwNyAzNy4wMjA3IDExMy45MDcgMzcuMjIxMUMxMTMuMjQxIDM3LjI0ODkgMTEyLjU3NCAzNy4yNzY3IDExMS44ODggMzcuMzA1M0MxMTEuODA1IDM3LjA4MyAxMTEuNzIxIDM2Ljg2MDcgMTExLjYzNiAzNi42MzE2QzExMC44OTUgMzYuMDQ0NSAxMDkuODM4IDM2LjA2MTEgMTA4LjkzNyAzNi4xNTI3QzEwOC40MDEgMzYuMjI4MiAxMDcuOTc3IDM2LjM0ODMgMTA3LjUxMyAzNi42MzE2QzEwNy4zMDMgMzYuOTQ3MSAxMDcuMjkzIDM3LjEwMDcgMTA3LjM0NSAzNy40NzM3QzEwNy43NzggMzcuODY3IDEwOC4yNjQgMzcuOTA3IDEwOC44MjggMzcuOTg0MkMxMDguOTIxIDM3Ljk5ODIgMTA5LjAxNCAzOC4wMTIyIDEwOS4xMSAzOC4wMjY1QzEwOS41NDYgMzguMDkxNiAxMDkuOTg0IDM4LjE1MTcgMTEwLjQyMSAzOC4yMDk5QzExMS43IDM4LjM4MzIgMTEzLjA3MiAzOC41OTU3IDExMy45NDQgMzkuNjQ3NEMxMTQuNDA0IDQwLjMyNSAxMTQuMzc3IDQxLjA2NjQgMTE0LjI0NCA0MS44NTI3QzExMy45ODQgNDIuNzMyMyAxMTMuNDMzIDQzLjM0NjQgMTEyLjY0NSA0My43ODk1QzExMi41ODkgNDMuODIxNSAxMTIuNTMzIDQzLjg1MzYgMTEyLjQ3NiA0My44ODY1QzExMC45OTEgNDQuNjM4MiAxMDguNzMzIDQ0LjYyNjIgMTA3LjE3NyA0NC4xMjYzQzEwNi4zNzcgNDMuODIxMSAxMDUuNzEyIDQzLjM4MjIgMTA1LjI5NSA0Mi42MTU4QzEwNS4wNzYgNDIuMTI1MSAxMDQuOTc4IDQxLjYzMDQgMTA0Ljk5IDQxLjA5NDhDMTA1LjY5MiA0MS4wMTQ0IDEwNi4zODYgNDEuMDAxMyAxMDcuMDkzIDQxLjAxMDZDMTA3LjEyNiA0MS4xMTQ4IDEwNy4xNTkgNDEuMjE5IDEwNy4xOTMgNDEuMzI2M0MxMDcuMzY3IDQxLjc4NTIgMTA3LjYxOSA0MS45NDkxIDEwOC4wNTUgNDIuMTYzMkMxMDkuMDY5IDQyLjUzNzMgMTEwLjM3MiA0Mi40NTI1IDExMS4zODMgNDIuMTA1M0MxMTEuNjkgNDEuOTU4NyAxMTEuODY1IDQxLjgwMzMgMTEyLjA1NiA0MS41MTU4QzExMi4xIDQxLjE3MzEgMTEyLjEgNDEuMTczMSAxMTIuMDU2IDQwLjg0MjFDMTExLjM2MyA0MC4xMjg4IDEwOS45MzcgNDAuMTUzIDEwOC45OTIgNDAuMDI5NEMxMDcuNzIxIDM5Ljg2MTYgMTA2LjI3NCAzOS42MjU4IDEwNS40MzIgMzguNTYzOEMxMDUuMDQgMzcuOTU2MyAxMDQuOTkyIDM3LjMxNzIgMTA1LjEyMyAzNi42MjI3QzEwNS4zMjkgMzUuODE1MSAxMDUuNzAzIDM1LjI0NjUgMTA2LjQwOSAzNC43OTIxQzEwOC4yNTYgMzMuODEyNCAxMTEuMTIgMzMuNzAwNSAxMTIuODc2IDM0Ljk2MzJaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik0xMjUuMTY0IDM3LjAyNjNDMTI1LjI1NCAzNy4wMjY4IDEyNS4zNDMgMzcuMDI3MyAxMjUuNDM1IDM3LjAyNzdDMTI2LjU3MiAzNy4wNDU0IDEyNy41OTQgMzcuMjkxOSAxMjguNDUgMzguMDg5NUMxMjkuMjc3IDM4Ljk5NDYgMTI5LjM4NiAzOS45Njk4IDEyOS4zODYgNDEuMTc4OUMxMjcuMjIxIDQxLjE3ODkgMTI1LjA1NSA0MS4xNzg5IDEyMi44MjQgNDEuMTc4OUMxMjMuMTk3IDQxLjkyNDkgMTIzLjQ5MSA0Mi4zMTUxIDEyNC4yNTUgNDIuNjEwNUMxMjUuMDEzIDQyLjgwNjkgMTI1Ljk4OSA0Mi44MTM1IDEyNi42OTQgNDIuNDQyMUMxMjcuMDUxIDQyLjIxMjQgMTI3LjIyOCA0MS45NzIxIDEyNy40NTEgNDEuNkMxMjguMDkgNDEuNjI3OCAxMjguNzI4IDQxLjY1NTYgMTI5LjM4NiA0MS42ODQyQzEyOS4yNjYgNDIuNTIzNiAxMjguOTYxIDQzLjE0NDggMTI4LjI4NiA0My42NzRDMTI2Ljk2NiA0NC41NDc4IDEyNS4xNiA0NC41ODQ3IDEyMy42NDUgNDQuMjg5NUMxMjIuNzIgNDQuMDIzOCAxMjEuODQxIDQzLjU1MDMgMTIxLjMzNyA0Mi43MTIyQzEyMC43NzggNDEuNjU2NCAxMjAuNjQ3IDQwLjY0NzYgMTIwLjk3MSAzOS40ODc4QzEyMS4zMTMgMzguNTE4OCAxMjEuOTUxIDM3Ljg4NSAxMjIuODY2IDM3LjQ0MjFDMTIzLjYyNSAzNy4wOTg5IDEyNC4zMzkgMzcuMDIxNyAxMjUuMTY0IDM3LjAyNjNaTTEyMy4zMjkgMzkuMjQyMUMxMjMuMDA1IDM5LjY4MDcgMTIzLjAwNSAzOS42ODA3IDEyMi45MzkgNDAuMTkyMUMxMjIuOTU3IDQwLjIzOTkgMTIyLjk3NSA0MC4yODc2IDEyMi45OTMgNDAuMzM2OEMxMjMuMDQxIDQwLjI5NjkgMTIzLjA5IDQwLjI1NjkgMTIzLjE0IDQwLjIxNThDMTIzLjQzIDQwLjAxMzggMTIzLjY5NSAzOS45ODk3IDEyNC4wNDQgMzkuOTgyN0MxMjQuMTU1IDM5Ljk4MDIgMTI0LjE1NSAzOS45ODAyIDEyNC4yNjggMzkuOTc3NkMxMjQuMzQ3IDM5Ljk3NjIgMTI0LjQyNyAzOS45NzQ4IDEyNC41MDggMzkuOTczM0MxMjQuNjMxIDM5Ljk3MDcgMTI0LjYzMSAzOS45NzA3IDEyNC43NTYgMzkuOTY4QzEyNS4wMTYgMzkuOTYyNSAxMjUuMjc3IDM5Ljk1NzYgMTI1LjUzNyAzOS45NTI2QzEyNS43MTQgMzkuOTQ5IDEyNS44OTEgMzkuOTQ1MyAxMjYuMDY4IDM5Ljk0MTZDMTI2LjUwMSAzOS45MzI2IDEyNi45MzQgMzkuOTI0MSAxMjcuMzY3IDM5LjkxNThDMTI3LjIyOSAzOS40NzgyIDEyNy4xMTEgMzkuMTQxOCAxMjYuNzAzIDM4Ljg5MTRDMTI1LjY4NCAzOC4zNjQ2IDEyNC4xNTggMzguMzM5IDEyMy4zMjkgMzkuMjQyMVoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTg2LjQ0NDQgMjAuOTkyMUM4Ny40MDA5IDIxLjY2NSA4OC4xMjMgMjIuNTcxNyA4OC4zMzI2IDIzLjc0NzNDODguNDU0NiAyNS4xMDggODguMzQxNiAyNi4yNiA4Ny40OTEzIDI3LjM2ODRDODcuNDU0NCAyNy40MjAyIDg3LjQxNzQgMjcuNDcxOSA4Ny4zNzkzIDI3LjUyNTNDODYuNzkwMyAyOC4yOTE2IDg1Ljg0IDI4Ljc0NjIgODQuOTA3NCAyOC45Mjg5QzgzLjc4NSAyOS4wNzA4IDgyLjc4NTIgMjguNzQ1MyA4MS44NzEzIDI4LjEwNDZDODEuMDMwMyAyNy40MzY1IDgwLjUyODIgMjYuNDg0OSA4MC4zNDA2IDI1LjQzMTVDODAuMzE3MiAyNS4xODM2IDgwLjMxMjUgMjQuOTM4NCA4MC4zMTQzIDI0LjY4OTRDODAuMzE0NyAyNC42MjMxIDgwLjMxNTIgMjQuNTU2NyA4MC4zMTU2IDI0LjQ4ODNDODAuMzI2MiAyMy45NTc4IDgwLjM5NzEgMjMuNDg1NiA4MC41OTMgMjIuOTg5NEM4MC42MjA2IDIyLjkxOSA4MC42NDgzIDIyLjg0ODUgODAuNjc2OCAyMi43NzZDODEuMTQyNyAyMS43MzE5IDgyLjAyNDYgMjAuOTIyOCA4My4wODUyIDIwLjUxMDVDODQuMjU0MSAyMC4xNjA3IDg1LjQyNDkgMjAuMzM5NSA4Ni40NDQ0IDIwLjk5MjFaTTgyLjYxMiAyMy4zMjYzQzgyLjI4ODcgMjMuODM3NCA4Mi4xNDMyIDI0LjMxODkgODIuMTkxNCAyNC45MjYzQzgyLjM2OSAyNS42NTM1IDgyLjY1NTkgMjYuMTQ5MSA4My4yOTA2IDI2LjU1MjZDODMuODQ4MyAyNi44NDAyIDg0LjQyNCAyNi44OTc5IDg1LjAyNDQgMjYuNzA3NUM4NS42NTE0IDI2LjQyNjYgODYuMTE5IDI1Ljk4ODcgODYuMzkyNCAyNS4zNTI2Qzg2LjU3OTkgMjQuNzUyIDg2LjUzMSAyNC4yMzU5IDg2LjI1MTEgMjMuNjc0NkM4NS44OTc4IDIzLjA3MzggODUuNDcwNyAyMi43MTE2IDg0Ljc5NTQgMjIuNTE5NEM4My45ODAxIDIyLjM0NDcgODMuMTQ2OSAyMi43MDI2IDgyLjYxMiAyMy4zMjYzWiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNMTQyLjg3OCAzNy45MzE2QzE0My41OTYgMzguNjMyOSAxNDMuNjkxIDM5LjQ0OSAxNDMuNzE0IDQwLjQxMjhDMTQzLjcxNiA0MC40NzM3IDE0My43MTggNDAuNTM0NSAxNDMuNzIgNDAuNTk3MkMxNDMuNzI1IDQwLjc4OTQgMTQzLjczIDQwLjk4MTUgMTQzLjczNSA0MS4xNzM3QzE0My43MzkgNDEuMzA0OSAxNDMuNzQyIDQxLjQzNjEgMTQzLjc0NiA0MS41NjczQzE0My43NTUgNDEuODg3IDE0My43NjMgNDIuMjA2NiAxNDMuNzcyIDQyLjUyNjNDMTQzLjk2NiA0Mi41ODE5IDE0NC4xNiA0Mi42Mzc1IDE0NC4zNjEgNDIuNjk0OEMxNDQuMzYxIDQzLjIyMjggMTQ0LjM2MSA0My43NTA4IDE0NC4zNjEgNDQuMjk0OEMxNDQuMDY2IDQ0LjMwNjIgMTQzLjc3MiA0NC4zMTQ1IDE0My40NzcgNDQuMzIxMUMxNDMuMzk1IDQ0LjMyNDYgMTQzLjMxMiA0NC4zMjgxIDE0My4yMjcgNDQuMzMxOEMxNDIuNjUxIDQ0LjM0MTQgMTQyLjI4MiA0NC4yNDc2IDE0MS44MzcgNDMuODczN0MxNDEuNjQ4IDQzLjY2NjEgMTQxLjU0IDQzLjQ1IDE0MS40MTYgNDMuMkMxNDEuMzgyIDQzLjI0NDcgMTQxLjM0NyA0My4yODk1IDE0MS4zMTEgNDMuMzM1NUMxNDAuNzY4IDQ0LjAwMDUgMTQwLjE2MiA0NC4zMDc5IDEzOS4zMDEgNDQuMzk3OEMxMzguMTk5IDQ0LjQ1NTQgMTM3LjIyNyA0NC4zNTIyIDEzNi4zNTYgNDMuNjA4MkMxMzUuOTc3IDQzLjIyODYgMTM1LjgxNCA0Mi44MzA1IDEzNS43NzcgNDIuMjk2MUMxMzUuNzg1IDQxLjc2NTMgMTM1Ljk0IDQxLjMzMzYgMTM2LjI4NSA0MC45MjYzQzEzNy4yMDMgNDAuMTEyOCAxMzguNDQ3IDQwLjEyNTkgMTM5LjYwOCA0MC4wNzlDMTM5LjggNDAuMDcwOSAxMzkuOTkyIDQwLjA2MjUgMTQwLjE4NSA0MC4wNTMyQzE0MC4zNTcgNDAuMDQ0OSAxNDAuNTMgNDAuMDM3NyAxNDAuNzAzIDQwLjAzMDZDMTQwLjk3NSA0MC4wMDIyIDE0MS4xNzEgMzkuOTQ4NCAxNDEuNDE2IDM5LjgzMTZDMTQxLjM5NCAzOS40NDE4IDE0MS4zOTQgMzkuNDQxOCAxNDEuMjIyIDM5LjEwNTNDMTQwLjY0OCAzOC42MzkyIDEzOS44NDEgMzguNjc5MiAxMzkuMTQ1IDM4LjczNjlDMTM4LjcxNyAzOC44MDkyIDEzOC40MDYgMzguODkyNCAxMzguMDg4IDM5LjE5NDhDMTM3Ljk4IDM5LjM4NzcgMTM3LjkzIDM5LjUzNDcgMTM3Ljg4MyAzOS43NDc0QzEzNy4yMzQgMzkuNzYwNiAxMzYuNTk1IDM5LjcyIDEzNS45NDggMzkuNjYzMkMxMzUuOTY3IDM4LjkxMjkgMTM2LjIxNiAzOC40Mzk0IDEzNi43NTMgMzcuOTIxMUMxMzguMzMyIDM2LjYxOTMgMTQxLjI4OCAzNi43MDMyIDE0Mi44NzggMzcuOTMxNlpNMTQxLjA4IDQxLjE3OUMxNDAuOTI2IDQxLjE5NDIgMTQwLjc3MyA0MS4yMDU4IDE0MC42MTkgNDEuMjE1MUMxNDAuNTI4IDQxLjIyMTMgMTQwLjQzNyA0MS4yMjc1IDE0MC4zNDMgNDEuMjMzOUMxNDAuMTUyIDQxLjI0NjMgMTM5Ljk2MiA0MS4yNTgxIDEzOS43NzEgNDEuMjY5NEMxMzguOTEzIDQxLjMyNDQgMTM4LjkxMyA0MS4zMjQ0IDEzOC4xNjIgNDEuNzE0MUMxMzguMDAxIDQxLjkxNTIgMTM4LjAwNiA0Mi4xMTI5IDEzOC4wMTcgNDIuMzYzNUMxMzguMDQyIDQyLjU0ODYgMTM4LjA0MiA0Mi41NDg2IDEzOC4yMjUgNDIuNzI2M0MxMzguOTMgNDMuMTE3MSAxMzkuNjE5IDQzLjA4MjQgMTQwLjM5NCA0Mi44ODY5QzE0MC44MTEgNDIuNzQwNyAxNDEuMTQ0IDQyLjU1NDMgMTQxLjQxOCA0Mi4yMDJDMTQxLjU3OSA0MS44NDg3IDE0MS41MzcgNDEuNDczNSAxNDEuNSA0MS4wOTQ4QzE0MS4zMTQgNDEuMDg1OSAxNDEuMzE0IDQxLjA4NTkgMTQxLjA4IDQxLjE3OVoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTIwLjAyMTkgNC4yOTQ3N0MyMC42ODQ2IDQuNzU2OTEgMjEuMDMwOSA1LjMwNCAyMS4yODM3IDYuMDYzMkMyMS4yOTEzIDYuMjYwNzQgMjEuMjkzNiA2LjQ1ODUgMjEuMjkzMyA2LjY1NjE4QzIxLjI5MzMgNi43MTYxOCAyMS4yOTMzIDYuNzc2MTggMjEuMjkzMyA2LjgzNzk5QzIxLjI5MzMgNy4wMzYyIDIxLjI5MjYgNy4yMzQ0IDIxLjI5MiA3LjQzMjZDMjEuMjkxOCA3LjU3MDA3IDIxLjI5MTcgNy43MDc1MyAyMS4yOTE2IDcuODQ1QzIxLjI5MTMgOC4yMDY3NCAyMS4yOTA1IDguNTY4NDggMjEuMjg5NSA4LjkzMDIzQzIxLjI4ODcgOS4yOTkzOSAyMS4yODgzIDkuNjY4NTQgMjEuMjg3OSAxMC4wMzc3QzIxLjI4NjkgMTAuNzYyIDIxLjI4NTUgMTEuNDg2MyAyMS4yODM3IDEyLjIxMDZDMjEuMTA4IDEyLjE5ODEgMjAuOTMyMiAxMi4xODU0IDIwLjc1NjUgMTIuMTcyNUMyMC42NTg3IDEyLjE2NTQgMjAuNTYwOCAxMi4xNTgzIDIwLjQ2IDEyLjE1MUMxOS4xNDAyIDEyLjAzMDMgMTcuODk3IDExLjI2NzQgMTcuMDM0MSAxMC4yODU2QzE2LjYxOTUgOS43NTUyMyAxNi4zMDIyIDkuMjIwNTEgMTYuMDY3OSA4LjU4OTUxQzE2LjAyNzcgOC40ODgyMyAxNi4wMjc3IDguNDg4MjMgMTUuOTg2OCA4LjM4NDkxQzE1LjY4MTQgNy40ODA2NCAxNS42MiA2LjI3ODkyIDE2LjAzMTEgNS40MDUzQzE2LjQ0OTMgNC43MTAzIDE2Ljk1NjkgNC4yNDUzMyAxNy43MzMgMy45OTMxM0MxOC41MDU1IDMuODQ2NjkgMTkuMzQxNSAzLjg4MTIzIDIwLjAyMTkgNC4yOTQ3N1oiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTEyMC4zODQgMzQuMjczNkMxMjAuMzg0IDM0LjgyOTQgMTIwLjM4NCAzNS4zODUyIDEyMC4zODQgMzUuOTU3OEMxMTkuNjY0IDM2LjAzMTUgMTE5LjY2NCAzNi4wMzE1IDExOS40NDEgMzYuMDUzNUMxMTkuMDM3IDM2LjA5NjcgMTE4LjY2OCAzNi4xNzA5IDExOC4yODEgMzYuMjk0N0MxMTguMjkgMzYuNTQyOSAxMTguMjkgMzYuNTQyOSAxMTguMzY1IDM2Ljc5OTlDMTE4Ljc0NSAzNy4wMjY1IDExOS4xODIgMzcuMDA1NiAxMTkuNjEyIDM3LjAyMUMxMTkuOTk0IDM3LjAzNjYgMTE5Ljk5NCAzNy4wMzY2IDEyMC4zODQgMzcuMDUyNkMxMjAuMzg0IDM3LjYzNjIgMTIwLjM4NCAzOC4yMTk3IDEyMC4zODQgMzguODIxQzExOS44MjkgMzguODIxIDExOS4yNzQgMzguODIxIDExOC43MDIgMzguODIxQzExOC43MDIgNDAuNjI3MyAxMTguNzAyIDQyLjQzMzYgMTE4LjcwMiA0NC4yOTQ3QzExOC4wMDggNDQuMjk0NyAxMTcuMzE0IDQ0LjI5NDcgMTE2LjU5OSA0NC4yOTQ3QzExNi41OTkgNDIuNDg4NCAxMTYuNTk5IDQwLjY4MjEgMTE2LjU5OSAzOC44MjFDMTE2LjA5OSAzOC44MjEgMTE1LjU5OSAzOC44MjEgMTE1LjA4NCAzOC44MjFDMTE1LjA4NCAzOC4yMzc0IDExNS4wODQgMzcuNjUzOCAxMTUuMDg0IDM3LjA1MjZDMTE1LjYxMiAzNy4wMjQ4IDExNi4xMzkgMzYuOTk3IDExNi42ODMgMzYuOTY4NEMxMTYuNTE2IDM2Ljc3MzggMTE2LjM1IDM2LjU3OTMgMTE2LjE3OCAzNi4zNzg5QzExNi4wODIgMzYuMDA1OCAxMTYuMTMxIDM1LjY1NTMgMTE2LjI3MyAzNS4yOTk5QzExNy4yODcgMzQuMTE1OSAxMTguOTU3IDM0LjI1MzEgMTIwLjM4NCAzNC4yNzM2WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNMTMyLjI0NyAzNy4xMzY4QzEzMy4zMjkgMzcuMTM2OCAxMzQuNDEyIDM3LjEzNjggMTM1LjUyNyAzNy4xMzY4QzEzNS41MjcgMzcuNjkyNiAxMzUuNTI3IDM4LjI0ODQgMTM1LjUyNyAzOC44MjExQzEzNC40NzMgMzguODIxMSAxMzMuNDE4IDM4LjgyMTEgMTMyLjMzMSAzOC44MjExQzEzMi4zMzEgNDAuNjU1MiAxMzIuMzMxIDQyLjQ4OTMgMTMyLjMzMSA0NC4zNzg5QzEzMS42MzcgNDQuMzc4OSAxMzAuOTQzIDQ0LjM3ODkgMTMwLjIyOCA0NC4zNzg5QzEzMC4yMjggNDIuNTE3MSAxMzAuMjI4IDQwLjY1NTIgMTMwLjIyOCAzOC43MzY4QzEzMC44OTQgMzguNzM2OCAxMzEuNTYgMzguNzM2OCAxMzIuMjQ3IDM4LjczNjhDMTMyLjI0NyAzOC4yMDg4IDEzMi4yNDcgMzcuNjgwOCAxMzIuMjQ3IDM3LjEzNjhaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik04NC4zMjYyIDM1Ljc1MjdDODQuNDIwNSAzNS43NTM0IDg0LjQyMDUgMzUuNzUzNCA4NC41MTY3IDM1Ljc1NDJDODUuMDA2NyAzNS43NjgxIDg1LjM5MzQgMzUuODU0NyA4NS44MDg5IDM2LjEyNjRDODUuODg3IDM2LjE3NSA4NS45NjUgMzYuMjIzNiA4Ni4wNDU1IDM2LjI3MzdDODYuMzkxOCAzNi42MzAzIDg2LjU4MyAzNy4wMTUzIDg2LjYwODEgMzcuNTE1OEM4Ni41ODE2IDM3Ljk5NzUgODYuNDI1MyAzOC4zNjg4IDg2LjA4NjkgMzguNzEzOUM4NS41MzI1IDM5LjE2OTkgODQuODY2NyAzOS4zNzk1IDg0LjE1MDcgMzkuMzUwNEM4My40ODU0IDM5LjI2NzQgODIuODU1MiAzOS4wNTIyIDgyLjQyMDIgMzguNTIxMUM4Mi4xNDQxIDM4LjA5NDggODIuMDU1NyAzNy42MjkgODIuMTM1NiAzNy4xMjhDODIuMjgyMiAzNi42MzU0IDgyLjYxMTcgMzYuMzIxNSA4My4wMzI3IDM2LjA0MjFDODMuNDc4OSAzNS44MTE4IDgzLjgzNDkgMzUuNzQ1OCA4NC4zMjYyIDM1Ljc1MjdaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik04NS44MzQgMTAuNDAxQzg2LjMxMjggMTAuNzkxIDg2LjUzMzQgMTEuMTYwNSA4Ni42MDI2IDExLjc4NDNDODYuNTc4MiAxMi4zMTEgODYuMzM5OSAxMi43MjE5IDg1Ljk2MTIgMTMuMDc5Qzg1LjMxMTQgMTMuNTgzNyA4NC41ODk0IDEzLjYyODYgODMuNzg5NyAxMy41NTc5QzgzLjE5NTcgMTMuNDU2OSA4Mi43NTA1IDEzLjE3MzIgODIuMzU5NSAxMi43MTU4QzgyLjEwODggMTIuMzE0NCA4Mi4wNjcyIDExLjkyOTIgODIuMTQ0MyAxMS40NjY1QzgyLjI5NDkgMTAuODgyMyA4Mi41ODU1IDEwLjU5OTQgODMuMDg2NyAxMC4yOTI1QzgzLjkyNDYgOS44NTcxNSA4NS4wMzg3IDkuODc3OTYgODUuODM0IDEwLjQwMVoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTc5LjI0NjkgMzQuNzc4OUM3OS44NDQ0IDM1LjE2OTUgODAuMjAwOSAzNS43MDY4IDgwLjQyNDcgMzYuMzc4OUM4MC40ODMxIDM2Ljk3MjMgODAuNDY4OCAzNy40OTc4IDgwLjA4ODIgMzcuOTc4OUM3OS43MjM2IDM4LjM1NDggNzkuMzUwNiAzOC40MzEzIDc4LjgzNjggMzguNDUyNkM3OC4wOTk3IDM4LjQyMjYgNzcuNTc3NiAzOC4yMDAxIDc3LjA3MDIgMzcuNjYzMUM3Ni42MjM2IDM3LjA5NzggNzYuNDA4NyAzNi41NzExIDc2LjQzODIgMzUuODQ0Qzc2LjUwNDggMzUuMzg3OCA3Ni42ODQgMzUuMDcwNyA3Ny4wMDcxIDM0Ljc0NzNDNzcuNzY2OSAzNC4yNzY0IDc4LjQ3NjQgMzQuNDAzOCA3OS4yNDY5IDM0Ljc3ODlaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik03OS43NzE4IDEwLjkyNEM4MC4wODczIDExLjE1MzUgODAuMjk5OSAxMS40MTU1IDgwLjQyNDQgMTEuNzg5NUM4MC41MTAxIDEyLjU5NDEgODAuMzcxIDEzLjE3NzQgNzkuODY4NCAxMy44MTM1Qzc5LjQ2MTYgMTQuMjUzNSA3OC45MTE3IDE0LjYwNjkgNzguMzA0NSAxNC42NzI3Qzc3Ljc4NDYgMTQuNjg4MyA3Ny40MDM3IDE0LjYyMTYgNzYuOTc1MyAxNC4zMTU4Qzc2LjYxMDggMTMuOTQxNCA3Ni40NjMyIDEzLjU5NjcgNzYuNDM5IDEzLjA3OUM3Ni40NTIyIDEyLjQ0NjQgNzYuNjg2NSAxMS44MTM1IDc3LjE0MzUgMTEuMzY4NUM3Ny45MzY0IDEwLjc2MyA3OC44MzQ1IDEwLjM5MDMgNzkuNzcxOCAxMC45MjRaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik05MS41NjEgMzQuNjczN0M5MS45MzU2IDM0Ljk5NTEgOTIuMTc2MiAzNS4yNjg3IDkyLjIyNTUgMzUuNzcxNEM5Mi4yNTE0IDM2LjYwODIgOTIuMDczOCAzNy4xNzc1IDkxLjUwODUgMzcuODAwMUM5MC45MDg5IDM4LjM0MzcgOTAuMzI2OCAzOC40NTc3IDg5LjUyNTkgMzguNDMyNkM4OS4wNzM5IDM4LjM3NzcgODguODcgMzguMTgzMiA4OC41NjQxIDM3Ljg2ODVDODguMjU3MiAzNy4zOTY4IDg4LjIxNjMgMzYuOTY1MSA4OC4yODc0IDM2LjQyMDFDODguNDMyOCAzNS43NDggODguODQyNCAzNS4xODE4IDg5LjQxMDYgMzQuODAwMUM5MC4wOTI3IDM0LjQwNDQgOTAuODQyMyAzNC4zMDA3IDkxLjU2MSAzNC42NzM3WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNNzMuNjQ3MyAxNi43Njg0Qzc0LjAyOTEgMTcuMDQ4IDc0LjMzNTYgMTcuMzg3NiA3NC40NTE4IDE3Ljg1MjZDNzQuNTMxMiAxOC41NDY5IDc0LjM5MjIgMTkuMDc0OCA3My45Nzk2IDE5LjYzNzhDNzMuNTMxMyAyMC4xNzYgNzMuMDM3NCAyMC41NzM2IDcyLjMyMTcgMjAuNjUzMkM3MS44MDg3IDIwLjY3NjggNzEuNDI0IDIwLjU5NTQgNzEuMDAyNiAyMC4yOTQ3QzcwLjYzODIgMTkuOTIwMyA3MC40OTA2IDE5LjU3NTUgNzAuNDY2MyAxOS4wNTc4QzcwLjQ3OTUgMTguNDI1MyA3MC43MTM5IDE3Ljc5MjQgNzEuMTcwOSAxNy4zNDczQzcxLjk0MDggMTYuNzY5MyA3Mi42ODMyIDE2LjQ1MjUgNzMuNjQ3MyAxNi43Njg0WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNNzEuNDM5MSAyMi40ODQyQzcyLjAxMSAyMi44OTEyIDcyLjM5MzkgMjMuMzg1NSA3Mi41MTcgMjQuMDg0MkM3Mi42MDM0IDI0Ljg4MzkgNzIuNDcyNiAyNS41Nzc2IDcxLjk4MDcgMjYuMjI2M0M3MS41OTU1IDI2LjYwMjcgNzEuMjkzMyAyNi43MTEzIDcwLjc2MDggMjYuNzM2OEM3MC4yNjUgMjYuNzIyIDY5LjkzMjYgMjYuNjE5OCA2OS41NzI1IDI2LjI3MzZDNjkuMTAxMyAyNS42OTcgNjguOTQxOCAyNS4wNjY5IDY4Ljk1NyAyNC4zMjk2QzY5LjAxNTUgMjMuNzkxIDY5LjE4MzggMjMuMzIxNyA2OS41MzA1IDIyLjkwNTJDNjkuNTk5OSAyMi44NDk2IDY5LjY2OTMgMjIuNzk0MSA2OS43NDA4IDIyLjczNjhDNjkuNzk5OCAyMi42ODgyIDY5Ljg1ODggMjIuNjM5NSA2OS45MTk2IDIyLjU4OTRDNzAuMzYxOSAyMi4yOTQyIDcwLjk0OTUgMjIuMzMwNCA3MS40MzkxIDIyLjQ4NDJaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik05MC43NTA5IDEwLjgzMTZDOTEuMzk0NCAxMS4xNTg4IDkxLjgyNzcgMTEuNjA5IDkyLjEzOSAxMi4yNjMyQzkyLjI1NjQgMTIuNzkyMSA5Mi4yNzY3IDEzLjMyOTcgOTIuMTA3NCAxMy44NDc0QzkxLjgyNzYgMTQuMjMwMyA5MS40ODg5IDE0LjUzMzcgOTEuMDI0MyAxNC42NTI2QzkwLjM1NjggMTQuNzM5MiA4OS43OTU2IDE0LjU1OTcgODkuMjM3OSAxNC4xOTE4Qzg4LjcxMiAxMy43NzMyIDg4LjM4MjIgMTMuMjA3NiA4OC4yNDgxIDEyLjU0NzRDODguMjEyMSAxMi4wNzg1IDg4LjI3MDIgMTEuNjkzNiA4OC41MDA1IDExLjI4NDJDODkuMDc4NyAxMC42MjU3IDg5Ljk2IDEwLjUwNjMgOTAuNzUwOSAxMC44MzE2WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNOTcuNTMzOCAyOC42OTQ4Qzk3LjkxMjIgMjkuMDE5NCA5OC4xNTg2IDI5LjMxNSA5OC4yNTk0IDI5LjgxMDZDOTguMjgwNSAzMC41NjQ2IDk4LjA5MjYgMzEuMTI2OSA5Ny41ODIxIDMxLjY4MjZDOTcuMDQ4NSAzMi4xODkxIDk2LjU5NjcgMzIuNDI0NSA5NS44NjE4IDMyLjQ1NzlDOTUuMzA1NiAzMi40NDA5IDk0LjkyMzQgMzIuMjc2NSA5NC41MjYzIDMxLjg4OTVDOTQuMTk3OCAzMS4zNDU2IDk0LjE3MzIgMzAuODM5NSA5NC4zMDU1IDMwLjIzMTZDOTQuNTc4NyAyOS40ODc2IDk1LjA0NzcgMjkuMDEwNyA5NS43MzU2IDI4LjYzMTZDOTYuMzIyIDI4LjM4MDkgOTYuOTY5NyAyOC40MDE5IDk3LjUzMzggMjguNjk0OFoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTczLjQ2ODMgMjguOTI2OUM3NC4wMTI1IDI5LjM2OTcgNzQuMzkxMiAyOS45NTQ0IDc0LjQ3MTkgMzAuNjU5MkM3NC40ODg3IDMxLjE1MSA3NC40MDI4IDMxLjUxMTggNzQuMTE1MyAzMS45MTU4QzczLjgzMDQgMzIuMjE1OCA3My41NjAxIDMyLjQwMjUgNzMuMTM5MyAzMi40NDExQzcyLjM4MTMgMzIuNDU5NyA3MS43ODQ3IDMyLjMxNzkgNzEuMjA1OCAzMS44QzcwLjcwMzcgMzEuMjIyNSA3MC40NjQ5IDMwLjY0MDggNzAuNDc0OSAyOS44ODI1QzcwLjUxNiAyOS40NTMgNzAuNjU5MSAyOS4xMzE2IDcwLjk3NjEgMjguODM2NUM3MS43NTg4IDI4LjI0NSA3Mi42NzU3IDI4LjQwODggNzMuNDY4MyAyOC45MjY5WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNOTguMzQ2MyAyMi40NTc2Qzk4LjkyMDIgMjIuNzYxOSA5OS4yMjA2IDIzLjIzNjEgOTkuNDM2OSAyMy44MzE2Qzk5LjYyMjEgMjQuNDg4NyA5OS41NDczIDI1LjE1MTEgOTkuMjY4NyAyNS43Njg0Qzk4Ljk5NTIgMjYuMTY3OSA5OC42OTU3IDI2LjQ3NzkgOTguMjU5MiAyNi42OTQ4Qzk3Ljc3NzggMjYuNzc0NSA5Ny4zMDYgMjYuNzY4OCA5Ni44NzY0IDI2LjUyMTFDOTYuMzgxNCAyNi4xNTU0IDk2LjA5NTUgMjUuNzAyMyA5NS45ODc4IDI1LjA5NDhDOTUuOTI3NiAyNC4zODc5IDk1Ljk2NDYgMjMuNzgxMyA5Ni4zMjQzIDIzLjE1NzlDOTYuODU5IDIyLjUyOSA5Ny41MTY0IDIyLjE3ODYgOTguMzQ2MyAyMi40NTc2WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNOTYuODgwOCAxNi44OTk3Qzk3LjUxNzIgMTcuMjUxIDk3Ljk2NzggMTcuNzM2MSA5OC4xNzUyIDE4LjQ0MjFDOTguMjM5NiAxOC45NTQ0IDk4LjI3OTEgMTkuNDk1OSA5OC4wMzg1IDE5Ljk2ODRDOTcuNzMyNCAyMC4zMzk0IDk3LjM5MDQgMjAuNTk3IDk2LjkwMjUgMjAuNjUxNkM5Ni4xNDEgMjAuNjc0OSA5NS41MDE4IDIwLjQ1NyA5NC45MzkzIDE5LjkzMzlDOTQuMzk0OCAxOS4zMjUxIDk0LjI3NDYgMTguNzgxOCA5NC4yODIxIDE3Ljk3NTZDOTQuMzE0MSAxNy41NzUgOTQuMzg2OSAxNy40MDM3IDk0LjY0MTkgMTcuMDk0N0M5NS4zNDQ5IDE2LjU0OSA5Ni4wODI2IDE2LjUzOTcgOTYuODgwOCAxNi44OTk3WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNNzMuMzA1MyAzNC4wODE5Qzc0LjA3MTggMzQuNDAyMSA3NC41Mjc4IDM0Ljg2NTQgNzQuODcyMSAzNS42MjExQzc0Ljk2NjcgMzUuOTcxNiA3NC45ODQ2IDM2LjI2MjkgNzQuODk4NCAzNi42MTU4Qzc0LjcxMjMgMzYuOTI2NCA3NC41NDQ3IDM3LjEwNTggNzQuMTk5MSAzNy4yMjExQzczLjU5MDEgMzcuMjgzMyA3My4wNTg2IDM3LjEwOSA3Mi41Nzc0IDM2LjczNTVDNzIuMDU2OSAzNi4yNDI2IDcxLjc0NyAzNS43ODU0IDcxLjcyMjcgMzUuMDU3OUM3MS43MzY4IDM0LjcwNTYgNzEuNzY3OSAzNC40NTk4IDcyLjAxMTggMzQuMTg5NUM3Mi40MjQ0IDMzLjk0NDEgNzIuODUwOCAzMy45NDM1IDczLjMwNTMgMzQuMDgxOVoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTc0LjM2NzMgMTEuOTU4Qzc0LjYyNTkgMTIuMTEwMiA3NC43NCAxMi4xOTg4IDc0Ljg3MjEgMTIuNDYzMkM3NC45NTMzIDEzLjExMzkgNzQuODg4IDEzLjU4NjYgNzQuNTE2OSAxNC4xMzEzQzc0LjA4NiAxNC42NjMxIDczLjU4MjMgMTUuMDM4MSA3Mi44OTUxIDE1LjE0NzRDNzIuNTM1IDE1LjE3NzIgNzIuMzE0NSAxNS4xMTQ1IDcyLjAxMTggMTQuOTA1M0M3MS43Mjk3IDE0LjYzMjcgNzEuNjg3OSAxNC4zNjUzIDcxLjY3NTMgMTMuOTc5QzcxLjc5MzggMTMuMzM4OCA3Mi4xMTMgMTIuNzk1MSA3Mi42Mjc2IDEyLjM5NjFDNzMuMjI2IDExLjk5MTYgNzMuNjM1NiAxMS44ODQ3IDc0LjM2NzMgMTEuOTU4WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNOTYuNTY2MiAzNC4xMDUzQzk2LjgyOTEgMzQuMzUzMSA5Ni45MzgyIDM0LjU5MTQgOTYuOTk3MyAzNC45NDc0Qzk2Ljk4MzMgMzUuNTcgOTYuNzM3NSAzNi4wODkxIDk2LjMyNDMgMzYuNTQ3NEM5NS43OTA3IDM2Ljk4NjYgOTUuMjYyOSAzNy4yODMgOTQuNTU3NiAzNy4yMjExQzk0LjIyMjggMzcuMDg4MyA5NC4wODYgMzcuMDE4MiA5My44ODQ2IDM2LjcxNThDOTMuNzQ2NiAzNi4wOTk1IDkzLjg0MzEgMzUuNjUxMyA5NC4xNTU3IDM1LjEwNDNDOTQuNzAyNSAzNC4zMDM5IDk1LjU5ODIgMzMuNzY4MyA5Ni41NjYyIDM0LjEwNTNaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik05NS4zMTUxIDExLjk1NzlDOTUuODk5NSAxMi4xMjg1IDk2LjM1NCAxMi41NDY3IDk2LjY4NTQgMTMuMDQ4M0M5Ni44OTkxIDEzLjQzOTkgOTYuOTkwNSAxMy43ODUyIDk2Ljk5NzYgMTQuMjMxNkM5Ni44NzQ3IDE0LjYyNjUgOTYuNzYzOCAxNC44NjA1IDk2LjQwODcgMTUuMDczN0M5NS44OTc1IDE1LjE2MTggOTUuNDE0OCAxNS4xMTEgOTQuOTY1NCAxNC44NDgzQzk0LjM2MDcgMTQuNDEzNiA5My45NDIxIDEzLjg3NDMgOTMuODA2IDEzLjEzMTZDOTMuOCAxMi43NTEzIDkzLjg2NjEgMTIuNTM5OCA5NC4wNTMyIDEyLjIxMDVDOTQuNDI5MiAxMS44NTggOTQuODI2NyAxMS44OTgxIDk1LjMxNTEgMTEuOTU3OVoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTEwMi4wMDMgMjMuMTQ3M0MxMDIuMzk2IDIzLjI3MzcgMTAyLjQ5MSAyMy40MDYzIDEwMi43MTggMjMuNzQ3M0MxMDIuODggMjQuMjM5NSAxMDIuODg5IDI0Ljg1OSAxMDIuNjk3IDI1LjM0MjFDMTAyLjQ1MiAyNS43NzA2IDEwMi40NTIgMjUuNzcwNiAxMDIuMjEzIDI1LjkzNjhDMTAxLjkyNCAyNS45ODQyIDEwMS45MjQgMjUuOTg0MiAxMDEuNjI0IDI1LjkzNjhDMTAxLjI0MiAyNS42NTY0IDEwMS4xMiAyNS4zODY4IDEwMS4wMzUgMjQuOTI2M0MxMDAuOTgzIDI0LjQwOTggMTAxLjAzMSAyMy45NTI3IDEwMS4yODggMjMuNDk0N0MxMDEuNTMgMjMuMjE4MSAxMDEuNjM0IDIzLjE2MDUgMTAyLjAwMyAyMy4xNDczWiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNNjcuMTMyNiAyMy4yNDIxQzY3LjUyMDEgMjMuNTk4NSA2Ny42MTYzIDI0LjAyNTMgNjcuNjYzNiAyNC41MzY4QzY3LjYyMjggMjUuMDEwMyA2Ny41MDY4IDI1LjQyMTQgNjcuMjM3NyAyNS44MTU3QzY2Ljk1OTIgMjUuOTkzOSA2Ni43ODMyIDI1Ljk4OCA2Ni40NTk2IDI1LjkzNjhDNjYuMTEzMSAyNS42NzQ3IDY1Ljk1NjkgMjUuNDM0MSA2NS44NzA3IDI1LjAxMDVDNjUuODEgMjQuNDAxNCA2NS44NDg0IDIzLjkxODggNjYuMjA3MiAyMy40MTA1QzY2LjQ5MDIgMjMuMDg4MSA2Ni43MzQ3IDIzLjEyODMgNjcuMTMyNiAyMy4yNDIxWiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNODUuMzg3OSA3LjE1Nzg0Qzg1LjU5ODIgNy4zMDUyIDg1LjU5ODIgNy4zMDUyIDg1LjcyNDQgNy40OTQ2OEM4NS43NzE3IDcuNzg0MTUgODUuNzcxNyA3Ljc4NDE1IDg1LjcyNDQgOC4wODQxNUM4NS40NDQyIDguNDY2NTkgODUuMTc0OSA4LjU4OTIxIDg0LjcxNDkgOC42NzM2M0M4NC4xOTkgOC43MjU3NCA4My43NDIzIDguNjc3ODYgODMuMjg0NyA4LjQyMDk5QzgzLjA2OTIgOC4yMzE1MiA4My4wNjkyIDguMjMxNTIgODIuOTQ4MiA3Ljk5OTk0QzgyLjk0ODIgNy42NzAwNiA4Mi45NzkzIDcuNDgzMyA4My4xNzQzIDcuMjE1NzNDODMuODIyMSA2Ljc0MjU3IDg0LjY5OTYgNi43ODk1MiA4NS4zODc5IDcuMTU3ODRaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik04NS4zODgzIDQwLjkyNjNDODUuNjQ2NyA0MS4xMTM4IDg1LjcxMzEgNDEuMTk0NyA4NS43NjY4IDQxLjUxMDVDODUuNzE0NSA0MS44MzE2IDg1LjYyMDEgNDEuOTYzNiA4NS4zODgzIDQyLjE4OTRDODQuODM3MiA0Mi40OTU5IDg0LjIzOTkgNDIuNDcwNiA4My42NDQ2IDQyLjMzNThDODMuMzg4MiA0Mi4yNTI1IDgzLjIxOTQgNDIuMTI5NyA4My4wMzI3IDQxLjkzNjhDODIuOTA5NyA0MS42OTA0IDgyLjkxMTEgNDEuNTM2MyA4Mi45NDg2IDQxLjI2MzFDODMuNjE1NCA0MC41NjM5IDg0LjU0NTggNDAuNDgyNyA4NS4zODgzIDQwLjkyNjNaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik03OS40NDE3IDQwLjA2OTdDNzkuNzU0MiA0MC4yMTQ5IDgwLjAxNjggNDAuMzUyNyA4MC4xNzIyIDQwLjY3MzdDODAuMjA5IDQwLjg4OTUgODAuMjA5IDQwLjg4OTUgODAuMTcyMiA0MS4wOTQ3Qzc5LjkxMDkgNDEuMzQ5NSA3OS43MTA3IDQxLjM4MzcgNzkuMzUyIDQxLjM4NDJDNzguODA4OSA0MS4zNzA5IDc4LjQyNjggNDEuMjM2NyA3OC4wMDk5IDQwLjg5MjFDNzcuODMwOCA0MC42NzE3IDc3Ljg4NTQgNDAuNDQwNSA3Ny45MDA4IDQwLjE2ODRDNzguNDA4MyAzOS44MzQyIDc4Ljg4NTkgMzkuODcwOCA3OS40NDE3IDQwLjA2OTdaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik04OS40NzAyIDcuNzExNzlDODkuOTU5IDcuNzk1MTUgOTAuMzc3MiA3Ljk1NDcxIDkwLjc0NTkgOC4yODk0M0M5MC44NTYzIDguNTA1MjIgOTAuODU2MyA4LjUwNTIyIDkwLjg1NjMgOC43MjYyN0M5MC43NDgyIDguOTgzMTIgOTAuNjg5MSA5LjA1ODM5IDkwLjQzNTcgOS4xNzg5Qzg5LjkyNjQgOS4yNTIxNSA4OS40NTY2IDkuMTc0MTYgODkuMDA1NSA4LjkyNjI3Qzg4Ljc4MzQgOC43NDE3NCA4OC42MzEyIDguNTg3NjQgODguNDkwMiA4LjMzNjc5Qzg4LjUwMDggOC4xNjgzNyA4OC41MDA4IDguMTY4MzcgODguNjU4NSA3LjkzNjc5Qzg4Ljk2MTQgNy43MTg0OCA4OS4xMDcxIDcuNjg5OTEgODkuNDcwMiA3LjcxMTc5WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNNjguNzMxMSAxOC4xMDUzQzY4LjkzODYgMTguMzMzOCA2OC45Nzk4IDE4LjQ5NDMgNjkuMDE1IDE4LjhDNjguOTYgMTkuMzUwNyA2OC44MjA3IDE5LjkxNTYgNjguMzk0NiAyMC4yOTQ4QzY4LjA3OTEgMjAuMzYzMiA2OC4wNzkxIDIwLjM2MzIgNjcuODA1NyAyMC4zNzlDNjcuNTEzNSAyMC4wNDQ2IDY3LjUwNjUgMTkuNzk3NCA2Ny41MTc1IDE5LjM2NTVDNjcuNTc1OSAxOC45NTgxIDY3Ljc0MDEgMTguNjEwNiA2Ny45NzM5IDE4LjI3MzdDNjguMjY2MSAxOC4wNzg3IDY4LjM4NzMgMTguMDU1MSA2OC43MzExIDE4LjEwNTNaIiBmaWxsPSIjQTJFRjFCIi8+CjxwYXRoIGQ9Ik04MC4wMDk0IDcuODczNzFDODAuMDYzMiA3LjkxNTQgODAuMTE3IDcuOTU3MDggODAuMTcyNCA4LjAwMDAzQzgwLjE2MjUgOC41MDgzMiA4MC4xNjI1IDguNTA4MzIgNzkuOTcyMyA4LjcxNjQ4Qzc5LjQ3NTggOS4wODc4NCA3OC45NDE0IDkuMjM2NDIgNzguMzIxNyA5LjE3ODk4Qzc4LjA3NjQgOS4wNzIyNiA3Ny45NDM0IDkuMDA1ODggNzcuODE2OSA4Ljc2ODQ1Qzc3LjgxNjkgOC40OTY3MiA3Ny45IDguMzgwMjMgNzguMDY5MyA4LjE2ODQ1Qzc4LjYzMjYgNy43NjA4MSA3OS4zNjEzIDcuNTU1OTQgODAuMDA5NCA3Ljg3MzcxWiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNMTAwLjk1MSAyOC44QzEwMS4yMTUgMjkuMTk1NCAxMDEuMTY3IDI5LjYwMDMgMTAxLjEyIDMwLjA2MzJDMTAwLjk4OSAzMC41MDIxIDEwMC43NjkgMzAuODQ1NSAxMDAuMzYzIDMxLjA3MzdDMTAwLjA3NSAzMS4wNjAzIDk5Ljk3ODcgMzEuMDI2MyA5OS43NzM3IDMwLjgyMTFDOTkuNjQ0NyAzMC4yNjQ2IDk5LjcxMjYgMjkuODI4MiA5OS45NDIgMjkuMzA1M0MxMDAuMjE5IDI4Ljg3NDUgMTAwLjQzMyAyOC42NDQyIDEwMC45NTEgMjguOFoiIGZpbGw9IiNBMkVGMUIiLz4KPHBhdGggZD0iTTY4LjA4NDUgMjguNzIxQzY4LjQ2NjQgMjguODU0NCA2OC42NDYxIDI5LjEzMDYgNjguODE2NyAyOS40ODA2QzY4Ljk4ODIgMjkuOTI2NCA2OS4wNDYxIDMwLjM1ODMgNjguODk5NSAzMC44MjFDNjguNzg0MSAzMC45NjkxIDY4Ljc4NDEgMzAuOTY5MSA2OC42NDcxIDMxLjA3MzdDNjguMzMxIDMxLjA3MzcgNjguMTg5NiAzMS4wMzQxIDY3Ljk1ODMgMzAuODE1OEM2Ny42MzAzIDMwLjI4MzYgNjcuNDYyMiAyOS43Njc2IDY3LjU1MzQgMjkuMTM2OEM2Ny42NzM3IDI4Ljg0MDUgNjcuNzQ1NyAyOC43MTE5IDY4LjA4NDUgMjguNzIxWiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNMTAwLjIxNSAxOC4wNjg0QzEwMC41MzEgMTguMTA1MiAxMDAuNTMxIDE4LjEwNTIgMTAwLjc2NyAxOC4zMjYzQzEwMS4xMzEgMTguODQzMSAxMDEuMjA3IDE5LjQyMSAxMDEuMTIgMjAuMDQyQzEwMC45OTIgMjAuMjM4OCAxMDAuOTkyIDIwLjIzODggMTAwLjg2NyAyMC4zNzg5QzEwMC41MjUgMjAuMzcwNyAxMDAuMzI2IDIwLjI5ODMgMTAwLjA4OSAyMC4wNTc4Qzk5Ljc3MzcgMTkuNjU1OCA5OS43MDgyIDE5LjE5NTcgOTkuNjg5NSAxOC42OTQ3Qzk5LjgxNTUgMTguMTIyMiA5OS44MTU1IDE4LjEyMjIgMTAwLjIxNSAxOC4wNjg0WiIgZmlsbD0iI0EyRUYxQiIvPgo8cGF0aCBkPSJNOTAuMDY3OCAzOS45NzM2QzkwLjIxIDM5Ljk3MTUgOTAuMjEgMzkuOTcxNSA5MC4zNTUgMzkuOTY5NEM5MC42MDQxIDQwIDkwLjYwNDEgNDAgOTAuNzYyOSA0MC4xMTMxQzkwLjg1NjUgNDAuMjUyNiA5MC44NTY1IDQwLjI1MjYgOTAuODcyMyA0MC40ODk0QzkwLjcyNjkgNDAuODgwMSA5MC40NSA0MS4wNTM1IDkwLjA5OTQgNDEuMjYzMUM4OS44MzM0IDQxLjM1MTkgODkuNjI3IDQxLjM2NzEgODkuMzQ3NSA0MS4zNzM2Qzg5LjI2IDQxLjM3NjggODkuMTcyNSA0MS4zNzk5IDg5LjA4MjMgNDEuMzgzMkM4OC43ODMgNDEuMzM5MyA4OC42ODkyIDQxLjI0MDYgODguNTAxIDQxLjAxMDVDODguNjA1NSA0MC41OTE5IDg4LjcyODEgNDAuNDA0MiA4OS4wODk5IDQwLjE2ODRDODkuNDIzMyA0MC4wMDcgODkuNjk4NiAzOS45NzQ5IDkwLjA2NzggMzkuOTczNloiIGZpbGw9IiNBMkVGMUIiLz4KPC9zdmc+Cg=="" alt=""Logo InnovaSfera"" style=""width: 100%; max-width: 480px;"" />
                                        <h2 style=""color: #fff; margin-top: 30px;"">Seu projeto está prestes a decolar!</h2>
                                    </div>
                                            <!-- Mensagem de boas-vindas -->
                                            <div style=""padding: 40px 20px; text-align: center;"">
                                                <h2 style=""font-size: 24px; color: #0f193a;"">Olá, {request.Body}!</h2>
                                                <p style=""font-size: 16px; max-width: 600px; margin: 0 auto;"">
                                                    Seu contrato personalizado chegou com sucesso! 🎉<br />
                                                    Agora é hora de revisar, tirar dúvidas e assinar digitalmente com total segurança através do GOV.BR.
                                                </p>
                                            </div>
                                            <!-- Etapas de assinatura -->
                                            <div style=""padding: 60px 20px; max-width: 700px; margin: 0 auto;"">
                                                <h3 style=""font-size: 22px; font-weight: bold; text-align: center; margin-bottom: 40px;"">Passo a Passo para
                                                    Assinatura:</h3>

                                                <table role=""presentation"" style=""width: 100%; max-width: 600px; margin: 0 auto; font-size: 16px; color: #0f193a;"" cellpadding=""0"" cellspacing=""0"">
                                                  <tr>
                                                    <td colspan=""2"" style=""padding: 20px 0; text-align: center;"">
                                                      <h3 style=""font-size: 22px; font-weight: bold; margin: 0;"">Passo a Passo para Assinatura:</h3>
                                                    </td>
                                                  </tr>

                                                  <!-- Passo 1 -->
                                                  <tr>
                                                    <td style=""width: 50px; padding: 10px;"">
                                                      <div style=""width: 40px; height: 40px; background-color: #a2ef1b; border-radius: 50%; text-align: center; line-height: 40px; font-weight: bold;"">1</div>
                                                    </td>
                                                    <td style=""padding: 10px 0;"">
                                                      <strong>Revisar o contrato com atenção</strong><br />
                                                      Leia o documento e verifique se todas as informações estão corretas.
                                                    </td>
                                                  </tr>

                                                  <!-- Passo 2 -->
                                                  <tr>
                                                    <td style=""width: 50px; padding: 10px;"">
                                                      <div style=""width: 40px; height: 40px; background-color: #a2ef1b; border-radius: 50%; text-align: center; line-height: 40px; font-weight: bold;"">2</div>
                                                    </td>
                                                    <td style=""padding: 10px 0;"">
                                                      <strong>Tire dúvidas, se necessário</strong><br />
                                                      Nossa equipe está à disposição para responder qualquer pergunta.
                                                    </td>
                                                  </tr>

                                                  <!-- Passo 3 -->
                                                  <tr>
                                                    <td style=""width: 50px; padding: 10px;"">
                                                      <div style=""width: 40px; height: 40px; background-color: #a2ef1b; border-radius: 50%; text-align: center; line-height: 40px; font-weight: bold;"">3</div>
                                                    </td>
                                                    <td style=""padding: 10px 0;"">
                                                      <strong>Acesse o portal de assinatura Gov.BR</strong><br />
                                                      Clique no link abaixo para iniciar a assinatura digital.
                                                    </td>
                                                  </tr>

                                                  <!-- Passo 4 -->
                                                  <tr>
                                                    <td style=""width: 50px; padding: 10px;"">
                                                      <div style=""width: 40px; height: 40px; background-color: #a2ef1b; border-radius: 50%; text-align: center; line-height: 40px; font-weight: bold;"">4</div>
                                                    </td>
                                                    <td style=""padding: 10px 0;"">
                                                      <strong>Finalize a assinatura:</strong><br />
                                                      Acesse o portal, faça login com sua conta <a href=""https://www.gov.br"" style=""color: #0f193a;"">GOV.BR</a>, envie o contrato, clique em <em>“Avançar”</em> e posicione sua assinatura sobre seu nome no documento.
                                                    </td>
                                                  </tr>
                                                </table>

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
