using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Tests.UnitTests;

public class MockDataSetup
{
    public List<User> CreateMockUserList()
    {
        var moqUsers = new List<User>
        {
            new User
            {
                Id = "19df82ba-a964-4dbf-8013-69c120e938de",
                Name = "test",
                Password = "test",
                AvatarUrl = ""
            },
            new User
            {
                Id = "e1a07078-fd94-4554-812a-383c0367de90",
                Name = "Bob",
                Password = "123",
                AvatarUrl = ""
            },
            new User
            {
                Id = "36ccb155-c564-4e85-bd99-a564b7d3d429",
                Name = "Michael",
                Password = "a_real_password",
                AvatarUrl = ""
            },
        };

        return moqUsers;
    }
}
