﻿using ExcelDataReader;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.Concrete
{
    public class BaBsReconciliationDetailsManager : IBaBsReconciliationDetailsService
    {
        private readonly IBaBsReconciliationDetailsDal _baBsReconciliationDetailsDal;

        public BaBsReconciliationDetailsManager(IBaBsReconciliationDetailsDal baBsReconciliationDetailsDal)
        {
            _baBsReconciliationDetailsDal = baBsReconciliationDetailsDal;
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailsService.Get")]
        public IResult Add(BaBsReconciliationDetail babsReconciliationDetail)
        {
            _baBsReconciliationDetailsDal.Add(babsReconciliationDetail);
            return new SuccessResult(Messages.AddedBabsReconciliationDetail);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailsService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int babsReconciliationId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);

                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double amount = reader.GetDouble(2);

                            BaBsReconciliationDetail baBsReconciliationDetail = new BaBsReconciliationDetail()
                            {
                                BaBsReconciliationId = babsReconciliationId,
                                Date = date,
                                Description = description,
                                Amount = Convert.ToDecimal(amount)
                            };

                            _baBsReconciliationDetailsDal.Add(baBsReconciliationDetail);
                        }
                    }
                }
            }
            File.Delete(filePath);

            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.Delete,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailsService.Get")]
        public IResult Delete(BaBsReconciliationDetail babsReconciliationDetail)
        {
            _baBsReconciliationDetailsDal.Delete(babsReconciliationDetail);
            return new SuccessResult(Messages.DeletedBabsReconciliationDetail);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<BaBsReconciliationDetail>(_baBsReconciliationDetailsDal.Get(x => x.Id == id));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliationDetail>> GetList(int babsReconciliationId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDetail>>(_baBsReconciliationDetailsDal.GetList(x => x.BaBsReconciliationId == babsReconciliationId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliationDetail.Update,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailsService.Get")]
        public IResult Update(BaBsReconciliationDetail babsReconciliationDetail)
        {
            _baBsReconciliationDetailsDal.Update(babsReconciliationDetail);
            return new SuccessResult(Messages.UpdatedBabsReconciliationDetail);
        }
    }
}
