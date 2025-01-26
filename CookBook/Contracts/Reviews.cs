namespace CookBook.Contracts;



public record ReviewVm(int Id, string Name, string Description, Enum Score);
public record ReviewsVm(List<ReviewVm> Ingredients);
public record CreateReviewDto(string Name, string Description);

public record UpdateReviewDto(string Name, string Description);
