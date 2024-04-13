
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
    GetMenues();
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
static void GetMenues()
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
    Console.WriteLine("║  10  ║ Get Student by Id                                ║");
    Console.WriteLine("║  11  ║ Delete Student                                   ║");
    Console.WriteLine("║  12  ║ Get Students by Age                              ║");
    Console.WriteLine("║  13  ║ Get all students by Group Id                     ║");
    Console.WriteLine("║  14  ║ Search Groups by Name                            ║");
    Console.WriteLine("║  15  ║ Search students by Name/Surname                  ║");
    Console.WriteLine("╚══════╩══════════════════════════                           ");
}
