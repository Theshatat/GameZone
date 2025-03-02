

namespace GameZone.Services
{
    public interface ICategoriesAppServices
    {
        public IEnumerable<SelectListItem> GetCategories();
    }
}
