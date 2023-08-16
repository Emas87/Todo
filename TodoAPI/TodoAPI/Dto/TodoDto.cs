namespace TodoAPI.Dto
{
    public class TodoDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
