using ExcelDataReader;
using Reconciliation.Business.Absctract;
using Reconciliation.Business.BusinessAspects;
using Reconciliation.Business.Constans;
using Reconciliation.Core.Aspects.Caching;
using Reconciliation.Core.Aspects.Performance;
using Reconciliation.Core.Aspects.Transaction;
using Reconciliation.Core.Utilities.Results.Abstract;
using Reconciliation.Core.Utilities.Results.Concrete;
using Reconciliation.DataAccess.Abstract;
using Reconciliation.Entities.Concrete;
using Reconciliation.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Concrete
{
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal _baBsReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;

        public BaBsReconciliationManager(IBaBsReconciliationDal baBsReconciliationDal, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _baBsReconciliationDal = baBsReconciliationDal;
            _currencyAccountService = currencyAccountService;
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Add(BaBsReconciliation baBsReconciliation)
        {
            string guid = Guid.NewGuid().ToString();
            baBsReconciliation.Guid = guid;
            _baBsReconciliationDal.Add(baBsReconciliation);
            return new SuccessResult(Messages.AddedBabsReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);


                        if (code != "Cari Kodu" && code != null)
                        {
                            string type = reader.GetString(1);
                            double mounth = reader.GetDouble(2);
                            double year = reader.GetDouble(3);
                            double quantity = reader.GetDouble(4);
                            double total = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetByCode(code, companyId).Data.Id;
                            string guid = Guid.NewGuid().ToString();

                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = Convert.ToInt16(mounth),
                                Year = Convert.ToInt16(year),
                                Quantity = Convert.ToInt16(quantity),
                                Total = Convert.ToInt16(total),
                                Guid = guid
                            };

                            _baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
            }
            File.Delete(filePath);

            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Delete(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Delete(baBsReconciliation);
            return new SuccessResult(Messages.UpdatedBabsReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(x => x.Guid == code));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliation> GetById(int id)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(x => x.Id == id));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliation>> GetList(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>(_baBsReconciliationDal.GetList(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliationDto>> GetListDto(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDto>>(_baBsReconciliationDal.GetAllDto(companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        public IResult SendReconciliationMail(BaBsReconciliationDto baBsReconciliationDto)
        {
            string subject = "Mutabakat Maili";
            string body = $"Şirket Adımız: {baBsReconciliationDto.CompanyName} <br /> " +
                $"Şirket Vergi Dairesi: {baBsReconciliationDto.CompanyTaxDepartment} <br />" +
                $"Şirket Vergi Numarası: {baBsReconciliationDto.CompanyTaxIdNumber} - {baBsReconciliationDto.CompanyIdentityNumber}<br /><hr>" +
                $"Sizin Şirket: {baBsReconciliationDto.AccountName}<br />" +
                $"Sizin Şirket Vergi Dairesi: {baBsReconciliationDto.AccountTaxDepartment} <br />" +
                $"Sizin Şirket Vergi Numarası: {baBsReconciliationDto.AccountTaxIdNumber} - {baBsReconciliationDto.AccountIdentityNumber}<br /><hr>" +
                $"Adet: {baBsReconciliationDto.Quantity} <br />" +
                $"Ay / Yıl: {baBsReconciliationDto.Mounth} / {baBsReconciliationDto.Year} <br />" +
                $"Tutar: {baBsReconciliationDto.Total} {baBsReconciliationDto.CurrencyCode} <br />";

            string link = "https://localhost:7042/api/BaBsReconciliations/GetByCode?code=" + baBsReconciliationDto.Guid;

            string linkDescription = "Mutabakatı Cevaplamak İçin Tıklayınız";

            var mailTemplate = _mailTemplateService.GetByTemplateName(3, "Kayıt");
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

            var mailParameter = _mailParameterService.Get(3);

            Entities.DTOs.SendMailDto sendMailDto = new Entities.DTOs.SendMailDto()
            {
                mailParameter = mailParameter.Data,
                email = baBsReconciliationDto.AccountEmail,
                subject = subject,
                body = templateBody
            };

            _mailService.SendMail(sendMailDto);

            return new SuccessResult(Messages.MailSendSuccessful);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Update,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Update(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Update(baBsReconciliation);
            return new SuccessResult(Messages.UpdatedBabsReconciliation);
        }
    }
}
