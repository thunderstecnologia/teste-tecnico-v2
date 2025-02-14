using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services.Internal.Interfaces;

namespace Thunders.TechTest.ApiService.Services.Internal
{
    public class TollRecordInternalService : ITollRecordInternalService
    {
        private readonly ITollRecordRepository _repository;

        public TollRecordInternalService(ITollRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TollRecordResponseDto>> GetAllAsync()
        {
            var records = await _repository.GetAllAsync()
                ?? Enumerable.Empty<TollRecord>();

            return records
                .Where(record => record != null)
                .Select(record => new TollRecordResponseDto
                {
                    Id = record!.Id,
                    Timestamp = record.Timestamp,
                    TollBooth = record.TollBooth,
                    City = record.City,
                    State = record.State,
                    AmountPaid = record.AmountPaid,
                    VehicleType = record.VehicleType,
                    LicensePlate = record.LicensePlate,
                    TransactionId = record.TransactionId
                })
                .ToList();
        }

        public async Task<TollRecordResponseDto?> GetByIdAsync(long id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
                return null;

            return new TollRecordResponseDto
            {
                Id = record.Id,
                Timestamp = record.Timestamp,
                TollBooth = record.TollBooth,
                City = record.City,
                State = record.State,
                AmountPaid = record.AmountPaid,
                VehicleType = record.VehicleType,
                LicensePlate = record.LicensePlate,
                TransactionId = record.TransactionId
            };
        }

        public async Task<IEnumerable<TollRecordResponseDto?>> GetByLicensePlateAsync(string licensePlate)
        {
            var records = await _repository.GetByLicensePlateAsync(licensePlate)
                  ?? Enumerable.Empty<TollRecord>();

            return records
                   .Where(record => record != null)
                   .Select(record => new TollRecordResponseDto
                   {
                       Id = record!.Id,
                       Timestamp = record.Timestamp,
                       TollBooth = record.TollBooth,
                       City = record.City,
                       State = record.State,
                       AmountPaid = record.AmountPaid,
                       VehicleType = record.VehicleType,
                       LicensePlate = record.LicensePlate,
                       TransactionId = record.TransactionId
                   })
                   .ToList();
        }

        public async Task<IEnumerable<TollRecordResponseDto?>> GetByTollBoothIdAsync(string tollBoothId)
        {
            var records = await _repository.GetByTollBoothIdAsync(tollBoothId)
                  ?? Enumerable.Empty<TollRecord>();

            return records
                   .Where(record => record != null)
                   .Select(record => new TollRecordResponseDto
                   {
                       Id = record!.Id,
                       Timestamp = record.Timestamp,
                       TollBooth = record.TollBooth,
                       City = record.City,
                       State = record.State,
                       AmountPaid = record.AmountPaid,
                       VehicleType = record.VehicleType,
                       LicensePlate = record.LicensePlate,
                       TransactionId = record.TransactionId
                   })
                   .ToList();
        }

        public async Task MarkAsDeletedAsync(MarkAsDeletedRequestDto request)
        {
            await _repository.MarkAsDeletedAsync(request.Id, request.DeletedBy);
        }
    }
}
