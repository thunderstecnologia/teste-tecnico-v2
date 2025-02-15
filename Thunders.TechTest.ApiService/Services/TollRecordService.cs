using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services.Interfaces;

namespace Thunders.TechTest.ApiService.Services
{
    public class TollRecordService : ITollRecordService
    {
        private readonly ITollRecordRepository _repository;

        public TollRecordService(ITollRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<long> CreateAsync(TollRecordRequestDto request)
        {
            var entity = new TollRecord
            {
                Timestamp = request.Timestamp,
                TollBooth = request.TollBooth,
                City = request.City,
                State = request.State,
                AmountPaid = request.AmountPaid,
                VehicleType = request.VehicleType,
                LicensePlate = request.LicensePlate,
                TransactionId = request.TransactionId,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.CreateAsync(entity);
        }
    }
}
