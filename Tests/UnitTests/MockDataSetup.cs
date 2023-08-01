using EmployeeRecognition.Core.Entities;

namespace EmployeeRecognition.Tests.UnitTests;

public class MockDataSetup
{
    public IEnumerable<User> CreateMockUserList()
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
            new User
            {
                Id = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
                Name = "Amanda",
                Password = "WEEWOO",
                AvatarUrl = "a_url"
            }
        };

        return moqUsers;
    }

    public IEnumerable<Kudo> CreateMockKudoList()
    {
        var moqKudos = new List<Kudo>
        {
            new Kudo
            {
                Id = 1,
                SenderId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
                ReceiverId = "cdc2f8b6-0e2c-4c27-b609-15e10a04cabc",
                Title = "title",
                Message = "A message",
                TeamPlayer = true,
                OneOfAKind = true,
                Creative = true,
                HighEnergy = true,
                Awesome = true,
                Achiever = true,
                Sweetness = true,
                TheDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new Kudo
            {
                Id = 2,
                SenderId = "ad3eb1e3-9f6b-4291-8d32-559e88e9de6e",
                ReceiverId = "cdc2f8b6-0e2c-4c27-b609-15e10a04cabc",
                Title = "A nice title",
                Message = "WEE WOO WEE WOO",
                TeamPlayer = false,
                OneOfAKind = false,
                Creative = false,
                HighEnergy = false,
                Awesome = false,
                Achiever = false,
                Sweetness = false,
                TheDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new Kudo
            {
                Id = 3,
                SenderId = "d68e2603-7934-40d3-9d80-ba8648888182",
                ReceiverId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
                Title = "Anotha one",
                Message = "Nice job today! Let's do it again tomorrow!",
                TeamPlayer = false,
                OneOfAKind = true,
                Creative = true,
                HighEnergy = true,
                Awesome = false,
                Achiever = true,
                Sweetness = true,
                TheDate = DateOnly.FromDateTime(DateTime.Now)
            },
            new Kudo
            {
                Id = 4,
                SenderId = "a1b1bef1-1426-43cc-bcd9-d8425fe6e8e3",
                ReceiverId = "cdc2f8b6-0e2c-4c27-b609-15e10a04cabc",
                Title = "A message",
                Message = "A title",
                TeamPlayer = true,
                OneOfAKind = false,
                Creative = false,
                HighEnergy = false,
                Awesome = false,
                Achiever = true,
                Sweetness = false,
                TheDate = DateOnly.FromDateTime(DateTime.Now)
            },
        };

        return moqKudos;
    }
}
