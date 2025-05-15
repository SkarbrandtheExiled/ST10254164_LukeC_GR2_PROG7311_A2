namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class farmerDashboardModel
    {
        public productModel NewProduct { get; set; } = new productModel();
        public IEnumerable<productModel> MyProducts { get; set; } = new List<productModel>();
    }
}
