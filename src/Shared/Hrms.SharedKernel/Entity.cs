namespace Hrms.SharedKernel;

/// <summary>
/// Lớp cơ sở cho mọi entity. Khóa chính kiểu Guid để dễ hợp nhất dữ liệu khi migration từ PMNS cũ.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
}
