using LAB7_WEB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB7_WEB.ContextModels;

public class StiriContext:DbContext
{
    public StiriContext(DbContextOptions<StiriContext> options): base(options)
    {

    }

    public DbSet<StireModel> Stiri { get; set; }
    public DbSet<CategorieModel> Categorii { get; set; }
    public DbSet<UserModel> User { get; set; }
    public DbSet<ComentariuModel> Comentarii { get; set; }
}
