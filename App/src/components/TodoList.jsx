import { useEffect, useState } from "react"
import { Accordion, Button, Form } from "react-bootstrap";
import TodoItem from "./TodoItem";
import axios from "axios";

function TodoList() {
    const [todos, setTodos] = useState([]);
    const [adding, setAdding] = useState(false);
    const [newName, setNewName] = useState("");
    const [newDescription, setNewDescription] = useState("");

    const addingTodo = () => {
        if(adding)
            return;
        setAdding(true);

    };
    const addTodo = () => {
        if(!adding)
            return;
        setAdding(false);
        
        axios.post('https://localhost:7124/api/Todo',
            {
                name:newName, description:newDescription
            }
        )
        .then(() => {
            getTodos();
            setNewDescription("");
            setNewName("");
        })
        .catch(error => {
            console.error('Error adding new Todo', error)
        });
    };
    const getTodos = () => {
        axios.get('https://localhost:7124/api/Todo')
        .then(response => {
            setTodos(response.data)
        })
        .catch(error => {
            console.error('Error fetching todos', error)
        });
    }
    const updateTodo = (todo) => {
        axios.put('https://localhost:7124/api/Todo',
            {
                ...todo
            }
        )
        .then(() => {
            getTodos();
        })
        .catch(error => {
            console.error('Error updating Todo', error)
        });
    }
    const onCheck = (index) => {
        const todo = todos[index];
        //todo.status = ! todo.status;
        updateTodo(todo);
    };
    const onDelete = (index) => {
        const todo = todos[index];
        deleteTodo(todo.id);
    };
    const deleteTodo = (id) => {
        axios.delete(`https://localhost:7124/api/Todo/${id}`)
        .then(() => {
            getTodos();
        })
        .catch(error => {
            console.error('Error updating Todo', error)
        });
    }
    useEffect(() => {
        getTodos();
    },[]);

    return (
        <div className="container">            
            <Accordion alwaysOpen>
                {todos.map((todo, index) => 
                    {
                        return <TodoItem onDelete={() => onDelete(index)} onCheck={() => onCheck(index)} key={index} todo={todo} index={index}></TodoItem>
                    }
                )}                
            </Accordion>
            <Button className="bi bi-plus-circle" onClick={addingTodo}>Todo</Button>
            {adding &&<Form className="hidden">
                <Form.Group>
                    <Form.Label>Todo Name</Form.Label>
                    <Form.Control value={newName} onChange={(e) => {setNewName(e.target.value)}}></Form.Control>
                    <Form.Label>Todo Description</Form.Label>
                    <Form.Control value={newDescription} onChange={(e) => {setNewDescription(e.target.value)}} as="textarea"></Form.Control>
                    <div className="container">
                        <Button className="centered-button" variant="success" type="submit" onClick={addTodo}>Add</Button>
                    </div>
                </Form.Group>
            </Form>}
        </div>
    )
}

export default TodoList