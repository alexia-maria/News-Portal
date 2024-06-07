namespace LAB7_WEB.Models
{
    public class ComentariuModel
    {
        public int Id { get; set; }
        public int StireId { get; set; }
        public StireModel? Stire { get; set; }
        public string Autor { get; set; }
        public string Continut { get; set; }
        public DateTime Data { get; set; }
    }
}
