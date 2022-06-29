namespace PhotoAggregatorApp.Models
{
    public class PhotoAggregatorModel
    {
        public int ID { get; set; }

        public int AlbumID { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }

        public string ThumbNailURL { get; set; }
    }
}
