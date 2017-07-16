using AzureElasticPoolWithEFCore.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureElasticPoolWithEFCore.Application.Services
{
    public interface IApplicationService
    {
        Task<List<RoverEquipment>> GetRoverEquipments();
    }

    public class ApplicationService : IApplicationService
    {
        private readonly MassRoverContext _context;

        public ApplicationService(IContextService contextService)
        {
            _context = contextService.MassRoverContext;
        }

        public async Task<List<RoverEquipment>> GetRoverEquipments()
        {
            return await _context.RoverEquipment.ToListAsync();
        }
    }
}
