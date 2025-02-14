using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Thunders.TechTest.ApiService.Enums;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Repositories
{
    public class TollRecordRepository : ITollRecordRepository
    {
        private readonly AppDbContext _context;

        public TollRecordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(TollRecord entity)
        {
            await _context.TollRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException("O método DeleteAsync não está disponível para esta entidade.");
        }

        public async Task<IEnumerable<TollRecord?>> GetAllAsync()
        {
            return await _context.TollRecords
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<TollRecord?> GetByIdAsync(long id)
        {
            return await _context.TollRecords
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<IEnumerable<TollRecord?>> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.TollRecords
                .Where(t => t.LicensePlate == licensePlate && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<TollRecord?>> GetByTollBoothIdAsync(string tollBoothId)
        {
            return await _context.TollRecords
                .Where(t => t.TollBooth == tollBoothId && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task MarkAsDeletedAsync(long id, string deletedBy)
        {
            var existingRecord = await _context.TollRecords.FindAsync(id);
            if (existingRecord != null)
            {
                existingRecord.IsDeleted = true;
                existingRecord.DeletedBy = deletedBy;
                existingRecord.DeletedAt = DateTime.UtcNow;

                var newRecord = new TollRecord
                {
                    Timestamp = existingRecord.Timestamp,
                    TollBooth = existingRecord.TollBooth,
                    City = existingRecord.City,
                    State = existingRecord.State,
                    AmountPaid = existingRecord.AmountPaid,
                    VehicleType = existingRecord.VehicleType,
                    LicensePlate = existingRecord.LicensePlate,
                    TransactionId = existingRecord.TransactionId,
                    CreatedBy = existingRecord.CreatedBy,
                    CreatedAt = existingRecord.CreatedAt,
                    IsDeleted = false,
                    PreviousRecordId = existingRecord.PreviousRecordId,
                };

                _context.TollRecords.Update(existingRecord);
                await _context.TollRecords.AddAsync(newRecord);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateAsync(TollRecord entity)
        {
            throw new NotImplementedException("O método UpdateAsync não está disponível para esta entidade.");
        }
    }
}

