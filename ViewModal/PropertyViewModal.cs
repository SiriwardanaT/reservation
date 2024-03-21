namespace test.ViewModal
{
    public class PropertyViewModal
    {
        public string Pid { get; set; }

        public double Price { get; set; }

        public string Facilities { get; set; }

        public int CountRoom { get; set; }

        public string landownerid { get; set; }

        public string location { get; set; }

        public List<IFormFile>  Image { get; set; }

        public string UrlList { get; set; }

        public double lat { get; set; }
        public double lng { get; set; }

        public int? status { get; set; }

        public string? reason { get; set; }

    }
}
