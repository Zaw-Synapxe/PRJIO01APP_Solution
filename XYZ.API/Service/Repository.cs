using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XYZ.API.Data;

namespace XYZ.API.Service
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Int64 id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            _entities.Add(entity);
        }
        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _entities.Update(entity);
        }
        public async Task Delete(Int64 id)
        {
            T entity = await _entities.FindAsync(id);
            _entities.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}

// https://dotnettutorials.net/lesson/transactions-in-entity-framework-core/

//public class Student
//{
//    public int StudentId { get; set; }
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//}

//Asynchronous Transactions in Entity Framework Core
//static async Task Main(string[] args)
//{
//    try
//    {
//        using (var context = new EFCoreDbContext())
//        {
//            // Begin a transaction asynchronously
//            await using (var transaction = await context.Database.BeginTransactionAsync())
//            {
//                try
//                {
//                    // Perform database operations within the transaction
//                    Student std1 = new Student()
//                    {
//                        FirstName = "Pranaya",
//                        LastName = "Rout"
//                    };
//                    context.Students.Add(std1);
//                    // SaveChangesAsync() but do not commit yet, this will persist changes but hold the commit until
//                    // we are sure that all operations succeed
//                    await context.SaveChangesAsync();
//                    Student std2 = new Student()
//                    {
//                        FirstName = "Tarun",
//                        LastName = "Kumar"
//                    };
//                    context.Students.Add(std2);
//                    // SaveChangesAsync() but do not commit yet, this will persist changes but hold the commit until
//                    // we are sure that all operations succeed
//                    await context.SaveChangesAsync();
//                    // If everything is fine until here, commit the transaction
//                    await transaction.CommitAsync();
//                    Console.WriteLine("Entities are Saved");
//                }
//                catch (Exception ex)
//                {
//                    // If there is any error, roll back all changes
//                    await transaction.RollbackAsync();
//                    // Handle or throw the exception as needed
//                    Console.WriteLine(ex.Message);
//                    throw;
//                }
//            }
//        }
//        Console.Read();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}"); ;
//    }
//}

//Distributed Transaction using TransactionScope in EF Core:
//static async Task Main(string[] args)
//{
//    try
//    {
//        // Define the scope of the transaction
//        var options = new TransactionOptions
//        {
//            IsolationLevel = IsolationLevel.ReadCommitted,
//            Timeout = TransactionManager.DefaultTimeout // Default is 1 minute
//        };
//        // Start a new TransactionScope
//        using (var scope = new TransactionScope(TransactionScopeOption.Required, options))
//        {
//            try
//            {
//                using (var context1 = new EFCoreDbContext())
//                {
//                    // Perform data access using context1 here
//                    Student std1 = new Student()
//                    {
//                        FirstName = "Pranaya",
//                        LastName = "Rout"
//                    };
//                    context1.Students.Add(std1);
//                    context1.SaveChanges();
//                } // The actual INSERT command is sent to the database here
//                  // You can even use another context or database operation here
//                using (var context2 = new EFCoreDbContext())
//                {
//                    // Perform data access using context2 here
//                    Student std2 = new Student()
//                    {
//                        FirstName = "Rakesh",
//                        LastName = "Kumar"
//                    };
//                    context2.Students.Add(std2);
//                    context2.SaveChanges();
//                } // The actual INSERT command is sent to the database here
//                  // Complete the scope here, if everything succeeded
//                  // If all operations complete successfully, commit the transaction
//                scope.Complete();
//                Console.WriteLine("Entities are Saved");
//            }
//            catch (Exception ex)
//            {
//                // Handle errors and the transaction will be rolled back
//                Console.WriteLine(ex.Message);
//                // The TransactionScope is disposed without calling Complete(), so the transaction will be rolled back
//                // Handle the exception as needed
//                Console.WriteLine(ex.Message);
//            }
//        } // The TransactionScope is disposed here, committing or rolling back the transaction
//        Console.Read();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}"); ;
//    }
//}







