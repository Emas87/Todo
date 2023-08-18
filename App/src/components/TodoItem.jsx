import PropTypes from 'prop-types'
import Card  from 'react-bootstrap/Card';
import { useAccordionButton } from 'react-bootstrap/AccordionButton';
import Accordion from 'react-bootstrap/Accordion';
import { Button, InputGroup } from 'react-bootstrap';
import { useState } from 'react';


function CustomToggle({ children, eventKey, onCheck, todo, onDelete}) {
    const decoratedOnClick = useAccordionButton(eventKey, () =>
      {}
    );
    const [checked, setChecked] = useState(todo.status);
    const onChange = (event) => {
        checked
        todo.status = event.target.checked;
        setChecked(todo.status);
        onCheck();
    };
  
    return (
        <InputGroup >
            <InputGroup.Checkbox checked={checked} onChange={onChange} aria-label="Checkbox for todo item"/>
            <InputGroup.Text className="card-header" aria-label="Todo Header" onClick={decoratedOnClick}>
                {children}
            </InputGroup.Text>
            <Button className="bi bi-trash" aria-label="Delete" variant="danger" onClick={onDelete}></Button>
        </InputGroup>
    );
  }
  CustomToggle.propTypes = {
    children: PropTypes.string.isRequired,
    eventKey: PropTypes.number.isRequired,
    onCheck: PropTypes.func.isRequired,
    onDelete: PropTypes.func.isRequired,
    todo: PropTypes.object.isRequired
};

function TodoItem({todo, index, onCheck, onDelete}) {
  return (
    <Card>
        <CustomToggle onDelete={onDelete} todo={todo} onCheck={onCheck} eventKey={index}>{todo.name}</CustomToggle>
        <Accordion.Collapse eventKey={index}>
            <Card.Body>
                {todo.description}
            </Card.Body>
        </Accordion.Collapse>
    </Card>
  )
}

TodoItem.propTypes = {
    todo: PropTypes.shape({
      name: PropTypes.string.isRequired,
      description: PropTypes.string.isRequired,
      // Add other prop validations as needed
    }).isRequired,
    index: PropTypes.number.isRequired,
    onCheck: PropTypes.func.isRequired,
    onDelete: PropTypes.func.isRequired
  };

export default TodoItem