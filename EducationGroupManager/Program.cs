
using EducationGroupManager.Controllers;

EducationController educationController= new EducationController();
//await educationController.CreateEducationAsync();
GroupController groupController = new GroupController();
//await groupController.GroupCreate();
//await groupController.GetAllGroupsAsync();
await educationController.GetAllEducationAsync();