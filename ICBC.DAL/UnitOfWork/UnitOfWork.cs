using System;

using Microsoft.EntityFrameworkCore;
using ICBC.DAL.Entities;
using ICBC.DAL.Repository;
using ICBC.DAL.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ICBC.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly ICBCContext _dbContext;

        public UnitOfWork(ICBCContext dbContext)
        {
            _dbContext = dbContext;
        }

        private RepositoryBase<Trade> _tradeRepository;
        public RepositoryBase<Trade> TradeRepository
        {
            get
            {

                if (this._tradeRepository == null)
                {
                    this._tradeRepository = new RepositoryBase<Trade>(_dbContext);
                }
                return _tradeRepository;
            }
        }

        public void LoadData()
        {
            if (TradeRepository.GetList().Count > 0) { return; }
            Trade[] newTrades = new Trade[] {
                new Trade()  {Instrument = "Bond", BusinessUnit = "BUI",  ProfitCentre= 101,ReportingAmount = 5000 },
                new Trade() { Instrument = "Bond", BusinessUnit = "BUI", ProfitCentre = 103, ReportingAmount = 1000 },
                new Trade() { Instrument = "CDS", BusinessUnit = "BUI", ProfitCentre = 101, ReportingAmount = 5000 },
                new Trade() { Instrument = "CDS", BusinessUnit = "BU2", ProfitCentre = 101, ReportingAmount = 3000 },
                new Trade() { Instrument = "Equity", BusinessUnit = "BU2", ProfitCentre = 105, ReportingAmount = 6000 }
            };

            _tradeRepository.AddRange(newTrades);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}