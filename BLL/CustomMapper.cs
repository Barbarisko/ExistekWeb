using AutoMapper;
using BLL.ModelsNew;
using DataAccess.Entities;


namespace BusinessLogic
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<ArticleModel, Article>()
                .ForMember(d => d.ArticleTags, opt => opt.MapFrom(src => src.ArticleTags))
                .ReverseMap();

            //as well for other models
        }
    }
}
