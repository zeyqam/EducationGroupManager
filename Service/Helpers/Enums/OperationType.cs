using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        GetAllEducation=1,
        GetByIdEducation,DeleteEducation,UpdateEducation,SearchEducation,GetAllWithGroups,SortWithCreatedDateEducation,GetAllgroup,GetByIdGroup,DeleteGroup,UpdateGroup,SearchGroup,FilterByEducationName,GetAllWithEducationId,SortWithCapacity
    }
}
