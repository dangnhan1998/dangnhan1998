namespace Hrms.Modules.Foundation.Contracts;

/// <summary>
/// Cổng giao tiếp duy nhất để module khác gọi vào module Nền tảng.
/// Thêm phương thức tại đây khi có nhu cầu liên thông dữ liệu, không tham chiếu thẳng vào project Hrms.Modules.Foundation.
/// </summary>
public interface IFoundationModuleApi;
