using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using BeepBong.DataAccess;

namespace BeepBong.DataAccess.Test
{
    public static class InMemoryContext
    {
        public static DbContextOptions<BeepBongContext> ContextGenerator(string name) {
            return new DbContextOptionsBuilder<BeepBongContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}
