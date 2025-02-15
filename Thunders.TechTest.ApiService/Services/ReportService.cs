using Rebus.Bus;
using System.Text.Json;
using Thunders.TechTest.ApiService.Dto.Request;
using Thunders.TechTest.ApiService.Dto.Response;
using Thunders.TechTest.ApiService.Mapper.Interfaces;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Reports;
using Thunders.TechTest.ApiService.Repositories.Interfaces;
using Thunders.TechTest.ApiService.Services.Interfaces;

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
            var message = _reportMapper.MapToGenerateReportMessage(request);
            return await GenerateAndSaveReportAsync(reportRequest, message);
        }

        public async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(TopTollPlazasReportRequestDto request)
        {
            var reportRequest = _reportMapper.MapToCreateReportRequest(request);
            var message = _reportMapper.MapToGenerateReportMessage(request);
            return await GenerateAndSaveReportAsync(reportRequest, message);
        }

        public async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(VehicleTypeCountReportRequestDto request)
        {
            var reportRequest = _reportMapper.MapToCreateReportRequest(request);
            var message = _reportMapper.MapToGenerateReportMessage(request);
            return await GenerateAndSaveReportAsync(reportRequest, message);
        }

        public async Task<string> GetReportByIdAsync(long reportId)
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (report == null)
            {
                throw new KeyNotFoundException($"Report with ID {reportId} not found.");
            }
            return JsonSerializer.Serialize(report.Data);
        }

        private async Task<ReportRequestResponseDto> GenerateAndSaveReportAsync(Report reportRequest, GenerateReportMessage message)
        {
            var savedReport = await _reportRepository.SaveReportRequestAsync(reportRequest);
            message.ReportId = savedReport.Id;
            await _bus.Send(message);
            return _reportMapper.MapToReportRequestResponse(savedReport);
        }
    }
}
