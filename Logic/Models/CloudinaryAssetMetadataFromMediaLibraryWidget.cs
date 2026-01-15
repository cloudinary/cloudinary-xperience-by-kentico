using System;

namespace CloudinaryDam.Logic.Models
{
    public class CloudinaryAssetMetadataFromMediaLibraryWidget
    {
        public string public_id { get; set; }
        public string resource_type { get; set; }
        public string type { get; set; }
        public string format { get; set; }
        public int version { get; set; }
        public string url { get; set; }
        public string secure_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bytes { get; set; }
        public float? duration { get; set; }
        public object[] tags { get; set; }
        public class Metadata
        {
        }
        public Metadata metadata { get; set; }
        public DateTime created_at { get; set; }
        public string access_mode { get; set; }
        public class Derived
        {
            public string url { get; set; }
            public string secure_url { get; set; }
            public string raw_transformation { get; set; }
        }
        public Derived[] derived { get; set; }
        public class Access_Control
        {
            public string access_type { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
        }
        public Access_Control[] access_control { get; set; }

        public class Created_By
        {
            public string type { get; set; }
            public string id { get; set; }
        }
        public Created_By created_by { get; set; }

        public class Uploaded_By
        {
            public string type { get; set; }
            public string id { get; set; }
        }
        public Uploaded_By uploaded_by { get; set; }
        public string folder_id { get; set; }
        public string id { get; set; }
        public string folder { get; set; }
    }
}
