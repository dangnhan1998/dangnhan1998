namespace Hrms.Modules.CnB.Contracts;

/// <summary>
/// Cổng giao tiếp duy nhất để module khác gọi vào module C&B.
/// Thêm phương thức tại đây khi có nhu cầu liên thông dữ liệu, không tham chiếu thẳng vào project Hrms.Modules.CnB.
/// </summary>
public interface ICnBModuleApi;
