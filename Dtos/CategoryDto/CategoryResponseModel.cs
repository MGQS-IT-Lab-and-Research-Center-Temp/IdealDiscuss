namespace IdealDiscuss.Dtos.CategoryDto
{
    public class CategoryResponseModel: BaseResponseModel
    {
        public ViewCategoryDto Data { get; set; }
    }

    public class CategoriesResponseModel: BaseResponseModel
    {
        public List<ViewCategoryDto> Data { get; set; }
    }
}
