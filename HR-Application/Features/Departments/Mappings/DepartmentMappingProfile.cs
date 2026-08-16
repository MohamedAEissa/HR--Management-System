using AutoMapper;
using HR_Application.Features.Departments.DTOs;
using HR_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Application.Features.Departments.Mappings
{
    public class DepartmentMappingProfile:Profile
    {
        public DepartmentMappingProfile() {

            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();


            CreateMap<Department,DepartmentResponseDto>();
        }
    }
}
