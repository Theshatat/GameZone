
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DeviceAppServices : IDeviceAppServices
    {
        private readonly AppDbContext _context;

        public DeviceAppServices(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetDevices()
        {
            return _context.Devices
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
