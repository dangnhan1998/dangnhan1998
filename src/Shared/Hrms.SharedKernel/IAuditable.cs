namespace Hrms.SharedKernel;

/// <summary>
/// Entity cần lưu vết người tạo/sửa (yêu cầu ISO 9001 và Nghị định 13/2023/NĐ-CP).
/// Giá trị được gán tự động ở tầng persistence, không gán tay trong nghiệp vụ.
/// </summary>
public interface IAuditable
{
    DateTimeOffset CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    string? UpdatedBy { get; set; }
}
