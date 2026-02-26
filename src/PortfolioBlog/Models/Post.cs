using System.ComponentModel.DataAnnotations;

namespace PortainerBlog.Models;

public sealed record Post
{
    [Key]
    public Guid Id { get; set; } = Guid.CreateVersion7();

    [Required]
    [MaxLength(30)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
