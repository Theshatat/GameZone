
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesAppServices : ICategoriesAppServices
    {
        private readonly AppDbContext _context;

        public CategoriesAppServices(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetCategories()
        {
            return _context.categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
