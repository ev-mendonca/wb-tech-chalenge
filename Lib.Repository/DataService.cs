using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public interface IDataService
    {
        Task ExecutaMigrations();
    }
    public class DataService : IDataService
    {
        private readonly Context _context;
        public DataService(Context context)
        {
            _context = context;
        }
        public async Task ExecutaMigrations()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
