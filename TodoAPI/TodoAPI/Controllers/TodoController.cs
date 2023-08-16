using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using TodoAPI.Dto;
using TodoAPI.Interfaces;
using TodoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository todoRepository, IMapper mapper )
        {
            this._todoRepository = todoRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Todo>))]
        public IActionResult GetAll()
        {
            var todos = _mapper.Map<List< TodoDto>>(_todoRepository.GetAll());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(todos);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Todo todo)
        {
            if(todo.Name == null || todo.Description == null)
            {
                return BadRequest(ModelState);
            }
            var oldTodo = _todoRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == todo.Name.Trim().ToUpper())
                .FirstOrDefault();
            if (oldTodo != null)
            {
                ModelState.AddModelError("", "Todo already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_todoRepository.Create(todo.Name, todo.Description))
            {
                ModelState.AddModelError("", "Something went wrong while creating todo");
                return StatusCode(500, ModelState);
            }
            if (!_todoRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving todo");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut()]
        [ProducesResponseType(200)]
        public IActionResult Put([FromBody] Todo todo)
        {
            var oldTodo = _todoRepository.TodoExists(todo.Id);
            if (oldTodo == null)
            {
                return NotFound();
            }

            if (todo.Name == null || todo.Description == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_todoRepository.Update(todo, oldTodo))
            {
                ModelState.AddModelError("", "Something went wrong while updating todo");
                return StatusCode(500, ModelState);
            }
            if (!_todoRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving todo");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(int id)
        {
            var oldTodo = _todoRepository.TodoExists(id);
            if (oldTodo == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_todoRepository.Delete(oldTodo))
            {
                ModelState.AddModelError("", "Something went wrong while deleting todo");
                return StatusCode(500, ModelState);
            }
            if (!_todoRepository.Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving DB");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}
