namespace Hrms.Modules.Training.Contracts;

/// <summary>
/// Cổng giao tiếp duy nhất để module khác gọi vào module Đào tạo.
/// Thêm phương thức tại đây khi có nhu cầu liên thông dữ liệu, không tham chiếu thẳng vào project Hrms.Modules.Training.
/// </summary>
public interface ITrainingModuleApi;
