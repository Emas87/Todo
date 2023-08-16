namespace TodoAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime UpdateDate { get; set;}
        public bool Status { get; set; }
        
    }
}
