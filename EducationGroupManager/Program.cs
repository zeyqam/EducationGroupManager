
using EducationGroupManager.Controllers;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;

EducationController educationController= new EducationController();
//await educationController.CreateEducationAsync();
GroupController groupController = new GroupController();
//await groupController.GroupCreate();
//await groupController.GetAllGroupsAsync();
//await educationController.GetAllEducationAsync();

while (true)
{
    GetMenus();
    Operation: string operationStr=Console.ReadLine();
    int operation;
    bool isCorrectOperationFormat=int.TryParse(operationStr, out operation);
    if (isCorrectOperationFormat)
    {
        switch(operation)
        {
            case (int)OperationType.CreateEducation:
                educationController.CreateEducationAsync();
                break;

            case (int)OperationType.CreateGroup:
                groupController.GroupCreateAsync();
                break;
            case (int)OperationType.GetAllEducation:
                educationController.GetAllEducationAsync();
                break;
            case (int)OperationType.GetAllGroups:
                groupController.GetAllGroupsAsync();
                break;
            case (int)OperationType.GetByIdEducation:
                educationController.GetEducationByIdAsync();
                break;
            case (int)OperationType.GetGroupById:
                groupController.GetGroupByIdAsync();
                break;
            case (int)OperationType.GetAllGroupsWithEducationId:
                groupController.GetGroupsByEducationIdAsync();
                break;
            case (int)OperationType.DeleteEducation:
                educationController.DeleteEducationAsync();
                break;
            case (int)OperationType.DeleteGroup:
                groupController.DeleteGroupsAsync();
                break;
            case (int)OperationType.UpdateEducation:
                educationController.UpdateEducationAsync();
                break;
            case (int)OperationType.UpdateGroup:
                groupController.UpdateGroupAsync();
                break;
            case (int)OperationType.SearchEducation:
                educationController.SearchEducationByNameAsync();
                break;
            case (int)OperationType.SearchGroup:
                groupController.SearchGroupByNameAsync();
                break;
            case (int)OperationType.GetAllWithGroups:
                educationController.GetAllWithGroupsAsync();
                break;
            case (int)OperationType.SortWithCreatedDateEducation:
                educationController.SortByCreatedDateAsync();
                break;
            case (int)OperationType.FilterByEducationName:
                groupController.FilterByEducationNameAsync();
                break;
            case (int)OperationType.SortWithCapacity:
                groupController.SortByCapacityAsync();
                break;










            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong,please choose again");
                goto Operation;
                break;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong,please add operation again");
        goto Operation;
    }
}
static void GetMenus()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
    Console.WriteLine("║                     Menu Operations                     ║");
    Console.WriteLine("╠══════╦══════════════════════════════════════════════════╣");
    Console.WriteLine("║ Code ║                 Operation                        ║");
    Console.WriteLine("╠══════╬══════════════════════════════════════════════════╣");
    Console.WriteLine("║   1  ║ Create Education                                 ║");
    Console.WriteLine("║   2  ║ Create Group                                     ║");
    Console.WriteLine("║   3  ║ GetAllEducation                                  ║");
    Console.WriteLine("║   4  ║ GetAllGroups                                     ║");
    Console.WriteLine("║   5  ║ GetEducationById                                 ║");
    Console.WriteLine("║   6  ║ GetGroupsBiyId                                   ║");
    Console.WriteLine("║   7  ║ GetAllGroupsWithEducationId                      ║");
    Console.WriteLine("║   8  ║ DeleteEducation                                  ║");
    Console.WriteLine("║   9  ║ DeleteGroup                                      ║");
    Console.WriteLine("║  10  ║ UpdateEducation                                  ║");
    Console.WriteLine("║  11  ║ UpdateGroup                                      ║");
    Console.WriteLine("║  12  ║ SearchEducation                                  ║");
    Console.WriteLine("║  13  ║ SearchGroups                                     ║");
    Console.WriteLine("║  14  ║ GetAllWithGroups                                 ║");
    Console.WriteLine("║  15  ║ SortWithCreatedDateEducation                     ║");
    Console.WriteLine("║  16  ║ FilterByEducationName                            ║");
    Console.WriteLine("║  17  ║ SortWithCapacity                                 ║");
    Console.WriteLine("╚══════╩══════════════════════                             ");
}