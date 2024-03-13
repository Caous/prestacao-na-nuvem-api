using DocumentFormat.OpenXml.Office2010.Excel;
using PrestacaoNuvem.Api.Domain.Interfacesk;

namespace PrestacaoNuvem.Api.Domain.Services;

public class PrestacaoServicoService : IPrestacaoServicoService
{
    private readonly IPrestacaoServicoRepository _repository;
    private readonly IEmailManager _emailManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PrestacaoServicoService(IPrestacaoServicoRepository repository, IEmailManager emailManager, IConfiguration configuration, IMapper mapper)
    {
        _repository = repository;
        _emailManager = emailManager;
        _configuration = configuration;
        _mapper = mapper;
    }
    public async Task ChangeStatus(Guid id, EPrestacaoServicoStatus status)
    {
        var prestacao = await _repository.FindById(id);

        var envioEmailTask = EnvioEmailOrdemServicoConcluida(status, prestacao);

        var alterandoStatusTask = _repository.ChangeStatus(prestacao, status);

        Task.WaitAll(envioEmailTask, alterandoStatusTask);
    }

    private async Task EnvioEmailOrdemServicoConcluida(EPrestacaoServicoStatus status, PrestacaoServico prestacao)
    {
        if (status == EPrestacaoServicoStatus.Concluido)
        {
            Email emailConfig = new(
                new EmailConfigHost(
                    _configuration.GetValue<string>("EmailConfiguration:Host"),
                    _configuration.GetValue<int>("EmailConfiguration:Port"),
                    _configuration.GetValue<string>("EmailConfiguration:UserName"),
                   _configuration.GetValue<string>("EmailConfiguration:Password")));

            emailConfig.Subject = "Prestação na Nuvem - Ordem De Serviço Concluída";
            emailConfig.FromEmail = _configuration.GetValue<string>("EmailConfiguration:UserName");
            emailConfig.ToEmail = new string[] { prestacao.Cliente.Email, "suporte@innovasfera.com.br", "caous.g@gmail.com" };
            emailConfig.Menssage = GerarMensagem();

            await _emailManager.SendEmailSmtpAsync(emailConfig);
        }
    }

    private string GerarMensagem()
    {
        return $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" lang=\"en\">\r\n <head>\r\n  <meta charset=\"UTF-8\">\r\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <meta content=\"telephone=no\" name=\"format-detection\">\r\n  <title>Nova mensagem</title><!--[if (mso 16)]>\r\n    <style type=\"text/css\">\r\n    a {{text-decoration: none;}}\r\n    </style>\r\n    <![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>\r\n<xml>\r\n    <o:OfficeDocumentSettings>\r\n    <o:AllowPNG></o:AllowPNG>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n    </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <style type=\"text/css\">\r\n#outlook a {{\r\n\tpadding:0;\r\n}}\r\n.es-button {{\r\n\tmso-style-priority:100!important;\r\n\ttext-decoration:none!important;\r\n}}\r\na[x-apple-data-detectors] {{\r\n\tcolor:inherit!important;\r\n\ttext-decoration:none!important;\r\n\tfont-size:inherit!important;\r\n\tfont-family:inherit!important;\r\n\tfont-weight:inherit!important;\r\n\tline-height:inherit!important;\r\n}}\r\n.es-desk-hidden {{\r\n\tdisplay:none;\r\n\tfloat:left;\r\n\toverflow:hidden;\r\n\twidth:0;\r\n\tmax-height:0;\r\n\tline-height:0;\r\n\tmso-hide:all;\r\n}}\r\n@media only screen and (max-width:600px) {{p, ul li, ol li, a {{ line-height:150%!important }} h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120%!important }} h1 {{ font-size:36px!important; text-align:left }} h2 {{ font-size:26px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:36px!important; text-align:left }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:26px!important; text-align:left }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important; text-align:left }} .es-menu td a {{ font-size:12px!important }} .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }} *[class=\"gmail-fix\"] {{ display:none!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-button-border {{ display:inline-block!important }} a.es-button, button.es-button {{ font-size:20px!important; display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .es-adapt-td {{ display:block!important; width:100%!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-m-p0 {{ padding:0!important }} .es-m-p0r {{ padding-right:0!important }} .es-m-p0l {{ padding-left:0!important }} .es-m-p0t {{ padding-top:0!important }} .es-m-p0b {{ padding-bottom:0!important }} .es-m-p20b {{ padding-bottom:20px!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} table.es-social {{ display:inline-block!important }} table.es-social td {{ display:inline-block!important }} .es-m-p5 {{ padding:5px!important }} .es-m-p5t {{ padding-top:5px!important }} .es-m-p5b {{ padding-bottom:5px!important }} .es-m-p5r {{ padding-right:5px!important }} .es-m-p5l {{ padding-left:5px!important }} .es-m-p10 {{ padding:10px!important }} .es-m-p10t {{ padding-top:10px!important }} .es-m-p10b {{ padding-bottom:10px!important }} .es-m-p10r {{ padding-right:10px!important }} .es-m-p10l {{ padding-left:10px!important }} .es-m-p15 {{ padding:15px!important }} .es-m-p15t {{ padding-top:15px!important }} .es-m-p15b {{ padding-bottom:15px!important }} .es-m-p15r {{ padding-right:15px!important }} .es-m-p15l {{ padding-left:15px!important }} .es-m-p20 {{ padding:20px!important }} .es-m-p20t {{ padding-top:20px!important }} .es-m-p20r {{ padding-right:20px!important }} .es-m-p20l {{ padding-left:20px!important }} .es-m-p25 {{ padding:25px!important }} .es-m-p25t {{ padding-top:25px!important }} .es-m-p25b {{ padding-bottom:25px!important }} .es-m-p25r {{ padding-right:25px!important }} .es-m-p25l {{ padding-left:25px!important }} .es-m-p30 {{ padding:30px!important }} .es-m-p30t {{ padding-top:30px!important }} .es-m-p30b {{ padding-bottom:30px!important }} .es-m-p30r {{ padding-right:30px!important }} .es-m-p30l {{ padding-left:30px!important }} .es-m-p35 {{ padding:35px!important }} .es-m-p35t {{ padding-top:35px!important }} .es-m-p35b {{ padding-bottom:35px!important }} .es-m-p35r {{ padding-right:35px!important }} .es-m-p35l {{ padding-left:35px!important }} .es-m-p40 {{ padding:40px!important }} .es-m-p40t {{ padding-top:40px!important }} .es-m-p40b {{ padding-bottom:40px!important }} .es-m-p40r {{ padding-right:40px!important }} .es-m-p40l {{ padding-left:40px!important }} .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }} }}\r\n@media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}\r\n</style>\r\n </head>\r\n <body style=\"width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0\">\r\n  <div dir=\"ltr\" class=\"es-wrapper-color\" lang=\"en\" style=\"background-color:#FAFAFA\"><!--[if gte mso 9]>\r\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\r\n\t\t\t\t<v:fill type=\"tile\" color=\"#fafafa\"></v:fill>\r\n\t\t\t</v:background>\r\n\t\t<![endif]-->\r\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA\">\r\n     <tr>\r\n      <td valign=\"top\" style=\"padding:0;Margin:0\">\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n         <tr>\r\n          <td class=\"es-info-area\" align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\" bgcolor=\"#FFFFFF\" role=\"none\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-infoblock\" style=\"padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px\"><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\">View online version</a></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-header\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table bgcolor=\"#ffffff\" class=\"es-header-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td class=\"es-m-p0r\" valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-bottom:10px;font-size:0px\"><img src=\"https://eevodqf.stripocdn.email/content/guids/CABINET_673f8493bc9cd8a7ba587044e08e71bbeff6d7186b6011dce5b19ed7b02fc75f/images/logo1920x1080_fundo_preto.png\" alt=\"Logo\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;font-size:12px\" width=\"200\" title=\"Logo\"></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-top:15px;padding-left:20px;padding-right:20px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:10px;padding-bottom:10px;font-size:0px\"><img src=\"https://eevodqf.stripocdn.email/content/guids/CABINET_54100624d621728c49155116bef5e07d/images/84141618400759579.png\" alt style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\" width=\"100\"></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0;padding-bottom:10px\"><h1 style=\"Margin:0;line-height:46px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:46px;font-style:normal;font-weight:bold;color:#333333\">Ordem de serviço concluída</h1></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table bgcolor=\"#ffffff\" class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-m-txt-c\" style=\"padding:0;Margin:0\"><h2 style=\"Margin:0;line-height:31px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:26px;font-style:normal;font-weight:bold;color:#333333\">Order&nbsp;<a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:26px\">{{idOdem}}</a></h2></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-m-p0r es-m-p0l\" style=\"Margin:0;padding-top:5px;padding-bottom:5px;padding-left:40px;padding-right:40px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">{{Data}}</p></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-m-p0r es-m-p0l\" style=\"Margin:0;padding-top:5px;padding-bottom:15px;padding-left:40px;padding-right:40px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Este é um e-mail oficial do oficina na nuvem, informamos para o senhor(a) {{Cliente] que seu carro foi concluído pelo seu mecânico em breve ele entrará em contato com você.</p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px\"><!--[if mso]><table style=\"width:560px\" cellpadding=\"0\" cellspacing=\"0\"><tr><td style=\"width:280px\" valign=\"top\"><![endif]-->\r\n               <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-left\" align=\"left\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left\">\r\n                 <tr>\r\n                  <td class=\"es-m-p0r es-m-p20b\" align=\"center\" style=\"padding:0;Margin:0;width:280px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"left\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Customer: <strong>sarah_powell@domain.com</strong></p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Order number:&nbsp;<strong>#65000500</strong></p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Invoice date:&nbsp;<strong>Apr 17, 2021</strong></p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Payment method:&nbsp;<strong>PayPal</strong></p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Currency:&nbsp;<strong>USD</strong></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table><!--[if mso]></td><td style=\"width:0px\"></td><td style=\"width:280px\" valign=\"top\"><![endif]-->\r\n               <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-right\" align=\"right\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right\">\r\n                 <tr>\r\n                  <td class=\"es-m-p0r\" align=\"center\" style=\"padding:0;Margin:0;width:280px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"left\" class=\"es-m-txt-l\" style=\"padding:0;Margin:0\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Shipping Method: <strong>UPS - Ground</strong></p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Shipping address:</p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\"><strong>Sarah Powell,<br>600 Montgomery St,<br>San Francisco, CA 94111</strong></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table><!--[if mso]></td></tr></table><![endif]--></td>\r\n             </tr>\r\n             <tr>\r\n              <td align=\"left\" style=\"Margin:0;padding-bottom:10px;padding-top:15px;padding-left:20px;padding-right:20px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"left\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:10px;padding-bottom:10px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px\">Got a question?&nbsp;Email us at&nbsp;<a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px\">support@</a><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px\">stylecasual</a><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px\">.com</a>&nbsp;or give us a call at&nbsp;<a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px\">+000 123 456</a>.</p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-footer\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top\">\r\n         <tr>\r\n          <td align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table class=\"es-footer-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:640px\" role=\"none\">\r\n             <tr>\r\n              <td align=\"left\" style=\"Margin:0;padding-top:20px;padding-bottom:20px;padding-left:20px;padding-right:20px\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"left\" style=\"padding:0;Margin:0;width:600px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:15px;padding-bottom:15px;font-size:0\">\r\n                       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-table-not-adapt es-social\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr>\r\n                          <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:40px\"><img title=\"Facebook\" src=\"https://eevodqf.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png\" alt=\"Fb\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n                          <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:40px\"><img title=\"Twitter\" src=\"https://eevodqf.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png\" alt=\"Tw\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n                          <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;padding-right:40px\"><img title=\"Instagram\" src=\"https://eevodqf.stripocdn.email/content/assets/img/social-icons/logo-black/instagram-logo-black.png\" alt=\"Inst\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n                          <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0\"><img title=\"Youtube\" src=\"https://eevodqf.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png\" alt=\"Yt\" width=\"32\" style=\"display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic\"></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-bottom:35px\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px\">Style Casual&nbsp;© 2021 Style Casual, Inc. All Rights Reserved.</p><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px\">4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898</p></td>\r\n                     </tr>\r\n                     <tr>\r\n                      <td style=\"padding:0;Margin:0\">\r\n                       <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"es-menu\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                         <tr class=\"links\">\r\n                          <td align=\"center\" valign=\"top\" width=\"33.33%\" style=\"Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0\"><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#999999;font-size:12px\">Visit Us </a></td>\r\n                          <td align=\"center\" valign=\"top\" width=\"33.33%\" style=\"Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0;border-left:1px solid #cccccc\"><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#999999;font-size:12px\">Privacy Policy</a></td>\r\n                          <td align=\"center\" valign=\"top\" width=\"33.33%\" style=\"Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0;border-left:1px solid #cccccc\"><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#999999;font-size:12px\">Terms of Use</a></td>\r\n                         </tr>\r\n                       </table></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table>\r\n       <table cellpadding=\"0\" cellspacing=\"0\" class=\"es-content\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%\">\r\n         <tr>\r\n          <td class=\"es-info-area\" align=\"center\" style=\"padding:0;Margin:0\">\r\n           <table class=\"es-content-body\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px\" bgcolor=\"#FFFFFF\" role=\"none\">\r\n             <tr>\r\n              <td align=\"left\" style=\"padding:20px;Margin:0\">\r\n               <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                 <tr>\r\n                  <td align=\"center\" valign=\"top\" style=\"padding:0;Margin:0;width:560px\">\r\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\r\n                     <tr>\r\n                      <td align=\"center\" class=\"es-infoblock\" style=\"padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC\"><p style=\"Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px\"><a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\"></a>No longer want to receive these emails?&nbsp;<a href=\"\" target=\"_blank\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\">Unsubscribe</a>.<a target=\"_blank\" href=\"\" style=\"-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px\"></a></p></td>\r\n                     </tr>\r\n                   </table></td>\r\n                 </tr>\r\n               </table></td>\r\n             </tr>\r\n           </table></td>\r\n         </tr>\r\n       </table></td>\r\n     </tr>\r\n   </table>\r\n  </div>\r\n </body>\r\n</html>";
    }

    public async Task<PrestacaoServicoDto> CreatePrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = prestacaoServico.PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Veiculo != null)
        {
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;
            prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        }

        if (prestacaoServico.Cliente != null)
        {
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;
            prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;
            prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        }

        if (prestacaoServico.Servicos != null)
        {
            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }


        prestacaoServico.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
        prestacaoServico.UsrCadastro = prestacaoServico.UsrCadastro;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Create(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<PrestacaoServicoDto> Desabled(Guid id, Guid userDesabled)
    {

        var result = await _repository.Desabled(id, userDesabled);
        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task<PrestacaoServicoDto> FindByIdPrestacaoServico(Guid id)
    {
        var result = await _repository.FindById(id);

        return _mapper.Map<PrestacaoServicoDto>(result);
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetAllPrestacaoServico(PrestacaoServicoDto item)
    {
        var result = await _repository.GetAll(item.PrestadorId.Value, _mapper.Map<PrestacaoServico>(item));

        return _mapper.Map<ICollection<PrestacaoServicoDto>>(result);
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetByPrestacoesServicosStatus(Guid prestadorId, ICollection<EPrestacaoServicoStatus> statusPrestacao)
    {
        return _mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestacoesServicosStatus(prestadorId, statusPrestacao));
    }

    public async Task<ICollection<PrestacaoServicoDto>> GetByPrestador(Guid prestadorId)
    {
        return _mapper.Map<ICollection<PrestacaoServicoDto>>(await _repository.GetByPrestador(prestadorId));
    }

    public async Task<PrestacaoServicoDto> UpdatePrestacaoServico(PrestacaoServicoDto prestacaoServico)
    {
        if (!prestacaoServico.PrestadorId.HasValue)
            prestacaoServico.PrestadorId = prestacaoServico.PrestadorId;

        if (prestacaoServico.Produtos != null)
        {
            foreach (var item in prestacaoServico.Produtos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Servicos != null)
        {

            foreach (var item in prestacaoServico.Servicos)
            {
                item.PrestadorId = prestacaoServico.PrestadorId.Value;
                item.UsrCadastro = prestacaoServico.UsrCadastro;
                item.UsrCadastroDesc = prestacaoServico.UsrCadastroDesc;
            }
        }

        if (prestacaoServico.Veiculo != null)
            prestacaoServico.Veiculo.PrestadorId = prestacaoServico.PrestadorId.Value;

        if (prestacaoServico.Cliente != null)
            prestacaoServico.Cliente.PrestadorId = prestacaoServico.PrestadorId.Value;


        var produtos = new List<ProdutoDto>();

        if (prestacaoServico.Produtos != null)
        {
            foreach (var prod in prestacaoServico.Produtos)
            {
                if (prod.Qtd == 0)
                    produtos.Add(prod);

                for (int i = 0; i < prod.Qtd; i++)
                {
                    produtos.Add(prod);
                }
            }
        }

        prestacaoServico.Produtos = produtos;

        var result = await _repository.Update(_mapper.Map<PrestacaoServico>(prestacaoServico));

        return _mapper.Map<PrestacaoServicoDto>(result);
    }
}
