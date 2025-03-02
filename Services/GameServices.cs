


namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;

        public GameServices(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}"; // wwwroot/assets/images/games
        }
        public async Task Create(CreateGameFormViewModel model)
        {

            var CoverName = await SaveCover(model.Cover);
            // Save the game to the database
            Game game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                Cover = CoverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId = d }).ToList()    
            };
            _context.Games.Add(game); // _context.Add(game); is also valid
            await _context.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            var game = _context.Games.Find(id);
            var isDeleted = false;
            if(game == null)
               return isDeleted;
            _context.Remove(game);
            var affectedRows = _context.SaveChanges();
            if(affectedRows > 0)
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                isDeleted = true;
            }
            return isDeleted;

        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g=>g.category)
                .Include(g=>g.Devices)
                .ThenInclude(gd => gd.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int Id)
        {
            return _context.Games
                .Include(g => g.category)
                .Include(g => g.Devices)
                .ThenInclude(gd => gd.Device)
                .AsNoTracking()
                .SingleOrDefault(g=>g.Id == Id);
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);

            if (game == null)
                return null;

            var hasNewName = model.Cover is not null;
            var OldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId = d}).ToList();

            if (hasNewName)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var AffectedRows = _context.SaveChanges();
            if(AffectedRows > 0)
            {
                if (hasNewName)
                {
                    var cover = Path.Combine(_imagePath, OldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
            
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var CoverPath = Path.Combine(_imagePath, CoverName);

            // Save the cover image to the server (wwwroot/assets/images/games)
            using var Stream = File.Create(CoverPath);
            await cover.CopyToAsync(Stream);
            //Stream.Dispose(); // No need to dispose the stream because it is inside a using block
            return CoverName;
        }
    }
}
