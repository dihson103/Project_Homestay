namespace HomestayWeb.Dtos
{
    public class VoteDto
    {
        public int UserId { get; set; }
        public int HomestayId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string VoterName { get; set; }
    }
}
