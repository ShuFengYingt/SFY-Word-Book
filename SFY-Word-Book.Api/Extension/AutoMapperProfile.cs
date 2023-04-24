using AutoMapper;
using SFY_Word_Book.Api.Context;
using SFY_Word_Book.Shared.Dtos;

namespace SFY_Word_Book.Api.Extension
{
    /// <summary>
    /// AutoMapper  映射
    /// </summary>
    public class AutoMapperProfile : MapperConfigurationExpression
    {
        public AutoMapperProfile()
        {
            //构建数据库到类的映射，使用AutoMapper Nuget包完成，记得到builder.service中注入一下
            CreateMap<UserInfo,UserDto>().ReverseMap();
        }
    }
}
