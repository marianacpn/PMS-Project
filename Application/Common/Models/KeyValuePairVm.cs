using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public class KeyValuePairVm : IMapFrom
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string InternalValue { get; set; }
        public bool Status { get; set; }


        public int RelationalEntityId { get; set; }
        public KeyValuePairVm RelationalEntity { get; set; }

        public void Mapping(Profile profile)
        {
          
        }
    }
}
