using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Enums
{
    public enum OperationType
    {
        CreateEducation=1,CreateGroup,
        GetAllEducation,GetAllGroups,
        GetByIdEducation,GetGroupById,GetAllGroupsWithEducationId,DeleteEducation,DeleteGroup,UpdateEducation,SearchEducation,GetAllWithGroups,SortWithCreatedDateEducation,UpdateGroup,SearchGroup,FilterByEducationName,SortWithCapacity
    }
}
