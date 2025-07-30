using Shows.Api.Api.Shows;
using FluentValidation.TestHelper;

namespace Shows.Tests.Api.Shows;

public class AddShowValidationTests
{
    [Theory]
    [Trait("Category", "UnitTest")]
    [MemberData(nameof(InvalidShows))]
    public void InvalidShowValidations(AddShowRequest show)
    {
        var validator = new AddShowRequestValidator();
        var validations = validator.TestValidate(show);
        Assert.False(validations.IsValid);
    }

    [Theory]
    [Trait("Category", "UnitTest")]
    [MemberData(nameof(ValidShows))]
    public void ValidShowValidations(AddShowRequest show)
    {
        var validator = new AddShowRequestValidator();
        var validations = validator.TestValidate(show);
        Assert.True(validations.IsValid);
    }

    private static readonly int DescriptionMinLength = 10;
    private static readonly int DescriptionMaxLength = 500;
    private static readonly int NameMinLength = 3;
    private static readonly int NameMaxLength = 100;

    public static IEnumerable<object[]> InvalidShows() =>
        new[]
        {
            new object[] { new AddShowRequest() },
            new object[]
            {
                new AddShowRequest()
                {
                    Name = new string('x', NameMinLength - 1),
                    Description = new string('x', DescriptionMinLength),
                    StreamingService = "Hulu"
                }
            },
             new object[]
            {
                new AddShowRequest()
                {
                    Name = new string('x', NameMinLength - 1),
                    Description = new string('x', DescriptionMinLength),
                    StreamingService = "Hulu"
                }
            },
            new object[]
            {
                new AddShowRequest()
                {
                    Name = new string('X', NameMaxLength + 1),
                    Description = new string('X', DescriptionMaxLength + 1),
                    StreamingService = "Hulu"
                }
            },
        };

    public static IEnumerable<object[]> ValidShows() =>
        new[]
        {
            new object[]
            {
                new AddShowRequest()
                {
                    Name = new string('X', NameMaxLength),
                    Description = new string('X', DescriptionMaxLength),
                    StreamingService = "Hulu"
                }
            },
            new object[]
            {
                new AddShowRequest()
                {
                    Name = new string('X', NameMinLength),
                    Description = new string('X', DescriptionMinLength),
                    StreamingService = "Netflix"
                }
            },
        };
}