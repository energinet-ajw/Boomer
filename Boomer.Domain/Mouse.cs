namespace Boomer.Domain
{
    public class Mouse
    {
        public Mouse() {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
