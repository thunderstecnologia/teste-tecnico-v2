using Rebus.Bus;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports.Strategies.Interfaces;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Services.Interfaces;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Mapper.Interfaces;

namespace Thunders.TechTest.ApiService.Services
{
    public class ReportService : IReportService
    {
        private readonly IBus _bus;
        private readonly IReportRepository _reportRepository;
        private readonly IReportMapper _reportMapper;

        public ReportService(
            IBus bus, 
            IReportRepository reportRepository,
            IReportMapper reportMapper)
        {
            _bus = bus;
            _reportRepository = reportRepository;
            _reportMapper = reportMapper;
        }

        public async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(TotalValueByHourByCityReportRequestDto request)
        {
            var reportRequest = _reportMapper.MapToCreateReportRequest(request);
            var savedReport = await _reportRepository.SaveReportRequestAsync(reportRequest);
            var message = _reportMapper.MapToGenerateReportMessage(request);
            message.ReportId = savedReport.Id;
            await _bus.Send(message);
            return _reportMapper.MapToReportRequestResponse(savedReport);
        }

        public async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(TopTollPlazasReportRequestDto request)
        {
            var reportRequest = _reportMapper.MapToCreateReportRequest(request);
            var savedReport = await _reportRepository.SaveReportRequestAsync(reportRequest);
            var message = _reportMapper.MapToGenerateReportMessage(request);
            message.ReportId = savedReport.Id;
            await _bus.Send(message);
            return _reportMapper.MapToReportRequestResponse(savedReport);
        }

        public async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(VehicleTypeCountReportRequestDto request)
        {
            var reportRequest = _reportMapper.MapToCreateReportRequest(request);
            var savedReport = await _reportRepository.SaveReportRequestAsync(reportRequest);
            var message = _reportMapper.MapToGenerateReportMessage(request);
            message.ReportId = savedReport.Id;
            await _bus.Send(message);
            return _reportMapper.MapToReportRequestResponse(savedReport);
        }


    }
}
