namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class farmerDashboardModel
    {
        //-------------------------------START OF FILE--------------------------------//
        public productModel NewProduct { get; set; } = new productModel(); //used to create a product
        public IEnumerable<productModel> MyProducts { get; set; } = new List<productModel>();
    }
}
//-------------------------------END OF FILE--------------------------------//