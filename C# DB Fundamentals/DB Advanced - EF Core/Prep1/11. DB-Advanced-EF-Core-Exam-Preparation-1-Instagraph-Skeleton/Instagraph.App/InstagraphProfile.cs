using AutoMapper;
using Instagraph.DataProcessor.SerializeTemplates;
using Instagraph.Models;
using Newtonsoft.Json.Linq;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<User, UserTemplate>()
                .ForMember(m => m.Followers, f => f.MapFrom(u => u.Followers.Count));
        }
    }
}
