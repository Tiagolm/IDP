using Domain.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        public UnitOfWork(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public Task Save()
        {
            UpdateEntityDates();
            return _applicationContext.SaveChangesAsync();
        }

        private void UpdateEntityDates()
        {
            foreach (var entry in _applicationContext.ChangeTracker.Entries())
            {
                if (typeof(Entity).IsAssignableFrom(entry.Entity.GetType()))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues[nameof(Entity.CreatedAt)] = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues[nameof(Entity.UpdatedAt)] = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
