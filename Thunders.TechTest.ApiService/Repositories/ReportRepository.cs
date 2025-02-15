using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Repositories.Configurations;
using Thunders.TechTest.ApiService.Repositories.Interfaces;

namespace Thunders.TechTest.ApiService.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Report> SaveReportRequestAsync(Report request)
        {
            _context.Set<Report>().Add(request);
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
