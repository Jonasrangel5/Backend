namespace Fisoterapia.Models
{
    public record IdResponse
    {
        public Guid Id { get; set; }

        public IdResponse() { }

        public IdResponse(Guid _id)
        {
            Id = _id;
        }
    }
}
