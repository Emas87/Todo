using System.Diagnostics.Metrics;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Todos.Any())
            {
                var todo = new List<Todo>()
                {
                    new Todo()
                    {
                        Name = "Wash Car",
                        Description = "Wash Car with water",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdateDate = DateTime.Now.ToUniversalTime(),
                        Status = true
                    },
                    new Todo()
                    {
                        Name = "Cook",
                        Description = "Cook dinner",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdateDate = DateTime.Now.ToUniversalTime(),
                        Status = false
                    },
                    new Todo()
                    {
                        Name = "Eat",
                        Description = "Eat dinner",
                        CreatedDate = DateTime.Now.ToUniversalTime(),
                        UpdateDate = DateTime.Now.ToUniversalTime(),
                        Status = false
                    },
                };
                dataContext.Todos.AddRange(todo);
                dataContext.SaveChanges();
            }
        }
    }
}
