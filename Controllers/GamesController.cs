using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoriesAppServices _categoriesAppServices;
        private readonly IDeviceAppServices _deviceAppServices;
        private readonly IGameServices _gameServices;

        public GamesController(AppDbContext context,
            ICategoriesAppServices categoriesAppServices,
            IDeviceAppServices deviceAppServices,
            IGameServices gameServices)
        {
            _context = context;
            _categoriesAppServices = categoriesAppServices;
            _deviceAppServices = deviceAppServices;
            _gameServices = gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel GameForm = new()
            {
                Categories = _categoriesAppServices.GetCategories(),
                Devices = _deviceAppServices.GetDevices()
            };
            return View(GameForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                Model.Categories = _categoriesAppServices.GetCategories();
                Model.Devices = _deviceAppServices.GetDevices();
                return View(Model);
            }
            await _gameServices.Create(Model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gameServices.GetById(id);
            if (game == null)
            {
                return NotFound();
            }

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesAppServices.GetCategories(),
                Devices = _deviceAppServices.GetDevices(),
                CurrentCover = game.Cover
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                Model.Categories = _categoriesAppServices.GetCategories();
                Model.Devices = _deviceAppServices.GetDevices();
                return View(Model);
            }
            var game = await _gameServices.Update(Model);
            if (game == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var game = _gameServices.Delete(id);
            if (game == null)
                return NotFound();
            return Ok();
        }
    }
}
