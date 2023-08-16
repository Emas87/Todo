using TodoAPI.Models;

namespace TodoAPI.Interfaces
{
    public interface ITodoRepository
    {
        ICollection<Todo> GetAll();
        Todo? TodoExists(int id);
        bool Delete(Todo todo);

        bool Create(string name, string description);
        bool Update(Todo todo, Todo oldTodo);
        bool Save();
    }
}
