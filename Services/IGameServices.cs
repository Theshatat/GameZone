namespace GameZone.Services
{
    public interface IGameServices
    {
        public IEnumerable<Game> GetAll();
        public Task Create(CreateGameFormViewModel model);
        public Game? GetById(int Id);
        public Task<Game?> Update(EditGameFormViewModel model);
        public bool Delete(int id);
    }
}
