namespace IdealDiscuss.Models.Category;

public class CategoryResponseModel : BaseResponseModel
{
    public CategoryViewModel Data { get; set; }
}

public class CategoriesResponseModel : BaseResponseModel
{
    public List<CategoryViewModel> Data { get; set; }
}
