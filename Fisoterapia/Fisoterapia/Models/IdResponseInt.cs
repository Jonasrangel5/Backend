namespace Fisoterapia.Models
{
    public record IdResponseInt
    {
        public int Id { get; set; }

        public IdResponseInt() { }

        public IdResponseInt(int _id)
        {
            Id = _id;
        }
    }
}
