using System.Xml.Linq;
using TodoAPI.Data;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _dataContext;
        public TodoRepository(DataContext context) 
        { 
            _dataContext = context;
        }
        public ICollection<Todo> GetAll()
        {
            return _dataContext.Todos.OrderBy(p => p.Id).ToList();
        }

        public Todo? TodoExists(int id)
        {
            Todo? todo = _dataContext.Todos.FirstOrDefault(d => d.Id == id);            
            return todo;
        }

        public bool Delete(Todo todo)
        {
            _dataContext.Todos.Remove(todo);
            return true;
        }

        public bool Create(string name, string description)
        {
            Todo newTodo = new Todo() { 
                Name = name,
                Description = description,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime(),
                Status = false
            };
            _dataContext.Todos.Add(newTodo);
            return true;
        }
        public bool Update(Todo todo, Todo oldTodo)
        {
            oldTodo.Name = todo.Name;
            oldTodo.Description = todo.Description;
            oldTodo.UpdateDate = DateTime.Now.ToUniversalTime();
            oldTodo.Status = todo.Status;            
            _dataContext.Update(oldTodo);
            return true;
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
