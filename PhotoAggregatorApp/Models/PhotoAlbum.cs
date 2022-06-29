using System.Collections.Generic;

namespace PhotoAggregatorApp.Models
{
    public class PhotoAlbum
    {
        public int AlbumID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; }

        public List<PhotoAggregatorModel> Photos { get; set; }
    }
}
