using FluentValidation;

namespace PortainerBlog.Features.Posts.Commands;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(30)
            .WithMessage("Title must not exceed 30 characters.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(150)
            .WithMessage("Content must not exceed 150 characters.");
    }
}
