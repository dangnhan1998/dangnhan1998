namespace Hrms.Modules.Personnel.Contracts;

/// <summary>
/// Cổng giao tiếp duy nhất để module khác gọi vào module Nhân sự.
/// Thêm phương thức tại đây khi có nhu cầu liên thông dữ liệu, không tham chiếu thẳng vào project Hrms.Modules.Personnel.
/// </summary>
public interface IPersonnelModuleApi;
