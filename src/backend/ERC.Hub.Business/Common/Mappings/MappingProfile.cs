using AutoMapper;
using ERC.Hub.Business.Departments.Queries;
using ERC.Hub.Business.Employees.Queries;
using ERC.Hub.Common.Enums;
using ERC.Hub.Common.Extensions;
using ERC.Hub.Data.Models;

namespace ERC.Hub.Business.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
               .ForMember(d => d.EmploymentType, o => o.MapFrom(s => ((EmploymentType)s.EmploymentType).GetDisplayName()))
               .ForMember(d => d.Department, o => o.MapFrom(s => s.Department))
               .ForMember(d => d.Position, o => o.MapFrom(s => PositionMapping(s.Position!)));

            CreateMap<Department, DepartmentDto>();
        }
        private static object PositionMapping(Position position) => new { name = position.Name };
    }
}
